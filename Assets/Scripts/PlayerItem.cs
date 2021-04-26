using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour {
    public GameObject flare;
    public int flareCount;
    public GameObject activeItem;
    public PlayerUI playerUI;

    // Update is called once per frame
    void Update() {
        handleInventorySwitching();
        handleItemUsage();
    }

    void handleItemUsage() {
        if(!activeItem || !Input.GetButtonDown("Fire1") || flareCount <= 0) {
            return;
        }

        flareCount -= 1;
        playerUI.SetFlareCount(flareCount);
        activeItem.SendMessage("UseItem");

        if(flareCount <= 0) {
            flare.SetActive(false);
            activeItem = null;
        }
    }

    public void SetFlares(int amount) {
        flareCount = amount;
        playerUI.SetFlareCount(flareCount);
    }

    void handleInventorySwitching() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            if(flareCount <= 0) {
                return;
            }
            flare.SetActive(!flare.activeSelf);
            if(flare.activeSelf) {
                activeItem = flare;
            } else {
                activeItem = null;
            }
        }
    }
}
