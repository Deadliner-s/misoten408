using UnityEngine;

public class TeleportationPair : MonoBehaviour
{
    public Transform linkedTeleportPoint; // テレポート先のオブジェクト
    private static bool isTeleporting = false; // 全体で共有されるテレポート中フラグ

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが接触し、テレポート中でない場合
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(Teleport(other.transform));
        }
    }

    private System.Collections.IEnumerator Teleport(Transform player)
    {
        // テレポート中フラグを有効化
        isTeleporting = true;

        // プレイヤーをリンク先の位置に移動
        player.position = linkedTeleportPoint.position;

        // 無限ループを防ぐため、少し待機
        yield return new WaitForSeconds(0.5f);

        // テレポート中フラグを解除
        isTeleporting = false;
    }
}
