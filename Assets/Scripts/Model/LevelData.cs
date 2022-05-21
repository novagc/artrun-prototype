using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Custom/Level/Level Data", fileName = "Level Data 1")]
public class LevelData : ScriptableObject
{
    public string LevelName;
    public string LevelDescription;
    public Texture2D LevelImage;

    public CellSettings CellSettings;
    
    [Range(2, 100)] public int RowCount = 10;
    [Range(2, 100)] public int ColumnCount = 10;

    [Range(2, 1000)] public int CoinsCount = 2;
    [FormerlySerializedAs("PalletCount")]
    [Range(2, 1000)] public int PaletteCount = 2;
    [Range(2, 1000)] public int TubeCount = 2;

    public Coordinate StartCoordinates;
    
    public List<Coordinate> BarriersCoordinates = new List<Coordinate>();

    public Vector3 CameraFinalPosition;
    
    public int CellCount => RowCount * ColumnCount;
}