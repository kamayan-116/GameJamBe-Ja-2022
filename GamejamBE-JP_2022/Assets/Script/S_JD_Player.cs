using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Player : MonoBehaviour
{
    public static S_JD_Player Instance;

    public GameObject sleepParticle;
    public GameObject smallsleepParticle;
    public float speed = 10;
    public GameObject PlayerCharacter;
    public float Distance = 11;
    public int WaterValue = 0;
    public int WoodValue = 0;
    [HideInInspector]
    public bool AvailableMouvement = true;
    public int SmashGameScore = 26;
    public Material[] Anim;

    private bool InMiniGame = false;
    private int MiniGameScore = 0;
    private bool CanLaunchMiniGame = true;
    public bool CutMode = false;

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
        GambleElement();
        //PlayerCharacter.transform.SetPositionAndRotation(new Vector3(0, Distance, 0), Quaternion.identity);
        sleepParticle.SetActive(false);
        smallsleepParticle.SetActive(false);
    }

    void Update()
    {
        if (!InMiniGame && AvailableMouvement)
        {
            if (S_JD_GameManager.Instance.Stamina < 30 && S_JD_GameManager.Instance.Stamina > 10)
            {
                SetInactiveSleep();
                SetActiveSmallSleep();
            }
            if (S_JD_GameManager.Instance.Stamina < 10 && S_JD_GameManager.Instance.Stamina > 6)
            {
                SetActiveSleep();
                transform.Rotate(new Vector3(0, 0, (-Input.GetAxis("Horizontal") * Time.deltaTime) * speed * (S_JD_GameManager.Instance.Stamina/10)));
            }
            else if (S_JD_GameManager.Instance.Stamina < 6)
            {
                SetActiveSleep();
                transform.Rotate(new Vector3(0, 0, (-Input.GetAxis("Horizontal") * Time.deltaTime) * speed * (0.2f)));
            }
            else
            {
                SetInactiveSmallSleep();
                transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * Time.deltaTime) * speed);
            }

            if (Input.GetAxis("Horizontal") != 0)
                PlayerCharacter.transform.localScale = new Vector3((Input.GetAxis("Horizontal") * (10 / (Mathf.Abs(Input.GetAxis("Horizontal") * 10)))) * 2, 2, 2);

            
        }

        if (CutMode)
        {
            if (PlayerCharacter.GetComponent<MeshRenderer>().material != Anim[3])
                PlayerCharacter.GetComponent<MeshRenderer>().material = Anim[3];
        }            
        else if (!AvailableMouvement)
        {
            if (PlayerCharacter.GetComponent<MeshRenderer>().material != Anim[2])
                PlayerCharacter.GetComponent<MeshRenderer>().material = Anim[2];
        }           
        else if (Input.GetAxis("Horizontal") != 0)
        {
            if (PlayerCharacter.GetComponent<MeshRenderer>().material != Anim[1])
            {
                PlayerCharacter.GetComponent<MeshRenderer>().material = Anim[1];
            }
        }           
        else
        {
            if (PlayerCharacter.GetComponent<MeshRenderer>().material != Anim[0])
                PlayerCharacter.GetComponent<MeshRenderer>().material = Anim[0];
        }
            

        SetElement();

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
        WaterValue = 5;
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
        else
        {
            WoodValue = 5;
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
        GambleElement();

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
        S_JD_GameManager.Instance.StressValue += 40;

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
        if (elementType == 0)
        {
            //Water Element
            print("Water");
            PlayerCharacter.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.59f, 0.78f, 0.90f));
            print(PlayerCharacter.GetComponent<MeshRenderer>().material.GetColor("_Color"));
        }
        else if (elementType == 1)
        {
            //FireElement
            print("Fire");
            PlayerCharacter.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.91f, 0.68f, 0.01f));
            print(PlayerCharacter.GetComponent<MeshRenderer>().material);
        }
        else if (elementType == 2)
        {
            //GreenElement
            print("Green");
            PlayerCharacter.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.74f, 0.75f, 0.44f));
            print(PlayerCharacter.GetComponent<MeshRenderer>().material);
        }
        else
            print("Error in the Element set");
    }

    public void GambleElement()
    {
        int previousElement = elementType;
        elementType = Random.Range(0, 3);
        if (previousElement == elementType)
            GambleElement();
        else
        {
            SetElement();
        }
    }

    public void SetActiveSleep()
    {
        sleepParticle.SetActive(true);
    }

    public void SetInactiveSleep()
    {
        sleepParticle.SetActive(false);
    }

    public void SetActiveSmallSleep()
    {
        smallsleepParticle.SetActive(true);
    }

    public void SetInactiveSmallSleep()
    {
        smallsleepParticle.SetActive(false);
    }
}
