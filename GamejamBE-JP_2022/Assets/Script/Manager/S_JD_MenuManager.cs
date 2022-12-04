using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_JD_MenuManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject ControlsPanel;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        S_JD_CanvasManager.Instance.RemoveEndingPanel();
        SceneManager.UnloadSceneAsync(1);
        //SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        StartCoroutine(S_JD_GameManager.Instance.LoadSceneAsync(true));
    }

    public void BackToMainMenu()
    {
        ControlsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void Controls()
    {
        MainMenuPanel.SetActive(false);
        ControlsPanel.SetActive(true);        
    }

    private IEnumerator LoadSceneAsync(bool useLoadingScreen)
    {

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        while (!asyncOp.isDone) yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        print(SceneManager.GetActiveScene());
    }
}
