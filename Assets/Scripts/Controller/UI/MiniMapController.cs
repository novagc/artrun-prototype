using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    public GameObject MiniMapCellPrefab;
    public RectTransform MiniMapContainer;

    public GameObject ItemPrefab;
    
    void Awake()
    {
        GlobalEventController.Instance.MapSpawned.AddListener(SpawnMiniMap);
        GlobalEventController.Instance.CellVisited.AddListener(ShowMiniMapCell);
    }

    void SpawnMiniMap()
    {
        var lvlData = GlobalDataController.Instance.CurrentLevelData;
        
        var height = 1f / lvlData.RowCount;
        var width = 1f / lvlData.ColumnCount;

        for (int i = 0; i < lvlData.RowCount; i++)
        {
            for (int j = 0; j < lvlData.ColumnCount; j++)
            {
                var cellInfo = GlobalDataController.Instance.CurrentMap[i][j];
                var obj = Instantiate(MiniMapCellPrefab, MiniMapContainer);
                var t = obj.GetComponent<RectTransform>();
                
                t.anchorMin = new Vector2(j * width, i * height);
                t.anchorMax = new Vector2((j + 1) * width, (i + 1) * height);
                t.rotation = Quaternion.Euler(180, 180, 0);

                cellInfo.MiniMapImage = obj.GetComponent<RawImage>();
                cellInfo.MiniMapImage.texture = cellInfo.Texture;
                cellInfo.MiniMapImage.color = lvlData.CellSettings.CellTextureSettings.DisabledMiniMapCellColor;
                
                if (cellInfo.IsBarrier)
                {
                    cellInfo.MiniMapImage.color = Color.black;
                }

                if (cellInfo.ItemInfo != null && cellInfo.ItemInfo.Object)
                {
                    cellInfo.ItemInfo.MiniMapIcon = Instantiate(ItemPrefab, t);
                    
                    var itemT = cellInfo.ItemInfo.MiniMapIcon.GetComponent<RectTransform>();
                    
                    itemT.anchorMin = new Vector2(0.25f, 0.25f);
                    itemT.anchorMax = new Vector2(0.75f, 0.75f);
                }
                
                obj.name = $"{i * GlobalDataController.Instance.CurrentLevelData.RowCount + j}";
            }
        }
    }

    void ShowMiniMapCell(Cell cellInfo)
    {
        cellInfo.MiniMapImage.color = Color.white;
        
    }
}
