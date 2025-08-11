using UnityEngine;
using Unity.Netcode;

public class UINetworkManager : MonoBehaviour
{
    [SerializeField] private UIScreen screen;
    [SerializeField] private GameObject gameplayScreen;
    public void StartHost_Btn()
    {
        screen.Hide();
        gameplayScreen.SetActive(true);
        NetworkManager.Singleton.StartHost();
        
    }

    public void FindMatch_Btn()
    {
        screen.Hide();
        gameplayScreen.SetActive(true);
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if(id == NetworkManager.Singleton.LocalClientId)
            {
                ExitGame_Btn();
            }
        };

        NetworkManager.Singleton.StartClient();

        
    }

    public void ExitGame_Btn()
    {
        NetworkManager.Singleton.Shutdown();
        gameplayScreen.SetActive(false);
        screen.Show();
    }

    public void ResetProgress_Btn()
    {
        PlayerPrefs.DeleteAll();
        QuitApplication_Btn();
    }
    public void QuitApplication_Btn()
    {
        
        Application.Quit();
    }
}
