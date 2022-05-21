using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
namespace Controller
{
    public class GlobalEventController : MonoBehaviour
    {
        public static GlobalEventController Instance;
        
        public UnityEvent PlayerTookItem;
        public UnityEvent GameLost;
        public UnityEvent GameWin;
        public UnityEvent GameEnd;
        public UnityEvent GamePaused;
        public UnityEvent GameResumed;
        public UnityEvent MapSpawned;
        public UnityEvent<Cell> CellVisited;
        
        public GlobalEventController()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;
        }

        public void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
