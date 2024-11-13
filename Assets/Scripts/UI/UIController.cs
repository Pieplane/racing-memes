using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public UIManager uiManager;
    public UpgradePanel upgradePanel;

    private void Start()
    {
        TryRegisterPanel("Upgrade", upgradePanel);
    }
    private void TryRegisterPanel(string panelName, IUIElement panel)
    {
        if(panel != null)
        {
            uiManager.RegisterUIElement(panelName, panel);
        }
    }
    public void OpenUpgradePanel()
    {
        uiManager.ShowUIElement("Upgrade");
    }
    public void CloseCurrentPanel()
    {
        uiManager.HideCurrentUIElement();
    }
}
