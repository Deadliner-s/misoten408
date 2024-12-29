using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // �v���C���[��Transform
    public Transform player;

    // �J�����̃I�t�Z�b�g
    public Vector3 offset;

    // �J�����̒Ǐ]���x
    public float followSpeed = 5.0f;

    void LateUpdate()
    {
        if (player != null)
        {
            // �ڕW�ʒu���v�Z
            Vector3 targetPosition = player.position + offset;

            // �J�������X���[�Y�ɒǏ]������
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // �J�����̉�]�p�x���Œ�
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
        }
    }
}
