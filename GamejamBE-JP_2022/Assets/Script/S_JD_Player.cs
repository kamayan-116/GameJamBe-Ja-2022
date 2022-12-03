using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Player : MonoBehaviour
{
    public static S_JD_Player Instance;

    public float speed = 10;
    public GameObject PlayerCharacter;
    public float Distance = 11;
    public int WaterValue = 0;
    public int WoodValue = 0;
    [HideInInspector]
    public bool AvailableMouvement = true;

    private bool InMiniGame = false;
    private int MiniGameScore = 0;
    private bool CanLaunchMiniGame = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        S_JD_CanvasManager.Instance.SetActiveHUD(true);
        PlayerCharacter.transform.SetPositionAndRotation(new Vector3(0, Distance, 0), Quaternion.identity);
    }

    void Update()
    {
        if (!InMiniGame && AvailableMouvement) transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * Time.deltaTime) * speed);
        if (InMiniGame)
        {
            SmashButton();
        }
    }

    public void GetWater()
    {
        if(WaterValue < 5)
        {
            WaterValue += 1;
            S_JD_CanvasManager.Instance.SetValueWater(WaterValue);
        }       
    }

    public void GetWood()
    {
        if(WoodValue < 5)
        {
            WoodValue += 1;
            S_JD_CanvasManager.Instance.SetValueTree(WoodValue);
        }
    }

    public void GetBath()
    {
        if (CanLaunchMiniGame && WoodValue >= 3)
            LaunchMiniGame();
    }

    public void Interact(string _action)
    {
        if(_action == "GetWater")
        {
            GetWater();
        }
        else if(_action == "GetWood")
        {
            GetWood();
        }
        else if(_action == "GetBath")
        {
            GetBath();
        }
        else
        {
            print("Wrong Action");
        }
    }

    private void SmashButton()
    {
        if (Input.GetButtonDown("Interact"))
        {
            MiniGameScore += 1;
            print(MiniGameScore);
            S_JD_CanvasManager.Instance.SmahButton.SetTrigger("Press");
        }
        if (MiniGameScore == 10)
        {
            StartCoroutine(ExitMiniGameLatence(2f));
        }
    }

    IEnumerator ExitMiniGameLatence(float _delay)
    {
        MiniGameScore = 0;
        InMiniGame = false;
        S_JD_CanvasManager.Instance.MiniGamePanel.SetActive(false);

        yield return new WaitForSeconds(_delay);
        CanLaunchMiniGame = true;
    }

    private void LaunchMiniGame()
    {
        CanLaunchMiniGame = false;
        InMiniGame = true;
        S_JD_CanvasManager.Instance.MiniGamePanel.SetActive(true);
        WoodValue -= 3;
        S_JD_CanvasManager.Instance.SetValueTree(WoodValue);
    }

    

}
