using UnityEngine;
using TMPro;

public class TV : MonoBehaviour
{
    [SerializeReference]
    private TextMeshPro attemps,
        jumps;

    [SerializeReference]
    private SpriteRenderer recordIcon;

    [SerializeReference]
    private SpriteRenderer heartIcon;
    private Color defaultRecordIconColor;

    public void Initialize()
    {
        defaultRecordIconColor = recordIcon.color;
        if (CrossSceneInfo.MultiplayerMode)
        {
            heartIcon.sprite = Resources.Load<Sprite>("2D/PlayerIcon");
            heartIcon.color = MaterialsManager.Materials[0].color;
            attemps.fontSize = 30;
            attemps.text = $"Player {0}";
        }
    }

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

    public void NextPlayer(int currentPlayer)
    {
        recordIcon.color = defaultRecordIconColor;
        heartIcon.color = MaterialsManager.Materials[currentPlayer].color;
        attemps.text = $"Player {currentPlayer}";
        jumps.text = "0";
    }
}
