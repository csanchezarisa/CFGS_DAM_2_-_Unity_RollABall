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

    void Update()
    {
        //float hMovement = Input.GetAxis("Horizontal");
        //float vMovement = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(camera.transform.forward.x * hMovement, 0, camera.transform.forward.z * vMovement);
        //rb.AddForce(movement * speed);

        //if (Input.GetButtonDown("Jump"))
        //{
        //    rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
        //}

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f)
        {
            Vector3 forward = camera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;
            Vector3 right = new Vector3(forward.z, 0, -forward.x);
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 moveDirection = (h * right + v * forward);
            moveDirection = Quaternion.Inverse(this.transform.rotation) * moveDirection;
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            moveDirection = this.transform.rotation * moveDirection;
            moveDirection.y = 0;

            rb.AddForce(moveDirection * speed);
        }
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
