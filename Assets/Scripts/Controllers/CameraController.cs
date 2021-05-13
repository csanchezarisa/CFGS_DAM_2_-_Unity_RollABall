using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform player;

    private Vector3 offsetX;
    private Vector3 offsetY;

    void Start()
    {
        offsetX = new Vector3(player.position.x, player.position.y + 5.0f, player.position.z + 7.0f);
        offsetY = new Vector3(0, 4.0f, 0);
    }

    void LateUpdate()
    {
        offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
        offsetY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
        transform.position = player.position + offsetX + offsetY;
        transform.LookAt(player.position);
    }

}
