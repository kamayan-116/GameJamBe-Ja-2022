using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Player : MonoBehaviour
{
    public float speed = 10;
    public GameObject PlayerCharacter;
    public float Distance = 11;
    void Start()
    {
        PlayerCharacter.transform.SetPositionAndRotation(new Vector3(0, Distance, 0), Quaternion.identity);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal") * Time.deltaTime) * speed);
    }
}
