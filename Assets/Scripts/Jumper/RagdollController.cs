using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] rbs;
    private Collider[] colliders;

    public void Initialize()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        SetRagdollActive(false);
    }

    public void SetRagdollActive(bool ragdollStatus)
    {
        foreach (var collider in colliders)
        {
            collider.enabled = ragdollStatus;
        }
        foreach (var rb in rbs)
        {
            rb.useGravity = ragdollStatus;
            rb.isKinematic = !ragdollStatus;
            if (rb.useGravity)
            {
                rb.velocity = Vector3.down * 12;
                rb.angularVelocity = Vector3.zero;
            }
        }
        rbs[0].isKinematic = false;
    }
}
