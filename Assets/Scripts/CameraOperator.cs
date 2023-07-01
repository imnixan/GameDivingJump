using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.Events;

public class CameraOperator : MonoBehaviour
{
    public event UnityAction OperatorOnFinish;
    public event UnityAction OperatorOnStart;
    private SplineFollower sf;
    private Vector3 startPos;
    private void Start()
    {
        sf = GetComponent<SplineFollower>();
        sf.follow = false;
        startPos = transform.position;
    }
    public void MoveToPedestal()
    {
        sf.follow = true;
        sf.direction = Spline.Direction.Forward;
    }

    public void MoveCameraToStart()
    {
        if(transform.position == startPos)
        {
            OperatorOnStart?.Invoke();
        }
        else
        {
            sf.follow = true;
            sf.direction = Spline.Direction.Backward;

        }
    }

    public void OnFinalDestination()
    {
        OperatorOnFinish?.Invoke();
        sf.follow = false;
    }

    public void OnStartDestination()
    {
        sf.follow = false;
        OperatorOnStart?.Invoke();
    }
}
