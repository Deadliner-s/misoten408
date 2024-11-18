using UnityEngine;

public class KeepOut : MonoBehaviour
{
    [Header("�Ώۂ̃I�u�W�F�N�g")]
    public Transform targetObject; // �����𑪂�ΏۃI�u�W�F�N�g

    [Header("�����x�ݒ�")]
    public float maxDistance = 10f; // �ő勗���i���S�����ɂȂ鋗���j
    public float minDistance = 1f; // �ŏ������i���S�s�����ɂȂ鋗���j

    [Header("���W����")]    
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
            Debug.LogError("SpriteRenderer���A�^�b�`����Ă��܂���");
        }

        newPosition = transform.position;
    }

    void Update()
    {        
        // �ΏۃI�u�W�F�N�g�Ƃ̋������v�Z
        float distance = Vector3.Distance(transform.position, targetObject.position);

        // �����Ɋ�Â��ē����x���v�Z
        float alpha = Mathf.InverseLerp(maxDistance, minDistance, distance);

        // SpriteRenderer�̓����x��ݒ�
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;

       if(x) {newPosition.x = targetObject.position.x; }
       if(y) {newPosition.y = targetObject.position.y; }
       if(z) {newPosition.z = targetObject.position.z; }

       transform.position = newPosition;
    }
}
