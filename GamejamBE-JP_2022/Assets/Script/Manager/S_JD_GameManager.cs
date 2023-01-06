using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// GameManagerに関するプログラム
// 鎌倉の担当行：111~127、139、156~164
public class S_JD_GameManager : MonoBehaviour
{
    public static S_JD_GameManager Instance;
    public bool LoadAtMenu = true;  // ゲーム開始の判定
    public float EarthValue = 50f;  // 環境ストレス
    public float StressValue = 50f;  // プレイヤーストレス
    public bool InGame = false;  // ゲームのプレイ中の判定
    public int TreeNumber = 0;  // 木の生えている本数
    public float Stamina = 100f;  // スタミナの最大値
    private float Timer = 1;  // 日数
    public float actualSpeedtree = 0;  // 現在の環境ストレスの回復率
    public float actualSpeedStress = 0;  // 現在のプレイヤーのストレス減少率
    public float actualEarthDamage = 0;  // 現在の環境ストレスの減少率


    [Space(10),Header("Game Balance")]
    [SerializeField]
    public float speedStress = 0;  // プレイヤーのストレス減少率の初期値
    public float speedTree = 0.3f;  // 環境ストレスの回復率の初期値
    public float earthDamage = 0.5f;  // 環境ストレスの減少率の初期値
    public float speedStamine = 1;  // スタミナ減少倍率
    public float maxspeedstress = 1;  // プレイヤーストレスの最大減少率
    public float speedStressIncreaseRate = 0.01f;  // プレイヤーストレスの減少率の増加率
    public float maxEarthDamage = 1;  //　環境ストレスの最大減少率
    public float earthDamageIncreaseRate = 0.001f;  // 環境ストレスの減少率の増加率

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(LoadAtMenu)
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InGame)
        {
            SetTimer();

            SetSpeedStress();

            SetStamina();

            CheckDeath();
        }
    }

    private void SetTimer()
    {
        Timer += Time.deltaTime * Time.timeScale;
    }

    private void SetSpeedStress()
    {
        if (actualSpeedStress < maxspeedstress)
        {
            actualSpeedStress += Time.deltaTime * speedStressIncreaseRate;
        }

        if (actualEarthDamage < maxEarthDamage)
        {
            actualEarthDamage += Time.deltaTime * earthDamageIncreaseRate;
        }


        SetStressValue();

        SetEarthValue();
    }

    public IEnumerator LoadSceneAsync(bool useLoadingScreen)
    {

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        StartGame();
        while (!asyncOp.isDone) yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        print(SceneManager.GetActiveScene());
    }

    public void StartGame()
    {
        EarthValue = 50f;
        StressValue = 50f;
        Stamina = 100f;
        Timer = 0;

        actualSpeedtree = speedTree;
        actualSpeedStress = speedStress;
        actualEarthDamage = earthDamage;

        InGame = true;
        S_JD_CanvasManager.Instance.MiniGamePanel.SetActive(false);
    }

    public void SetStressValue()
    {
        if (StressValue <= 100)
            StressValue -= (1 * Time.deltaTime * actualSpeedStress);
        else
            StressValue = 100;
        S_JD_CanvasManager.Instance.SetValuePlayer(StressValue);
    }

    public void SetEarthValue()
    {
        if (EarthValue <= 100)
            EarthValue += (TreeNumber * Time.deltaTime * actualSpeedtree);
        else
            EarthValue = 100;
        S_JD_CanvasManager.Instance.SetValueEarth(EarthValue);
    }


    public void GameOver(int _cause)
    {
        int time = (int)Mathf.Round(Timer) / 60;
        GameObject[] Traker = GameObject.FindGameObjectsWithTag("Finish");
        foreach (GameObject obj in Traker)
        {
            Destroy(obj);
        }
        TreeNumber = 0;
        S_JD_CanvasManager.Instance.EndingPanel(time, _cause);
        S_JD_Player.Instance.AvailableMouvement = false;
        InGame = false;
    }

    public void CheckDeath()
    {
        if(EarthValue <= 0)
        {
            GameOver(0);
        }
        else if (StressValue <= 0)
        {
            GameOver(1);
        }
    }

    public void SetStamina()
    {
        if (Stamina <= 0)
        {
            Stamina = 0;
        }
        Stamina -= (1 * Time.deltaTime * speedStamine);
        S_JD_CanvasManager.Instance.SetValueStamina(Stamina);
    }
}