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
        if (other.gameObject.CompareTag("GoldCoins") && count == 0)
        {
            other.gameObject.SetActive(false);
            Destroy(tutorialMoveText);
            Destroy(tutorialEastWall.GetComponent<MeshRenderer>());
            tutorialJumpText.SetActive(true);
            count++;
            SetCountText();
        }
        
    }
}
