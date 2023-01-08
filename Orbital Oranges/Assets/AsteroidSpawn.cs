using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    [SerializeField] int amountOfAsteroidsToSpawn;
    [SerializeField] GameObject[] asteroidsPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountOfAsteroidsToSpawn; i++)
        {
            Instantiate(asteroidsPrefab[Random.Range(0,asteroidsPrefab.Length)]);
        }  
    }
}
