using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpaceRotation : MonoBehaviour
{

    [SerializeField] float maxSpeed;

    private void Start()
    {
        Vector3 randomRot = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        GetComponent<Rigidbody>().AddTorque(randomRot * Random.Range(0, maxSpeed),ForceMode.Impulse);
    }
}
