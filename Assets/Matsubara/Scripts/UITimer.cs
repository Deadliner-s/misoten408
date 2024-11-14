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
        timerText.text = gameManager.time.ToString("f2");

    }


}
