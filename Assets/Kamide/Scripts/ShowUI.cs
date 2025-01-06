using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [Header("表示時間")]
    public float limitTime;
    [Header("UI移動時間")]
    public float animTime;
    public UIinfo info;
    public GameObject UI;
    GameObject createdUI;
    private Vector3 StartDeckPos;
    private Vector3 EndHandPos;
    private bool isFinished;    
    private int state;
    private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 変数の初期化
        isFinished = false;        
        state = 0;
        time = 0.0f;        

        // UIの生成
        CreateUI();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                break;

            case 1:
                if (isFinished)
                {
                    // 時間加算
                    time += Time.deltaTime;

                    if (time > limitTime)
                    {
                        // UI移動処理
                        DestroyUI();

                        // ステートの切り替え
                        state = 2;

                        // 変数の初期化
                        time = 0.0f;
                    }
                }
                break;

            case 2:
                if (isFinished)
                {
                    // UI削除
                    Destroy(createdUI);

                    // フラグ切り替え
                    isFinished = false;                    

                    // ステート切り替え
                    state = 0;
                }
                break;
        }
    }

    private void CreateUI()
    {
        // UIの生成
        if (createdUI == null)
        {
            createdUI = Instantiate(UI);
            createdUI.transform.Find("backGround2/Description").GetComponent<TextMeshProUGUI>().text = info.description;
            createdUI.transform.Find("backGround2/checkPointName").GetComponent<TextMeshProUGUI>().text = info.name;

            // UI移動処理
            StartDeckPos = createdUI.transform.GetChild(0).localPosition;
            EndHandPos = new Vector3(-530, StartDeckPos.y, StartDeckPos.z);
            StartCoroutine(MoveUI());

            // フラグの切り替え
            isFinished = false;

            // ステートの切り替え
            state = 1;
        }
    }

    private void DestroyUI()
    {
        if (createdUI != null && isFinished)
        {
            // フラグの切り替え
            isFinished = false;

            // UI移動処理
            StartDeckPos = createdUI.transform.GetChild(0).localPosition;
            EndHandPos = new Vector3(-1400, StartDeckPos.y, StartDeckPos.z);
            StartCoroutine(MoveUI());
        }
    }

    private IEnumerator MoveUI()
    {
        float animDuration = animTime;
        float startTime = Time.time;
        while(Time.time - startTime < animDuration)
        {
            float journeyFraction = (Time.time - startTime) / animDuration;
            journeyFraction = Mathf.SmoothStep(0f, 1f, journeyFraction);
            createdUI.transform.GetChild(0).localPosition = Vector3.Lerp(StartDeckPos, EndHandPos, journeyFraction);
            yield return null;
        }

        isFinished = true;
    }

    [System.Serializable]
    public  class UIinfo
    {
        [Header("名前")]
        public string name;
        [Header("説明文")]
        public string description;
    }

}
