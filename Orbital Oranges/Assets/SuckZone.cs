using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckZone : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float suckForce;
    [SerializeField] float openingTime;
    private float _timeElapsed;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(MoveToCenter(other.gameObject));
    }

    IEnumerator MoveToCenter(GameObject other)
    {
        Vector3 startPos = other.transform.position;
        while (_timeElapsed < openingTime)
        {
            _timeElapsed += Time.deltaTime;

            Vector3 currentPos = Vector3.Lerp(startPos, transform.position, curve.Evaluate(_timeElapsed / openingTime));
            other.transform.position = currentPos;
            yield return null;
        }
        Shoot(other);

        
    }
    void Shoot(GameObject shootableObject)
    {
        shootableObject.GetComponent<Rigidbody>().velocity = transform.right * suckForce;
        Destroy(shootableObject, 4f);
    }
}
