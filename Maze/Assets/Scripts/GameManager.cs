using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gameplay Settings")]
    public int totalCoins = 12;
    public float timeLimit = 1800f; // 30 minutes

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text coinCounterText;
    public GameObject successUI;
    public GameObject gameOverUI;

    private float timeRemaining;
    private int coinsCollected = 0;
    private bool gameActive = true;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateCoinUI();
        UpdateTimerUI();

        if (successUI) successUI.SetActive(false);
        if (gameOverUI) gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (!gameActive) return;

        timeRemaining -= Time.deltaTime;
        UpdateTimerUI();

        if (timeRemaining <= 0)
        {
            EndGame(false);
        }
    }

    public void CollectCoin()
    {
        coinsCollected++;
        UpdateCoinUI();

        if (coinsCollected >= totalCoins)
        {
            Debug.Log("[GameManager] All coins collected!");
            EndGame(true);
        }
    }

    void UpdateCoinUI()
    {
        if (coinCounterText != null)
            coinCounterText.text = $"Coins: {coinsCollected}/{totalCoins}";
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void EndGame(bool success)
    {
        gameActive = false;

        if (success && successUI != null)
        {
            successUI.SetActive(true);
        }
        else if (!success && gameOverUI != null)
        {
            Transform cam = Camera.main.transform;

            // Set position 10 meters in front of the camera and slightly up
            gameOverUI.transform.position = cam.position + cam.forward * 10f + Vector3.up * 0.5f;

            gameOverUI.transform.rotation = Quaternion.identity;

            gameOverUI.SetActive(true);
        }


        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
