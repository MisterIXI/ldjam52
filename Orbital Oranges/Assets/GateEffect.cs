using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEffect : MonoBehaviour
{
    [SerializeField] MeshRenderer[] renderers;
    [SerializeField] Material onMat;
    [SerializeField] Material offMat;
    [SerializeField] float lightSpeed;

    private void Start()
    {
        StartCoroutine(ToggleLight());
    }

    IEnumerator ToggleLight()
    {
        while (true)
        {
            foreach(MeshRenderer renderer in renderers)
            {
                renderer.material = offMat;
                yield return new WaitForSeconds(lightSpeed);
            }
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.material = onMat;
                yield return new WaitForSeconds(lightSpeed);
            }
        }
    }

}
