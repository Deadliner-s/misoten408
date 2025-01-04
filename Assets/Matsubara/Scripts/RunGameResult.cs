using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunGameResult : MonoBehaviour
{
    private ScoreSystem scoreSystem;

    [SerializeField] private GameObject resultCanvas; // ���U���g�p�L�����o�X
    [SerializeField] private TextMeshProUGUI coinText; // �R�C������\������UI�e�L�X�g
    [SerializeField] private TextMeshProUGUI item1Text; // �A�C�e��1�̐���\������UI�e�L�X�g
    [SerializeField] private TextMeshProUGUI item2Text; // �A�C�e��2�̐���\������UI�e�L�X�g
    [SerializeField] private TextMeshProUGUI item3Text; // �A�C�e��3�̐���\������UI�e�L�X�g

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreSystem = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<ScoreSystem>();

        if (scoreSystem == null)
        {
            Debug.LogError("ScoreSystem��������܂���ł����B�V�[���ɒǉ����Ă��������B");
            return;
        }
    }
    public void UpdateResultDisplay()
    {       
        // ���U���g�L�����o�X���A�N�e�B�u��
        if (resultCanvas != null)
        {
            resultCanvas.SetActive(true);
        }
        // �X�R�A�V�X�e������l���擾����UI�ɕ\��
        coinText.text = "Coins: " + RunGameManager.instance.coin;
        item1Text.text = "Item 1: " + RunGameManager.instance.item1;
        item2Text.text = "Item 2: " + RunGameManager.instance.item2;
        item3Text.text = "Item 3: " + RunGameManager.instance.item3;
    }
}
