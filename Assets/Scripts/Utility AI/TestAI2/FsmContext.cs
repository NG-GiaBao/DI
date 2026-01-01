using UnityEngine;

public class FsmContext
{
    public FsmContext(IMover mover, IAnimationController controller , IRotate rotate)
    {
        Mover = mover;
        Controller = controller;
        Rotate = rotate;
    }

    public IMover Mover { get; private set; }
    public IAnimationController Controller { get; private set; }
    public IRotate Rotate { get; private set; }
}
