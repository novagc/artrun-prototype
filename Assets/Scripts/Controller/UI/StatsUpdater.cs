using Controller;
using TMPro;
using UnityEngine;

public class StatsUpdater : MonoBehaviour
{
    public TMP_Text CoinStatsText;
    public TMP_Text PaletteStatsText;
    public TMP_Text TubeStatsText;
    public TMP_Text FinishStatsText;

    private int _startCoinCount;
    private int _startPaletteCount;
    private int _startTubeCount;

    private int _finishCellsCount;

    private void Start()
    {
        _startCoinCount = GlobalDataController.Instance.CoinsCount;
        _startPaletteCount = GlobalDataController.Instance.PalettesCount;
        _startTubeCount = GlobalDataController.Instance.TubesCount;
        
        UpdateStats();
        UpdateFinishStats(null);
        
        GlobalEventController.Instance.PlayerTookItem.AddListener(UpdateStats);
        GlobalEventController.Instance.CellVisited.AddListener(UpdateFinishStats);
    }

    public void UpdateFinishStats(Cell cell)
    {
        var percent = ++_finishCellsCount * 100 / 
            (GlobalDataController.Instance.CurrentLevelData.CellCount - GlobalDataController.Instance.CurrentLevelData.BarriersCoordinates.Count);
        GlobalDataController.Instance.FinishPercent = percent;
        
        if (percent >= 95)
        {
            GlobalEventController.Instance.GameEnd.Invoke();
        }

        FinishStatsText.text = $"{percent}%";
    }

    public void UpdateStats()
    {
        CoinStatsText.text = $"{GlobalDataController.Instance.CoinsCount - _startCoinCount}/{GlobalDataController.Instance.CurrentLevelData.CoinsCount}";
        PaletteStatsText.text = $"{GlobalDataController.Instance.PalettesCount - _startPaletteCount}/{GlobalDataController.Instance.CurrentLevelData.PaletteCount}";
        TubeStatsText.text = $"{GlobalDataController.Instance.TubesCount - _startTubeCount}/{GlobalDataController.Instance.CurrentLevelData.TubeCount}";
    }
}
