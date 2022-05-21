using System;
using Controller;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public GameObject StatsUI;
    public GameObject DeathUI;

    private void Start()
    {
        GlobalEventController.Instance.GameLost.AddListener(Death);
    }

    public void Death()
    {
        StatsUI.SetActive(false);
        DeathUI.SetActive(true);
    }
}
