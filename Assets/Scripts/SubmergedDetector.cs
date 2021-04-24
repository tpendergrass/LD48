using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmergedDetector : MonoBehaviour {
    public bool isSubmerged = false;
    
    public void StopSwimming() {
        isSubmerged = false;
    }

    public void StartSwimming() {
        isSubmerged = true;
    }
}
