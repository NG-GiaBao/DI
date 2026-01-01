using UnityEngine;

public class TranformRotate : IRotate
{
    private readonly Transform current;
    private readonly Transform target;
   

    public TranformRotate(Transform current, Transform target)
    {
        this.current = current;
        this.target = target;
    }

    public bool IsRotate()
    {
        if(current.rotation == target.rotation) return true;
        return false;
    }

    public void Rotate()
    {
        if (IsRotate()) return;
        Quaternion currentRotate = current.rotation;
        Quaternion targetRotate = target.rotation;
        Quaternion rotate = Quaternion.RotateTowards(currentRotate, targetRotate, 360f * Time.deltaTime);
        current.rotation = rotate;
    }

    
}
