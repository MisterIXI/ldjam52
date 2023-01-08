using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Cargo"))
        {
            PlayerScore.IncreaseScore(other.gameObject.GetComponent<Cargo>().GetScore());
            Debug.Log(PlayerScore.GetCurrentScore());
        }
    }
}
