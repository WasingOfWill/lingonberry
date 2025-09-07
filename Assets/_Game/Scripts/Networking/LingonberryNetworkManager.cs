using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class LingonberryNetworkManager : MonoBehaviour
{
    public static LingonberryNetworkManager Instance { get; private set; }
    private NetworkManager networkManager;

    [SerializeField]
    private string gameSceneName = "Lingonberry_scene01"; // The scene we want to load

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        networkManager = GetComponent<NetworkManager>();
        if (networkManager != null)
        {
            networkManager.OnServerStarted += OnServerStarted;
            networkManager.OnClientConnectedCallback += OnClientConnectedCallback;
            networkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
        }
    }

    private void OnDestroy()
    {
        if (networkManager != null)
        {
            networkManager.OnServerStarted -= OnServerStarted;
            networkManager.OnClientConnectedCallback -= OnClientConnectedCallback;
            networkManager.OnClientDisconnectCallback -= OnClientDisconnectCallback;
        }
    }

    private void OnServerStarted()
    {
        Debug.Log("Server started!");
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }
    }

    private void OnClientConnectedCallback(ulong clientId)
    {
        Debug.Log($"Client connected: {clientId}");
    }

    private void OnClientDisconnectCallback(ulong clientId)
    {
        Debug.Log($"Client disconnected: {clientId}");
    }

    // Helper methods to start networking
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene(gameSceneName);
    }
}