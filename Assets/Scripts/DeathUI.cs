using UnityEngine;

public class DeathUI : MonoBehaviour
{
    public HealthBar healthBar;
    public int reviveHealth = 50;

    public void Revive()
    {
        Time.timeScale = 1f;
        healthBar.currentHealth = reviveHealth;
        healthBar.SetHealth(reviveHealth);
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
