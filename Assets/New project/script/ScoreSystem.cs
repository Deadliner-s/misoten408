using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int currentScore = 0;      // ���݂̃X�R�A
    public int currentkoin = 0;         // �R�C���l������
    public int currentitem1 = 0;        // �Œ�l���Z�A�C�e��
    public int currentitem2 = 0;        // �����R�C����1.3�{�ɂ���{�����Z�A�C�e��
    public int currentitem3 = 0;        // �l�������R�C�������ɉ����ĉ��Z����A�C�e��
    public Text scoreText;               // �X�R�A��\������UI

    private void Start()
    {
        UpdateScoreUI(); // �����X�R�A��\��
    }

    void OnTriggerEnter(Collider other)
    {
        // �X�R�A�A�C�e���^�O�̃I�u�W�F�N�g�ɐG�ꂽ�ꍇ
        if (other.CompareTag("ScoreItem"))
        {
            currentScore += 10;  // �X�R�A�����Z
            currentkoin += 1;   //�R�C���J�E���g
            UpdateScoreUI();    // UI���X�V
            Destroy(other.gameObject); // �A�C�e�����폜
        }
        // ��Q���^�O�̃I�u�W�F�N�g�ɐG�ꂽ�ꍇ
        if (other.CompareTag("syougaibutu"))
        {
            currentScore -= 50; // �X�R�A�����Z
            UpdateScoreUI();    // UI���X�V

        }
        // item1�^�O�̃I�u�W�F�N�g�ɐG�ꂽ�ꍇ
        if (other.CompareTag("item1"))
        {
            currentScore += 200; // �X�R�A�����Z�i100�j
            UpdateScoreUI();    // UI���X�V
            Destroy(other.gameObject); // �A�C�e�����폜

        }
        // item2�^�O�̃I�u�W�F�N�g�ɐG�ꂽ�ꍇ
        if (other.CompareTag("item2"))
        {
            currentScore = currentScore / 10 * 12; ; // �X�R�A��1.2�{�ɂ���
            UpdateScoreUI();    // UI���X�V
            Destroy(other.gameObject); // �A�C�e�����폜

        }
        // item3�^�O�̃I�u�W�F�N�g�ɐG�ꂽ�ꍇ
        if (other.CompareTag("item3"))
        {
            currentScore = currentScore + currentkoin * 2; // �R�C���P�����Ƃ�2����
            UpdateScoreUI();    // UI���X�V
            Destroy(other.gameObject); // �A�C�e�����폜

        }

    }
    // �X�R�AUI���X�V���郁�\�b�h
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }
    
}

