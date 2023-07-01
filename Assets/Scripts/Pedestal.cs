using UnityEngine;
using TMPro;

public class Pedestal : MonoBehaviour
{
    
    [SerializeReference] private Champion gameWinner, second, third;
    private CameraOperator co;
    private bool playerWin;

    public void Initial(bool isPlayerWin)
    {
        playerWin = isPlayerWin;
        var soundPlayer = FindAnyObjectByType<SoundPlayer>();
        AudioClip sound;
        GetComponentInChildren<TextMeshPro>().text = isPlayerWin? "New record!" : "Game end!";
        int lotCoin = Random.Range(0,2);
        third.SetChampColor(lotCoin == 0? GameSettings.greenColor : GameSettings.blueColor);
        if(isPlayerWin)
        {   
            sound = Resources.Load<AudioClip>("Sounds/Pedestal_Win_Fanfar");
            gameWinner.SetChampColor(GameSettings.playerRedColor);
            second.SetChampColor(lotCoin == 1? GameSettings.greenColor : GameSettings.blueColor);
        }
        else
        {   
            sound = Resources.Load<AudioClip>("Sounds/Pedestal_Lose_Fanfar");
            gameWinner.SetChampColor(lotCoin == 1? GameSettings.greenColor : GameSettings.blueColor);
            second.SetChampColor(GameSettings.playerRedColor);
        }
        soundPlayer.PlaySound(sound);
    }

    public void PlayPetards()
    {
        if(playerWin)
        {
            GetComponentInChildren<Petards>().PlayPetards();
        }
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
