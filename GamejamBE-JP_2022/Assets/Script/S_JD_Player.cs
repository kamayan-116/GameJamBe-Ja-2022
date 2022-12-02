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

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        PlayerCharacter.transform.SetPositionAndRotation(new Vector3(0, Distance, 0), Quaternion.identity);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * Time.deltaTime) * speed);
    }

    public void GetWater()
    {
        WaterValue += 1;
    }

    public void GetWood()
    {
        WoodValue += 1;
    }
    public void Interact(string action)
    {
        if(action == "GetWater")
        {
            GetWater();
        }
        else if(action == "GetWood")
        {
            GetWood();
        }
        else
        {
            print("Wrong Action");
        }
    }
}
