using Controller;
using UnityEngine;
public class PlayerTakingItems : PlayerBehaviourComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            attachedController.Animator.SetTrigger("Take");
            GlobalEventController.Instance.PlayerTookItem.Invoke();
        }
    }
}
