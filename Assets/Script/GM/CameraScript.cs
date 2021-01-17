using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    CinemachineVirtualCamera VCam;
    private GameObject Player;
    [SerializeField] GameObject BuildModeOriginPoint;

    private void Awake()
    {
        VCam = GetComponent<CinemachineVirtualCamera>();
        Player = GameObject.Find("Player");
    }
    private void Update()
    {
        if(GM.GMinstanse.GetisPC == false)
        {
            if(BuildManager.BMinstanse.GetSetinBuildMode == false)
            {
                VCam.m_Follow = Player.transform;
            }   
            else
            {
                VCam.m_Follow = BuildModeOriginPoint.transform;
            } 
        }
    }
}
