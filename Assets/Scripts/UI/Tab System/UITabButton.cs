using System;
using UnityEngine;
using UnityEngine.UI;

public class UITabButton : MonoBehaviour
{
    public Action<int> OnClickTabBtn;

    [SerializeField] private Button buttonOwner;
    [SerializeField] private Image buttonIcon;

    private int tabIndex;

    private void Awake()
    {
        buttonOwner.onClick.AddListener(ClickTab);
    }

    private void ClickTab()
    {
        OnClickTabBtn?.Invoke(tabIndex);
    }

    public void ConnectTabButton(int index)
    {
        tabIndex = index;
    }

    public void SetTabIcon(Sprite icon)
    {
        buttonIcon.sprite = icon;
    }


}
