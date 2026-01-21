using UnityEngine;
using AYellowpaper.SerializedCollections;

public class UIManager : BaseInject
{
    public enum NameCanvas
    {
        Overlay,
        WorldSpace,
    }    

    [SerializeField] private UiService uiService;
    [SerializedDictionary("NameCanvas", "Canvas")]
    [SerializeField] private SerializedDictionary<NameCanvas, Canvas> canvasDictionary = new ();

    private void Awake()
    {
        Register.RegisterInject(this);
    }

    public override void OnInject(CoreContext coreContext)
    {
        uiService = coreContext.UiService;
        if (uiService == null) Debug.Log("UiService initialized falled.".ToColor(this, Color.red));
    }

    public Canvas GetCanvas(NameCanvas nameCanvas)
    {
        if (canvasDictionary.TryGetValue(nameCanvas, out Canvas canvas))
        {
            return canvas;
        }
        Debug.LogError($"Canvas with name {nameCanvas} not found!");
        return null;
    }
}
