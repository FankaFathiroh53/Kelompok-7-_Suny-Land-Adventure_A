using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject panelGameOver;
    public Text skorText;

    public void Setup(int skor)
    {
        panelGameOver.SetActive(true);
        skorText.text = "Skor: " + skor;
    }
}