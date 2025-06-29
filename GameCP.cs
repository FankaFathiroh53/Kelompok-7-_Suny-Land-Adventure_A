using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCP : MonoBehaviour
{
    public static GameCP instance;

    [Header("UI Game Over")]
    public GameObject panelGameOver;
    public Button restartButton;

    private void Awake()
    {
        // Pastikan hanya satu instance GameCP
        if (instance == null)
        {
            instance = this;
            // Opsional jika ingin bertahan antar scene:
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Pastikan UI game over tidak aktif di awal
        if (panelGameOver != null)
            panelGameOver.SetActive(false);

        // Hubungkan tombol restart jika tersedia
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
    }

    public void TampilkanGameOver()
    {
        if (panelGameOver != null)
            panelGameOver.SetActive(true);

        Time.timeScale = 0f; // Pause game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload scene
    }
}
