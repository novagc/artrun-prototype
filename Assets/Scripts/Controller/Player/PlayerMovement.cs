using UnityEngine;

public class PlayerMovement : PlayerBehaviourComponent
{
    public float MoveSpeed = 8f;
    public float AirMoveCoefficient = 0.2f;

    public LayerMask GroundMask;
    
    private bool _groundCollided = true;

    private float _xAxis;
    private float _zAxis;

    private Rigidbody _rb;

    public override void OnStart()
    {
        _rb = attachedController.Rigidbody;
    }

    public override void OnUpdate()
    {
        var input = VirtualJoystick.Instance.JoystickInput;
        
        _zAxis = input.magnitude;

        if (!IsGrounded())
        {
            _groundCollided = false;
            _xAxis *= AirMoveCoefficient;
        }
        else if (IsGrounded() && !_groundCollided)
        {
            _groundCollided = true;
            _rb.velocity = Vector3.zero;
        }

        attachedController.SetRunAnimationSpeed(_zAxis);
    }

    public override void OnFixedUpdate()
    {
        _rb.MovePosition(transform.position + Time.deltaTime * MoveSpeed * transform.TransformDirection(new Vector3(0, 0, _zAxis) ));
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.1f, GroundMask);
    }
}
