using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatController : MonoBehaviour
{

    public GameObject canonBallPrefab;
    public float canonSpeed = 100;
    public bool right;

    private bool waitToShoot = false;

    void Update()
    {

        if (!waitToShoot)
        {
            float time = Random.Range(0, 5);
            print("numero random " + time);
            StartCoroutine(WaitToShoot(time));
        }
    }

    /** Dispara una bola de cañon en la dirección donde se encuentra el player */
    private void Shoot()
    {
        GameObject canonBall = Instantiate(canonBallPrefab, transform.position, transform.rotation) as GameObject;
        Rigidbody canonRb = canonBall.GetComponent<Rigidbody>();

        Vector3 direction;

        if (right)
            direction = transform.right;
        else
            direction = -transform.right;

        canonRb.AddForce(direction * canonSpeed, ForceMode.Impulse);

        Destroy(canonBall, 3);
        Destroy(canonRb, 3);
    }

    IEnumerator WaitToShoot(float time)
    {
        this.waitToShoot = true;
        print("Esperando " + time);
        yield return new WaitForSeconds(time);
        print("disparando");
        Shoot();
        this.waitToShoot = false;
    }
}
