using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayerController : MonoBehaviour
{

    public Transform player;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, player.position.y + 5.0f, 0);
    }

    void Update()
    {
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
