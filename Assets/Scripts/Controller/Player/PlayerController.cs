using System.Collections.Generic;
using Controller;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<PlayerBehaviourComponent> PlayerBehaviours;
    
    public Rigidbody Rigidbody;
    public Animator Animator;

    public void PlayAnimation(string triggerName)
    {
        if (Animator)
        {
            Animator.SetTrigger(triggerName);
        }
    }

    public void SetRunAnimationSpeed(float speed)
    {
        if (Animator)
        {
            Animator.SetFloat("RunningSpeed", speed);
        }
    }

    public void GameEnd()
    {
        PlayerBehaviours.Clear();
    }
    
    private void Awake()
    {
        foreach (var behaviour in PlayerBehaviours)
        {
            behaviour.attachedController = this;
        }
        
        GlobalEventController.Instance.GameLost.AddListener(GameEnd);
        GlobalEventController.Instance.GameEnd.AddListener(GameEnd);
    }

    private void Start()
    {
        foreach (var behaviour in PlayerBehaviours)
        {
            behaviour.OnStart();
        }
    }

    private void Update()
    {
        foreach (var behaviour in PlayerBehaviours)
        {
            behaviour.OnUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach (var behaviour in PlayerBehaviours)
        {
            behaviour.OnFixedUpdate();
        }
    }

    private void LateUpdate()
    {
        foreach (var behaviour in PlayerBehaviours)
        {
            behaviour.OnLateUpdate();
        }
    }
}