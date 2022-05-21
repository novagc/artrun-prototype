using UnityEngine;

public class PlayerRotation : PlayerBehaviourComponent
{
    [Range(0f, 10f)] public float Speed;
    
    public override void OnFixedUpdate()
    {
        var input = VirtualJoystick.Instance.JoystickInput;
        var angle = Mathf.Clamp(Vector3.Angle(Vector3.forward, new Vector3(input.x, 0, input.y)) * 2, 0, 180);

        if (input.x < 0)
        {
            angle *= -1;
        }
        
        gameObject.transform.Rotate(0, angle * Time.fixedDeltaTime * Speed * input.magnitude, 0);
    }
}