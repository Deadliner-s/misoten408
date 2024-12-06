using UnityEngine;

public class TalkArea : MonoBehaviour
{
    [Header("イベント名")]
    public EventData.EventNameEnum eventName;    // イベント名

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug用F2を押すと強制的にチェックポイントを通過
        if (Input.GetKeyDown(KeyCode.F2))
        {
            NPCDialogueManager.instance.SetCheckPoint(EventData.EventNameEnum.パターゴルフ);
        }
    }
}
