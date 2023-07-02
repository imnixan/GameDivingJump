using System.Collections;
using UnityEngine;

public class Tribune : MonoBehaviour
{
    private Fan[] fans;
    private Material jumpingFanMaterial;
    private SoundPlayer soundPlayer;
    private AudioClip succes,
        fail;
    private int _oppositeFansColorId;
    private int OppositeFansColorId
    {
        get { return _oppositeFansColorId; }
        set
        {
            _oppositeFansColorId = value;
            if (_oppositeFansColorId >= MaterialsManager.Materials.Count)
            {
                _oppositeFansColorId = 0;
            }
        }
    }

    private void Start()
    {
        fans = GetComponentsInChildren<Fan>();
        soundPlayer = FindAnyObjectByType<SoundPlayer>();
        succes = Resources.Load<AudioClip>("Sounds/Tribune_Success");
        fail = Resources.Load<AudioClip>("Sounds/Tribune_Fail");
        ColorizeFans();
    }

    public void TribunesJump(bool jumpWasSuccess, int playerMaterialId)
    {
        SetFanMat(jumpWasSuccess, playerMaterialId);
        StartCoroutine(StandUpFans());
    }

    public void TribunesJump(bool jumpWasSuccess)
    {
        SetFanMat(jumpWasSuccess, 0);
        StartCoroutine(StandUpFans());
    }

    private void ColorizeFans()
    {
        foreach (var fan in fans)
        {
            fan.SetMaterial(GetRandomMaterial());
        }
    }

    private Material GetRandomMaterial()
    {
        return MaterialsManager.Materials[Random.Range(0, 3)];
    }

    private void SetFanMat(bool jumpWasSuccess, int playerMaterialId)
    {
        if (jumpWasSuccess)
        {
            soundPlayer.PlaySound(succes);
            jumpingFanMaterial = MaterialsManager.Materials[playerMaterialId];
        }
        else
        {
            soundPlayer.PlaySound(fail);
            OppositeFansColorId = playerMaterialId + 1;
            jumpingFanMaterial = MaterialsManager.Materials[OppositeFansColorId];
        }
    }

    IEnumerator StandUpFans()
    {
        foreach (var fan in fans)
        {
            if (fan.SameMaterial(jumpingFanMaterial))
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
