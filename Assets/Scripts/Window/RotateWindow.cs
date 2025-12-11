using UnityEngine;
using DG.Tweening;

public class RotateY : MonoBehaviour
{

    [SerializeField] private float maxAngle = 100f;
    [SerializeField] private float durationMin = 2f;
    [SerializeField] private float durationMax = 2f;
    [SerializeField] private Ease ease = Ease.InOutSine;
    [SerializeField] private bool isRight;

    Tween rotateTween;

    void Start()
    {
        if(isRight)
        {
            Vector3 rotation = new(0f, maxAngle, transform.eulerAngles.z);
            rotateTween = transform.DOLocalRotate(rotation, Random.Range(durationMin,durationMax))
                .SetEase(ease)
                .SetLoops(-1, LoopType.Yoyo);
        } 
        else
        {
            Vector3 rotation = new(0f, -maxAngle, transform.eulerAngles.z);
            rotateTween = transform.DOLocalRotate(rotation, Random.Range(durationMin, durationMax))
                .SetEase(ease)
                .SetLoops(-1, LoopType.Yoyo);
        }    
       
           
    }
}
