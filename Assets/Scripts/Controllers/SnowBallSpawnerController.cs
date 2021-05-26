using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallSpawnerController : MonoBehaviour
{

    public GameObject snowballPrefab;

    private bool waiting = false;

    void Update()
    {
        if (!waiting)
        {
            float time = Random.Range(3, 5);
            StartCoroutine(WaitForSpawn(time));
        }
    }

    IEnumerator WaitForSpawn(float time)
    {
        waiting = true;
        yield return new WaitForSeconds(time);
        GameObject snowball = Instantiate(snowballPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(snowball, 20);
        waiting = false;
    }
}
