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
            SetEarthValue();

            EarthValue = TreeNumber * 1;
        }
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
    }

    public void SetEarthValue()
    {
        StressValue = StressValue - 1 * Time.deltaTime;
        S_JD_CanvasManager.Instance.SetValueEarth(EarthValue);
    }
}
