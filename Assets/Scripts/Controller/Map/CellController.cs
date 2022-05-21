using System;
using Controller;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class CellController : MapCellController
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (CellInfo.Visited)
        {
            GlobalEventController.Instance.GameEnd.Invoke();
            return;
        }
        
        GetComponent<Renderer>().material.color = Color.white;
        
        GlobalEventController.Instance.CellVisited.Invoke(CellInfo);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        CellInfo.Visited = true;
    }
}
