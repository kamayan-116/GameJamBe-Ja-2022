using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_JD_CanvasManager : MonoBehaviour
{
    [SerializeField] Slider earthSlider;
    [SerializeField] Slider playerSlider;
    [SerializeField] Text waterText;
    [SerializeField] Text treeText;

    public Animator DeathFade;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DeathFading()
    {
        DeathFade.SetTrigger("DeathTrigger");
    }

    // Argument(_value) is 0~1
    public void SetValueEarth(float _value)
    {
        earthSlider.value = _value;
    }

    /// <summary>
    /// Player slider value
    /// </summary>
    /// <param name="_value">value between 0 and 1</param>
    public void SetValuePlayer(float _value)
    {
        playerSlider.value = _value;
    }

    public void SetValueWater(int _value)
    {
        waterText.text = "×" + _value;
    }

    public void SetValueTree(int _value)
    {
        treeText.text = "×" + _value;
    }
}
