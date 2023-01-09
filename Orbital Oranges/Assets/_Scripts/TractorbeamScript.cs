using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorbeamScript : MonoBehaviour
{
    [SerializeField] public Rigidbody playerRigidbody;
    [SerializeField] public Rigidbody itemRigidbody;
    public Transform targetTransform;
    [SerializeField] public bool active = true;
    [SerializeField] [Range(3,20)] public int minBeamLenght = 5;
    [SerializeField] private float _pullStrength = 100f;

    private LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    
    void FixedUpdate()
    {
        if(itemRigidbody == null)
        {
            Destroy(gameObject);
        }
        if (active)
        {
            // Moving Item towards sphere
            if (Vector3.Distance(itemRigidbody.transform.position, transform.position) > minBeamLenght)
            {
                // Vector3 itemForce = Vector3.Lerp(, Time.deltaTime); // Lerping the for needed for the banana
                Vector3 itemForce = (targetTransform.position - itemRigidbody.transform.position) * _pullStrength * Time.fixedDeltaTime; // Lerping the for needed for the banana
                itemRigidbody.velocity = itemRigidbody.velocity * 0.9f;
                itemRigidbody.angularVelocity = itemRigidbody.angularVelocity / 2;
                // itemRigidbody.transform.position = itemForce;   // moving banana
                // itemRigidbody.MovePosition(itemForce);   // moving banana
                itemRigidbody.AddForce(itemForce);   // adding force to banana
                playerRigidbody.AddForce(itemForce * -1);   // adds counter force for holder
            }
        
            // Keeping sphere at player position
            // transform.position = new Vector3(playerRigidbody.transform.position.x, playerRigidbody.transform.position.y - 1.5f, playerRigidbody.transform.position.z);
            transform.position = targetTransform.position;
            // Drawing a line between them.
            _lineRenderer.SetPosition(0, targetTransform.position);
            _lineRenderer.SetPosition(1, new Vector3 (itemRigidbody.transform.position.x, itemRigidbody.transform.position.y + 1, itemRigidbody.transform.position.z));
        }
    }
}
