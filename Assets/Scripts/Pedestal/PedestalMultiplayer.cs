using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class PedestalMultiplayer : Pedestal
{
    private TextMeshPro[] textFields;
    private List<KeyValuePair<int, int>> winnersList;

    public override void Initial(Dictionary<int, int> playersJumps)
    {
        textFields = GetComponentsInChildren<TextMeshPro>();
        InitialChampions();
        winnersList = playersJumps.OrderByDescending(x => x.Value).ToList();
        SetFinalText();
        SetChampionsMat();
        PlayFinalSound();
    }

    private void SetFinalText()
    {
        textFields[0].text = $"Player №{winnersList[0].Key}\nwon";
        for (int i = 1; i < textFields.Length; i++)
        {
            textFields[i].text = winnersList[i - 1].Value.ToString();
        }
    }

    private void SetChampionsMat()
    {
        for (int i = 0; i < winnersList.Count; i++)
        {
            champions[i].SetChampMat(MaterialsManager.Materials[winnersList[i].Key]);
        }
    }

    protected override void PlayFinalSound()
    {
        FindAnyObjectByType<SoundPlayer>()
            .PlaySound(Resources.Load<AudioClip>("Sounds/Pedestal_Win_Fanfar"));
    }
}
