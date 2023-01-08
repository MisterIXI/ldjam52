using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Orbital Oranges/PlayerSettings", order = 1)]
public class PlayerSettings : ScriptableObject
{
    [Header("LookSettings")]
    [Tooltip("The speed at which the camera rotates around the player")]
    [Range(0.1f,10f)] public float cameraSensitivity = 3;
    [Tooltip("The speed at which the mouse gains speed while lerping to targetPoint")]
    [Range(0.01f, 1)] public float mouseMultiplier = 0.5f;
    [Tooltip("The speed at which the mouse loses speed while lerping to targetPoint")]
    [Range(0.1f, 5f)] public float mousePointDrag = 0.4f;
    [Tooltip("The amount the break can stop rotation of the spaceship")]
    [Range(0.01f, 0.5f)] public float angularBreakingPower = 0.1f;
    [Tooltip("Minimum velocity magnitude at which the breaking can stop the velocity down to 0")]
    [Range(0f, 0.5f)] public float breakingStopPoint = 0.3f;
}