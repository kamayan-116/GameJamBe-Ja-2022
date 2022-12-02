using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_JD_GameManager : MonoBehaviour
{
    public S_JD_CanvasManager CanvaManager;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
