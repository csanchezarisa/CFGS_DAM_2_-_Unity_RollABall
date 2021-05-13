using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public TextMeshProUGUI countText;
    public GameObject camera;
    public GameObject winTextObject;

    private int count;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 1)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        float hMovment = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hMovment, 0.0f, vMovement);
        movement = camera.transform.rotation * movement;
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GoldCoins"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
