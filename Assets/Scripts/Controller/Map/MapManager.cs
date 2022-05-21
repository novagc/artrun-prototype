using System;
using System.Linq;
using Controller;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public LevelData LevelData;
    public Transform CellsContainer;
    public Transform ItemsContainer;

    public GameObject CellPrefab;
    public GameObject BarrierPrefab;
    public GameObject WallPrefab;
    
    public GameObject CoinPrefab;
    public GameObject PalettePrefab;
    public GameObject TubePrefab;

    public GameObject Player;

    private System.Random _rnd;
    
    private Vector3 _cellScale;
    private Vector3 _barrierScale;

    void InitMap()
    {
        GlobalDataController.Instance.CurrentMap = Enumerable
            .Range(0, LevelData.RowCount)
            .Select(y =>
                Enumerable.Range(0, LevelData.ColumnCount)
                    .Select(x => new Cell())
                    .ToArray())
            .ToArray();
    }
    
    // TODO Переписать разрезку текстуры
    void SliceTexture()
    {
        var textures = TextureSlicer
            .Slice(LevelData.LevelImage, LevelData.RowCount, LevelData.ColumnCount, LevelData.CellSettings.CellTextureSettings);

        for (int i = 0; i < LevelData.RowCount; i++)
        {
            for (int j = 0; j < LevelData.ColumnCount; j++)
            {
                //GlobalDataController.Instance.LastMap[i][j].Texture = textures[LevelData.CellCount - i * LevelData.RowCount - j - 1];
                GlobalDataController.Instance.CurrentMap[i][j].Texture = textures[i][j];
            }
        }
    }

    GameObject CreateMapCell(GameObject prefab, Transform container, Vector3 scale, Vector3 position, string objName)
    {
        var obj = Instantiate(prefab, container);
        obj.transform.localScale = scale;
        obj.transform.position = position;
        obj.name = objName;

        return obj;
    }
    
    void SpawnCell(int i, int j)
    {
        GlobalDataController.Instance.CurrentMap[i][j].GameObject = CreateMapCell(
            CellPrefab,
            CellsContainer,
            _cellScale,
            new Vector3(j * LevelData.CellSettings.CellSize, 0, i * LevelData.CellSettings.CellSize),
            $"{i * LevelData.RowCount + j}");

        var mat = GlobalDataController.Instance.CurrentMap[i][j].GameObject.GetComponent<Renderer>().material;
        mat.mainTexture = GlobalDataController.Instance.CurrentMap[i][j].Texture;
        mat.color = GlobalDataController.Instance.CurrentLevelData.CellSettings.CellTextureSettings.DisabledMapCellColor;
        mat.SetFloat("_Glossiness", 0f);
    }

    void SpawnBarrier(int i, int j)
    {
        GlobalDataController.Instance.CurrentMap[i][j].GameObject = CreateMapCell(
            BarrierPrefab,
            CellsContainer,
            _barrierScale,
            new Vector3(
                j * LevelData.CellSettings.CellSize, 
                1.5f, 
                i * LevelData.CellSettings.CellSize),
            $"{i * LevelData.RowCount + j}");
    }
    
    void SpawnMap()
    {
        for (int i = 0; i < LevelData.RowCount; i++)
        {
            for (int j = 0; j < LevelData.ColumnCount; j++)
            {
                if (GlobalDataController.Instance.CurrentMap[i][j].IsBarrier)
                {
                        SpawnBarrier(i, j);
                }
                else
                {
                    SpawnCell(i, j);
                }
            }
        }
    }

    void SpawnWalls()
    {
        var p1 = new Vector3(
            -LevelData.CellSettings.CellSize / 8f * 5, 
            0, 
            LevelData.CellSettings.CellSize * (LevelData.RowCount - 1) / 2f);
        
        var p2 = new Vector3(
            LevelData.CellSettings.CellSize * (LevelData.ColumnCount - 1) + LevelData.CellSettings.CellSize / 8f * 5, 
            0, 
            LevelData.CellSettings.CellSize * (LevelData.RowCount - 1) / 2f);

        var p3 = new Vector3(
            LevelData.CellSettings.CellSize * (LevelData.ColumnCount - 1) / 2f, 
            0,
            -LevelData.CellSettings.CellSize / 8f * 5);
        
        var p4 = new Vector3(
            LevelData.CellSettings.CellSize * (LevelData.ColumnCount - 1) / 2f, 
            0, 
            LevelData.CellSettings.CellSize * (LevelData.RowCount - 1) + LevelData.CellSettings.CellSize / 8f * 5);

        var o1 = Instantiate(WallPrefab);
        o1.transform.position = p1;
        o1.transform.localScale = new Vector3(
            LevelData.CellSettings.CellSize / 4f, 
            1.5f, 
            LevelData.CellSettings.CellSize * LevelData.RowCount);
        
        var o2 = Instantiate(WallPrefab);
        o2.transform.position = p2;
        o2.transform.localScale = new Vector3(
            LevelData.CellSettings.CellSize / 4f, 
            1.5f, 
            LevelData.CellSettings.CellSize * LevelData.RowCount);
        
        var o3 = Instantiate(WallPrefab);
        o3.transform.position = p3;
        o3.transform.localScale = new Vector3(
            LevelData.CellSettings.CellSize * LevelData.ColumnCount + LevelData.CellSettings.CellSize / 2f, 
            1.5f, 
            LevelData.CellSettings.CellSize / 4f);
        
        var o4 = Instantiate(WallPrefab);
        o4.transform.position = p4;
        o4.transform.localScale = new Vector3(
            LevelData.CellSettings.CellSize * LevelData.ColumnCount + LevelData.CellSettings.CellSize / 2f, 
            1.5f, 
            LevelData.CellSettings.CellSize / 4f);
    }
    
    void InitBarriers()
    {
        foreach (var coord in LevelData.BarriersCoordinates)
        {
            GlobalDataController.Instance.CurrentMap[coord.Y][coord.X].IsBarrier = true;
        }
    }

    Cell GetRandomFreeCell()
    {
        Cell res;
        var i = 0;
        do
        {
            var num = _rnd.Next(0, LevelData.CellCount);
            res = GlobalDataController.Instance.CurrentMap[num / LevelData.ColumnCount][num % LevelData.ColumnCount];
            if (++i > LevelData.CellCount * 2)
            {
                throw new Exception();
            }
        }
        while (res.IsStart || res.IsBarrier || res.ItemInfo != null);

        return res;
    }

    void SpawnItem(GameObject prefab, string prefix, int count, Transform container)
    {
        for (int i = 0; i < count; i++)
        {
            var cell = GetRandomFreeCell();

            cell.ItemInfo = new Item
            {
                Object = Instantiate(prefab, container),
                Type = prefix
            };           
            
            cell.ItemInfo.Object.GetComponent<ItemController>().Info = cell.ItemInfo;
            cell.ItemInfo.Object.transform.position = cell.GameObject.transform.position + new Vector3(0, LevelData.CellSettings.CellSize * 0.5f, 0);
            cell.ItemInfo.Object.transform.localScale *= LevelData.CellSettings.CellSize * 0.5f;
            cell.ItemInfo.Object.name = $"{prefix} {i}";
        }
    }
    
    void SpawnItems()
    {
        SpawnItem(CoinPrefab, "Coin", LevelData.CoinsCount, ItemsContainer);
        SpawnItem(TubePrefab, "Tube", LevelData.TubeCount, ItemsContainer);
        SpawnItem(PalettePrefab, "Palette", LevelData.PaletteCount, ItemsContainer);
    }

    void SpawnPlayer()
    {
        Player.transform.position = GlobalDataController.Instance.CurrentMap[LevelData.StartCoordinates.Y][LevelData.StartCoordinates.X].GameObject.transform.position +
            new Vector3(0, 3, 0);
        
        GlobalDataController.Instance.CurrentMap[LevelData.StartCoordinates.Y][LevelData.StartCoordinates.X].IsStart = true;
        GlobalDataController.Instance.CurrentMap[LevelData.StartCoordinates.Y][LevelData.StartCoordinates.X].Visited = false;
    }
    
    void Init()
    {
        LevelData = GlobalDataController.Instance.CurrentLevelData;
        _rnd = new System.Random();
        _cellScale = new Vector3(LevelData.CellSettings.CellSize, 1, LevelData.CellSettings.CellSize);
        
        var localScale = BarrierPrefab.transform.localScale;
        
        _barrierScale = new Vector3(
            localScale.x * LevelData.CellSettings.CellSize, 
            localScale.y * LevelData.CellSettings.BarrierHeight, 
            localScale.z * LevelData.CellSettings.CellSize);
    }
    
    void Start()
    {
        Init();
        InitMap();
        InitBarriers();
        
        SliceTexture();
        
        SpawnMap();
        SpawnWalls();
        SpawnPlayer();
        SpawnItems();

        GlobalEventController.Instance.MapSpawned.Invoke();
    }
}
