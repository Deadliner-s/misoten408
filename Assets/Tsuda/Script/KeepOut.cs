using UnityEngine;

public class KeepOut : MonoBehaviour
{
    [Header("対象のオブジェクト")]
    public Transform targetObject; // 距離を測る対象オブジェクト

    [Header("透明度設定")]
    public float maxDistance = 10f; // 最大距離（完全透明になる距離）
    public float minDistance = 1f; // 最小距離（完全不透明になる距離）

    [Header("座標調整")]    
    public bool x = false;
    public bool y = false;
    public bool z = false;

    private SpriteRenderer spriteRenderer;
    private Vector3 newPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRendererがアタッチされていません");
        }

        newPosition = transform.position;
    }

    void Update()
    {        
        // 対象オブジェクトとの距離を計算
        float distance = Vector3.Distance(transform.position, targetObject.position);

        // 距離に基づいて透明度を計算
        float alpha = Mathf.InverseLerp(maxDistance, minDistance, distance);

        // SpriteRendererの透明度を設定
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;

       if(x) {newPosition.x = targetObject.position.x; }
       if(y) {newPosition.y = targetObject.position.y; }
       if(z) {newPosition.z = targetObject.position.z; }

       transform.position = newPosition;
    }
}
