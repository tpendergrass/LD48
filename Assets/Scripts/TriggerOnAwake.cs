using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnAwake : MonoBehaviour {
    public UnityEvent events;
    
    void OnEnable() {
        events.Invoke();
    }
}
