using UnityEngine;

public abstract class JumperSubscribers : MonoBehaviour
{
    protected void OnEnable() 
    {
        Jumper.IdleStateEvent += OnJumperIdle;
        Jumper.JumpStateEvent += OnJumperJump;
        Jumper.LayStateEvent += OnJumperLay;
    }

    protected void OnDisable()
    {
        Jumper.IdleStateEvent -= OnJumperIdle;
        Jumper.JumpStateEvent -= OnJumperJump;
        Jumper.LayStateEvent -= OnJumperLay;
    }

    protected virtual void OnJumperIdle()
    {

    }
    protected virtual void OnJumperJump(){

    }
    protected virtual void OnJumperLay(){

    }
}
