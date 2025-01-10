using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    private ScoreSystem scoreSystem;

    [SerializeField] private GameObject GamePanel; // �Q�[���p�L�����o�X
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

    // Update is called once per frame
    void Update()
    {
        // �X�R�A�V�X�e������l���擾����UI�ɕ\��
        coinText.text = scoreSystem.currentScore.ToString();
        item1Text.text = scoreSystem.currentitem1.ToString();
        item2Text.text = scoreSystem.currentitem2.ToString();
        item3Text.text = scoreSystem.currentitem3.ToString();
    }

    public void HideCanvas()
    {
        GamePanel.SetActive(false);
    }
}
