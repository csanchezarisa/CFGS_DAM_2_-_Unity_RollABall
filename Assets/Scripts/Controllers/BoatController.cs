using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public Transform[] platformPositions;
    public float platformSpeed;
    public float waitTime;

    private Rigidbody boat;
    private int nextPosition = 1;
    private bool moveToNext = false;
    private bool moving = true;

    private void Start()
    {
        boat = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (moveToNext)
        {
            StopCoroutine(WaitforMove(0));
            boat.MovePosition(Vector3.MoveTowards(boat.position, platformPositions[nextPosition].position, platformSpeed * Time.deltaTime));
        }
        else
        {

            StartCoroutine(WaitforMove(waitTime));
        }

        if (moving && Vector3.Distance(boat.position, platformPositions[nextPosition].position) <= 0)
        {

            moving = false;

            StartCoroutine(WaitforMove(waitTime));

            if (nextPosition >= platformPositions.Length - 1)
            {
                Destroy(gameObject, 10);
                Destroy(this, 10);
            }
        }
    }

    IEnumerator WaitforMove(float time)
    {
        moveToNext = false;
        yield return new WaitForSeconds(time);
        moveToNext = true;
    }
}
