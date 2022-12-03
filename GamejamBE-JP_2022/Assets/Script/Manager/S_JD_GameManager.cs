using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_JD_GameManager : MonoBehaviour
{
    public static S_JD_GameManager Instance;
    public bool LoadAtMenu = true;
    public float EarthValue = 50f;
    public float StressValue = 50f;
    public bool InGame = false;
    public int TreeNumber = 0;
    public float speedStress = 1;
    public float speedTree = 1;
    public float speedStamine = 1;
    public float Stamina = 100f;

    private float Timer = 0;


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
            SetStressValue();

            SetEarthValue();

            SetTimer();

            SetStamina();

            CheckDeath();
        }
    }

    private void SetTimer()
    {
        Timer += Time.deltaTime * Time.timeScale;
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
        InGame = true;
        S_JD_Player.Instance.AvailableMouvement = true;

    }

    public void SetStressValue()
    {
        StressValue = StressValue - (1 * Time.deltaTime * speedStress);
        S_JD_CanvasManager.Instance.SetValuePlayer(StressValue);
    }

    public void SetEarthValue()
    {
        EarthValue = EarthValue + (TreeNumber * Time.deltaTime * speedStress);
        S_JD_CanvasManager.Instance.SetValueEarth(EarthValue);
    }

    public void GameOver(int _cause)
    {
        S_JD_CanvasManager.Instance.EndingPanel(Timer, 0);
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
        if (Stamina <= 0) Stamina = 0;
        else
        {
            Stamina = Stamina - (1 * Time.deltaTime * speedStamine);
            S_JD_CanvasManager.Instance.SetValueStamina(Stamina);
        }
    }
}