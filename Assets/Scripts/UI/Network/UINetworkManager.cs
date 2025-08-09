using UnityEngine;
using Unity.Netcode;

public class UINetworkManager : MonoBehaviour
{
    [SerializeField] private UIScreen screen;
    public void StartHost_Btn()
    {
        NetworkManager.Singleton.StartHost();
        screen.Hide();
    }

    public void FindMatch_Btn()
    {
        NetworkManager.Singleton.StartClient();
        screen.Hide();
    }
}
