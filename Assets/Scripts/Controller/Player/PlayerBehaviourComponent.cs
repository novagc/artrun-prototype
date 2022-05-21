using UnityEngine;

public abstract class PlayerBehaviourComponent : MonoBehaviour
{
    public PlayerController attachedController { get; set; }

    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnLateUpdate() { }
}
