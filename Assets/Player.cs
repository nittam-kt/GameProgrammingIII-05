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

        rb.linearVelocityX = move.x * speed;

        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            rb.linearVelocityY = jumpSpeed;
        }
    }
}
