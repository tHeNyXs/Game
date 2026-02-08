using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    [SerializeField] FloatingHealthBar healthBar;

    void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
