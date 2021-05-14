using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform player;

    private Vector3 offsetX;
    private Vector3 offsetY;
    private Transform currentTransparentWall;

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
        CheckView();
    }

    void CheckView()
    {
        Debug.DrawRay(transform.position, player.position - transform.position, Color.red, 0.0f);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, player.position - transform.position, out hit, Vector3.Distance(player.position, transform.position), LayerMask.GetMask("Wall")))
        {

            Transform transparentWall = hit.transform;
            Color transparentWallColor = transparentWall.gameObject.GetComponent<MeshRenderer>().material.color;

            if (currentTransparentWall && currentTransparentWall != transparentWall)
            {
                transparentWallColor.a = 1.0f;
                currentTransparentWall.gameObject.GetComponent<MeshRenderer>().material.color = transparentWallColor;
            }

            transparentWallColor.a = 0.3f;
            transparentWall.gameObject.GetComponent<MeshRenderer>().material.color = transparentWallColor;
            currentTransparentWall = transparentWall;

        }
        else
        {
            if (currentTransparentWall)
            {
                Color myColor = currentTransparentWall.gameObject.GetComponent<MeshRenderer>().material.color;
                myColor.a = 1.0f;
                currentTransparentWall.gameObject.GetComponent<MeshRenderer>().material.color = myColor;
            }
        }
    }
}
