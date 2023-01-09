using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;
using TMPro;


public class MenuButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler {

    private TextMeshProUGUI _buttonText;
    private Button _button;

    private void Awake() {
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
    }

    private void OnEnable() {
        if (_button.name != "Button-Start")
        {
            _buttonText.color = new Color32 (255,255,255,255);
        }
        else
        {
            _button.Select();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _button.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _buttonText.color = new Color32 (0, 231, 245, 255);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _buttonText.color = new Color32 (255,255,255,255);
    }
}