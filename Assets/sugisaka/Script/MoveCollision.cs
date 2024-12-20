using UnityEngine;

public class MoveCollision : MonoBehaviour
{
    // 衝突したオブジェクトのタグがPlayerだった場合
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Playerの親オブジェクトを取得
            GameObject player = collision.gameObject;

            // 衝突フラグを立てる
            //player.GetComponent<Player>().isCollision = true;

        }
    }
}
