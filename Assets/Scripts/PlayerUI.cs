using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Text interactMessage;
    public GameObject interactPanel;

    public void ShowInteractionPrompt(string message) {
        interactMessage.text = message;
        interactPanel.SetActive(true);
    }

    public void HideInteractionPrompt() {
        interactPanel.SetActive(false);
    }
}
