using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 100;
    public int lives = 3;

    private AudioSource audioShoot;
    private AudioSource audioDie;
    private bool waitToShoot = false;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioShoot = audioSources[0];
        audioDie = audioSources[1];
    }

    void Update()
    {
        transform.LookAt(player);

        if (!waitToShoot)
        {
            float time = Random.Range(3, 10);
            StartCoroutine(WaitToShoot(time));
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hit();
        }
    }

    public void hit()
    {
        lives--;

        if (lives <= 0)
        {

            audioDie.Play();
            notifyTextController();

            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<BoxCollider>());
            Destroy(gameObject, 1);
        }
    }

    void notifyTextController()
    {
        GameObject player = GameObject.Find("PlayerSphere");
        TextController textController = player.GetComponent<TextController>();
        textController.enemyHit();
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
