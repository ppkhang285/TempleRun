using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CameraManager
{
    private CinemachineBrain brainCamera;
    private CinemachineVirtualCamera startCamera;
    private CinemachineVirtualCamera playerCamera;

    private GameObject playerRoot;
    private GameObject cameraRoot;

    private Coroutine startCamCoroutine;
    public CameraManager(GameObject cameraRoot, GameObject playerRoot)
    {

        this.cameraRoot = cameraRoot;
        this.playerRoot = playerRoot;   
        
        brainCamera = cameraRoot.GetComponentInChildren<CinemachineBrain>();
        startCamera = cameraRoot.GetComponentInChildren<CinemachineVirtualCamera>();
        //playerCamera = playerRoot.GetComponentInChildren<CinemachineVirtualCamera>();

        if ( startCamera == null || brainCamera == null)
        {
            Debug.LogError("Not found camera");
        
        }


    }


    public void Update()
    {
        //Vector3 playerPos = GameplayManager.Instance.player.transform.position;
        //playerCamera.transform.position = Vector3.right * playerPos.x + Vector3.forward * playerPos.z + Vector3.up * playerCamera.gameObject.transform.position.y; ;
    }
    public void Reset()
    {
        //GameplayManager.Instance.Stop_Coroutine(startCamCoroutine);
        DefaultCamera();
    }

    public void GameplayCamera()
    {
        brainCamera.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        brainCamera.m_DefaultBlend.m_Time = 2f;
        startCamera.enabled = false;
        startCamCoroutine = GameplayManager.Instance.RunCoroutine(ChangeBlendTimeAfterStart());
    }

    public void DefaultCamera()
    {
        brainCamera.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        startCamera.enabled = true;
        
    }

    public void Rotate()
    {
        Quaternion rotation = Constants.ROTATION_VECTOR[GameplayManager.Instance.currentDirecion];
        playerRoot.transform.rotation = rotation;

    }

    IEnumerator ChangeBlendTimeAfterStart()
    {
        yield return new WaitForSeconds(2.1f);
        brainCamera.m_DefaultBlend.m_Time = 0.5f;
    }

}
