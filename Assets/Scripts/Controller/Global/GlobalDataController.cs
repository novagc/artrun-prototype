using UnityEngine;
public class GlobalDataController : MonoBehaviour
{
    public static GlobalDataController Instance;

    public LevelData CurrentLevelData;
    
    public int CoinsCount => PlayerPrefs.GetInt("Coin");
    public int PalettesCount => PlayerPrefs.GetInt("Palette");
    public int TubesCount => PlayerPrefs.GetInt("Tube");

    public Cell[][] CurrentMap;
    public int FinishPercent;
    
    public GlobalDataController()
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
        
        DontDestroyOnLoad(gameObject);
        
        CurrentLevelData = Resources.Load<LevelData>("Settings/Level Data 1");
    }
}
