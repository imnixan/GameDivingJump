using UnityEngine;
using Dreamteck.Splines;

public class GameJudge : JumperSubscribers
{
    [SerializeReference] private TV tv;
    [SerializeReference] private Jumper jumper;
    [SerializeReference] private Pool pool;
    [SerializeReference] private SplineFollower arrow;
    [SerializeReference] private BigYellowButton byb;
    [SerializeReference] private Pedestal pedestal;
    [SerializeReference] private Tribune tribune;
    
    private const int MaxPlayersAttemps = 3;
    private CameraOperator co;
    private int _attempsLeft;
    private int _jumpsCount;
    private int oldRecord;
    private bool jumpWasSuccess;
    private bool newRecord;
    private int AttepmpsLeft
    {
        get
        {
            return _attempsLeft;
        }

        set
        {
            _attempsLeft = value;
            tv.SetAttemps(_attempsLeft);
            if(_attempsLeft == 0)
            {
                EndGame();
            }
        }
    }

    private int JumpsCount
    {
        get 
        {
            return _jumpsCount;
        }
        set
        {
            _jumpsCount = value;
            tv.SetJumps(_jumpsCount);
            if(_jumpsCount > oldRecord)
            {
                RegisterNewRecord();
            }
        }
    }
    private void Start()
    {
        co = Camera.main.GetComponent<CameraOperator>();
        oldRecord = PlayerPrefs.GetInt("Record");
        AttepmpsLeft = MaxPlayersAttemps;
        JumpsCount = 0;
        pedestal.gameObject.SetActive(false);
    }
    
    public void OnJumperTouchedWater()
    {
        jumpWasSuccess = true;
    }
   
    public void JumpFinished()
    {
        tribune.TribunesJump(jumpWasSuccess);
        if(jumpWasSuccess)
        {
            jumpWasSuccess = false;
            JumpsCount ++;
        }
        else
        {
            AttepmpsLeft--;
        }
    }

    public void UpdateScene()
    {
        pool.ChangeSizeAndPos();
        arrow.followSpeed = (Mathf.Abs(arrow.followSpeed) / arrow.followSpeed) * ((Mathf.Abs(arrow.followSpeed) + 1));
    }

    private void RegisterNewRecord()
    {
        tv.SetNewRecord();
        newRecord = true;
    }

    private void EndGame()
    {
        byb.Hide();
        jumper.enabled = false;
        if(newRecord)
        {
            PlayerPrefs.SetInt("Record", JumpsCount);
            PlayerPrefs.Save();
        }
        pedestal.gameObject.SetActive(true);
        pedestal.Initial(newRecord);
        co.MoveToPedestal();
    }

    protected override void OnJumperIdle()
    {
        UpdateScene();
    }

    protected override void OnJumperLay()
    {
        JumpFinished();
    }

    
}
