using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float kecepatan = 5f;
    public float kekuatanLompat = 10f;

    [Header("Ground Check")]
    public Transform deteksi_tanah;
    public float jangkauan = 0.2f;
    public LayerMask targetlayer;
    private bool isGrounded;

    [Header("Star Collection")]
    public int bintang1 = 0;
    public int bintang2 = 0;
    public int star3 = 0;
    public int bintang4 = 0;
    public int bintang5 = 0;

    [Header("Player Status")]
    public int nyawa = 3;
    private Vector3 posisiRespawn;
    private bool bisaKenaDamage = true;
    public float delayDamage = 1f;

    [Header("Jatuh Game Over")]
    public float batasJatuh = -25f;

    [Header("Icon Nyawa")]
    public GameObject[] ikonNyawa;

    [Header("Icon Bintang")]
    public GameObject[] ikonBintang;
    public int jumlahBintang = 0;

    private Rigidbody2D rb;
    private bool menghadapKanan = true;
    private int arahGerak;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posisiRespawn = transform.position;
    }

    void Update()
    {
        // Deteksi tanah
        isGrounded = Physics2D.OverlapCircle(deteksi_tanah.position, jangkauan, targetlayer);

        // Gerakan kiri dan kanan
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * kecepatan * Time.deltaTime);
            arahGerak = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * kecepatan * Time.deltaTime);
            arahGerak = -1;
        }

        // Melompat
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * kekuatanLompat, ForceMode2D.Impulse);
        }

        // Batas kecepatan lompat
        if (rb.linearVelocity.y > 8f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 8f);
        }

        // Balik arah visual
        if (arahGerak > 0 && !menghadapKanan || arahGerak < 0 && menghadapKanan)
        {
            BalikArah();
        }

        // ✅ DETEKSI JATUH
        if (transform.position.y < batasJatuh)
        {
            KurangiNyawa(1); // atau langsung Mati(); kalau tanpa nyawa
        }
    }

    void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawLine(new Vector3(-100, batasJatuh, 0), new Vector3(100, batasJatuh, 0));
}

    void BalikArah()
    {
        menghadapKanan = !menghadapKanan;
        Vector3 skala = transform.localScale;
        skala.x *= -1;
        transform.localScale = skala;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bintang"))
        {
            collision.tag = "Untagged";
            TambahBintang(1);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Finish"))
        {
            collision.tag = "Finish";
            Time.timeScale = 0;
            SceneManager.LoadScene("FinishScene");
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            posisiRespawn = collision.transform.position;
            Debug.Log("Checkpoint disimpan di: " + posisiRespawn);
        }
    }

    public void TambahNyawa(int jumlah)
    {
        nyawa += jumlah;
        Debug.Log("Nyawa bertambah. Total nyawa: " + nyawa);
        for (int i = 0; i < ikonNyawa.Length; i++)
        {
            if (ikonNyawa[i] != null)
                ikonNyawa[i].SetActive(i < nyawa);
        }
    }

    public void KurangiNyawa(int jumlah)
    {
        if (!bisaKenaDamage) return;

        nyawa -= jumlah;
        Debug.Log("Nyawa tersisa: " + nyawa);

        for (int i = 0; i < ikonNyawa.Length; i++)
        {
            if (ikonNyawa[i] != null)
                ikonNyawa[i].SetActive(i < nyawa);
        }

        if (nyawa <= 0)
        {
            Mati();
        }
        else
        {
            Respawn();
            StartCoroutine(TundaDamage());
        }
    }

    public void TambahBintang(int jumlah)
    {
        jumlahBintang = Mathf.Min(jumlahBintang + jumlah, ikonBintang.Length);
        Debug.Log("Bintang terkumpul: " + jumlahBintang);

        for (int i = 0; i < ikonBintang.Length; i++)
        {
            if (ikonBintang[i] != null)
                ikonBintang[i].SetActive(i < jumlahBintang);
        }
    }

    IEnumerator TundaDamage()
    {
        bisaKenaDamage = false;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 5; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        bisaKenaDamage = true;
    }

    void Mati()
    {
        Debug.Log("Player mati!");
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
        SceneManager.LoadScene("GameOver");
    }

    void Respawn()
    {
        Debug.Log("Respawn ke checkpoint.");
        transform.position = posisiRespawn;
        rb.linearVelocity = Vector2.zero;
    }

    void OnDrawGizmos()
    {
        if (deteksi_tanah != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(deteksi_tanah.position, jangkauan);
        }
    }
}