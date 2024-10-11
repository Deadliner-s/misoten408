using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
    public float speed = 5f; // 移動速度を設定

    void Update()
    {
        // 入力の取得
        float horizontal = Input.GetAxis("Horizontal"); // AとD、または左矢印キーと右矢印キー
        float vertical = Input.GetAxis("Vertical"); // WとS、または上矢印キーと下矢印キー

        // 移動の計算
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        // オブジェクトの位置を移動
        transform.Translate(movement);
    }
}

