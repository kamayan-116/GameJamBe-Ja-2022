using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Bath : S_JD_Interact
{
    public ParticleSystem Smoke;
    public ParticleSystem Fire;
    public AudioSource FireSound;
    public AudioSource WaterSound;
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
        yield return new WaitForSeconds(10f);
        Smoke.Stop();
        Fire.Stop();
        FireSound.Stop();
        WaterSound.Stop();
    }
}
