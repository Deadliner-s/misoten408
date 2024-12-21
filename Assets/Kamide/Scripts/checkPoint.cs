using JetBrains.Annotations;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("チェックポイント番号")]
    public int cp_num = 0;                  // チェックポイントの番号
    [Header("ステージ番号")]
    public int stage = 0;
    private bool isUIAppeared = false;
    private bool isEnterCheckPointArea = false;

    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        // 回転処理
        Transform transform = this.transform;
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 0.2f;
        transform.eulerAngles = rotation;  
        
        // ボタン入力
        if (isEnterCheckPointArea && NPCDialogueManager.instance.isTalking == false &&
            (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.Return)))
        {
            if (isUIAppeared == false)
            {
                // UI表示
                GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().CreateUI(stage, cp_num);

                // フラグの切り替え
                isUIAppeared = true;
            }
            else
            {
                // UI削除
                GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().DestroyUI();

                // フラグの切り替え
                isUIAppeared = false;
            }

        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // タグによって触れられるオブジェクトを指定
        if (collider.tag == "Player")
        {
            // フラグの切り替え
            isEnterCheckPointArea = true;           
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // タグによって判定
        if (collider.tag == "Player")
        {
            if (isUIAppeared == true)
            {
                // UI削除
                GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().DestroyUI();
            }

            // フラグの切り替え
            isUIAppeared = false;
            isEnterCheckPointArea = false;
        }
    }
}
