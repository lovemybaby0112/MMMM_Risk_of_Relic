using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SliderTextChanged : MonoBehaviour
{
    private Text ValueText;
    // Start is called before the first frame update
    void Start()
    {
        ValueText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSliderValueChanged(Slider s)
    {
        ValueText.text = s.value.ToString("0%");
    }
}
