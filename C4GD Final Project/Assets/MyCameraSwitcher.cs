using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MyCameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera redMonster1;
    public CinemachineVirtualCamera finalBossCam;
    public CinemachineVirtualCamera oceanBtnCam;
    public CinemachineVirtualCamera puzzleCam;
    public CinemachineVirtualCamera beforeBossCam;
    


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

    public void ChangeToFinalBossCam(){
        SwitchCamera(finalBossCam);
    }

    public void ChangeToOceanBtnCam(){
        SwitchCamera(oceanBtnCam);
    }

    public void ChangeToPuzzleCam(){
        SwitchCamera(puzzleCam);
    }

    public void ChangeToBeforeBossCam(){
        SwitchCamera(beforeBossCam);
    }
    
}
