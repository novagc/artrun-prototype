using System;
using System.Linq;
using Controller;
using UnityEngine;

public class PlayerMiniMapIconUpdation : PlayerBehaviourComponent
{
    public RectTransform PlayerIconContainer;
    public GameObject PlayerIconPrefab;

    private float _height;
    private float _width;

    private RectTransform _iconTransform;
    private CoordinateTranslator _translator;
    
    private void Awake()
    {
        GlobalEventController.Instance.MapSpawned.AddListener(Init);
    }


    private void Init()
    {
        var lvlData = GlobalDataController.Instance.CurrentLevelData;
        var containerSizes = PlayerIconContainer.sizeDelta;
        
        _height = containerSizes.y;
        _width = containerSizes.x;

        var cellSize = new Vector2(_width / lvlData.RowCount, _height / lvlData.ColumnCount);

        var fromMin = GlobalDataController.Instance.CurrentMap[0][0].GameObject.transform.position;
        var fromMax = GlobalDataController.Instance.CurrentMap.Last().Last().GameObject.transform.position;

        _translator = new CoordinateTranslator(
            new Vector2(fromMin.x, fromMin.z),
            new Vector2(fromMax.x, fromMax.z),
            new Vector2(cellSize.x / 2, cellSize.y / 2),
            new Vector2(containerSizes.x - cellSize.x / 2, containerSizes.y - cellSize.y / 2));
        
        _iconTransform = Instantiate(PlayerIconPrefab, PlayerIconContainer).GetComponent<RectTransform>();
        _iconTransform.sizeDelta = cellSize;
        
        UpdateIconPosition();
    }

    private void UpdateIconPosition()
    {
        var coords = _translator.Translate(attachedController.transform.position.WithoutY());
        var rotation = attachedController.transform.rotation.eulerAngles;
        
        _iconTransform.anchoredPosition = coords;
        _iconTransform.rotation = Quaternion.Euler(0, 0, rotation.y * -1);
    }

    public override void OnUpdate()
    {
        UpdateIconPosition();
    }
}
