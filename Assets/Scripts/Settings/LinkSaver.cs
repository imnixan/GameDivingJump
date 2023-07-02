using UnityEngine;

public class LinkSaver : MonoBehaviour
{
    [SerializeReference]
    private TV _tv;

    [SerializeReference]
    private Jumper _jumper;

    [SerializeReference]
    private Pool _pool;

    [SerializeReference]
    private Arrow _arrow;

    [SerializeReference]
    private BigYellowButton _byb;

    [SerializeReference]
    private Tribune _tribune;

    private Pedestal _pedestal;

    public TV tv
    {
        get { return _tv; }
    }

    public Jumper jumper
    {
        get { return _jumper; }
    }

    public Pool pool
    {
        get { return _pool; }
    }

    public Arrow arrow
    {
        get { return _arrow; }
    }

    public BigYellowButton byb
    {
        get { return _byb; }
    }

    public Pedestal pedestal
    {
        get { return _pedestal; }
    }

    public Tribune tribune
    {
        get { return _tribune; }
    }

    public void Initial(Jumper currentJumper, Pedestal currentPedestal)
    {
        _jumper = currentJumper;
        _pedestal = currentPedestal;
    }
}
