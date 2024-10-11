using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// このスクリプトがアタッチされたRectTransformを、スクロールでズームし、
/// ドラッグで上下左右に移動させることができます。
/// このスクリプトは、Canvasの設定が以下のいずれかの場合に動作します。
/// * ScreenSpace - Overlay
/// * ScreenSpace - Camera
/// また、CanvasScalerの「UI Scale Mode」が「Scale With Screen Size」に設定されていても動作します。
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class RectTransformZoomer : MonoBehaviour, IDragHandler
{
    [SerializeField] Canvas uiCanvas;           // ズーム操作を行うために参照するCanvas  
    [SerializeField] float zoomSpeed = 1f;      // ズーム速度を調整するための倍率
    [SerializeField] float minZoomRate = 1f;    // ズームの最小倍率
    [SerializeField] float maxZoomRate = 10f;   // ズームの最大倍率

    RectTransform targetContent;                // 対象のRectTransform
    CanvasScaler canvasScaler;                  // Canvasのスケーリング設定を取得するための変数

    // 現在のズームスケールを取得（RectTransformのスケールのX成分）
    float CurrentZoomScale => targetContent.localScale.x;

    // スクリーンサイズにスケールする場合、ドラッグ時の移動スケールを調整するかどうかを判定
    bool ShouldScaleDragMove =>
        canvasScaler != null &&     // canvasScalerが存在するかどうか
        canvasScaler.IsActive() &&  // canvasScalerがアクティブかどうか
        canvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize; // UI Scale Modeが「Scale With Screen Size」に設定されているかどうか

    void Awake()
    {
        // このコンポーネントがアタッチされたRectTransformを取得
        targetContent = GetComponent<RectTransform>();

        // CanvasScalerコンポーネントを取得
        canvasScaler = uiCanvas.GetComponent<CanvasScaler>();
    }

    void Update()
    {
        // マウスのスクロール量を取得
        var scroll = Input.mouseScrollDelta.y;

        // マウスの現在位置とスクロール量を渡してズーム処理を行う
        ScrollToZoomMap(Input.mousePosition, scroll);
    }

    /// <summary>
    /// ズーム後も、マウスの位置がコンテンツ上の同じ場所にあるように調整する。
    /// </summary>
    /// <param name="mousePosition">現在のマウス位置</param>
    /// <param name="scroll">マウススクロールの量</param>
    public void ScrollToZoomMap(Vector2 mousePosition, float scroll)
    {
        // ズーム前のローカル座標を取得
        GetLocalPointInRectangle(mousePosition, out var beforeZoomLocalPosition);

        // 現在のズームスケールにスクロール量を加算して新しいズームスケールを計算
        var afterZoomScale = CurrentZoomScale + scroll * zoomSpeed;

        // ズームスケールが最小値と最大値の範囲内に収まるようにClampで制限
        afterZoomScale = Mathf.Clamp(afterZoomScale, minZoomRate, maxZoomRate);

        // ズーム処理を実行
        DoZoom(afterZoomScale);

        // ズーム後のローカル座標を再度取得
        GetLocalPointInRectangle(mousePosition, out var afterZoomLocalPosition);

        // ズーム前後の位置の差分を計算
        var positionDiff = afterZoomLocalPosition - beforeZoomLocalPosition;

        // 差分をズームスケールに応じて拡大
        var scaledPositionDiff = positionDiff * afterZoomScale;

        // 新しいアンカーポジションを計算
        var newAnchoredPosition = targetContent.anchoredPosition + scaledPositionDiff;

        targetContent.anchoredPosition = newAnchoredPosition;
    }

    /// <summary>
    /// ウィンドウをドラッグした量に応じて移動させる。
    /// IDragHandlerによって自動的に呼び出される。
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグによる移動量を取得
        var dragMoveDelta = eventData.delta;

        // スクリーンサイズに応じてスケールする場合、移動量を調整
        if (ShouldScaleDragMove)
        {
            // 解像度に基づいてドラッグ移動のスケールを計算
            var dragMoveScale = canvasScaler.referenceResolution.x / Screen.width;
            dragMoveDelta *= dragMoveScale;
        }

        // ドラッグに応じてRectTransformの位置を更新
        targetContent.anchoredPosition += dragMoveDelta;
    }

    /// <summary>
    /// ズーム処理を実行する。
    /// </summary>
    /// <param name="zoomScale">ズーム倍率</param>
    void DoZoom(float zoomScale)
    {
        // RectTransformのスケールを更新
        targetContent.localScale = Vector3.one * zoomScale;
    }

    /// <summary>
    /// 指定されたマウス位置がRectTransformのどのローカル座標に対応するかを取得する。
    /// </summary>
    /// <param name="mousePosition">マウスのスクリーン上の位置</param>
    /// <param name="localPosition">出力されるローカル座標</param>
    void GetLocalPointInRectangle(Vector2 mousePosition, out Vector2 localPosition)
    {
        // Canvasのレンダーモードに応じて使用するカメラを選択
        var targetCamera = uiCanvas.renderMode switch
        {
            RenderMode.ScreenSpaceCamera => uiCanvas.worldCamera,   // ScreenSpace - Cameraの場合はワールドカメラを使用
            RenderMode.ScreenSpaceOverlay => null,                  // ScreenSpace - Overlayの場合はカメラを使用しない
            _ => throw new System.NotSupportedException(),          // その他のレンダーモードはサポートしない
        };

        // マウスのスクリーン座標をRectTransformのローカル座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetContent, mousePosition, targetCamera, out localPosition);
    }
}
