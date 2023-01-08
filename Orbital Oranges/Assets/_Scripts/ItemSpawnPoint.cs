using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject[] spawnableObject;

    private void Start()
    {
        GameObject spawnedObject = Instantiate(spawnableObject[Random.Range(0,spawnableObject.Length)]);
        spawnedObject.transform.position = transform.position;
    }
}
