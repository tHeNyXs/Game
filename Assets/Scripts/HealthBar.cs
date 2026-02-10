using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public int maxHealth;
    public int currentHealth;

    [Header("Death UI")]
    public GameObject deathCanvas;

    void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);

        deathCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        SetHealth(currentHealth);
    }

    void Die()
    {
        deathCanvas.SetActive(true);
        // Time.timeScale = 0f;
        Debug.Log("Player Died");
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        currentHealth = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}