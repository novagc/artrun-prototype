using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Cell's Settings", menuName = "Custom/Settings/Cell settings", order = 0)]
public class CellSettings : ScriptableObject
{
    [Range(0.1f, 10f)] public float CellSize = 1f;
    [Range(0.1f, 10f)] public float BarrierHeight = 1f;

    public CellTextureSettings CellTextureSettings;
}