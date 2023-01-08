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
        StartCoroutine(MoveToCenter(other.transform.root.gameObject));
    }

    IEnumerator MoveToCenter(GameObject other)
    {
        Vector3 startPos = other.transform.position;
        Transform otherTransform = other.transform;
        Rigidbody rb = otherTransform.root.GetComponent<Rigidbody>();
        Collider[] colliders = other.transform.root.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        rb.angularDrag = 2;
        // rb.drag = 1;
        // isSucking == moving towards center
        float lastCall = Time.time;
        while (Vector3.Distance(otherTransform.position, transform.position) > 0.5f)
        {
            float lastCallDelta = Time.time - lastCall;
            rb.velocity = rb.velocity / 2;
            float minDelta = Mathf.Min(Vector3.Distance(otherTransform.position, transform.position) / 2, 2f * lastCallDelta);
            otherTransform.position = Vector3.MoveTowards(otherTransform.position, transform.position, minDelta);
            lastCall = Time.time;
            yield return null;
        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        for (int i = 0; i < 30; i++)
        {
            rb.velocity += transform.right * suckForce;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(other, 4f);
    }

}
