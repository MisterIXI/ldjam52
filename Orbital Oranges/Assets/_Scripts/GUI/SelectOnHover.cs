using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;


public class SelectOnHover : MonoBehaviour, IPointerEnterHandler, ISelectHandler {

    private Button _button;

    private void Start() {
        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _button.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        RefManager.soundManager.playhoverSFX();
    }
}