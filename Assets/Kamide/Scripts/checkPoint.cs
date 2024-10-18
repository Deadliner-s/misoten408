using JetBrains.Annotations;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("チェックポイント番号")]
    public int cp_num = 0;                  // チェックポイントの番号
    [Header("ステージ番号")]
    public int stage = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 回転処理
        Transform transform = this.transform;
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 0.1f;
        transform.eulerAngles = rotation;

    }

    private void OnTriggerEnter(Collider collider)
    {
        // タグによって触れられるオブジェクトを指定
        if (collider.tag == "Player")
        {
            // フラグ切り替え
            GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().ChangeFlag(stage, cp_num, false);

            // ゲージ回復処理

            // スコア加算

            // このオブジェクトの削除
            Destroy(this);
        }
    }
}
