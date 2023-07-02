using System.Collections.Generic;

public class GameJudgeMultiplayer : GameJudge
{
    private const int MaxPlayers = 3;
    private int _currentPlayer;
    private Dictionary<int, int> playersJumps;
    private int currentRecord;

    private int currentPlayer
    {
        get { return _currentPlayer; }
        set
        {
            _currentPlayer = value;
            if (_currentPlayer == MaxPlayers)
            {
                EndGame();
            }
            else
            {
                ChangePlayer();
            }
        }
    }

    protected override void InitializeFields()
    {
        base.InitializeFields();
        playersJumps = new Dictionary<int, int>();
        _currentPlayer = 0;
        playersJumps.Add(0, 0);
        linkSaver.tv.Initialize();
    }

    public override void JumpFinished()
    {
        linkSaver.tribune.TribunesJump(jumpWasSuccess, currentPlayer);
        if (jumpWasSuccess)
        {
            jumpWasSuccess = false;
            playersJumps[currentPlayer]++;
            linkSaver.tv.SetJumps(playersJumps[currentPlayer]);
            if (playersJumps[currentPlayer] > currentRecord)
            {
                linkSaver.tv.SetNewRecord();
                currentRecord = playersJumps[currentPlayer];
            }
        }
        else
        {
            currentPlayer++;
        }
    }

    private void ChangePlayer()
    {
        playersJumps.Add(currentPlayer, 0);
        linkSaver.pool.MakeDefaultPool();
        linkSaver.jumper.ChangeMaterial(MaterialsManager.Materials[currentPlayer]);
        linkSaver.tv.NextPlayer(currentPlayer);
        linkSaver.byb.NextPlayer();
    }

    protected override void EndGame()
    {
        HideUIAndMoveToPedestal();
        linkSaver.pedestal.Initial(playersJumps);
    }
}
