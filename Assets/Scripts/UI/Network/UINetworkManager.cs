using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class UINetworkManager : MonoBehaviour
{
    [SerializeField] private UIScreen screen;
    public void StartHost_Btn()
    {
        screen.Hide();

        NetworkManager.Singleton.StartHost();
        
    }

    public void FindMatch_Btn()
    {
        screen.Hide();

        NetworkManager.Singleton.OnClientStarted += () =>
        {
            if(!NetworkManager.Singleton.IsConnectedClient)
            {
                NetworkManager.Singleton.Shutdown();
                Debug.LogWarning("No matches found, try hosting a private match");
                screen.Show();
            }
            
        };

        NetworkManager.Singleton.StartClient();

        
    }
}
