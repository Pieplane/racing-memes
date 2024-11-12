using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, IUIElement> uiElements = new Dictionary<string, IUIElement>();
    private IUIElement currentUIElement;

    public void RegisterUIElement(string elementName, IUIElement element)
    {
        if(!uiElements.ContainsKey(elementName))
        {
            uiElements.Add(elementName, element);
        }
    }
    public void ShowUIElement(string elementName)
    {
        if(uiElements.TryGetValue(elementName, out IUIElement element))
        {
            if(currentUIElement != null)
            {
                currentUIElement.Hide();
            }

            element.Show();
            currentUIElement = element;
        }
        else
        {
            Debug.Log($"UI element {elementName} not found!");
        }
    }
    public void HideCurrentUIElement()
    {
        if(currentUIElement != null)
        {
            currentUIElement.Hide();
            currentUIElement = null;
        }
    }
}
