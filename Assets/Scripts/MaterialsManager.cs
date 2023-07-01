using UnityEngine;

public class MaterialsManager : MonoBehaviour
{
    public static Material playerRedColor,
        greenColor,
        blueColor;

    private void Awake()
    {
        SetupMaterials();
    }

    private void SetupMaterials()
    {
        playerRedColor = Resources.Load<Material>("Materials/PlayerRedMat");
        greenColor = Resources.Load<Material>("Materials/GreenMat");
        blueColor = Resources.Load<Material>("Materials/BlueMat");
    }

    public static Material[] GetMaterials()
    {
        return new Material[] { playerRedColor, greenColor, blueColor };
    }
}
