using Cysharp.Threading.Tasks;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform player;               // プレイヤーを Inspector で登録

    [SerializeField] float moveSpeed = 2.0f;         // 左右移動速度
    [SerializeField] float moveDuration = 2f;      // 移動を続ける時間 (s)

    Rigidbody2D rb;

    async UniTask Start()
    {
        rb = GetComponent<Rigidbody2D>();

        while (true)
        {
            await MoveTowardPlayerAsync();
        }
    }

    /// <summary>プレイヤー方向へ一定時間水平移動。</summary>
    async UniTask MoveTowardPlayerAsync()
    {
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            float speed;
            if(player.position.x > transform.position.x)
            {
                speed = moveSpeed;
            }
            else
            {
                speed = -moveSpeed;
            }
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

            elapsed += Time.deltaTime;
            await UniTask.DelayFrame(1);
        }

        // 停止
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}
