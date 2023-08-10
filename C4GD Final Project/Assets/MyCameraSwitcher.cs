using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MyCameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera redMonster1;
    public CinemachineVirtualCamera finalBossCam;
    


    private CinemachineVirtualCamera currentCamera;
    private CinemachineVirtualCamera previousCamera;

    private void Start()
    {
        currentCamera = defaultCamera;
        currentCamera.gameObject.SetActive(true);
    }

    private void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        previousCamera = currentCamera;
        currentCamera.gameObject.SetActive(false);

        newCamera.gameObject.SetActive(true);
        currentCamera = newCamera;
    }

   

    public void ChangeToRedMonster1()
    {
        SwitchCamera(redMonster1);
    }

    public void ChangeToDefaultCam()
    {
        SwitchCamera(defaultCamera);
    }

    
}
