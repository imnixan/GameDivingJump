using UnityEngine;

public class Pool : MonoBehaviour
{
    private const float StartMaxX = 7.38f;
    private const float StartMinX = -3.23f;
    private const float SizeScale = 1.4f;
    private const float PosScale = 1.1f;
    private const int ResizeMaxCount = 3;
    private float currentMaxX;
    private float currentMinX;
    private Vector3 startLocalScale;
    private int resizeCount;

    private void Start()
    {
        startLocalScale = transform.localScale;
        InitializeFields();
    }

    private void InitializeFields()
    {
        currentMaxX = StartMaxX;
        currentMinX = StartMinX;
        resizeCount = ResizeMaxCount;
    }

    public void ChangeSizeAndPos()
    {
        if (resizeCount > 0)
        {
            resizeCount--;
            currentMaxX *= PosScale;
            currentMinX *= PosScale;
            transform.localScale = transform.localScale / SizeScale;
        }
        transform.position = new Vector3(
            Random.Range(currentMaxX, currentMinX),
            transform.position.y,
            transform.position.z
        );
    }

    public void MakeDefaultPool()
    {
        transform.localScale = startLocalScale;
        InitializeFields();
    }
}
