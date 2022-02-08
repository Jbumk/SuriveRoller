using UnityEngine;

namespace Mirror
{
    /// <summary>Start position for player spawning, automatically registers itself in the NetworkManager.</summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/NetworkStartPosition")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-start-position")]
    public class NetworkStartPosition : MonoBehaviour
    {

        private GameObject MultiStage;
        private GameObject MultiPincet;
        private GameObject GameManager;
        private NetworkManager manager;

        public void Awake()
        {
            GameManager = GameObject.Find("MultiManager");
            manager = GameManager.GetComponent<NetworkManager>();

            MultiStage = GameObject.Find("MultiStage");
            MultiPincet = GameObject.Find("MultiPincet");
            
            if (manager.ChkPlayerCode() == 1)
            {
                NetworkManager.RegisterStartPosition(MultiStage.transform);
                MultiStage.transform.SetParent(transform);
            }
            else if(manager.ChkPlayerCode()==2)
            {
                NetworkManager.RegisterStartPosition(MultiPincet.transform);
                MultiPincet.transform.SetParent(transform);
            }

        }

        public void OnDestroy()
        {
            NetworkManager.UnRegisterStartPosition(transform);
        }
    }
}
