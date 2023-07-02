using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Pedestal : MonoBehaviour
{
    private const int WinnerID = 0;
    private const int SecondID = 1;
    private const int ThirdID = 2;
    protected Champion[] champions;
    private CameraOperator co;
    private bool playerWin;

    public void Initial(bool isPlayerWin)
    {
        playerWin = isPlayerWin;
        InitialChampions();
        SetFinalText();
        SetChampionsMat();
        PlayFinalSound();
    }

    protected void InitialChampions()
    {
        champions = GetComponentsInChildren<Champion>();
    }

    protected virtual void PlayFinalSound()
    {
        var soundPlayer = FindAnyObjectByType<SoundPlayer>();
        AudioClip sound = playerWin
            ? sound = Resources.Load<AudioClip>("Sounds/Pedestal_Win_Fanfar")
            : sound = Resources.Load<AudioClip>("Sounds/Pedestal_Lose_Fanfar");
        soundPlayer.PlaySound(sound);
    }

    private void SetChampionsMat()
    {
        champions[ThirdID].SetChampMat(MaterialsManager.Materials[ThirdID]);
        if (playerWin)
        {
            champions[WinnerID].SetChampMat(MaterialsManager.Materials[WinnerID]);
            champions[SecondID].SetChampMat(MaterialsManager.Materials[SecondID]);
        }
        else
        {
            champions[WinnerID].SetChampMat(MaterialsManager.Materials[SecondID]);
            champions[SecondID].SetChampMat(MaterialsManager.Materials[WinnerID]);
        }
    }

    private void PlayPetards()
    {
        if (playerWin)
        {
            GetComponentInChildren<Petards>().PlayPetards();
        }
    }

    private void SetFinalText()
    {
        GetComponentInChildren<TextMeshPro>().text = playerWin ? "New record!" : "Game end!";
    }

    private void OnEnable()
    {
        co = Camera.main.GetComponent<CameraOperator>();
        co.OperatorOnFinish += PlayPetards;
    }

    private void OnDisable()
    {
        co.OperatorOnFinish -= PlayPetards;
    }

    public virtual void Initial(Dictionary<int, int> playersJumps) { }
}
