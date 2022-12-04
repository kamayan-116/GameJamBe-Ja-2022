using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Bath : S_JD_Interact
{
    public ParticleSystem Smoke;
    public ParticleSystem Fire;
    private void Start()
    {
        interactionType = "GetBath";
    }
    protected override void Interaction()
    {
        StartCoroutine(Particle());
    }
    IEnumerator Particle()
    {
        Smoke.Play();
        Fire.Play();
        yield return new WaitForSeconds(10f);
        Smoke.Stop();
        Fire.Stop();
    }
}
