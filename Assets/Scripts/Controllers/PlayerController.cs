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
    public Transform rampRespawn;

    private Rigidbody rb;
    private AudioSource audioJump;
    private AudioSource audioHurt;
    private AudioSource audioRolling;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioJump = audioSources[0];
        audioHurt = audioSources[2];
        audioRolling = audioSources[3];
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Choca con la bola de nieve en la rampa
        if (collision.gameObject.CompareTag("Snowball"))
            respawn(rampRespawn);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Se mantiene en una superfície etiquetada como Ground
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        // Cuando deja de colisionar con una superfície etiquetada como Ground
        if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Ground"))
            isGrounded = false;
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

            if (!audioRolling.isPlaying)
                audioRolling.Play();
        }
        else if (audioRolling.isPlaying)
        {
            audioRolling.Stop();
        }

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();
    }

    /** Aplica una fuerza vertical positiva al personaje */
    void Jump()
    {
        audioJump.Play();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    /** Suena el audio de hurt y cambia la posición el personaje */
    void respawn(Transform respawnPosition)
    {
        audioHurt.Play();
        transform.position = respawnPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Se cae y toca el borde del tutorial. Vuelve al respawn del tutorial
        if (other.gameObject.CompareTag("TutorialBorder"))
            respawn(tutorialRespawn);
        
        // Toca un cactus (enemigo del desierto) o se cae y toca el borde del Desierto. Vuelve al respawn del desierto
        else if (other.gameObject.CompareTag("DesertEnemy") || other.gameObject.CompareTag("DesertBorder"))
            respawn(desertRespawn);

        // Toca el rio o una bala de cañon o se cae y toca el borde del Rio. Vuelve al respawn del rio
        else if (other.gameObject.CompareTag("River") || other.gameObject.CompareTag("CanonBall"))
            respawn(riverRespawn);

        // Al tocar el lago o los bordes de la jungla. Vuelve al respawn de la jungla
        else if (other.gameObject.CompareTag("Lake") || other.gameObject.CompareTag("JungleBorder") || other.gameObject.CompareTag("EnemyBullet"))
            respawn(jungleRespawn);
    }
}
