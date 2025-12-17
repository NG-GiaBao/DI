using UnityEngine;

public class ObjectOutline : MonoBehaviour
{
    [SerializeField] private LayerMask layerOutline;
    [SerializeField] private LayerMask layerDefault;

    private EventBus _events;
    
    private void OnDestroy()
    {
        if(_events != null)
        {
            _events.Unsubscribe<Test>(OnTest);
            _events.Unsubscribe<Test2>(OnTest1);
        }
    }

    public void Init(EventBus events)
    {
        _events = events;
        _events.Subscribe<Test>(OnTest);
        _events.Subscribe<Test2>(OnTest1);

    }

    public void SetLayerOutline()
    {
        int layer = LayerMarkHelper.ExtractSingleLayer(layerOutline);
        if (this.gameObject.layer != layer)
        {
            this.gameObject.layer = layer;
        }
    }

    public void SetLayerDefault()
    {
        int layer = LayerMarkHelper.ExtractSingleLayer(layerDefault);
        if (this.gameObject.layer != layer)
        {
            this.gameObject.layer = layer;
        }
    }

    private void OnTest(Test t)
    {
        Debug.Log($"OnTest received: a={t.a}, b={t.b}");
    }

    private void OnTest1(Test2 t2)
    {
        Debug.Log($"OnTest2 received: message={t2.message}");
    }
}
