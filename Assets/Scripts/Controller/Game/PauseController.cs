using System;
using Controller;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject StatsUI;
    public GameObject PauseUI;

    public void Pause()
    {
        Time.timeScale = 0;
        StatsUI.SetActive(false);
        PauseUI.SetActive(true);
        GlobalEventController.Instance.GamePaused.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        StatsUI.SetActive(true);
        PauseUI.SetActive(false);
        GlobalEventController.Instance.GameResumed.Invoke();
    }
}
