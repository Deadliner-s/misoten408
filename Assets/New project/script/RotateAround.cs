using UnityEngine;

public class RotateAround : MonoBehaviour
{
    // ‰ñ“]‘¬“x
    public float rotationSpeed = 50f;

    void Update()
    {
        // Y²‚ğ’†S‚É‰ñ“]‚³‚¹‚é
        // transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
