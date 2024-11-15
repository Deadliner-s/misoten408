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
    }

    private void OnTriggerEnter(Collider collider)
    {
        // タグによって触れられるオブジェクトを指定
        if (collider.tag == "Player" && isUIAppeared == false)
        {            
            // UI表示
            GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().CreateUI(stage, cp_num);

            // ゲージ回復処理

            // スコア加算
            
            // フラグ切り替え
            isUIAppeared = true;                        
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // タグによって判定
        if (collider.tag == "Player" && isUIAppeared == true)
        {
            // Object取得
            GameObject gameCPM =  GameObject.Find("GameCheckPointManager");

            // フラグ切り替え
            //gameCPM.GetComponent<GameCheckPointManager>().ChangeFlag(stage, cp_num, false);

            // UIの削除
            gameCPM.GetComponent<GameCheckPointManager>().DestroyUI();

            // このオブジェクトの削除
            //Destroy(this);

            // フラグの切り替え
            isUIAppeared = false;
        }
    }
}
