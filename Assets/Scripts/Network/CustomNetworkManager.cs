using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public short lobbycount = 4;
    
    public override void OnStartServer()
    {
        gameObject.GetComponent<SteamLobbyManager>().CreateAndOpenLobbyAsync(lobbycount);
    }

}
