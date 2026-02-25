using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneTrigger : MonoBehaviour
{
    public string nextSceneName; // wpisz dok³adn¹ nazwê sceny z Build Settings

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // sprawdzenie czy to gracz
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}