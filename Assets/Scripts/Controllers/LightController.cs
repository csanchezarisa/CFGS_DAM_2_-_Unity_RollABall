using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    public Transform player;
    public Transform cameraPosition;

    void Start()
    {
        transform.position = cameraPosition.position;
        transform.LookAt(player.position);
    }

    void LateUpdate()
    {
        transform.position = cameraPosition.position;
        transform.LookAt(player.position);
    }
}
