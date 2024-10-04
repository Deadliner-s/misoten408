using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
    public float speed = 5f; // �ړ����x��ݒ�

    void Update()
    {
        // ���͂̎擾
        float horizontal = Input.GetAxis("Horizontal"); // A��D�A�܂��͍����L�[�ƉE���L�[
        float vertical = Input.GetAxis("Vertical"); // W��S�A�܂��͏���L�[�Ɖ����L�[

        // �ړ��̌v�Z
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        // �I�u�W�F�N�g�̈ʒu���ړ�
        transform.Translate(movement);
    }
}

