using Controller;
using UnityEngine;

public class BarrierController : MapCellController
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalEventController.Instance.GameEnd.Invoke();
        }
    }
}
