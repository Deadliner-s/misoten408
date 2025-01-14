using UnityEngine;

public class RunGameManager : MonoBehaviour
{
    public static RunGameManager instance;

    public enum DifficultyLevel
    {
        None,
        C_����,
        C_����,
        C_�㋉,
        A_����,
        A_����,
        A_�㋉,
        B_����,
        B_����,
        B_�㋉
    }

    [Header("HomeC")]
    public bool C_intermediate;
    public bool C_Advanced;

    [Header("HomeA")]
    public bool A_Home;
    public bool A_intermediate;
    public bool A_Advanced;

    [Header("HomeB")]
    public bool B_Home;
    public bool B_intermediate;
    public bool B_Advanced;

    [Header("�R�C��")]
    public int coin;

    [Header("�A�C�e����")]
    public int item1;
    public int item2;
    public int item3;

    private void Awake()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �X�R�A�����Z�b�g����
    public void ResetScore()
    {
        // �ǂ̃t���O��false��
        C_intermediate = false;
        C_Advanced = false;
        A_Home = false;
        A_intermediate = false;
        A_Advanced = false;
        B_Home = false;
        B_intermediate = false;
        B_Advanced = false;

        // �R�C���ƃA�C�e���̐���0��
        coin = 0;
        item1 = 0;
        item2 = 0;
        item3 = 0;
    }
}
