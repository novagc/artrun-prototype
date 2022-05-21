using System;
using UnityEngine;
namespace Controller.Player
{
    public class PlayerVisitingCells : PlayerBehaviourComponent
    {
        private void Awake()
        {
            GlobalEventController.Instance.GameLost.AddListener(DeathAnimation);
            GlobalEventController.Instance.GameEnd.AddListener(DeathAnimation);
        }

        private void DeathAnimation()
        {
            attachedController.PlayAnimation("Death");
        }
    }
}
