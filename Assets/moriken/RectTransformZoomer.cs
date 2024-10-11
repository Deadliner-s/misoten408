using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ���̃X�N���v�g���A�^�b�`���ꂽRectTransform���A�X�N���[���ŃY�[�����A
/// �h���b�O�ŏ㉺���E�Ɉړ������邱�Ƃ��ł��܂��B
/// ���̃X�N���v�g�́ACanvas�̐ݒ肪�ȉ��̂����ꂩ�̏ꍇ�ɓ��삵�܂��B
/// * ScreenSpace - Overlay
/// * ScreenSpace - Camera
/// �܂��ACanvasScaler�́uUI Scale Mode�v���uScale With Screen Size�v�ɐݒ肳��Ă��Ă����삵�܂��B
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class RectTransformZoomer : MonoBehaviour, IDragHandler
{
    [SerializeField] Canvas uiCanvas;           // �Y�[��������s�����߂ɎQ�Ƃ���Canvas  
    [SerializeField] float zoomSpeed = 1f;      // �Y�[�����x�𒲐����邽�߂̔{��
    [SerializeField] float minZoomRate = 1f;    // �Y�[���̍ŏ��{��
    [SerializeField] float maxZoomRate = 10f;   // �Y�[���̍ő�{��

    RectTransform targetContent;                // �Ώۂ�RectTransform
    CanvasScaler canvasScaler;                  // Canvas�̃X�P�[�����O�ݒ���擾���邽�߂̕ϐ�

    // ���݂̃Y�[���X�P�[�����擾�iRectTransform�̃X�P�[����X�����j
    float CurrentZoomScale => targetContent.localScale.x;

    // �X�N���[���T�C�Y�ɃX�P�[������ꍇ�A�h���b�O���̈ړ��X�P�[���𒲐����邩�ǂ����𔻒�
    bool ShouldScaleDragMove =>
        canvasScaler != null &&     // canvasScaler�����݂��邩�ǂ���
        canvasScaler.IsActive() &&  // canvasScaler���A�N�e�B�u���ǂ���
        canvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize; // UI Scale Mode���uScale With Screen Size�v�ɐݒ肳��Ă��邩�ǂ���

    void Awake()
    {
        // ���̃R���|�[�l���g���A�^�b�`���ꂽRectTransform���擾
        targetContent = GetComponent<RectTransform>();

        // CanvasScaler�R���|�[�l���g���擾
        canvasScaler = uiCanvas.GetComponent<CanvasScaler>();
    }

    void Update()
    {
        // �}�E�X�̃X�N���[���ʂ��擾
        var scroll = Input.mouseScrollDelta.y;

        // �}�E�X�̌��݈ʒu�ƃX�N���[���ʂ�n���ăY�[���������s��
        ScrollToZoomMap(Input.mousePosition, scroll);
    }

    /// <summary>
    /// �Y�[������A�}�E�X�̈ʒu���R���e���c��̓����ꏊ�ɂ���悤�ɒ�������B
    /// </summary>
    /// <param name="mousePosition">���݂̃}�E�X�ʒu</param>
    /// <param name="scroll">�}�E�X�X�N���[���̗�</param>
    public void ScrollToZoomMap(Vector2 mousePosition, float scroll)
    {
        // �Y�[���O�̃��[�J�����W���擾
        GetLocalPointInRectangle(mousePosition, out var beforeZoomLocalPosition);

        // ���݂̃Y�[���X�P�[���ɃX�N���[���ʂ����Z���ĐV�����Y�[���X�P�[�����v�Z
        var afterZoomScale = CurrentZoomScale + scroll * zoomSpeed;

        // �Y�[���X�P�[�����ŏ��l�ƍő�l�͈͓̔��Ɏ��܂�悤��Clamp�Ő���
        afterZoomScale = Mathf.Clamp(afterZoomScale, minZoomRate, maxZoomRate);

        // �Y�[�����������s
        DoZoom(afterZoomScale);

        // �Y�[����̃��[�J�����W���ēx�擾
        GetLocalPointInRectangle(mousePosition, out var afterZoomLocalPosition);

        // �Y�[���O��̈ʒu�̍������v�Z
        var positionDiff = afterZoomLocalPosition - beforeZoomLocalPosition;

        // �������Y�[���X�P�[���ɉ����Ċg��
        var scaledPositionDiff = positionDiff * afterZoomScale;

        // �V�����A���J�[�|�W�V�������v�Z
        var newAnchoredPosition = targetContent.anchoredPosition + scaledPositionDiff;

        targetContent.anchoredPosition = newAnchoredPosition;
    }

    /// <summary>
    /// �E�B���h�E���h���b�O�����ʂɉ����Ĉړ�������B
    /// IDragHandler�ɂ���Ď����I�ɌĂяo�����B
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        // �h���b�O�ɂ��ړ��ʂ��擾
        var dragMoveDelta = eventData.delta;

        // �X�N���[���T�C�Y�ɉ����ăX�P�[������ꍇ�A�ړ��ʂ𒲐�
        if (ShouldScaleDragMove)
        {
            // �𑜓x�Ɋ�Â��ăh���b�O�ړ��̃X�P�[�����v�Z
            var dragMoveScale = canvasScaler.referenceResolution.x / Screen.width;
            dragMoveDelta *= dragMoveScale;
        }

        // �h���b�O�ɉ�����RectTransform�̈ʒu���X�V
        targetContent.anchoredPosition += dragMoveDelta;
    }

    /// <summary>
    /// �Y�[�����������s����B
    /// </summary>
    /// <param name="zoomScale">�Y�[���{��</param>
    void DoZoom(float zoomScale)
    {
        // RectTransform�̃X�P�[�����X�V
        targetContent.localScale = Vector3.one * zoomScale;
    }

    /// <summary>
    /// �w�肳�ꂽ�}�E�X�ʒu��RectTransform�̂ǂ̃��[�J�����W�ɑΉ����邩���擾����B
    /// </summary>
    /// <param name="mousePosition">�}�E�X�̃X�N���[����̈ʒu</param>
    /// <param name="localPosition">�o�͂���郍�[�J�����W</param>
    void GetLocalPointInRectangle(Vector2 mousePosition, out Vector2 localPosition)
    {
        // Canvas�̃����_�[���[�h�ɉ����Ďg�p����J������I��
        var targetCamera = uiCanvas.renderMode switch
        {
            RenderMode.ScreenSpaceCamera => uiCanvas.worldCamera,   // ScreenSpace - Camera�̏ꍇ�̓��[���h�J�������g�p
            RenderMode.ScreenSpaceOverlay => null,                  // ScreenSpace - Overlay�̏ꍇ�̓J�������g�p���Ȃ�
            _ => throw new System.NotSupportedException(),          // ���̑��̃����_�[���[�h�̓T�|�[�g���Ȃ�
        };

        // �}�E�X�̃X�N���[�����W��RectTransform�̃��[�J�����W�ɕϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetContent, mousePosition, targetCamera, out localPosition);
    }
}
