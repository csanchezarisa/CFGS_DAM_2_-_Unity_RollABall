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

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        SetCountText();

        winText.SetActive(false);
        tutorialMoveText.SetActive(true);
        tutorialJumpText.SetActive(false);
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
            // Se elimina el texto y se hace invisible el muro
            Destroy(tutorialMoveText);
            Destroy(tutorialEastWall.GetComponent<MeshRenderer>());

            // Se activa el trigger para el muro
            BoxCollider wallCollider = tutorialEastWall.GetComponent<BoxCollider>();
            wallCollider.isTrigger = true;

            // Se muestra el texto para indicar c�mo saltar
            tutorialJumpText.SetActive(true);
        }
        else if (other.gameObject.CompareTag("JumpWall") && count > 0)
        {
            Destroy(tutorialEastWall);
            Destroy(tutorialJumpText);
        }
        
        // Se consigue una moneda dorada. 5 puntos
        if (other.gameObject.CompareTag("GoldCoins"))
        {
            // Se oculta la moneda
            other.gameObject.SetActive(false);

            // Se suma en uno el contador de monedas recogidas
            count += 5;
            SetCountText();
        }
    }
}