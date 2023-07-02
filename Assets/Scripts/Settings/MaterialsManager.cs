using System.Collections.Generic;
using UnityEngine;

public class MaterialsManager : MonoBehaviour
{
    public static Dictionary<int, Material> Materials;

    //red = 0
    //green = 1
    //blue = 2
    private void Awake()
    {
        SetupMaterials();
    }

    private void SetupMaterials()
    {
        Materials = new Dictionary<int, Material>()
        {
            { 0, Resources.Load<Material>("Materials/RedMat") },
            { 1, Resources.Load<Material>("Materials/GreenMat") },
            { 2, Resources.Load<Material>("Materials/BlueMat") }
        };
    }
}
