using UnityEngine;

public class JumperMultiplayer : Jumper
{
    private SkinnedMeshRenderer[] mrs;
    private Material currentMaterial;

    protected override void Initialize()
    {
        base.Initialize();
        mrs = GetComponentsInChildren<SkinnedMeshRenderer>();
        currentMaterial = MaterialsManager.Materials[0];
    }

    public override void ChangeMaterial(Material material)
    {
        currentMaterial = material;
    }

    protected override void SetIdle()
    {
        base.SetIdle();
        foreach (var mr in mrs)
        {
            mr.material = currentMaterial;
        }
    }
}
