using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{

    public TextMeshProUGUI countText;
    public GameObject winText;
    public GameObject tutorialMoveText;
    public GameObject tutorialEastWall;
    public GameObject tutorialJumpText;
    public GameObject tutorialShootText;
    public GameObject desertPlatform;
    public GameObject riverPlatforms;

    private int count;
    private AudioSource audioNewZone;

    // Start is called before the first frame update
    void Start()
    {

        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioNewZone = audioSources[1];

        count = 0;
        SetCountText();

        winText.SetActive(false);
        tutorialMoveText.SetActive(true);
        tutorialJumpText.SetActive(false);
        tutorialShootText.SetActive(false);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        //if (count >= 1)
        //{
        //    winText.SetActive(true);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {

        // Se coje la primera moneda del tutorial para moverse
        if (other.gameObject.CompareTag("GoldCoins") && count == 0)
        {
            // Se reproduce el audio de nueva zona
            audioNewZone.Play();

            // Se elimina el texto y se hace invisible el muro
            Destroy(tutorialMoveText);
            Destroy(tutorialEastWall.GetComponent<MeshRenderer>());

            // Se activa el trigger para el muro
            BoxCollider wallCollider = tutorialEastWall.GetComponent<BoxCollider>();
            wallCollider.isTrigger = true;

            // Se muestra el texto para indicar cómo saltar
            tutorialJumpText.SetActive(true);
        }

        // Se salta el muro
        else if (other.gameObject.CompareTag("JumpWall") && count == 1)
        {
            Destroy(tutorialEastWall);
            Destroy(tutorialJumpText);
            tutorialShootText.SetActive(true);
            Destroy(tutorialShootText, 5);
        }

        // Se consigue una moneda dorada. 5 puntos
        if (other.gameObject.CompareTag("GoldCoins"))
        {
            // Reproduce el sonido de la moneda
            AudioSource audioCoin = other.GetComponent<AudioSource>();
            audioCoin.Play();

            // Se oculta la moneda
            Destroy(other.gameObject, 1);

            // Se suma en uno el contador de monedas recogidas
            count += 5;
            SetCountText();
        }

        CheckCountTriggers();
    }

    /** Ejecuta ciertas acciones cuando se llega a obtener un número determinado de puntos */
    void CheckCountTriggers()
    {
        // Se han conseguido todas las monedas del desierto
        if (count == 30)
        {
            audioNewZone.Play();
            desertPlatform.SetActive(true);
        }

        // Se han conseguido todas las monedas del rio
        if (count == 55)
        {
            audioNewZone.Play();
            riverPlatforms.SetActive(true);
        }
    }
}
