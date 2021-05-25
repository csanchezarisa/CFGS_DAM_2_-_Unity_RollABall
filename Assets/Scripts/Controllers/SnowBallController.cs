using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        transform.localScale += transform.localScale * 0.02f * Time.deltaTime;
    }
}
