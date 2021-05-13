using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float turnSpeed = 4;

    public float height = 5f;
    public float distance = 10f;

    private Vector3 offsetX;
    private Vector3 offsetY;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = new Vector3(player.transform.position.x, player.transform.position.y + height, player.transform.position.z + distance);
        offsetY = new Vector3(player.transform.position.x, player.transform.position.y + height, player.transform.position.z + distance);
    }

    void LateUpdate()
    {
        offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
        offsetY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;

        transform.position = player.transform.position + offsetX + offsetY;
        transform.LookAt(player.transform);
    }
}
