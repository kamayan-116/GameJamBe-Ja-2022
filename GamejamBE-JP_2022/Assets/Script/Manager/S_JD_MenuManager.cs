using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class S_JD_MenuManager : MonoBehaviour
{

    public GameObject MainMenuPanel;
    public GameObject ControlsPanel;
    public AudioSource BouttonSon;
    public AudioSource ButtonApprove;


    public GameObject TutoPanel1;
    public GameObject TutoPanel2;
    public GameObject TutoPanel3;

    public GameObject playButton, returnButton, tuto1Button, tuto2Button, tuto3Button;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(playButton);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        PlayApproveSound();
        S_JD_CanvasManager.Instance.RemoveEndingPanel();
        SceneManager.UnloadSceneAsync(1);
        //SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        StartCoroutine(S_JD_GameManager.Instance.LoadSceneAsync(true));
    }

    public void BackToMainMenu()
    {
        PlayApproveSound();
        ControlsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void Controls()
    {
        PlayApproveSound();
        MainMenuPanel.SetActive(false);
        ControlsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(returnButton);
    }

    private IEnumerator LoadSceneAsync(bool useLoadingScreen)
    {

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        while (!asyncOp.isDone) yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        print(SceneManager.GetActiveScene());
    }

    public void PlayButtonSound()
    {
        BouttonSon.Play();
    }

    //When you push the button
    void PlayApproveSound()
    {
        ButtonApprove.Play();
    }

    public void SetTutoPanel1()
    {
        ButtonApprove.Play();
        MainMenuPanel.SetActive(false);
        TutoPanel1.SetActive(true);
        EventSystem.current.SetSelectedGameObject(tuto1Button);

    }
    public void SetTutoPanel2()
    {
        ButtonApprove.Play();
        TutoPanel1.SetActive(false);
        TutoPanel2.SetActive(true);
        EventSystem.current.SetSelectedGameObject(tuto2Button);

    }
    public void SetTutoPanel3()
    {
        ButtonApprove.Play();
        TutoPanel2.SetActive(false);
        TutoPanel3.SetActive(true);
        EventSystem.current.SetSelectedGameObject(tuto3Button);

    }
}
