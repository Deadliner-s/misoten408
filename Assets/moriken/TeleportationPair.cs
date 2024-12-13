using UnityEngine;

public class TeleportationPair : MonoBehaviour
{
    public Transform linkedTeleportPoint; // �e���|�[�g��̃I�u�W�F�N�g
    private static bool isTeleporting = false; // �S�̂ŋ��L�����e���|�[�g���t���O

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���ڐG���A�e���|�[�g���łȂ��ꍇ
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(Teleport(other.transform));
        }
    }

    private System.Collections.IEnumerator Teleport(Transform player)
    {
        // �e���|�[�g���t���O��L����
        isTeleporting = true;

        // �v���C���[�������N��̈ʒu�Ɉړ�
        player.position = linkedTeleportPoint.position;

        // �������[�v��h�����߁A�����ҋ@
        yield return new WaitForSeconds(0.5f);

        // �e���|�[�g���t���O������
        isTeleporting = false;
    }
}
