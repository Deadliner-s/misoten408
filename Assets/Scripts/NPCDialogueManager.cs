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
    public static NPCDialogueManager instance;  // �V���O���g��

    [Header("UI Components")]
    public GameObject dialogueText;             // ��b���e��\������Text
    public GameObject speakerText;              // �b�Җ���\������Text
    public GameObject nextButton;               // ���̉�b�ɐi�ރ{�^��
    public GameObject dialoguePanel;            // ��b�S�̂�UI�p�l��
    public GameObject image;                    // �摜��\�����邽�߂�GameObject
    public float typingSpeed = 0.05f;           // ��������̑��x

    [Header("CSV File")]
    public TextAsset csvFile;                   // CSV�t�@�C����Unity�ɃC���|�[�g���Ďw��

    private class Dialogue
    {
        public string EventName;                // �C�x���g��
        public int ID;                          // ID
        public string Name;                     // �b���Ă���l
        public string Dialog;                   // ��b�̓��e
        public int NextID;                      // ���̉�b��ID
        public int Cnt;                         // �b����������
    }

    private Dictionary<string, List<Dialogue>> dialogues = new Dictionary<string, List<Dialogue>>();
    private Dialogue currentDialogue;
    private Coroutine typingCoroutine;          // ��������p�R���[�`��
    private bool isTyping;                      // �������蒆���ǂ����𔻒肷��t���O

    [NonSerialized]
    public bool isTalking = false;              // ��b�����ǂ����𔻒肷��t���O

    private Texture2D icon;                     // �摜��\�����邽�߂̕ϐ�

    private void Awake()
    {
        // �V���O���g���̏���
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
        dialoguePanel.SetActive(false); // ������ԂŔ�\��

        // Text�̏�����
        speakerText.GetComponent<TMP_Text>().text = "";
        dialogueText.GetComponent<TMP_Text>().text = "";
    }

    // CSV��ǂݍ���
    private void LoadCSV()
    {
        StringReader reader = new StringReader(csvFile.text);
        string line = reader.ReadLine(); // �w�b�_�[�s���X�L�b�v

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

    // �C�x���g���J�n
    public void StartEvent(string eventName, int Cnt, Texture2D tex)
    {
        // TextArea�ɐݒ肳�ꂽ�摜�̎擾
        icon = tex;

        if (!dialogues.ContainsKey(eventName))
        {
            Debug.LogError("�C�x���g��������܂���: " + eventName);
            return;
        }

        dialoguePanel.SetActive(true); // ��bUI��\��

        // �C�x���g���ɑΉ������b���X�g���擾
        currentDialogue = dialogues[eventName].Find(d => d.ID == 1 && d.Cnt == Cnt);

        // d.Cnt�����݂��Ȃ��ꍇ�́Ad.Cnt���ő�̂��̂��擾(���ڈȍ~�ɘb���������ꍇ�͍Ō�̉�b��\�����邽��)
        if (currentDialogue == null)
        {
            int maxCnt = dialogues[eventName].Max(d => d.Cnt);
            currentDialogue = dialogues[eventName].Find(d => d.ID == 1 && d.Cnt == maxCnt);
        }

        if (currentDialogue != null)
            DisplayDialogue();
    }

    // ��b��\��
    private void DisplayDialogue()
    {
        if (currentDialogue == null)
        {
            EndDialogue();
            return;
        }

        speakerText.GetComponent<TMP_Text>().text = currentDialogue.Name;


        // currentDialogue.Dialog�̉�b���e�Ɂu��ɓ��ꂽ!!�v�Ƃ��������񂪊܂܂�Ă���ꍇ�͉摜��\������
        if (currentDialogue.Dialog.Contains("��ɓ��ꂽ!!"))
        {
            // �摜��\�����鏈��
            image.SetActive(true);
            image.GetComponent<Image>().sprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), Vector2.zero);
        }
        else
        {
            image.SetActive(false);
        }

        // �R���[�`�����J�n���ĕ�����������s
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeDialogue(currentDialogue.Dialog));
    }

    // ���̉�b�܂��͕������蒆�̃X�L�b�v
    public void NextDialogue()
    {
        if (isTyping)
        {
            // �������蒆�Ƀ{�^���������ꂽ�ꍇ�͑����ɑS����\��
            StopCoroutine(typingCoroutine);
            dialogueText.GetComponent<TMP_Text>().text = currentDialogue.Dialog;
            isTyping = false;

            // ���{�^����L����
            //nextButton.SetActive(currentDialogue.NextID != 0);
        }
        else
        {
            // ���̉�b�ɐi��
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

    // ��b�I��
    private void EndDialogue()
    {
        //speakerText.text = "";
        //dialogueText.text = "��b�I��";
        nextButton.SetActive(false);
        speakerText.GetComponent<TMP_Text>().text = "";
        dialogueText.GetComponent<TMP_Text>().text = "";
        dialoguePanel.SetActive(false);
        isTalking = false;

        // ��莞�Ԍ��UI���\���ɂ���i�I�v�V�����j
        //Invoke(nameof(HideDialoguePanel), 2f);
    }

    private void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

    // ������1�������\������R���[�`��
    private IEnumerator TypeDialogue(string dialog)
    {
        dialogueText.GetComponent<TMP_Text>().text = ""; // �\�����e��������
        isTyping = true; // �������蒆�t���O��L����
        foreach (char letter in dialog.ToCharArray())
        {
            dialogueText.GetComponent<TMP_Text>().text += letter; // 1�����ǉ�
            yield return new WaitForSeconds(typingSpeed); // �\���Ԋu�𒲐�
        }
        isTyping = false; // ��������I��

        // ���{�^����L����
        //nextButton.SetActive(currentDialogue.NextID != 0);
    }
}
