using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public TMP_InputField inputField; 
    public Button sendButton;  
    public OSCSender sender;
    void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);
    }

    void OnSendButtonClicked()
    {
        string userInput = inputField.text;
        Debug.Log("send button is clicked");
        sender.SendText();
    }
}
