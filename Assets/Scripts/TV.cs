using UnityEngine;
using TMPro;

public class TV : MonoBehaviour
{
    [SerializeReference] private TextMeshPro attemps, jumps;
    [SerializeReference] private SpriteRenderer recordIcon;

    public void SetAttemps(int attempsLeft)
    {
        attemps.text = attempsLeft.ToString();
    }

    public void SetJumps(int jumpsWas)
    {
        jumps.text = jumpsWas.ToString();
    }

    public void SetNewRecord()
    {
        recordIcon.color = Color.yellow;
    }
}
