using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayerController : MonoBehaviour
{

    public Transform player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(player.position.x, player.position.y + 10.0f, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
