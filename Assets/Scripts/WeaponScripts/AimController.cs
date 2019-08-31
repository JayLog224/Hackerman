using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float radius = 4;
    private Vector3 v3Pos;
    private float angle;
    private float distance = .24f;
    public float crosshairSpeed = 1.0f;

    private void FixedUpdate()
    {
        v3Pos = Input.mousePosition;
        v3Pos.z = (player.transform.position.z - Camera.main.transform.position.z);

        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - player.transform.position;
        angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;

        if (angle < 0.0f) angle += 360f;
        transform.localEulerAngles = new Vector3(0, 0, angle);

        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;

        transform.localPosition = new Vector3(player.transform.position.x + xPos * radius, player.transform.position.y + yPos * radius, 0);

    }
}
