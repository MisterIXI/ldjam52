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

    private float startEmmission = 0.006f;
    private float stage2Emmission = 0.05f;
    private float endEmmission = 1f;

    private float stage1Time = 5f;
    private float stage2Time = 2.4f;
    private float explosionTime = 0.1f;

    private float _time;
    

    void Start()
    {
        _cargoship = GetComponent<GameObject>();
        ExplosionYellowMat.SetColor("_EmissionColor", new Color(255,255,0) * startEmmission);
    }

    void Update()
    {
        _time += Time.deltaTime;
        if ((_time > stage1Time) && (_time < (stage1Time + stage2Time)))
        {
            Material[] TempMats = GetComponent<Renderer>().materials;
            TempMats[0] = ExplosionRedwMat;
            TempMats[1] = ExplosionYellowMat;
            GetComponent<Renderer>().materials = TempMats;

            // lerp the time here
            float tmp = Mathf.Lerp(startEmmission, stage2Emmission, stage1Time);
            ExplosionYellowMat.SetColor("_EmissionColor", new Color(255,255,0) * tmp);
        }
        else if (_time >= (stage1Time + stage2Time))
        {
            // lerp the time here and scale fast
            ExplosionYellowMat.SetColor("_EmissionColor", new Color(255,255,0) * 1);
            instableCore.transform.localScale = new Vector3 (0.1f, 0.3f, 0.3f);


            // YANNIK we could switch to game over screen with a better camera here
        }
    }
}