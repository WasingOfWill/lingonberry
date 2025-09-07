using UnityEngine;
using PolymindGames.UserInterface;

public class NetworkUIManager : MonoBehaviour
{
    [SerializeField] private SelectableButton hostButton;
    [SerializeField] private SelectableButton joinButton;
    [SerializeField] private SelectableButton disconnectButton;

    private void Start()
    {
        // Subscribe to the SelectableButton's Clicked events
        if (hostButton != null)
            hostButton.Clicked += OnHostButtonClicked;
        
        if (joinButton != null)
            joinButton.Clicked += OnJoinButtonClicked;
        
        if (disconnectButton != null)
            disconnectButton.Clicked += OnDisconnectButtonClicked;
    }

    private void OnDestroy()
    {
        // Clean up our event subscriptions
        if (hostButton != null)
            hostButton.Clicked -= OnHostButtonClicked;
        
        if (joinButton != null)
            joinButton.Clicked -= OnJoinButtonClicked;
        
        if (disconnectButton != null)
            disconnectButton.Clicked -= OnDisconnectButtonClicked;
    }

    private void OnHostButtonClicked(SelectableButton button)
    {
        LingonberryNetworkManager.Instance.StartHost();
    }

    private void OnJoinButtonClicked(SelectableButton button)
    {
        LingonberryNetworkManager.Instance.StartClient();
    }

    private void OnDisconnectButtonClicked(SelectableButton button)
    {
        LingonberryNetworkManager.Instance.Disconnect();
    }
}