using System;
using Controller;
using UnityEngine;

public class GameEndController : MonoBehaviour
{
    private void Start()
    {
        GlobalEventController.Instance.GameEnd.AddListener(ChooseEndType);
    }

    public void ChooseEndType()
    {
        if (GlobalDataController.Instance.FinishPercent >= 75)
        {
            GlobalEventController.Instance.GameWin.Invoke();
        }
        else
        {
            GlobalEventController.Instance.GameLost.Invoke();
        }
    }
}