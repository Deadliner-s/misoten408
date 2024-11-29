using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueManager : MonoBehaviour
{
    public static NPCDialogueManager instance;  // シングルトン

    [Header("UI Components")]
    public GameObject dialogueText;             // 会話内容を表示するText
    public GameObject speakerText;              // 話者名を表示するText
    public GameObject nextButton;               // 次の会話に進むボタン
    public GameObject dialoguePanel;            // 会話全体のUIパネル
    public GameObject image;                    // 画像を表示するためのGameObject
    public float typingSpeed = 0.05f;           // 文字送りの速度

    [Header("CSV File")]
    public TextAsset csvFile;                   // CSVファイルをUnityにインポートして指定

    private class Dialogue
    {
        public string EventName;                // イベント名
        public int ID;                          // ID
        public string Name;                     // 話している人
        public string Dialog;                   // 会話の内容
        public int NextID;                      // 次の会話のID
        public int Cnt;                         // 話しかけた回数
    }

    private Dictionary<string, List<Dialogue>> dialogues = new Dictionary<string, List<Dialogue>>();
    private Dialogue currentDialogue;
    private Coroutine typingCoroutine;          // 文字送り用コルーチン
    private bool isTyping;                      // 文字送り中かどうかを判定するフラグ

    [NonSerialized]
    public bool isTalking = false;              // 会話中かどうかを判定するフラグ

    private Texture2D icon;                     // 画像を表示するための変数

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

    void Start()
    {
        LoadCSV();
        dialoguePanel.SetActive(false); // 初期状態で非表示

        // Textの初期化
        speakerText.GetComponent<TMP_Text>().text = "";
        dialogueText.GetComponent<TMP_Text>().text = "";
    }

    // CSVを読み込む
    private void LoadCSV()
    {
        StringReader reader = new StringReader(csvFile.text);
        string line = reader.ReadLine(); // ヘッダー行をスキップ

        while ((line = reader.ReadLine()) != null)
        {
            string[] fields = line.Split(',');
            if (fields.Length < 5) continue;

            Dialogue dialogue = new Dialogue
            {
                EventName = fields[0],
                ID = int.Parse(fields[1]),
                Name = fields[2],
                Dialog = fields[3],
                NextID = int.Parse(fields[4]),
                Cnt = int.Parse(fields[5])
            };

            if (!dialogues.ContainsKey(dialogue.EventName))
                dialogues[dialogue.EventName] = new List<Dialogue>();

            dialogues[dialogue.EventName].Add(dialogue);
        }
    }

    // イベントを開始
    public void StartEvent(string eventName, int Cnt, Texture2D tex)
    {
        // TextAreaに設定された画像の取得
        icon = tex;

        if (!dialogues.ContainsKey(eventName))
        {
            Debug.LogError("イベントが見つかりません: " + eventName);
            return;
        }

        dialoguePanel.SetActive(true); // 会話UIを表示

        // イベント名に対応する会話リストを取得
        currentDialogue = dialogues[eventName].Find(d => d.ID == 1 && d.Cnt == Cnt);

        // d.Cntが存在しない場合は、d.Cntが最大のものを取得(二回目以降に話しかけた場合は最後の会話を表示するため)
        if (currentDialogue == null)
        {
            int maxCnt = dialogues[eventName].Max(d => d.Cnt);
            currentDialogue = dialogues[eventName].Find(d => d.ID == 1 && d.Cnt == maxCnt);
        }

        if (currentDialogue != null)
            DisplayDialogue();
    }

    // 会話を表示
    private void DisplayDialogue()
    {
        if (currentDialogue == null)
        {
            EndDialogue();
            return;
        }

        speakerText.GetComponent<TMP_Text>().text = currentDialogue.Name;


        // currentDialogue.Dialogの会話内容に「手に入れた!!」という文字列が含まれている場合は画像を表示する
        if (currentDialogue.Dialog.Contains("手に入れた!!"))
        {
            // 画像を表示する処理
            image.SetActive(true);
            image.GetComponent<Image>().sprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), Vector2.zero);
        }
        else
        {
            image.SetActive(false);
        }

        // コルーチンを開始して文字送りを実行
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeDialogue(currentDialogue.Dialog));
    }

    // 次の会話または文字送り中のスキップ
    public void NextDialogue()
    {
        if (isTyping)
        {
            // 文字送り中にボタンが押された場合は即座に全文を表示
            StopCoroutine(typingCoroutine);
            dialogueText.GetComponent<TMP_Text>().text = currentDialogue.Dialog;
            isTyping = false;

            // 次ボタンを有効化
            //nextButton.SetActive(currentDialogue.NextID != 0);
        }
        else
        {
            // 次の会話に進む
            if (currentDialogue.NextID == 0)
            {
                EndDialogue();
                return;
            }

            string eventName = currentDialogue.EventName;
            currentDialogue = dialogues[eventName].Find(d => d.ID == currentDialogue.NextID);
            DisplayDialogue();
        }
    }

    // 会話終了
    private void EndDialogue()
    {
        //speakerText.text = "";
        //dialogueText.text = "会話終了";
        nextButton.SetActive(false);
        speakerText.GetComponent<TMP_Text>().text = "";
        dialogueText.GetComponent<TMP_Text>().text = "";
        dialoguePanel.SetActive(false);
        isTalking = false;

        // 一定時間後にUIを非表示にする（オプション）
        //Invoke(nameof(HideDialoguePanel), 2f);
    }

    private void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

    // 文字を1文字ずつ表示するコルーチン
    private IEnumerator TypeDialogue(string dialog)
    {
        dialogueText.GetComponent<TMP_Text>().text = ""; // 表示内容を初期化
        isTyping = true; // 文字送り中フラグを有効化
        foreach (char letter in dialog.ToCharArray())
        {
            dialogueText.GetComponent<TMP_Text>().text += letter; // 1文字追加
            yield return new WaitForSeconds(typingSpeed); // 表示間隔を調整
        }
        isTyping = false; // 文字送り終了

        // 次ボタンを有効化
        //nextButton.SetActive(currentDialogue.NextID != 0);
    }
}
