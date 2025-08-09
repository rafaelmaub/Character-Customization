using UnityEngine;
using UnityEngine.Events;
public class UIScreen : MonoBehaviour
{
    public UnityEvent OnScreenEnter = new UnityEvent();
    public UnityEvent OnScreenExit = new UnityEvent();

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private bool hideOnAwake;

    private void Awake()
    {
        if(hideOnAwake)
        {
            Hide();
        }
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        OnScreenEnter.Invoke();
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        OnScreenExit.Invoke();
    }
}
