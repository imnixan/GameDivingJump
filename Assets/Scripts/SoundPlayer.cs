using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource soundPlayer;

    void Start()
    {
        soundPlayer = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        if (GameSettings.SoundOn)
        {
            soundPlayer.PlayOneShot(sound);
        }
    }

    public void Vibrate()
    {
        if (GameSettings.VibrationOn)
        {
            Handheld.Vibrate();
        }
    }
}
