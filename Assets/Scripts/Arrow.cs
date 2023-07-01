using UnityEngine;

public class Arrow : JumperSubscribers
{
    private GameObject arrowObj;

    private void Start()
    {
        arrowObj = transform.GetChild(0).gameObject;
    }

    protected override void OnJumperIdle()
    {
        arrowObj.SetActive(true);
    }

    protected override void OnJumperJump()
    {
        arrowObj.SetActive(false);
    }
}
