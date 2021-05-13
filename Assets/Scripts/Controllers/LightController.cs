using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    public Transform player;
    public Transform camera;

    //private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //offset = new Vector3(player.position.x, player.position.y + 20.0f, player.position.z);
        transform.position = camera.position;
        transform.LookAt(player.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = camera.position;
        transform.LookAt(player.position);
    }
}
