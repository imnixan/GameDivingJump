using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField]
    private float maxX,
        minX;
    private const float SizeScale = 1.4f;
    private const float PosScale = 1.1f;
    private int resizeCount = 3;

    public void ChangeSizeAndPos()
    {
        if (resizeCount > 0)
        {
            resizeCount--;
            maxX *= PosScale;
            minX *= PosScale;
            transform.localScale = transform.localScale / SizeScale;
        }
        transform.position = new Vector3(
            Random.Range(minX, maxX),
            transform.position.y,
            transform.position.z
        );
    }
}
