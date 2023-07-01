using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameJudge gj; 
    private ParticleSystem ps;
    private AudioClip[] waterSplashes;
    private SoundPlayer soundPlayer;

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        soundPlayer = FindAnyObjectByType<SoundPlayer>();
        waterSplashes = Resources.LoadAll<AudioClip>("Sounds/WaterSplashes");
    }
    public  void OnTriggerEnter(Collider collider)
    {  
        if(collider.CompareTag("Player"))
        {
            gj.OnJumperTouchedWater();
            soundPlayer.PlaySound(waterSplashes[Random.Range(0, waterSplashes.Length)]);
            ps.Play();
        }
    }
}
