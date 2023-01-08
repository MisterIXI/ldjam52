using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThrusterSettings", menuName = "Orbital Oranges/ThrusterSettings", order = 0)]
public class ThrusterSettings : ScriptableObject {
    [Header("Strength")]
    [Range(0.1f, 100)]public float Strength = 50f;

    [Header("Responsiveness")]
    [Range(0f,20f)] public float Acceleration = 3f;
    [Range(0f,20f)] public float Deceleration = 3f;
}