
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class UiService
{
    private readonly Transform mainCanvas;

    private readonly Dictionary<Type, IUiController> controllerDict = new();

    public UiService(Transform mainCanvas)
    {
        this.mainCanvas = mainCanvas;
    }

    public void Show<TController>(object data = null) where TController : IUiController , new()
    {
        Type controllerType = typeof(TController);
        //1 kiểm tra cache : Nếu đã có thì dùng luôn
        if(controllerDict.TryGetValue(controllerType, out IUiController uiController))
        {
            uiController.OnShow(data);
            return;
        }

        //2 Nếu chưa có -> Tạo mới 
        Type viewType = null;
        Type baseType = controllerType.BaseType;
        while(baseType != null)
        {
            if(baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(BaseUiController<>))
            {
                // Lấy tham số T thứ nhất (chính là TView)
                viewType = baseType.GetGenericArguments()[0];
                break;
            }
            baseType = baseType.BaseType;
        }
        if (viewType == null)
        {
            Debug.LogError($"Controller {controllerType.Name} không kế thừa từ BaseUiController<TView> nên không tìm thấy View tương ứng!");
            return;
        }

        //2.b Load Prefab View dựa trên Type của View 
        //Type viewType = attribute.ViewType;
        GameObject viewPrefabs = Resources.Load<GameObject>($"UI/{viewType.Name}");
        if(viewPrefabs == null)
        {
            Debug.LogError($"Không tìm thấy Prefab UI tại đường dẫn: UI/{viewType.Name}");
            return;
        }

        // Bước 2c: Instantiate View lên Canvas
        GameObject viewObj = Object.Instantiate(viewPrefabs, mainCanvas);
        UiElement viewComp = viewObj.GetComponent<UiElement>();
        TController newController = new();
        newController.Init(viewComp);
        controllerDict[controllerType] = newController;
        newController.OnShow(data);
    }
    public void Hide<TController>()
    {
        Type controllerType = typeof(TController);
        if( controllerDict.TryGetValue(controllerType,out IUiController uiController))
        {
            uiController.OnHide();
        }    
    }
}
