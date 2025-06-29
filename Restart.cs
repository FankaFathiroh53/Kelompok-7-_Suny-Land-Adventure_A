using UnityEngine;
using UnityEngine.SceneManagement; // ✅ Tambahkan ini

public class Restart : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}