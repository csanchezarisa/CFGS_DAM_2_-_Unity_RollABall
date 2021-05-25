using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterController : MonoBehaviour
{

    public Transform cameraPosition;
    public Transform player;
    public float bulletSpeed;
    public GameObject bulletPrefav;

    private AudioSource audioShoot;

    void Start()
    {
        transform.position = player.position;
        audioShoot = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Quaternion cameraPositionRotation = cameraPosition.rotation;
        cameraPositionRotation.y = 0;
        cameraPositionRotation.w = 0;

        transform.position = player.position;
        transform.rotation = cameraPositionRotation;

        // Dispara
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    /** Crea un objeto "Bullet" y lo dispara hacia adelante */
    void Shoot()
    {
        Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject bullet = Instantiate(bulletPrefav, bulletPosition, transform.rotation) as GameObject;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        audioShoot.Play();

        bulletRb.AddForce(-transform.forward * bulletSpeed, ForceMode.Impulse);

        Destroy(bullet, 5);
        Destroy(bulletRb, 5);
    }
}
