using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnvironmentSetting {
    public Material skyboxMaterial;
    public Color fogColor;
    public float fogDensity;
}
