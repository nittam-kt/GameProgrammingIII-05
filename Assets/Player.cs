using UnityEngine;
using UnityEngine.InputSystem;
using R3;               // R3 core
using R3.Triggers;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;

    public float MaxLife => 100f;
    public ReactiveProperty<float> life { get; private set; } = new();

    PlayerInput playerInput;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        life.Value = MaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        var move = playerInput.actions["Move"].ReadValue<Vector2>();

        if (move.x != 0f)
        {
            rb.linearVelocityX = move.x * speed;
        }

        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            rb.linearVelocityY = jumpSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        const float damage = 5f;
        const float damageSpeed = 8f;

        foreach(var col in collision.contacts)
        {
            if(col.rigidbody?.tag == "Enemy")
            {
                // ダメージを受ける
                life.Value -= damage;

                // 相手の反対方向にrbの速度を設定
                var dir = (rb.position - col.rigidbody.position).normalized;
                rb.linearVelocity = dir * damageSpeed;
            }
        }
    }
}
