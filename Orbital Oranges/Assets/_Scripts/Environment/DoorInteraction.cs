using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] float openingTime;
    [SerializeField] float openingAngle;
    bool isOpening;
    private float _timeElapsed;
    private float _startAngle;
    [SerializeField] Material closedMat;
    [SerializeField] Material openedMat;
    [SerializeField] Vector3 startVec;
    bool once;
    private void Start()
    {
        _timeElapsed = 0;
        _startAngle = transform.parent.eulerAngles.x;
    }
    public void Interact()
    {
        GetComponent<MeshRenderer>().material = openedMat;
        if (!once)
        {
            once = true;
            if(!isOpening)
                isOpening = true;
        }
    }
    
    void Update()
    {
        if (isOpening)
        {
            _timeElapsed += Time.deltaTime;
            float currentAngle= Mathf.Lerp(_startAngle, openingAngle, _timeElapsed / openingTime);
            transform.parent.localRotation = Quaternion.Euler(currentAngle, startVec.y, startVec.z);
     
            if(_timeElapsed >= openingTime)
            {
                transform.parent.localRotation = Quaternion.Euler(openingAngle, startVec.y, startVec.z);
                isOpening = false;
            }
        }

    }
}
