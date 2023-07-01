using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeReference] private GameSettings gs;
    private AudioSource soundPlayer;
    void Start()
    {
        soundPlayer = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        if(gs.SoundOn)
        {
            soundPlayer.PlayOneShot(sound);
        }    
    }

    public void Vibrate()
    {
        if(gs.VibrationOn)
        {
            Handheld.Vibrate();
        }
    }


}
