using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Camera PlayerCam1;
    public Camera PlayerCam2;

    public CinemachineVirtualCamera CVC1;
    public CinemachineVirtualCamera CVC2;

    public Material NormalColor;
    public Material ItColor;

    private bool firstPlayer = true;

    private void Start() {
        Instance = this;
    }

    private void OnPlayerJoined(PlayerInput playerInput) {
        PlayerController player = playerInput.gameObject.GetComponent<PlayerController>();
        if (firstPlayer) {
            CVC1.m_LookAt = playerInput.gameObject.transform;
            CVC1.m_Follow = playerInput.gameObject.transform;
            player.MyCamera = PlayerCam1;
            player.SetAsNotIt();
            firstPlayer = false;
        }
        else {
            CVC2.m_LookAt = playerInput.gameObject.transform;
            CVC2.m_Follow = playerInput.gameObject.transform;
            player.MyCamera = PlayerCam2;
            player.SetAsIt();
        }
    }
}
