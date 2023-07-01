using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour
{
    public void SetChampColor(Material color)
    {
        foreach(var champColor in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            champColor.material = color;
        }
    }
}
