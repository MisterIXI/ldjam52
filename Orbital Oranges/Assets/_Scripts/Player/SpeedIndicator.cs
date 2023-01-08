using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    public Vector3 VelocityVector;
    public bool IsBreaking;
    [SerializeField] private bool _isThrustIndicator;
    [SerializeField] private MeshRenderer _up;
    [SerializeField] private MeshRenderer _down;
    [SerializeField] private MeshRenderer _left;
    [SerializeField] private MeshRenderer _right;
    [SerializeField] private MeshRenderer _forward;
    [SerializeField] private MeshRenderer _back;

    private void Awake()
    {
        if (!_isThrustIndicator)
        {
            if (RefManager.speedIndicator != null)
            {
                Destroy(transform.parent.gameObject);
                return;
            }
            RefManager.speedIndicator = this;
        }
        else
        {
            if (RefManager.thrustIndicator != null)
            {
                Destroy(transform.parent.gameObject);
                return;
            }
            RefManager.thrustIndicator = this;
        }
    }
    private void Start()
    {
        VelocityVector = Vector3.zero;
        // check if any Meshrenderer is missing
        if (_up == null || _down == null || _left == null || _right == null || _forward == null || _back == null)
        {
            Debug.LogError("Missing MeshRenderer in SpeedIndicator");
            Destroy(this);
        }
    }

    private void Update()
    {
        if (!IsBreaking)
        {
            // update each material's _progress property according to the VelocityVector
            _up.material.SetFloat("_Progress", VelocityVector.y);
            _down.material.SetFloat("_Progress", -VelocityVector.y);
            _left.material.SetFloat("_Progress", -VelocityVector.x);
            _right.material.SetFloat("_Progress", VelocityVector.x);
            _forward.material.SetFloat("_Progress", VelocityVector.z);
            _back.material.SetFloat("_Progress", -VelocityVector.z);
        }
        else
        {
            _up.material.SetFloat("_Progress", 1);
            _down.material.SetFloat("_Progress", 1);
            _left.material.SetFloat("_Progress", 1);
            _right.material.SetFloat("_Progress", 1);
            _forward.material.SetFloat("_Progress", 1);
            _back.material.SetFloat("_Progress", 1);
        }
    }

}
