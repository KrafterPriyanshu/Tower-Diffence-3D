using UnityEngine;
using TMPro;
using UnityEngine.UI; // Make sure you import this for using Slider

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int gold = 100;
    public int health = 100;
    public TextMeshProUGUI goldText;
    public Slider healthSlider;  // Slider reference
    public GameObject gameOverPanel;

    // Assuming maxHealth is 100, you can modify it as needed
    public int maxHealth = 100;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Ensure slider is initialized with the max health at the start of the game
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;  // Ensure the max value of the slider matches max health
            healthSlider.value = health;  // Set the initial health value
        }
    }

    void Update()
    {
        if (goldText != null)
        {
            goldText.text = $"Gold: {gold}";
        }

        // Update the health slider if it's assigned
        if (healthSlider != null)
        {
            healthSlider.value = health;  // Update the slider value with the current health
        }
    }

    public void UpdateGold(int amount)
    {
        gold += amount;
        Debug.Log($"Gold updated: {gold}");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;  // Subtract damage from health
        Debug.Log($"Base took {damage} damage. Remaining health: {health}");

        // Ensure health doesn't drop below 0
        if (health < 0)
        {
            health = 0;
        }

        // Update the health slider's value
        if (healthSlider != null)
        {
            healthSlider.value = health;  // Update slider value when health changes
        }

        // If health reaches 0, trigger game over
        if (health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0; // Stops the game
        gameOverPanel.SetActive(true);
    }
}
