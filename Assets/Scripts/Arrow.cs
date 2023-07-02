using UnityEngine;
using Dreamteck.Splines;

public class Arrow : JumperSubscribers
{
    private GameObject arrowObj;
    private SplineFollower sf;

    private void Start()
    {
        arrowObj = transform.GetChild(0).gameObject;
        sf = GetComponent<SplineFollower>();
    }

    protected override void OnJumperIdle()
    {
        arrowObj.SetActive(true);
        sf.follow = true;
    }

    protected override void OnJumperJump()
    {
        arrowObj.SetActive(false);
        sf.follow = false;
    }

    public void UpSpeed()
    {
        sf.followSpeed =
            (Mathf.Abs(sf.followSpeed) / sf.followSpeed) * ((Mathf.Abs(sf.followSpeed) + 1));
    }
}
