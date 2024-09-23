using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using TMPro;


public class OSCSender : MonoBehaviour
{
    public bool isPC = true;
    public string pc_IP = "192.168.86.41";
    public string wifi_IP = "143.248.197.146";
    public string outIP;
    public int outPort = 9998;
    public InteractionManager interactionManager;

    void Awake()
    {
        outIP = isPC? pc_IP: wifi_IP;
        // init OSC
        OSCHandler.Instance.Init();
        Debug.Log("connecting ip: " + outIP + " port: " + outPort);
        // client
        OSCHandler.Instance.CreateClient("myClient", IPAddress.Parse(outIP), outPort);
    }

    public void SendOSC(string pattern, string message)
    {
        OSCHandler.Instance.SendMessageToClient("myClient", pattern, message);
    }

    public void SendText()
    {
        string text2send = "test";
        text2send = interactionManager.inputField.text;
        print("sending text: "+text2send);
        SendOSC("/text_input", text2send);
    }
}
