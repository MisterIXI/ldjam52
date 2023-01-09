using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;
using TMPro;


public class SliderSelect : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler {

    [SerializeField] public GameObject SliderSelector;

    private TextMeshProUGUI _textSlider;
    private Slider _slider;


    private void Start() {
        _slider = GetComponent<Slider>();
        _textSlider = GetComponentInChildren<TextMeshProUGUI>();
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


    private void Update() 
    {
        float tmp = Mathf.Round(_slider.value * 100) / 100;
        _textSlider.text = _slider.value.ToString("F2");
    }
}