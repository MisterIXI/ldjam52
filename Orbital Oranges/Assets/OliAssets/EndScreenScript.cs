using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreenScript : MonoBehaviour
{

    [Header("I know I know i should have done it in code. But well...")]
    [SerializeField] public TextMeshProUGUI TextHighscore;
    [SerializeField] public TextMeshProUGUI TextCargo;
    [SerializeField] public Slider SliderCargo;



    [Header("JUST FOR TESTING")]
    [SerializeField] public int Highscore = 42; // YANNIK I NEED THOSE -_-
    [SerializeField] public int CargoHarvested = 5; // YANNIK I NEED THOSE :)
    [SerializeField] public int CargoMaximum = 9; // YANNIK I NEED THOSE °o°

    void Start()
    {
        TextHighscore.text = Highscore.ToString();

        SliderCargo.value = 0;
        SliderCargo.maxValue = CargoMaximum;
    }


    void Update()
    {
        UpdateSlider();
    }


    private void UpdateSlider()
    {
        if (SliderCargo.value != CargoHarvested)
        {
            SliderCargo.value = Mathf.Lerp(SliderCargo.value, CargoHarvested, Time.deltaTime);

            int SliderPercent = Mathf.RoundToInt(SliderCargo.value / CargoMaximum * 100);
            TextCargo.text = SliderPercent.ToString() + " %";
        }
    }
}


// Maybe fix lerp last percent
// fix slider reset on hide/unhide
// fix sure menu