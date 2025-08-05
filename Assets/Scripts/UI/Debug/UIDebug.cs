using UnityEngine;
using GameEconomy;
public class UIDebug : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroupComponent;
    private bool showing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroupComponent.alpha = 0;
        showing = false;
        canvasGroupComponent.blocksRaycasts = showing;
        canvasGroupComponent.interactable = showing;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            canvasGroupComponent.alpha = showing ? 0f : 1f;
            showing = !showing;
            canvasGroupComponent.blocksRaycasts = showing;
            canvasGroupComponent.interactable = showing;
        }
    }

    public void Button_AddCoins(int value)
    {
        CurrencyManager.ChangeCoins(value);
    }


}
