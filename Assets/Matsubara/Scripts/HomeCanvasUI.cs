using TMPro;
using UnityEngine;

public class HomeCanvasUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText; // �R�C������\������UI�e�L�X�g
    [SerializeField] private TextMeshProUGUI item1Text; // �A�C�e��1�̐���\������UI�e�L�X�g
    [SerializeField] private TextMeshProUGUI item2Text; // �A�C�e��2�̐���\������UI�e�L�X�g
    [SerializeField] private TextMeshProUGUI item3Text; // �A�C�e��3�̐���\������UI�e�L�X�g

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �X�R�A�V�X�e������l���擾����UI�ɕ\��
        coinText.text  = "Coins: " + RunGameManager.instance.coin;
        item1Text.text = "Item1: " + RunGameManager.instance.item1;
        item2Text.text = "Item2: " + RunGameManager.instance.item2;
        item3Text.text = "Item3: " + RunGameManager.instance.item3;
    }
}