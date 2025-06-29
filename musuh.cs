using UnityEngine;
using System.Collections;

public class musuh : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] points;
    private int currentPoint = 0;

    private bool stopMovement = false;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (stopMovement || points.Length == 0) return;

        Transform target = points[currentPoint];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Flip arah jika perlu
        if (transform.position.x < target.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        // Ganti target jika sampai
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (anim != null)
                anim.SetBool("plant", true);

            stopMovement = true;

            // Kurangi nyawa player
            player playerScript = other.GetComponent<player>();
            if (playerScript != null)
            {
                playerScript.KurangiNyawa(1);
            }

            // Mulai delay 3 detik lalu lanjut jalan lagi
            StartCoroutine(JedaDanLanjut());
        }
    }

    IEnumerator JedaDanLanjut()
    {
        yield return new WaitForSeconds(3f); // jeda 3 detik
        stopMovement = false;

        if (anim != null)
            anim.SetBool("plant", false);
    }
}