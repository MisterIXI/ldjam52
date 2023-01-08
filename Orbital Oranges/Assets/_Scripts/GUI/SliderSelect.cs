using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;


public class SliderSelect : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler {

    private Slider _slider;
    public GameObject SliderSelector;

    private void Start() {
        _slider = GetComponent<Slider>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _slider.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        SliderSelector.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        SliderSelector.SetActive(false);
    }
}