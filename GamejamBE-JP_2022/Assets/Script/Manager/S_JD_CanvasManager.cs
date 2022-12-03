using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_JD_CanvasManager : MonoBehaviour
{
    public static S_JD_CanvasManager Instance;

    [SerializeField] Slider earthSlider;
    [SerializeField] Slider playerSlider;
    [SerializeField] Text waterText;
    [SerializeField] Text treeText;
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject endingPanel;
    [SerializeField] Text playTimeText;
    [SerializeField] Image clearImage;
    [SerializeField] Sprite[] clearSprite;
    public GameObject HUD;
    public GameObject MiniGamePanel;
    public Animator SmahButton;

    public Animator DeathFade;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

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

    // Argument(_value) is 0~100
    public void SetValueEarth(float _value)
    {
        StartCoroutine(SliderValueChange(earthSlider, earthSlider.value, _value));
    }

    /// <summary>
    /// Player slider value
    /// </summary>
    /// <param name="_value">value between 0 and 100</param>
    public void SetValuePlayer(float _value)
    {
       StartCoroutine(SliderValueChange(earthSlider, earthSlider.value, _value));
    }

    public void SetValueWater(int _value)
    {
        waterText.text = "×" + _value;
    }

    public void SetValueTree(int _value)
    {
        treeText.text = "×" + _value;
    }

    public void SetActiveHUD(bool _active)
    {
        HUD.SetActive(_active);
    }

    private IEnumerator SliderValueChange(Slider slider, float nowValue, float targetValue)
    {
        while(nowValue <= targetValue)
        {
            slider.value += 0.1f;
            nowValue = slider.value;
            yield return null;
        }
        while(nowValue > targetValue)
        {
            slider.value -= 0.1f;
            nowValue = slider.value;
            yield return null;
        }
    }

    public void GoToPause()
    {
        HUD.SetActive(false);
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void BackToGame()
    {
        HUD.SetActive(true);
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void BackToTitle()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void EndingPanel(float _clearTime, int _clearCause)
    {
        HUD.SetActive(false);
        endingPanel.SetActive(true);
        playTimeText.text = "PlayTime：" + _clearTime;
        clearImage.sprite = clearSprite[_clearCause];
    }
}
