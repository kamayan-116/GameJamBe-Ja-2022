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
    public int SmashGameScore = 26;

    private bool InMiniGame = false;
    private int MiniGameScore = 0;
    private bool CanLaunchMiniGame = true;

    public int elementType = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        S_JD_CanvasManager.Instance.SetActiveHUD(true);
        S_JD_CanvasManager.Instance.SetValueWater(WaterValue);
        S_JD_CanvasManager.Instance.SetValueTree(WoodValue);
        SetElement();
        //PlayerCharacter.transform.SetPositionAndRotation(new Vector3(0, Distance, 0), Quaternion.identity);
    }

    void Update()
    {
        if (!InMiniGame && AvailableMouvement)
        {
            if (S_JD_GameManager.Instance.Stamina < 10 && S_JD_GameManager.Instance.Stamina > 3)
            {
                transform.Rotate(new Vector3(0, 0, (-Input.GetAxis("Horizontal") * Time.deltaTime) * speed * (S_JD_GameManager.Instance.Stamina/10)));
            }
            else if (S_JD_GameManager.Instance.Stamina < 3)
            {
                transform.Rotate(new Vector3(0, 0, (-Input.GetAxis("Horizontal") * Time.deltaTime) * speed * (0.2f)));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * Time.deltaTime) * speed);
            }
        }
        if (InMiniGame)
        {
            SmashButton();
        }
    }

    public void GetWater()
    {
        if(WaterValue < 5)
        {
            if (elementType == 0)
            {
                WaterValue += 2;
                S_JD_CanvasManager.Instance.SetValueWater(WaterValue);
            }
            else
            {
                WaterValue += 1;
                S_JD_CanvasManager.Instance.SetValueWater(WaterValue);
            }
        }       
    }

    public void GetWood()
    {
        if(WoodValue < 5)
        {

            if (elementType == 2)
            {
                WoodValue += 2;
                S_JD_CanvasManager.Instance.SetValueTree(WoodValue);
            }
            else
            {
                WoodValue += 1;
                S_JD_CanvasManager.Instance.SetValueTree(WoodValue);
            }
        }
    }

    public void GetBath()
    {
        if (CanLaunchMiniGame && WoodValue >= 3)
            LaunchMiniGame();       
    }

    public void GiveWater()
    {
        /*if (WaterValue > 0)
        {
            WaterValue -= 1;
            S_JD_CanvasManager.Instance.SetValueWater(WaterValue);
        }*/
    }

    public void Sleep()
    {
        S_JD_GameManager.Instance.Stamina = 100;
        SetElement();

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
        else if (_action == "GiveWater")
        {
            GiveWater();
        }
        else if (_action == "Sleep")
        {
            Sleep();
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
            //print(MiniGameScore);
            S_JD_CanvasManager.Instance.SmahButton.SetTrigger("Press");
        }
        if (elementType == 1)
        {
            if (MiniGameScore == Mathf.Floor(SmashGameScore * 0.5f))
            {
                StartCoroutine(ExitMiniGameLatence(2f));
            }
        }
        else
        {
            if (MiniGameScore == SmashGameScore)
            {
                StartCoroutine(ExitMiniGameLatence(2f));
            }
        }

    }

    IEnumerator ExitMiniGameLatence(float _delay)
    {
        MiniGameScore = 0;
        InMiniGame = false;
        S_JD_CanvasManager.Instance.MiniGamePanel.SetActive(false);
        S_JD_GameManager.Instance.StressValue += 30;

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
        S_JD_GameManager.Instance.EarthValue -= 30;
    }

    public void SetElement()
    {
        elementType = Random.Range(0, 3);
        if (elementType == 0)
        {
            //Water Element
            print("Water");
            PlayerCharacter.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.13f, 0.52f, 0.77f));
            print(PlayerCharacter.GetComponent<MeshRenderer>().material.GetColor("_Color"));
        }
        else if (elementType == 1)
        {
            //FireElement
            print("Fire");
            PlayerCharacter.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.75f, 0f, 0.01f));
            print(PlayerCharacter.GetComponent<MeshRenderer>().material);
        }
        else if (elementType == 2)
        {
            //GreenElement
            print("Green");
            PlayerCharacter.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0f, 0.75f, 0f));
            print(PlayerCharacter.GetComponent<MeshRenderer>().material);
        }
        else
            print("Error in the Element set");
    }

}
