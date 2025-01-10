using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private Image progressGauge;
    private float time;
    private float maxTime;
    [SerializeField] private float seconds;

    [SerializeField] private Image bicycle;
    private Vector3 bicycleStartPos;    // ç≈èâÇÃé©ì]é‘ÇÃPosition
    private Vector3 bicycleEndPos;      // åªç›ÇÃé©ì]é‘ÇÃPosition
    private Vector3 bicycleNowPos;                       // åªç›ÇÃé©ì]é‘ÇÃPosition
    private float maxDistanceX;                          // é©ì]é‘ÇÃXç¿ïWÇÃç≈èâÇ©ÇÁç≈å„ÇÃä‘ÇÃãóó£

    [SerializeField] private float bicyclePosX;
    [SerializeField] private float bicyclePosY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressGauge = gameObject.GetComponent<Image>();
        maxTime = seconds *  60.0f;

        bicycleStartPos = bicycleNowPos = new Vector3(bicyclePosX, bicyclePosY, 0.0f);
        bicycleEndPos = new Vector3(465.0f, bicyclePosY, 0.0f);
        bicycle.rectTransform.localPosition = bicycleStartPos;

        maxDistanceX = -bicycleStartPos.x + bicycleEndPos.x;
    }

    // Update is called once per frame
    void Update()
    {
        // ÉQÅ[ÉWÇìÆÇ©Ç∑èàóù
        time += 1 / maxTime;
        progressGauge.fillAmount = time;

        // ÉQÅ[ÉWè„ÇÃé©ì]é‘ÇìÆÇ©Ç∑èàóù
        if (bicycleNowPos.x < bicycleEndPos.x)
        {
            float moveX = (1 / maxTime) * maxDistanceX;
            bicycleNowPos.x += moveX;
            bicycle.rectTransform.localPosition = bicycleNowPos;
        }
    }
}
