using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerController))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    // teraz dokładnie pasuje do Twojego Animatora
    private string isMovingParam = "isMoving";
    private string isGroundedParam = "isGrounded";

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (animator == null || playerController == null) return;

        float horizontal = playerController.GetComponent<Rigidbody2D>().linearVelocity.x;

        // ustawienie parametrów Animatora
        animator.SetBool(isMovingParam, Mathf.Abs(horizontal) > 0.1f);
        animator.SetBool(isGroundedParam, playerController.isGrounded);

        // flip sprite
        if (spriteRenderer != null)
        {
            if (horizontal > 0f) spriteRenderer.flipX = false;
            else if (horizontal < 0f) spriteRenderer.flipX = true;
        }
    }
}