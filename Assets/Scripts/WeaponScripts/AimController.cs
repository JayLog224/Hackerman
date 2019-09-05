using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimController : MonoBehaviour
{
    public Camera cam;
    //public CinemachineVirtualCamera virtualCam;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    //void CameraFollowMouse()
    //{
    //    virtualCam.de
    //}
}
