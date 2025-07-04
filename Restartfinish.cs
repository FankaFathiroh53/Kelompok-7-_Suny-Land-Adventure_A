using UnityEngine;
using UnityEngine.SceneManagement; // ✅ Tambahkan ini

public class Restartfinish : MonoBehaviour
{
    public void OnRestartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}