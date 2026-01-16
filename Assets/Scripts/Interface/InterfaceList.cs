using UnityEngine;

public interface IFsmIdentity
{

}

public interface IMover
{
    void MoveTo(Vector3 position);
    bool ReachedDestination();
    void Stop();
    void SetUpdateRotation(bool value);
    bool IsInFront(Transform from , Transform to);
}

public interface IAnimationController
{
    void SetRun(bool value);
    void SetSit(bool value);
}
public interface IRotate
{
    void Rotate();
    bool IsRotate();
}
