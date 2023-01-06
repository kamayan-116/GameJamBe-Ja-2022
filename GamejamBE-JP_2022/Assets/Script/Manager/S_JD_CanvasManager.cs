using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// SC_CodeのCanvasに関するプログラム
// 鎌倉の担当行：15~28、47~51、59~93、100~135、156~190
public class S_JD_CanvasManager : MonoBehaviour
{
    #region 変数宣言
    public static S_JD_CanvasManager Instance;

    [SerializeField] GameObject HUD;  // ゲーム中UIパネル
    [SerializeField] GameObject pauseMenuPanel;  // ポーズメニューUIパネル
    [SerializeField] GameObject endingPanel;  // 結果UIパネル
    [SerializeField] Slider earthSlider;  // 環境のスライダー
    [SerializeField] Slider playerSlider;  // プレイヤーのストレススライダー
    [SerializeField] Text waterText;  // 水の所持数テキスト
    [SerializeField] Text treeText;  // 木の所持数テキスト
    [SerializeField] Text playTimeText;  // 現在のスコアテキスト
    [SerializeField] Text bestPlayTimeText;  // ハイスコアテキスト
    [SerializeField] GameObject pressTextObj;  // Interactの際のPushオブジェクト
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;  // ボタンに関する音の配列
    [SerializeField] Image gameOverImage;  // ゲームオーバ画像
    [SerializeField] Sprite[] gameOverSprite;  // ゲームオーバー時のSprite配列
    public GameObject MiniGamePanel;  // 風呂の時のPushUIパネル
    public Animator SmahButton;
    public Slider staminaSlider;  // プレイヤーのスタミナスライダー

    public Animator DeathFade;

    public GameObject backToGameButton;  // ポーズメニューのゲーム再開ボタン
    #endregion


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Update()
    {
        // ポーズ画面への遷移
        if(Input.GetButton("Cancel") && S_JD_GameManager.Instance.InGame)
        {
            GoToPause();
        }
    }

    public void DeathFading()
    {
        DeathFade.SetTrigger("DeathTrigger");
    }

    // 環境のスライダーの値代入
    public void SetValueEarth(float _value)
    {
        //StartCoroutine(SliderValueChange(earthSlider, earthSlider.value, _value));
        earthSlider.value = _value;
    }

    /// <summary>
    /// Player slider value
    /// </summary>
    /// <param name="_value">value between 0 and 100</param>
    // プレイヤーストレスのスライダーの値代入
    public void SetValuePlayer(float _value)
    {
        playerSlider.value = _value;
       //StartCoroutine(SliderValueChange(earthSlider, earthSlider.value, _value));
    }

    // プレイヤーのスタミナのスライダーの値代入
    public void SetValueStamina(float _value)
    {
        staminaSlider.value = _value;
    }

    // 水の保持数のテキスト
    public void SetValueWater(int _value)
    {
        waterText.text = "×" + _value;
    }

    // 木の保持数のテキスト
    public void SetValueTree(int _value)
    {
        treeText.text = "×" + _value;
    }

    public void SetActiveHUD(bool _active)
    {
        HUD.SetActive(_active);
    }

    // 流動的な値な変化のためのコルーチン関数
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

    // Function of the button to go to the pause menu
    // ポーズメニューの表示
    public void GoToPause()
    {
        HUD.SetActive(false);
        pauseMenuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backToGameButton);
        Time.timeScale = 0;
    }

    // Function of the button to return to the game in the pause
    // ポーズメニューからゲーム再開
    public void BackToGame()
    {
        HUD.SetActive(true);
        pauseMenuPanel.SetActive(false);
        audioSource.PlayOneShot(audioClips[1]);
        Time.timeScale = 1.0f;
    }

    // Function of the button to return to the title in the pause
    public void BackToTitle()
    {
        //DeathFading();
        Time.timeScale = 1f;
        S_JD_GameManager.Instance.InGame = false;
        pauseMenuPanel.SetActive(false);
        endingPanel.SetActive(false);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(2);
        RemoveEndingPanel();
        audioSource.PlayOneShot(audioClips[1]);
        GameObject[] DeleteTree = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject obj in DeleteTree)
        {
            Destroy(obj);
        }
    }

    // ゲーム結果の表示
    public void EndingPanel(float _clearTime, int _clearCause)
    {
        HUD.SetActive(false);
        endingPanel.SetActive(true);
        // 今回の結果の表示
        playTimeText.text = _clearTime + " Days";

        // ハイスコア更新
        var bestScore = PlayerPrefs.GetFloat("BestScore", 0);
        if(_clearTime > bestScore)
        {
            bestScore = _clearTime;
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }

        PlayerPrefs.Save();
        bestPlayTimeText.text = "Best Day：" + bestScore + "Days";

        // ゲームオーバ理由に応じたSpriteチェンジ
        if(_clearCause <= 1)
        {
            gameOverImage.sprite = gameOverSprite[_clearCause];
        }
        else
        {
            print("Error in the ending condition");
        }
    }

    // 結果画面の終了
    public void RemoveEndingPanel()
    {
        endingPanel.SetActive(false);
    }

    public void SetActivePressE()
    {
        pressTextObj.SetActive(true);
    }

    public void SetInactivePressE()
    {
        pressTextObj.SetActive(false);
    }

    public void PlayOnSound()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
}
