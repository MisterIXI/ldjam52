using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorbeamScript : MonoBehaviour
{
    [SerializeField] public Rigidbody playerRigidbody;
    [SerializeField] public Rigidbody itemRigidbody;

    [SerializeField] public bool active = true;
    [SerializeField] [Range(3,20)] public int minBeamLenght = 6;

    private LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        if (active)
        {
            // Moving Item towards sphere
            if (Vector3.Distance(itemRigidbody.transform.position, transform.position) > minBeamLenght)
            {
                Vector3 itemForce = Vector3.Lerp(itemRigidbody.transform.position, transform.position, Time.deltaTime); // Lerping the for needed for the banana

                itemRigidbody.transform.position = itemForce;   // moving banana
                
                playerRigidbody.AddForce(itemForce * -1);   // adds counter force for holder
            }
        
            // Keeping sphere at player position
            transform.position = new Vector3(playerRigidbody.transform.position.x, playerRigidbody.transform.position.y - 1.5f, playerRigidbody.transform.position.z);

            // Drawing a line between them.
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, new Vector3 (itemRigidbody.transform.position.x, itemRigidbody.transform.position.y + 1, itemRigidbody.transform.position.z));
        }
    }
}
