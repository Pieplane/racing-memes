using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementBase : MonoBehaviour, IUIElement
{
    [SerializeField] private GameObject uiContent;

    public virtual void Show()
    {
        if (uiContent != null)
        {
            uiContent.SetActive(true);
        }
    }
    public virtual void Hide()
    {
        if (uiContent != null)
        {
            uiContent.SetActive(false);
        }
    }

    
}
