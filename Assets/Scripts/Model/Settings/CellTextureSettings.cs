using UnityEngine;

[CreateAssetMenu(fileName = "New Cell's Texture Settings", menuName = "Custom/Settings/Cell texture settings", order = 1)]
public class CellTextureSettings : ScriptableObject
{
    [Range(0, 5)] public int BorderSize = 1;
    public Color BorderColor = Color.black;
    public Color DisabledMapCellColor = new Color(100 / 255f, 100 / 255f, 100 / 255f);
    public Color DisabledMiniMapCellColor = new Color(100 / 255f, 100 / 255f, 100 / 255f);
}
