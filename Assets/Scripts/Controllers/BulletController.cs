using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DesertEnemy"))
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
    }
}
