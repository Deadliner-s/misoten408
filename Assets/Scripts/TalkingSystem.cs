using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TalkingSystem : MonoBehaviour
{
    public static TalkingSystem instance;   // シングルトン

    public GameObject window;   // ウィンドウUI

    public enum Event
    {
        Event1,
        Event2,
        Event3,
        Event4,
        Event5,
        Event6,
        Event7,
        Event8,
        Event9
    }

    private GameObject player;  // プレイヤーオブジェクト
    private List<string> conversationData;  // 会話データリスト

    private void Awake()
    {
        // シングルトンの処理
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        LoadConversationData("Assets/ConversationData.csv");
    }

    // Update is called once per frame
    void Update()
    {
        // PlayerのisTalkingがtrueの時、ウィンドウを表示
        if (player.GetComponent<Player>().isTalking)
        {
            window.SetActive(true);
        }
        else
        {
            window.SetActive(false);
        }
    }

    // 文字送り(決定ボタンの入力)
    public void NextText()
    {
        Debug.Log("会話が進みました。");

        // とりあえず会話の終了
        //EndTalking();
    }

    // 会話の終了
    private void EndTalking()
    {
        player.GetComponent<Player>().isTalking = false;
    }

    // CSVファイルから会話データを読み込む
    private void LoadConversationData(string filePath)
    {
        conversationData = new List<string>();

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    conversationData.Add(line);
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError("ファイルの読み込みに失敗しました: " + e.Message);
        }
    }
}
