using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Cell
{
    private GameObject _gameObject;
    
    public Texture2D Texture;
    public RawImage MiniMapImage;
    
    public bool IsStart;
    public bool IsBarrier;
    public bool Visited;
    
    public Item ItemInfo;

    public GameObject GameObject
    {
        get => _gameObject;
        set
        {
            _gameObject = value;
            _gameObject.GetComponent<MapCellController>().CellInfo = this;
        }
    }
}
