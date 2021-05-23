using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;
    public float jumpForce = 10;
    public Transform playerShooter;
    public Transform desertRespawn;
    public Transform riverRespawn;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Movimiento horizontal
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.01f)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 moveDirection = (h * playerShooter.right + v * -playerShooter.forward);

            rb.AddForce(moveDirection * speed, ForceMode.Force);
        }

        // Salto
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.1f)
            Jump();
    }

    /** Aplica una fuerza vertical positiva al personaje */
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Toca un cactus (enemigo del desierto) y vuelve al respawn del desierto
        if (other.gameObject.CompareTag("DesertEnemy"))
        {
            transform.position = desertRespawn.position;
        }

        // Toca el rio o una bala de cañon y vuelve al respawn del rio
        else if (other.gameObject.CompareTag("River") || other.gameObject.CompareTag("CanonBall"))
        {
            transform.position = riverRespawn.position;
        }
    }
}
