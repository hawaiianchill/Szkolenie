using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("Ground Check")]
    public Transform groundCheckPoint;      // punkt pod stopami
    public LayerMask groundLayer;           // warstwa ziemi
    private float groundCheckRadius = 1f;   // SZTYWNO ustawiony na 1
    public bool isGrounded;                 // dostêpne dla animatora

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // jeœli nie ma GroundCheckPoint, tworzymy automatycznie
        if (groundCheckPoint == null)
        {
            GameObject gcp = new GameObject("GroundCheckPoint");
            gcp.transform.parent = this.transform;
            gcp.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            groundCheckPoint = gcp.transform;
        }
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // --- Ruch poziomy ---
        float x = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            x = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            x = 1f;

        // --- Sprawdzenie ziemi ---
        if (groundCheckPoint != null)
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position + Vector3.down * 0.01f, groundCheckRadius, groundLayer);

        // --- Skok ---
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        // --- Ustaw prêdkoœæ poziom¹ ---
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
    }

    void OnDrawGizmos()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position + Vector3.down * 0.01f, groundCheckRadius);
        }
    }
}