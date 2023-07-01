using UnityEngine;
using TMPro;

public class Pedestal : MonoBehaviour
{
    private Champion gameWinner,
        second,
        third;
    private CameraOperator co;
    private bool playerWin;

    public void Initial(bool isPlayerWin)
    {
        playerWin = isPlayerWin;
        SetFinalText();
        SetChampionsMat();
        PlayFinalSound();
    }

    private Material GetRandomChampMat(int bet)
    {
        int lotCoin = Random.Range(0, 2);
        return lotCoin == bet ? MaterialsManager.greenColor : MaterialsManager.blueColor;
    }

    private void PlayFinalSound()
    {
        var soundPlayer = FindAnyObjectByType<SoundPlayer>();
        AudioClip sound = playerWin
            ? sound = Resources.Load<AudioClip>("Sounds/Pedestal_Win_Fanfar")
            : sound = Resources.Load<AudioClip>("Sounds/Pedestal_Lose_Fanfar");
        soundPlayer.PlaySound(sound);
    }

    private void SetChampionsMat()
    {
        third.SetChampMat(GetRandomChampMat(0));
        if (playerWin)
        {
            gameWinner.SetChampMat(MaterialsManager.playerRedColor);
            second.SetChampMat(GetRandomChampMat(1));
        }
        else
        {
            gameWinner.SetChampMat(GetRandomChampMat(1));
            second.SetChampMat(MaterialsManager.playerRedColor);
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
}
