using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    public Transform player;
    public Transform camera;

    void Start()
    {
        transform.position = camera.position;
        transform.LookAt(player.position);
    }

    void LateUpdate()
    {
        transform.position = camera.position;
        transform.LookAt(player.position);
    }
}
