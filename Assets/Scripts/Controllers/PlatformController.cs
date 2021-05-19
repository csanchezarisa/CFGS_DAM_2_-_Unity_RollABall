using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public Rigidbody platformRb;
    public Transform[] platformPositions;
    public float platformSpeed;
    public float waitTime;

    private int actualPosition = 0;
    private int nextPosition = 1;
    private bool moveToNext = true;

    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (moveToNext)
        {
            StopCoroutine(WaitforMove(0));
            platformRb.MovePosition(Vector3.MoveTowards(platformRb.position, platformPositions[nextPosition].position, platformSpeed * Time.deltaTime));
        }

        if (Vector3.Distance(platformRb.position, platformPositions[nextPosition].position) <= 0)
        {
            StartCoroutine(WaitforMove(waitTime));
            actualPosition = nextPosition;
            nextPosition++;

            if (nextPosition > platformPositions.Length - 1)
                nextPosition = 0;
        }
    }

    IEnumerator WaitforMove(float time)
    {
        moveToNext = false;
        yield return new WaitForSeconds(time);
        moveToNext = true;
    }
}
