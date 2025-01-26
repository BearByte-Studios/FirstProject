using UnityEngine;
using Mirror;
using Steamworks;



public class SteamLobby : MonoBehaviour 
{

    public GameObject hostButton = null;
    private const string HostAddressKey = "HostAddress";

    private NetworkManager networkManager;


    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> LobbyJoinRequested;
    protected Callback<LobbyEnter_t> LobbyEnter;


    






    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();

        if(!SteamManager.Initialized) {return;}

        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        LobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyRequested);
        LobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEnter);   
        
    
    }

        private void OnLobbyCreated(LobbyCreated_t callback)
        {
            if (callback.m_eResult != EResult.k_EResultOK)
            {
                hostButton.SetActive(true);
                return;

            }
            networkManager.StartHost();
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());

        }
        private void OnGameLobbyRequested(GameLobbyJoinRequested_t callback)
        {
            SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);

        }

        private void OnLobbyEnter(LobbyEnter_t callback)
        {
            if(NetworkServer.active)
            {
                return;
            }



            string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
            networkManager.networkAddress = hostAddress;
            networkManager.StartClient();
            hostButton.SetActive(false);
        }




        public void HostLobby()
        {
            hostButton.SetActive(false);
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);



        }

        

        


        
    }


