using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    GameManager gameManager;

    private TextMeshProUGUI timerText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.instance;

        timerText = GetComponent<TextMeshProUGUI>();


    }

    // Update is called once per frame
    void Update()
    {
        float time = gameManager.time; // •b”
        int minutes = Mathf.FloorToInt(time / 60); // •ª‚ğŒvZ
        int seconds = Mathf.FloorToInt(time % 60); // •b‚ğŒvZ

        timerText.text = $"{minutes:00}:{seconds:00}";
    }


}
