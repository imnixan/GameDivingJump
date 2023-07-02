using UnityEngine;

public class Water : MonoBehaviour
{
    private GameJudge gj;
    private ParticleSystem ps;
    private AudioClip[] waterSplashes;
    private SoundPlayer soundPlayer;

    public void Initialize(GameJudge gameJudge)
    {
        ps = GetComponentInChildren<ParticleSystem>();
        soundPlayer = FindAnyObjectByType<SoundPlayer>();
        waterSplashes = Resources.LoadAll<AudioClip>("Sounds/WaterSplashes");
        gj = gameJudge;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            gj.OnJumperTouchedWater();
            soundPlayer.PlaySound(waterSplashes[Random.Range(0, waterSplashes.Length)]);
            ps.Play();
        }
    }
}
