using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCargoship : MonoBehaviour
{
    [SerializeField] public GameObject instableCore;
    [SerializeField] public Material ExplosionYellowMat;
    [SerializeField] public Material ExplosionRedwMat;

    private GameObject _cargoship;

    private float startEmmission = 0.006f;
    private float stage2Emmission = 0.05f;
    private float endEmmission = 1f;

    private float stage1Time = 459f;
    private float explosionTime = 0.1f;

    private float _startTime;
    private bool _gameRunning;
    [SerializeField] private AnimationCurve _Stage1Curve;
    [SerializeField] private AnimationCurve _Stage2Curve;
    [SerializeField] private float ExplosionRadius = 450f;
    private Vector3 _startScale;
    private Vector3 _endScale;
    private bool _matsSet = false;
    private bool _exploded = false;
    private Rigidbody _rb;
    void Start()
    {
        _cargoship = GetComponent<GameObject>();
        ExplosionYellowMat.SetColor("_EmissionColor", new Color(255, 255, 0) * startEmmission);
        _startScale = instableCore.transform.localScale;
        _endScale = new Vector3(0.1f, 0.3f, 0.3f);
        _rb = GetComponent<Rigidbody>();
        RefManager.gameManager.OnGameStateChange += OnGameStateChange;
    }
    private void OnDestroy()
    {
        RefManager.gameManager.OnGameStateChange -= OnGameStateChange;
    }
    public void OnGameStateChange(bool newState)
    {
        if (newState)
        {
            _startTime = Time.time;
            _gameRunning = true;
            _matsSet = false;
            _exploded = false;
        }
        else
        {
            _gameRunning = false;
        }
    }
    public void StopGame()
    {
        _gameRunning = false;
    }
    void Update()
    {
        if (_gameRunning)
        {
            float timeDelta = Time.time - _startTime;
            if (timeDelta > stage1Time)
            {
                if (timeDelta < GameManager.GAME_DURATION)
                {
                    if (!_matsSet)
                    {
                        Material[] TempMats = GetComponent<Renderer>().materials;
                        TempMats[0] = ExplosionRedwMat;
                        TempMats[1] = ExplosionYellowMat;
                        GetComponent<Renderer>().materials = TempMats;
                        _matsSet = true;
                    }
                    //Stage 1 
                    float t = (timeDelta) / stage1Time;
                    float tmp = Mathf.Lerp(startEmmission, stage2Emmission, _Stage1Curve.Evaluate(t));
                    ExplosionYellowMat.SetColor("_EmissionColor", new Color(255, 255, 0) * tmp);
                }
            }
        }
        else
        {
            // lerp the time here and scale fast
            float t = (Time.time - _startTime - GameManager.GAME_DURATION) / explosionTime;
            t = _Stage2Curve.Evaluate(Mathf.Clamp(t, 0, 1));
            float tmp = Mathf.Lerp(stage2Emmission, endEmmission, t);
            ExplosionYellowMat.SetColor("_EmissionColor", new Color(255, 255, 0) * tmp);
            instableCore.transform.localScale = Vector3.Lerp(_startScale, _endScale, t);

            // Explosion force in radius
            if (!_exploded)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(1000f, transform.position, ExplosionRadius, 3.0f);
                    }
                }
                _exploded = true;
            }
        }
    }


}