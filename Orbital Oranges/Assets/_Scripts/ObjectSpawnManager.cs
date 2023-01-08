using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    [SerializeField] Vector3 minPos;
    [SerializeField] Vector3 maxPos;
    [SerializeField] int amountSpawnableCargo;
    [SerializeField] GameObject[] cargoObjects;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountSpawnableCargo; i++)
        {
            GameObject spawnedObject = Instantiate(cargoObjects[Random.Range(0, cargoObjects.Length)]);
            spawnedObject.transform.position = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), Random.Range(minPos.z, maxPos.z));
        }
    }


}
