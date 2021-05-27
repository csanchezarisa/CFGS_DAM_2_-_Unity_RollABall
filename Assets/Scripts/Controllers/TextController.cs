using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject hallZone;
    public GameObject rampZone;
    public GameObject rampWall;
    public GameObject snowballController;
    public GameObject finalZone;

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
        SceneManager.LoadScene("CreditsScene", LoadSceneMode.Single);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
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
        else if (other.gameObject.CompareTag("JumpWall") && count > 0)
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
            Destroy(other.gameObject.GetComponent<MeshRenderer>());
            Destroy(other.gameObject.GetComponent<BoxCollider>());
            Destroy(other.gameObject, 1);

            // Se suma en uno el contador de monedas recogidas
            count += 5;
            SetCountText();

            CheckCountTriggers();
        }
        else if (other.gameObject.CompareTag("BigGoldCoins"))
        {
            // Reproduce el sonido de la moneda
            AudioSource audioCoin = other.GetComponent<AudioSource>();
            audioCoin.Play();

            // Se oculta la moneda
            Destroy(other.gameObject.GetComponent<MeshRenderer>());
            Destroy(other.gameObject.GetComponent<BoxCollider>());
            Destroy(other.gameObject, 1);

            // Se suma en uno el contador de monedas recogidas
            count += 30;
            SetCountText();

            CheckCountTriggers();
        }
    }

    /** Se mata a un enemigo, se suman 10 puntos */
    public void enemyHit()
    {
        count += 10;
        SetCountText();

        CheckCountTriggers();
    }

    /** Activa una nueva zona y reproduce el sonido */
    void newZone(GameObject zone)
    {
        audioNewZone.Play();
        zone.SetActive(true);
    }

    /** Ejecuta ciertas acciones cuando se llega a obtener un número determinado de puntos */
    void CheckCountTriggers()
    {
        // Se han conseguido todas las monedas del desierto
        if (count == 30)
            newZone(desertPlatform);

        // Se han conseguido todas las monedas del rio
        else if (count == 55)
            newZone(riverPlatforms);

        // Se activa la zona del pasillo
        else if (count == 100)
            newZone(hallZone);

        // Se han eliminado todos los monstruos
        else if (count == 190)
            newZone(rampZone);

        // Se han conseguido todas las monedas de la rampa
        else if (count == 220)
        {
            newZone(finalZone);
            Destroy(rampWall);
            Destroy(snowballController);
        }
    }
}
