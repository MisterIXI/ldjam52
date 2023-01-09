using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCargoship : MonoBehaviour
{
    [SerializeField] public GameObject instableCore;
    [SerializeField] public Material ExplosionYellowMat;
    [SerializeField] public Material ExplosionRedwMat;
    [SerializeField] public bool explode1 = false, explode2 = false;
    
    private GameObject _cargoship;
    

    void Start()
    {
        _cargoship = GetComponent<GameObject>();
        ExplosionYellowMat.SetColor("_EmissionColor", new Color(255,255,0) * 0.006f);
    }

    void Update()
    {
        if (explode1)
        {
            Material[] TempMats = GetComponent<Renderer>().materials;
            TempMats[0] = ExplosionRedwMat;
            TempMats[1] = ExplosionYellowMat;

            GetComponent<Renderer>().materials = TempMats;
        }
        if (explode2)
        {
            ExplosionYellowMat.SetColor("_EmissionColor", new Color(255,255,0) * 1);
            instableCore.transform.localScale = new Vector3 (0.1f, 0.3f, 0.3f);
        }
    }
}