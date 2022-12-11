using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

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
    public float scoreRate = 0.75f;
    public Material[] Anim;

    private bool InMiniGame = false;
    private int MiniGameScore = 0;
    public float EarthAttackValue = 0f;
    public bool CanLaunchMiniGame = true;
    public bool CutMode = false;

    public enum ElementType { Water = 0, Fire = 1, Wood = 2,}
    public ElementType elementType = 0;
    public bool RecoltWater = false;

    public AudioSource CuttingWood;
    public AudioSource Walk1;
    public AudioSource Walk2;
    public ParticleSystem NinjaSmoke;
    public AudioSource NinjaSoundEffect;
    public GameObject sleepParticle;
    // public ParticleSystem sleep;
    public GameObject smallsleepParticle;
    // public ParticleSystem smallsleep;
    public ParticleSystem heartParticle;
    public ParticleSystem footStepParticle;

    public Animator Sky;
    public S_JD_Bath Bath;
    public AudioSource NoSound;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        S_JD_Player.Instance.AvailableMouvement = true;
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
            if (S_JD_GameManager.Instance.Stamina < 40 && S_JD_GameManager.Instance.Stamina > 10)
            {
                SetInactiveSleep();
                SetActiveSmallSleep();
                transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * Time.deltaTime) * speed);
            }
            else if (S_JD_GameManager.Instance.Stamina < 10 && S_JD_GameManager.Instance.Stamina > 6)
            {
                SetActiveSleep();
                transform.Rotate(new Vector3(0, 0, (-Input.GetAxis("Horizontal") * Time.deltaTime) * speed * (S_JD_GameManager.Instance.Stamina/10)));
            }
            else if (S_JD_GameManager.Instance.Stamina < 6)
            {
                SetActiveSleep();
                transform.Rotate(new Vector3(0, 0, (-Input.GetAxis("Horizontal") * Time.deltaTime) * speed * (0.6f)));
            }
            else
            {
                SetInactiveSmallSleep();
                transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * Time.deltaTime) * speed);
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                PlayerCharacter.transform.localScale = new Vector3((Input.GetAxis("Horizontal") * (10 / (Mathf.Abs(Input.GetAxis("Horizontal") * 10)))) * 2, 2, 2);
                PlaySoundWalk();
                if (footStepParticle.isStopped) footStepParticle.Play();
            }
            else
            {
                if (footStepParticle.isPlaying) footStepParticle.Stop();
            }


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

        if (EarthAttackValue > 0)
        {
            float atkValue = Time.deltaTime * 5;
            S_JD_GameManager.Instance.EarthValue -= atkValue;
            EarthAttackValue -= atkValue;
            if (EarthAttackValue < 0) EarthAttackValue = 0;
        }
    }

    public void GetWater()
    {

    }

    public void GetWood()
    {

    }

    public void GetBath()
    {
        if (CanLaunchMiniGame && WoodValue >= 3 && WaterValue >= 1)
            LaunchMiniGame();
        else if (!InMiniGame)
            NoSound.Play();
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
        SetInactiveSmallSleep();
        SetInactiveSleep();
        NinjaSmoke.Play();
        NinjaSoundEffect.Play();
        StartCoroutine(SleepingLatence());
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
            //S_JD_GameManager.Instance.EarthValue -= S_JD_GameManager.Instance.actualEarthDamage;
            EarthAttackValue += S_JD_GameManager.Instance.actualEarthDamage;
            Bath.Soaping();

        }
        if (elementType == ElementType.Fire)
        {
            PlayHeartParticle();
            if (MiniGameScore == Mathf.Floor(SmashGameScore * scoreRate))
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
        S_JD_GameManager.Instance.actualSpeedtree = S_JD_GameManager.Instance.speedTree;
        yield return new WaitForSeconds(_delay);
        CanLaunchMiniGame = true;
        S_JD_CanvasManager.Instance.SetActivePressE();
    }

    private void LaunchMiniGame()
    {
        CanLaunchMiniGame = false;
        InMiniGame = true;
        S_JD_CanvasManager.Instance.SetInactivePressE();
        S_JD_CanvasManager.Instance.MiniGamePanel.SetActive(true);
        WoodValue -= 3;
        WaterValue -= 1;
        S_JD_CanvasManager.Instance.SetValueWater(WaterValue);
        S_JD_CanvasManager.Instance.SetValueTree(WoodValue);
        //S_JD_GameManager.Instance.EarthValue -= 40;
        S_JD_GameManager.Instance.actualSpeedtree = 0f;
    }

    public void SetElement()
    {
        if (elementType == ElementType.Water)
        {
            //Water Element
            PlayerCharacter.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.59f, 0.78f, 0.90f));
            //print(PlayerCharacter.GetComponent<MeshRenderer>().material.GetColor("_Color"));
        }
        else if (elementType == ElementType.Fire)
        {
            //FireElement
            PlayerCharacter.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.77f, 0.45f, 0.26f));
            //print(PlayerCharacter.GetComponent<MeshRenderer>().material);
        }
        else if (elementType == ElementType.Wood)
        {
            //GreenElement
            PlayerCharacter.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.74f, 0.75f, 0.44f));
            //print(PlayerCharacter.GetComponent<MeshRenderer>().material);
        }
        else
            print("Error in the Element set");
    }

    public void GambleElement()
    {
        ElementType previousElement = elementType;
        elementType = (ElementType)System.Enum.ToObject(typeof(ElementType), Random.Range(0, 3));
        if (previousElement == elementType)
            GambleElement();
        else
        {
            SetElement();
        }
    }

    #region Activate Particles

    public void SetActiveSleep()
    {
        sleepParticle.SetActive(true);
        // sleep.Play();
    }

    public void SetInactiveSleep()
    {
        sleepParticle.SetActive(false);
        // sleep.Stop();
    }

    public void SetActiveSmallSleep()
    {
        smallsleepParticle.SetActive(true);
        // smallsleep.Play();
    }

    public void SetInactiveSmallSleep()
    {
        smallsleepParticle.SetActive(false);
        // smallsleep.Stop();
    }

    public void PlayHeartParticle()
    {
        heartParticle.Play();
    }

    #endregion

    public void PlaySoundWalk()
    {
        if (!Walk1.isPlaying && !Walk2.isPlaying)
        {
            int index = Random.Range(0, 1);
            if (index == 0)
            {
                Walk1.pitch = Random.Range(1f, 1.3f);
                Walk1.Play();
            }
            else if (index == 1)
            {
                Walk2.pitch = Random.Range(1f, 1.3f);
                Walk2.Play();
            }
        }
    }

    IEnumerator SleepingLatence()
    {
        AvailableMouvement = false;
        yield return new WaitForSeconds(0.5f);
        AvailableMouvement = true;
    }

}
