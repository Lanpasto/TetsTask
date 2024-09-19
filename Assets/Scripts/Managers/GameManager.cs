using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("References UI")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Image[] keyImages;
    [SerializeField] private TextMeshProUGUI timerText;
    [Header("References Player")]
    [SerializeField] private GameObject player;
    [HideInInspector]
    public int keysCollected = 0;
    [HideInInspector]
    public int totalKeys = 3;
    private float gameTime = 0f;
    private bool isGamePaused = false;
    private bool startGame = false;
    // public int KeysCollected => keysCollected;
    // public int TotalKeys => totalKeys;
    private int deathHigh = -10;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartSettingGame();
    }
    private void StartSettingGame()
    {
        Time.timeScale = 1f;
        timerText.text = "Time: 0";
        UpdateKeysUI();
        ResetUI();
        HideCursor();
    }

    private void Update()
    {
        Debug.Log(Time.timeScale);
        if (!isGamePaused)
        {
            if (startGame)
            {
                gameTime += Time.deltaTime;
                //formating text
                timerText.text = "Time: " + gameTime.ToString("F2");
            }

            if (player != null && player.transform.position.y < deathHigh)
            {
                GameOver();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    //add your key to the collection
    public void CollectKey()
    {
        if (keysCollected < totalKeys)
        {
            keysCollected++;
            UpdateKeysUI();
        }
    }
    //Update image how many you collected keys
    private void UpdateKeysUI()
    {
        for (int i = 0; i < keysCollected; i++)
        {
            keyImages[i].gameObject.SetActive(true);
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        ShowCursor();
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        HideCursor();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        ShowGameOverScreen();
        ShowCursor();
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        ShowWinScreen();
        ShowCursor();
    }

    public void StartTimer()
    {
        timerText.gameObject.SetActive(true);
        gameTime = 0f;
        startGame = true;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    //Reset GameUi to start condition when ReTry
    private void ResetUI()
    {
        timerText.gameObject.SetActive(false);
        foreach (var image in keyImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    private void ShowWinScreen()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }
        Time.timeScale = 0f;
    }
    // Cursor function
    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}