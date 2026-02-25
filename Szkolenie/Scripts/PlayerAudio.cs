using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAudio : MonoBehaviour
{
    public AudioClip stepSound;    // d�wi�k chodzenia
    public AudioClip jumpSound;    // d�wi�k skoku (opcjonalny)

    public float stepInterval = 0.4f; // odst�p mi�dzy krokami
    private float stepTimer = 0f;

    private AudioSource audioSource;
    private Rigidbody2D rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        stepTimer = stepInterval; // gotowe do odtwarzania
    }

    void Update()
    {
        float horizontal = rb.linearVelocity.x;

        // je�li mamy d�wi�k skoku i posta� skacze w g�r�
        bool isJumping = jumpSound != null && rb.linearVelocity.y > 0.1f;

        if (isJumping)
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(jumpSound);
            stepTimer = stepInterval; // reset timera krok�w
            return; // skok nadpisuje chodzenie
        }

        // odtwarzanie d�wi�ku chodzenia, je�li posta� si� porusza w poziomie
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                if (stepSound != null)
                    audioSource.PlayOneShot(stepSound);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval; // reset timera je�li stoi
        }
    }
}