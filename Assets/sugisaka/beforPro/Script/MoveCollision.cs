using UnityEngine;

public class MoveCollision : MonoBehaviour
{
    // �Փ˂����I�u�W�F�N�g�̃^�O��Player�������ꍇ
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player�̐e�I�u�W�F�N�g���擾
            GameObject player = collision.gameObject;

            // �Փ˃t���O�𗧂Ă�
            //player.GetComponent<Player>().isCollision = true;

        }
    }
}
