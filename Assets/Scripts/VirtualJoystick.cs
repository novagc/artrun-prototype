using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static VirtualJoystick Instance;
    
    [SerializeField] private Image JoystickLayout;
    [SerializeField] private Image Joystick;
    
    public Vector2 JoystickInput => _joystickInput;
    
    private Vector2 _joystickInput;
    private Vector2 _joystickInputRaw;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    } 

    private void Update()
    {
        Joystick.rectTransform.localPosition = _joystickInputRaw;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                JoystickLayout.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out _joystickInputRaw
            ))
        {
            var radius = JoystickLayout.rectTransform.sizeDelta.x / 2;
            var x = _joystickInputRaw.x;
            var y = _joystickInputRaw.y;

            if (x * x + y * y > radius * radius)
            {
                _joystickInputRaw = _joystickInputRaw.normalized * radius;
            }

            _joystickInput.x = _joystickInputRaw.x / radius;
            _joystickInput.y = _joystickInputRaw.y / radius;            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickInput = Vector2.zero;
        _joystickInputRaw = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
