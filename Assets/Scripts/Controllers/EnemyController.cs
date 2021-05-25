using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 100;

    private AudioSource audioShoot;
    private bool waitToShoot = false;

    void Start()
    {
        audioShoot = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        transform.LookAt(player);

        if (!waitToShoot)
        {
            float time = Random.Range(3, 10);
            StartCoroutine(WaitToShoot(time));
        }

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        audioShoot.Play();

        bulletRb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

        Destroy(bullet, 3);
        Destroy(bulletRb, 3);
    }

    IEnumerator WaitToShoot(float time)
    {
        this.waitToShoot = true;
        yield return new WaitForSeconds(time);
        Shoot();
        this.waitToShoot = false;
    }
}
