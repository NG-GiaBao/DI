using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [Header("Reference")]
    [Space()]
    [SerializeField] private Transform mainCanvas;

    [SerializeField] private List<BaseInject> injectables;

    private CoreContext _core;

    private void Awake()
    {
        OnInitialized();
       
    }
    private void Start()
    {
        GetListInject();
        foreach (var inj in injectables)
        {
            inj.OnInject(_core);
            inj.OnInit();

        }
    }

    private void OnInitialized()
    {
        _core = new CoreContext(mainCanvas);
        if (_core != null)
        {
            Debug.Log("CoreContext initialized successfully.".ToColor(Color.green));
        }
    }

    private void GetListInject()
    {
        injectables = new List<BaseInject>(Register.GetAllInject());
    }    
   

}