using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneTrigger : MonoBehaviour
{
    [Header("Video Player Settings")]
    public VideoPlayer videoPlayer;  // przypisany w Inspectorze

    [Header("Player Settings")]
    public GameObject player;
    public MonoBehaviour playerMovementScript;

    [Header("Scene Settings (Optional)")]
    public string nextSceneName;     // wpisz dokładną nazwę sceny z Build Settings

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;
        if (collision.gameObject != player) return;

        hasTriggered = true;
        StartCutscene();
    }

    void StartCutscene()
    {
        // blokujemy ruch gracza
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // odtwarzamy VideoPlayer (clip ustawiony w Inspectorze)
        if (videoPlayer != null)
        {
            videoPlayer.targetCameraAlpha = 1f; // upewniamy się, że film jest widoczny
            videoPlayer.Play();
            videoPlayer.loopPointReached += EndCutscene;
        }
    }

    void EndCutscene(VideoPlayer vp)
    {
        // przywracamy ruch gracza
        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        // reset VideoPlayer po zakończeniu
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.targetCameraAlpha = 0f;
        }

        // przejście do następnej sceny po nazwie
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}