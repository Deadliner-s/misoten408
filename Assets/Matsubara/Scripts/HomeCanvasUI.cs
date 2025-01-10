using TMPro;
using UnityEngine;

public class HomeCanvasUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText; // コイン数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item1Text; // アイテム1の数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item2Text; // アイテム2の数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item3Text; // アイテム3の数を表示するUIテキスト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // スコアシステムから値を取得してUIに表示
        coinText.text  = RunGameManager.instance.coin.ToString();
        item1Text.text = RunGameManager.instance.item1.ToString();
        item2Text.text = RunGameManager.instance.item2.ToString();
        item3Text.text = RunGameManager.instance.item3.ToString();


    }
}