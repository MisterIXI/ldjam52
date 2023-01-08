using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float minDistance;
    [SerializeField] Vector3 minRange;
    [SerializeField] Vector3 maxRange;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minRange.x, maxRange.x), Random.Range(minRange.y, maxRange.y), Random.Range(minRange.z, maxRange.z));
        while (Vector3.Distance(spawnPosition,Vector3.zero) < minDistance)
        {
            spawnPosition = new Vector3(Random.Range(minRange.x, maxRange.x), Random.Range(minRange.y, maxRange.y), Random.Range(minRange.z, maxRange.z));
        }
        transform.position = spawnPosition;
        transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, maxSpeed);
    }

}
