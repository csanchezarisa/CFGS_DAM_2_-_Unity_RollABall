using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverDianaController : MonoBehaviour
{
    public GameObject boatPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GameObject boat = Instantiate(boatPrefab, boatPrefab.transform.position, boatPrefab.transform.rotation) as GameObject;
            boat.SetActive(true);
        }
    }
}
