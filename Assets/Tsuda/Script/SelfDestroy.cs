using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // ����(�b)��ݒ�
    public float timeToDestroy = 5.0f;

    void Start()
    {
        // �w�肵�����Ԍ�ɂ��̃I�u�W�F�N�g���폜
        Destroy(gameObject, timeToDestroy);
    }
}
