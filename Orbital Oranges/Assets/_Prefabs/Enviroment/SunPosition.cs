using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPosition : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // Update is called once per frame
    private float distance;
    private void Start() {
        distance = Vector3.Distance(Player.transform.position,transform.position);
    }
    void FixedUpdate()
    {
      
        transform.position=(transform.position - Player.transform.position).normalized * distance + Player.transform.position;
    }
}
