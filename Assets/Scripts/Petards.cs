using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petards : MonoBehaviour
{
    private const float petardDelay = 0.3f;
    private SoundPlayer soundPlayer;
    private AudioClip[] petardSounds;
    private ParticleSystem[] particles;
    public void PlayPetards()
    {
        soundPlayer = FindAnyObjectByType<SoundPlayer>();
        petardSounds = Resources.LoadAll<AudioClip>("Sounds/Petards");
        particles = GetComponentsInChildren<ParticleSystem>();
        StartCoroutine(PetardsExplosions());
    }

    IEnumerator PetardsExplosions()
    {
        foreach(var petard in particles)
        {
            soundPlayer.PlaySound(petardSounds[Random.Range(0, petardSounds.Length)]);
            soundPlayer.Vibrate();
            petard.Play();
            yield return new WaitForSeconds(petardDelay);
        }
    }
}
