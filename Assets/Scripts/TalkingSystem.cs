using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TalkingSystem : MonoBehaviour
{
    public static TalkingSystem instance;   // �V���O���g��

    public GameObject window;   // �E�B���h�EUI

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

    private GameObject player;  // �v���C���[�I�u�W�F�N�g
    private List<string> conversationData;  // ��b�f�[�^���X�g

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        LoadConversationData("Assets/ConversationData.csv");
    }

    // Update is called once per frame
    void Update()
    {
        // Player��isTalking��true�̎��A�E�B���h�E��\��
        if (player.GetComponent<Player>().isTalking)
        {
            window.SetActive(true);
        }
        else
        {
            window.SetActive(false);
        }
    }

    // ��������(����{�^���̓���)
    public void NextText()
    {
        Debug.Log("��b���i�݂܂����B");

        // �Ƃ肠������b�̏I��
        //EndTalking();
    }

    // ��b�̏I��
    private void EndTalking()
    {
        player.GetComponent<Player>().isTalking = false;
    }

    // CSV�t�@�C�������b�f�[�^��ǂݍ���
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
            Debug.LogError("�t�@�C���̓ǂݍ��݂Ɏ��s���܂���: " + e.Message);
        }
    }
}
