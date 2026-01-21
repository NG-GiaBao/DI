using UnityEngine;

public interface IFsmIdentity { }


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
    void SetTrigger(string name);
    void SetBool(string name, bool value);
    bool IsAnimFinished(string name);
}
public interface IRotate
{
    void Rotate();
    bool IsRotate();
}
