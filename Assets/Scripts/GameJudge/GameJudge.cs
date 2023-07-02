using UnityEngine;
using Dreamteck.Splines;

public class GameJudge : JumperSubscribers
{
    protected LinkSaver linkSaver;

    private const int MaxPlayersAttemps = 3;
    private int oldRecord;
    private int _jumpsCount;
    protected CameraOperator co;
    protected int _attempsLeft;
    protected bool jumpWasSuccess;
    protected bool newRecord;
    private int AttepmpsLeft
    {
        get { return _attempsLeft; }
        set
        {
            _attempsLeft = value;
            linkSaver.tv.SetAttemps(_attempsLeft);
            if (_attempsLeft == 0)
            {
                EndGame();
            }
        }
    }

    private int JumpsCount
    {
        get { return _jumpsCount; }
        set
        {
            _jumpsCount = value;
            linkSaver.tv.SetJumps(_jumpsCount);
            if (_jumpsCount > oldRecord)
            {
                RegisterNewRecord();
            }
        }
    }

    private void Start()
    {
        InitializeFields();
        linkSaver.pedestal.gameObject.SetActive(false);
    }

    protected virtual void InitializeFields()
    {
        linkSaver = GameObject.FindWithTag("LinkSaver").GetComponent<LinkSaver>();
        co = Camera.main.GetComponent<CameraOperator>();
        oldRecord = PlayerPrefs.GetInt("Record");
        AttepmpsLeft = MaxPlayersAttemps;
        JumpsCount = 0;
    }

    public void OnJumperTouchedWater()
    {
        jumpWasSuccess = true;
    }

    public virtual void JumpFinished()
    {
        linkSaver.tribune.TribunesJump(jumpWasSuccess);
        if (jumpWasSuccess)
        {
            jumpWasSuccess = false;
            JumpsCount++;
        }
        else
        {
            AttepmpsLeft--;
        }
    }

    public void UpdateScene()
    {
        linkSaver.pool.ChangeSizeAndPos();
        linkSaver.arrow.UpSpeed();
    }

    protected void RegisterNewRecord()
    {
        linkSaver.tv.SetNewRecord();
        newRecord = true;
    }

    protected virtual void EndGame()
    {
        HideUIAndMoveToPedestal();

        if (newRecord)
        {
            PlayerPrefs.SetInt("Record", JumpsCount);
            PlayerPrefs.Save();
        }
        linkSaver.pedestal.Initial(newRecord);
    }

    protected override void OnJumperIdle()
    {
        UpdateScene();
    }

    protected override void OnJumperLay()
    {
        JumpFinished();
    }

    protected void HideUIAndMoveToPedestal()
    {
        linkSaver.byb.Hide();
        linkSaver.jumper.enabled = false;
        linkSaver.pedestal.gameObject.SetActive(true);
        co.MoveToPedestal();
    }
}
