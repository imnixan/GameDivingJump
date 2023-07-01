using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.Events;

public class Jumper : MonoBehaviour
{
    public static event UnityAction IdleStateEvent;
    public static event UnityAction JumpStateEvent;
    public static event UnityAction LayStateEvent;

    private bool touchedWater;
    private SoundPlayer soundPlayer;
    private AudioClip jump,
        fall;
    private SplineFollower sf;
    private RagdollController rc;
    private Animator animator;
    private Vector3 startPos;
    private Vector3 startRot;
    private BodyStates _bodyState = BodyStates.None;

    private BodyStates BodyState
    {
        get { return _bodyState; }
        set
        {
            if (_bodyState != value)
            {
                _bodyState = value;
                OnStateChanged();
            }
        }
    }

    private enum BodyStates
    {
        None,
        Idle,
        Jump,
        Fall,
        Lay
    }

    private void Start()
    {
        Initialize();
        BodyState = BodyStates.Idle;
    }

    private void Initialize()
    {
        startPos = transform.position;
        startRot = transform.eulerAngles;
        animator = GetComponent<Animator>();
        sf = GetComponent<SplineFollower>();
        rc = GetComponent<RagdollController>();
        soundPlayer = GameObject.FindAnyObjectByType<SoundPlayer>();
        fall = Resources.Load<AudioClip>("Sounds/Jumper_GroundFall");
        jump = Resources.Load<AudioClip>("Sounds/Jumper_Jump");
        rc.Initialize();
    }

    private void SetIdle()
    {
        animator.enabled = true;
        animator.SetInteger("State", (int)BodyState);
        rc.SetRagdollActive(false);
        transform.eulerAngles = startRot;
        transform.position = startPos;
        sf.enabled = false;
        touchedWater = false;
        IdleStateEvent?.Invoke();
    }

    private void SetJump()
    {
        animator.SetInteger("State", (int)BodyState);
        soundPlayer.PlaySound(jump);
        sf.enabled = true;
        sf.Restart();
        JumpStateEvent?.Invoke();
    }

    private void SetFall()
    {
        sf.enabled = false;
        rc.SetRagdollActive(true);
        animator.enabled = false;
    }

    private void SetLay()
    {
        LayStateEvent?.Invoke();
        if (!touchedWater)
        {
            soundPlayer.PlaySound(fall);
        }
    }

    private void OnStateChanged()
    {
        switch (BodyState)
        {
            case BodyStates.Idle:
                SetIdle();
                break;
            case BodyStates.Jump:
                SetJump();
                break;
            case BodyStates.Fall:
                SetFall();
                break;
            case BodyStates.Lay:
                SetLay();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (BodyState == BodyStates.Jump)
        {
            if (transform.eulerAngles.x > 25)
            {
                transform.eulerAngles = Vector3.MoveTowards(
                    transform.eulerAngles,
                    new Vector3(25, transform.eulerAngles.y, transform.eulerAngles.z),
                    Time.fixedDeltaTime
                );
            }
        }
    }

    public void OnJumpSplineFinal()
    {
        BodyState = BodyStates.Fall;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            BodyState = BodyStates.Lay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            touchedWater = true;
        }
    }

    public void BigButtonPressed()
    {
        switch (BodyState)
        {
            case BodyStates.Lay:
                BodyState = BodyStates.Idle;
                break;
            case BodyStates.Idle:
                BodyState = BodyStates.Jump;
                break;
        }
    }
}
