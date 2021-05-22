using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public Rigidbody boat;
    public Transform[] platformPositions;
    public float platformSpeed;
    public float waitTime;

    private int nextPosition = 1;
    private bool moveToNext = false;

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

        if (Vector3.Distance(boat.position, platformPositions[nextPosition].position) <= 0)
        {
            StartCoroutine(WaitforMove(waitTime));
            nextPosition++;

            if (nextPosition > platformPositions.Length - 1)
                Destroy(gameObject, 5);
        }
    }

    IEnumerator WaitforMove(float time)
    {
        moveToNext = false;
        yield return new WaitForSeconds(time);
        moveToNext = true;
    }
}
