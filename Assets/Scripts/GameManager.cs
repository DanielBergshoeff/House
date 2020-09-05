using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public Camera PlayerCam1;
    public Camera PlayerCam2;

    public CinemachineVirtualCamera CVC1;
    public CinemachineVirtualCamera CVC2;

    private bool firstPlayer = true;

    private void OnPlayerJoined(PlayerInput playerInput) {
        if (firstPlayer) {
            CVC1.m_LookAt = playerInput.gameObject.transform;
            CVC1.m_Follow = playerInput.gameObject.transform;
            playerInput.gameObject.GetComponent<PlayerController>().MyCamera = PlayerCam1;
            firstPlayer = false;
        }
        else {
            CVC2.m_LookAt = playerInput.gameObject.transform;
            CVC2.m_Follow = playerInput.gameObject.transform;
            playerInput.gameObject.GetComponent<PlayerController>().MyCamera = PlayerCam2;
        }
    }
}
