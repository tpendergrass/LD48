using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour {
    public GameObject flare;
    public int flareCount;
    public GameObject activeItem;

    // Update is called once per frame
    void Update() {
        handleInventorySwitching();
        handleItemUsage();
    }

    void handleItemUsage() {
        if(!activeItem || !Input.GetButtonDown("Fire1")) {
            return;
        }

        activeItem.SendMessage("UseItem");
    }

    void handleInventorySwitching() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            if(flareCount < 0) {
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
