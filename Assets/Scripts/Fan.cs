using System.Collections;
using UnityEngine;

public class Fan : MonoBehaviour
{
    private const float Speed = 2f;
    private Material material;
    private Vector3 startPos;
    private Vector3 jumpPos;
    private bool jump;
    private bool onTop;

    private void Start()
    {
        startPos = transform.position;
        jumpPos = startPos + Vector3.up;
    }

    public void SetMaterial(Material fanMat)
    {
        MeshRenderer fanMR = GetComponent<MeshRenderer>();
        fanMR.material = fanMat;
        material = fanMR.material;
    }

    public void Jump(bool jump)
    {
        StopAllCoroutines();
        if (jump)
        {
            StartCoroutine(FanJumpAnim());
        }
    }

    public bool SameMaterial(Material material)
    {
        bool sameMat = this.material.color == material.color;
        return sameMat;
    }

    IEnumerator FanJumpAnim()
    {
        while (true)
        {
            if (onTop)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    startPos,
                    Time.fixedDeltaTime * Speed
                );
                if (transform.position == startPos)
                {
                    onTop = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    jumpPos,
                    Time.fixedDeltaTime * Speed
                );
                if (transform.position == jumpPos)
                {
                    onTop = true;
                }
            }
            yield return Time.fixedDeltaTime;
        }
    }
}
