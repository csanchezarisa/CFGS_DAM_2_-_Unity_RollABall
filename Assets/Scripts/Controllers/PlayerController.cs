using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;
    public float jumpForce = 10;
    public Transform playerShooter;
    public Transform tutorialRespawn;
    public Transform desertRespawn;
    public Transform riverRespawn;
    public Transform jungleRespawn;

    private Rigidbody rb;
    private AudioSource audioJump;
    private AudioSource audioHurt;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioJump = audioSources[0];
        audioHurt = audioSources[2];
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
        audioJump.Play();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Se cae y toca el borde del tutorial. Vuelve al respawn del tutorial
        if (other.gameObject.CompareTag("TutorialBorder"))
        {
            audioHurt.Play();
            transform.position = tutorialRespawn.position;
        }

        // Toca un cactus (enemigo del desierto) o se cae y toca el borde del Desierto. Vuelve al respawn del desierto
        else if (other.gameObject.CompareTag("DesertEnemy") || other.gameObject.CompareTag("DesertBorder"))
        {
            audioHurt.Play();
            transform.position = desertRespawn.position;
        }

        // Toca el rio o una bala de cañon o se cae y toca el borde del Rio. Vuelve al respawn del rio
        else if (other.gameObject.CompareTag("River") || other.gameObject.CompareTag("CanonBall"))
        {
            audioHurt.Play();
            transform.position = riverRespawn.position;
        }

        // Al tocar el lago o los bordes de la jungla. Vuelve al respawn de la jungla
        else if (other.gameObject.CompareTag("Lake") || other.gameObject.CompareTag("JungleBorder"))
        {
            audioHurt.Play();
            transform.position = jungleRespawn.position;
        }
    }
}
