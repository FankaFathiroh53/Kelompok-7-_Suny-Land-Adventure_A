using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Game Data")]
    [SerializeField] private int nyawa = 3;
    [SerializeField] private int jumlahBintang = 0;
    [SerializeField] private int maxNyawa = 3;
    [SerializeField] private int maxBintang = 5;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void KurangiNyawa()
    {
        nyawa--;

        if (nyawa <= 0)
        {
            GameOver();
        }
    }

    public void TambahNyawa()
    {
        nyawa = Mathf.Min(nyawa + 1, maxNyawa);
    }

    public void TambahBintang()
    {
        jumlahBintang = Mathf.Min(jumlahBintang + 1, maxBintang);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    // Getter
    public int GetNyawa() => nyawa;
    public int GetJumlahBintang() => jumlahBintang;
}