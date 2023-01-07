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
    private void Start()
    {
        _timeElapsed = 0;
        _startAngle = transform.parent.eulerAngles.x;
    }
    public void Interact()
    {
        if(!isOpening)
            isOpening = true;
    }
    
    void Update()
    {
        if (isOpening)
        {
            _timeElapsed += Time.deltaTime;
            float currentAngle= Mathf.Lerp(_startAngle, _startAngle+openingAngle, _timeElapsed / openingTime);
            transform.parent.localRotation = Quaternion.Euler(currentAngle, 0, 0);
     
            if(_timeElapsed >= openingTime)
            {
                transform.parent.localRotation = Quaternion.Euler(_startAngle + openingAngle, 0, 0);
                Destroy(gameObject);
            }
        }

    }
}
