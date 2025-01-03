using UnityEngine;

public class RunGameManager : MonoBehaviour
{
    public static RunGameManager instance;

    public enum DifficultyLevel
    {
        None,
        C_初級,
        C_中級,
        C_上級,
        A_初級,
        A_中級,
        A_上級,
        B_初級,
        B_中級,
        B_上級
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

    [Header("コイン")]
    public int coin;

    [Header("アイテム数")]
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
}
