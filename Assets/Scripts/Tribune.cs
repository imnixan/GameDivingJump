using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tribune : MonoBehaviour
{
    private Fan[] fans;
    private Material jumpingFanMaterial;
    private SoundPlayer soundPlayer;
    private AudioClip succes, fail;
    private void Start()
    {
        fans = GetComponentsInChildren<Fan>();
        soundPlayer = FindAnyObjectByType<SoundPlayer>();
        succes = Resources.Load<AudioClip>("Sounds/Tribune_Success");
        fail = Resources.Load<AudioClip>("Sounds/Tribune_Fail");
    }

    public void TribunesJump(bool jumpWasSuccess)
    {
        SetFanMat(jumpWasSuccess);
        StartCoroutine(StandUpFans());
    }

    private void SetFanMat(bool jumpWasSuccess)
    {
        if(jumpWasSuccess)
        {
            jumpingFanMaterial = GameSettings.playerRedColor;
            soundPlayer.PlaySound(succes);
        }
        else
        {
            int tolCoin = Random.Range(0,2);
            soundPlayer.PlaySound(fail);
            jumpingFanMaterial = tolCoin == 1? GameSettings.greenColor : GameSettings.blueColor;
        }
    }

    IEnumerator StandUpFans()
    {
        foreach(var fan in fans)
        {

            if(fan.SameMaterial(jumpingFanMaterial))
            {
                fan.Jump(true);
                yield return null;
                continue;
            }
            else
            {
                fan.Jump(false);
            }
        }
    }
}
