using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    public Transform CameraTransform;

    public GameObject StatsUI;
    public GameObject WinUI;

    public RawImage Star1;
    public RawImage Star2;
    public RawImage Star3;

    public float MovingTime = 2;
    public float ShowingTime = 1;

    private void Start()
    {
        GlobalEventController.Instance.GameWin.AddListener(StartWinning);
    }

    public void StartWinning()
    {
        StatsUI.SetActive(false);
        
        StartCoroutine(Win(MovingTime, ShowingTime));
    }
    
    private void ShowWinUI()
    {
        WinUI.SetActive(true);

        if (GlobalDataController.Instance.FinishPercent >= 85)
        {
            Star2.texture = Star1.texture;
        }

        if (GlobalDataController.Instance.FinishPercent >= 95)
        {
            Star3.texture = Star1.texture;
        }
    }

    private IEnumerator Win(float movingTime, float showingTime)
    {
        var stepCount = movingTime / Time.fixedDeltaTime;
        
        var transformStep = 
            (GlobalDataController.Instance.CurrentLevelData.CameraFinalPosition - CameraTransform.position) / stepCount;
        var rotationStep = 
            (new Vector3(90, 0, 0) - CameraTransform.rotation.eulerAngles) / stepCount;

        for (int i = 0; i < stepCount; i++)
        {
            yield return new WaitForFixedUpdate();
            CameraTransform.position += transformStep;
            CameraTransform.rotation = Quaternion.Euler(CameraTransform.rotation.eulerAngles + rotationStep);
        }

        CameraTransform.position = GlobalDataController.Instance.CurrentLevelData.CameraFinalPosition;
        CameraTransform.rotation = Quaternion.Euler(90, 0, 0);

        yield return new WaitForSeconds(showingTime);

        ShowWinUI();
    }
}