using UnityEngine;

public class bintang4 : MonoBehaviour
{
    // Inisialisasi
    player komponenGerak;

    // Start is called before the first frame update
    void Start()
    {
        komponenGerak = GameObject.Find("player").GetComponent<player>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        // Jika musuh bertabrakan dengan objek yang memiliki tag "Player"
        if (other.transform.tag == "Player")
        {
            komponenGerak.bintang4++; // Tambah star karakter
            Destroy(gameObject); // Hancurkan objek star
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}