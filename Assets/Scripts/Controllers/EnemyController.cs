using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform player;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    private AudioSource audioShoot;

    void Start()
    {
        audioShoot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
