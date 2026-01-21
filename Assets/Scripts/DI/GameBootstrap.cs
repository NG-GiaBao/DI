using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class GameBootstrap : MonoBehaviour
{
    [Header("Reference")]
    [Space()]

    [SerializeField] private List<BaseInject> injectables;
    [SerializeField] private SerializedDictionary<string, BaseInject> injectableDictionary;

    private CoreContext _core;

    private void Awake()
    {


    }
    private void Start()
    {
        GetListInject();
        OnInitialized();
        foreach (var inj in injectables)
        {
            inj.OnInject(_core);
            inj.OnInit();
        }
    }

    private void OnInitialized()
    {
        UIManager ui = GetBaseSystem(nameof(UIManager)) as UIManager;
        if(ui == null) Debug.Log("UIManager not found.".ToColor(this, Color.red));

        Transform mainCanvas = ui.GetCanvas(UIManager.NameCanvas.Overlay).transform;
        if(mainCanvas == null) Debug.Log("Main Canvas not found.".ToColor(this, Color.red));

        _core = new CoreContext(mainCanvas);
        if (_core == null) Debug.Log("CoreContext initialized falled.".ToColor(this, Color.red));
    }

    private void GetListInject()
    {
        injectables = new List<BaseInject>(Register.GetAllInject());
        injectableDictionary = new SerializedDictionary<string, BaseInject>(Register.GetMapInject());
    }
    private BaseInject GetBaseSystem(string name)
    {
        if (injectableDictionary.TryGetValue(name, out BaseInject baseInject))
        {
            return baseInject;
        }

        return null;
    }
}