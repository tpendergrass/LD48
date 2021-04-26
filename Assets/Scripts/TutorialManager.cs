using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    public Text messageText;
    public GameObject messageBox;

    public void ShowMessage(string message) {
        CancelInvoke();
        messageText.text = message;
        messageBox.SetActive(true);
        Invoke("ClearMessage", 8);
    }

    void ClearMessage() {
        messageBox.SetActive(false);
    }
}
