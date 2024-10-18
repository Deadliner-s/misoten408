using UnityEngine;

public class StageCheckPointManager : MonoBehaviour
{
    // 変数宣言
    [Header("チェックポイントPrefab")]
    public GameObject obj;       
    public CheckPoint[] checkPoints;    // チェックポイントクラス配列        


    void Start()
    {
        // このオブジェクトを破壊しないようにする
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            
        }
    }

    /// <summary>
    /// 説明文取得関数
    /// </summary>
    /// <param name="cp_num">取得するチェックポイントの番号</param>
    /// <returns>対象チェックポイントの説明文</returns>
    public string GetDescription(int cp_num)
    {
        // 範囲内判定
        if (cp_num > checkPoints.Length ||
            cp_num < 1)
            return null;

        // 引数調整
        cp_num--;

        // 戻り値
        return checkPoints[cp_num].cp_description;
    }

    /// <summary>
    /// チェックポイント名取得関数
    /// </summary>
    /// <param name="cp_num">取得するチェックポイントの番号</param>
    /// <returns>対象チェックポイントの名前</returns>
    public string GetName(int cp_num)
    {
        // 範囲内判定
        if (cp_num > checkPoints.Length ||
            cp_num < 1)
            return null;

        // 引数調整
        cp_num--;

        // 戻り値
        return checkPoints[cp_num].cp_name;
    }
    

    /// <summary>
    /// チェックポイントを全て生成
    /// </summary>
    public void CreateAllCheckPoints()
    {
        // フラグがtrueのチェックポイントを生成
        for (int i = 0; i < checkPoints.Length; i++)
        {
            // 空やフラグがfalseの場合処理をスキップ
            if (checkPoints[i].cp_isWorked == false ||
                checkPoints[i] == null)
            {
                continue;
            }

            // オブジェクトの生成
            GameObject checkpoint = Instantiate(obj,
                new Vector3(
                checkPoints[i].cp_position.x,
                checkPoints[i].cp_position.y,
                checkPoints[i].cp_position.z),
                Quaternion.Euler(90, 0, 0));
       
            // 値の代入
            checkpoint.GetComponent<checkPoint>().cp_num = checkPoints[i].cp_num;
        }
    }

    /// <summary>
    /// 指定のチェックポイントを1つ生成
    /// </summary>
    /// <param name="cp_num"></param>
    public void CreateCheckPoint(int cp_num)
    {
        // 空やフラグがfalseの場合処理をスキップ
        if (checkPoints[cp_num].cp_isWorked == false ||
            checkPoints[cp_num] == null)
        {
            return;
        }

        // オブジェクトの生成
        GameObject checkpoint = Instantiate(obj,
            new Vector3(
                checkPoints[cp_num].cp_position.x,
                checkPoints[cp_num].cp_position.y,
                checkPoints[cp_num].cp_position.z),
                 Quaternion.Euler(90, 0, 0));

        // 値の代入
        checkpoint.GetComponent<checkPoint>().cp_num = cp_num;
    }

    // チェックポイントクラス
    [System.Serializable]
    public  class CheckPoint
    {       
        public string cp_name;  
        [Header("チェックポイント番号")]
        public int cp_num;
        [Header("説明文")]
        public string cp_description;
        [Header("設置座標")]
        public Vector3 cp_position;
        [Header("稼動フラグ")]
        public bool cp_isWorked;
    }
}

