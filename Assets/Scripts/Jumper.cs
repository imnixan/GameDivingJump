using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.Events;

public class Jumper : MonoBehaviour
{
    public static event UnityAction IdleStateEvent;
    public static event UnityAction JumpStateEvent;
    public static event UnityAction LayStateEvent;

    [SerializeField] private string CURRENT_STATE;
    [SerializeReference] private GameObject arrow;
    private SoundPlayer soundPlayer;
    private AudioClip jump, fall;
    private SplineFollower sf;
    private RagdollController rc;
    private Animator animator;
    private Vector3 startPos;
    private Vector3 startRot;
    private BodyStates _bodyState = BodyStates.None;
    private bool touchedWater;


    private BodyStates BodyState
    {
        get
        {
            return _bodyState;
        }

        set
        {
            if(_bodyState != value)
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
        AnimJump,
        RagdollFall,
        RagdollLay
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
        rc.SetRagdollActive(false);
        touchedWater = false;
        animator.enabled = true;
        transform.eulerAngles = startRot;
        sf.enabled = false;
        animator.SetInteger("State", (int) BodyState);
        transform.position = startPos;
        arrow.SetActive(true);
        IdleStateEvent?.Invoke();
    }

    private void SetAnimJump()
    {
        animator.SetInteger("State", (int) BodyState);
        soundPlayer.PlaySound(jump);
        arrow.SetActive(false);
        sf.enabled = true;
        sf.Restart();
        JumpStateEvent?.Invoke();
    }

    private void SetRagdollFall()
    {
        sf.enabled = false;
        rc.SetRagdollActive(true);
        animator.enabled = false;
    }

    private void SetRagdollLay()
    {
        LayStateEvent?.Invoke();
        if(!touchedWater)
        {
            soundPlayer.PlaySound(fall);
        }
    }




    private void OnStateChanged()
    {
        switch(BodyState)
        {
            case BodyStates.Idle:
                CURRENT_STATE = "Idle";
                SetIdle();
                break;
            case BodyStates.AnimJump:
                CURRENT_STATE = "Jump";
                SetAnimJump();
                break;
            case BodyStates.RagdollFall:
                CURRENT_STATE = "Fall";
                SetRagdollFall();
                break;
            case BodyStates.RagdollLay:
                CURRENT_STATE = "Lay";
                SetRagdollLay();
                break;
        }
    }

    private void FixedUpdate()
    {
        if(BodyState == BodyStates.AnimJump)
        {
            if(transform.eulerAngles.x > 25)
            {
                transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, new Vector3(25, transform.eulerAngles.y, transform.eulerAngles.z), Time.fixedDeltaTime);
            }

        }
    }

    public void OnJumpSplineFinal()
    {
        BodyState = BodyStates.RagdollFall;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("PLAYER TOUCH GROUND");
            BodyState = BodyStates.RagdollLay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            touchedWater = true;
        }
    }

    private void OnDisable()
    {
        arrow.SetActive(false);
    }

    public void BigButtonPressed()
    {
        switch(BodyState)
        {
            case BodyStates.RagdollLay:
                BodyState = BodyStates.Idle;
                break;
            case BodyStates.Idle:
                BodyState = BodyStates.AnimJump;
                break;
        }
    }
}
