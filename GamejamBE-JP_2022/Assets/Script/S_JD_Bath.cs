using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Bath : S_JD_Interact
{
    public ParticleSystem Smoke;
    public ParticleSystem Fire;
    public AudioSource FireSound;
    public AudioSource WaterSound;
    public GameObject BathFull;
    public GameObject BathEmpty;
    public ParticleSystem Soap;
    public AudioSource SoapSound;

    private void Start()
    {
        interactionType = "GetBath";
    }
    protected override void Interaction()
    {
        if (!S_JD_Player.Instance.CanLaunchMiniGame)
            StartCoroutine(Particle());
    }
    IEnumerator Particle()
    {
        Smoke.Play();
        Fire.Play();
        FireSound.Play();
        WaterSound.Play();
        BathEmpty.SetActive(false);
        BathFull.SetActive(true);
        yield return new WaitForSeconds(10f);
        Smoke.Stop();
        Fire.Stop();
        FireSound.Stop();
        WaterSound.Stop();
        BathEmpty.SetActive(true);
        BathFull.SetActive(false);
    }

    public void Soaping()
    {
        Soap.Play();
        SoapSound.Play();
    }
}
