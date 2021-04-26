using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Text interactMessage;
    public GameObject interactPanel;
    public Text flashlightText;
    public Text flareText;

    public void ShowInteractionPrompt(string message) {
        interactMessage.text = message;
        interactPanel.SetActive(true);
    }

    public void HideInteractionPrompt() {
        interactPanel.SetActive(false);
    }

    public void SetFlashlight(bool isOn) {
        if(isOn) {
            flashlightText.text = "F | Flashlight: ON";
        } else {
            flashlightText.text = "F | Flashlight: OFF";
        }
    }

    public void SetFlareCount(int amount) {
        flareText.text = "1 | Flares: " + amount.ToString() + " Remaining";
    }
}
