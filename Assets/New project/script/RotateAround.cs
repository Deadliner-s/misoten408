using UnityEngine;

public class RotateAround : MonoBehaviour
{
    // ��]���x
    public float rotationSpeed = 50f;

    void Update()
    {
        // Y���𒆐S�ɉ�]������
        // transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
