using System.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityOSC;
using TMPro;

public class OSCReceiver : MonoBehaviour, OSCListener
{
    OSCServer myServer;
    public int inPort = 9998;
    //private int buffersize = 100;
    DxlController dxlValue;
    public string m_videoState;
    private string m_videoState_prev;

    public string m_feedbackCondition;
    private string m_feedbackCondition_prev;
    
    public TextMeshProUGUI txt;
    public string test_string;
    
    void Start()
    {

        OSCHandler.Instance.Init();
        myServer = OSCHandler.Instance.CreateServer("myServer", inPort);
        OSCHandler.Instance.AddCallback(this);

        myServer.ReceiveBufferSize = 1024;
        myServer.SleepMilliseconds = 10;
        dxlValue = GetComponent<DxlController>();

        m_videoState = "default";
        m_videoState_prev = m_videoState;
        m_feedbackCondition = "default";
        m_feedbackCondition_prev = m_feedbackCondition;

    }

    public void OnOSC(OSCPacket pckt)
    {


        if (pckt.Address.Equals("/position_feedback"))
        {
            string receivedData = pckt.Data[0].ToString();
            dxlValue.positionFeedback = int.Parse(receivedData);
        }
        else if(pckt.Address.Equals("/load_feedback"))
        {
            string receivedData = pckt.Data[0].ToString();
            dxlValue.loadFeedback = int.Parse(receivedData);
        }
        else if (pckt.Address.Equals("/load_feedback"))
        {
            string receivedData = pckt.Data[0].ToString();
            dxlValue.tempFeedback = int.Parse(receivedData);
        }
        // test
        else if (pckt.Address.Equals("/follow_path"))
        {
            string receivedData = pckt.Data[0].ToString();
            print(receivedData);
            test_string = receivedData;
        }
        else if (pckt.Address.Equals("/videoControl"))
        {
            m_videoState = pckt.Data[0].ToString();
        }
        else if (pckt.Address.Equals("/feedbackCondition"))
        {
            m_feedbackCondition = pckt.Data[0].ToString();
        }
        // else if (pckt.Address.Equals("/frame"))
        // {
        //     int.TryParse(pckt.Data[0].ToString(), out m_videoFrame);
        // }

    }
    void Update() 
    {
        //OSCHandler.Instance.UpdateLogs();
        print("------------");
        print(test_string);
        txt.text += test_string;
        SetFeedbackCondition();
        MediaControl();
    }

    void SetFeedbackCondition()
    {
        if(m_feedbackCondition!=null)
        {
            if(m_feedbackCondition != m_feedbackCondition_prev)
            {
                // overlayManager.GetComponent<OverlayManager>().feedbackCondition = m_feedbackCondition;
                m_feedbackCondition_prev = m_feedbackCondition;
            }
        }
    }

    void MediaControl()
    {
        //DO THIS ONLY ONCE WHEN CHANGED
        if(m_videoState!=null)
        {
            if(m_videoState != m_videoState_prev)
            {
                switch(m_videoState)
                {
                    case "play":
                        // videoManager.GetComponent<VideoManager>().playPause = true;
                        break;
                    case "pause":
                        // videoManager.GetComponent<VideoManager>().playPause = false;
                        break;
                    case "stop":
                        // videoManager.GetComponent<VideoManager>().triggerStop = true; //ERROR
                        break;
                    case "jump":
                        // videoManager.GetComponent<VideoManager>().triggerJump = true; //ERROR
                        break;
                    default:
                        break;
                }
                m_videoState_prev = m_videoState;
            }
        }

    }
}
