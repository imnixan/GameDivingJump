using UnityEngine;

public class Champion : MonoBehaviour
{
    public void SetChampMat(Material mat)
    {
        foreach (var champMat in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            champMat.material = mat;
        }
    }
}
