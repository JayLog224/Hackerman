using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceMouse : MonoBehaviour
{
    void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x,
                                        mousePosition.y - transform.position.y);

        transform.up = direction;
    }
}
