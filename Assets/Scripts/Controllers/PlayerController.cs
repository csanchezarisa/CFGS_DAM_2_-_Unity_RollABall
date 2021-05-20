using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;
    public float jumpForce = 10;
    public GameObject camera;
    public Transform desertRespawn;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Movimiento horizontal
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

            rb.AddForce(moveDirection * speed, ForceMode.Force);
        }

        // Salto
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
            Jump();
    }

    /** Aplica una fuerza vertical positiva al personaje */
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DesertEnemy"))
        {
            transform.position = desertRespawn.position;
        }
    }
}
