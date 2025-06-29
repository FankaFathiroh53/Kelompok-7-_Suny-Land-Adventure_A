using UnityEngine;

public class musuh1 : MonoBehaviour
{
    public int tambahanNyawa = 1;

    [Header("Patrol Settings")]
    public float kecepatan = 2f;
    public Transform BatasKiri_snail;
    public Transform BatasKanan_snail;

    private Transform target;
    private SpriteRenderer sr;

    void Start()
{
    sr = GetComponent<SpriteRenderer>();

    // Auto perbaiki kalau batas kiri dan kanan kebalik
    if (BatasKiri_snail.position.x > BatasKanan_snail.position.x)
    {
        Transform temp = BatasKiri_snail;
        BatasKiri_snail = BatasKanan_snail;
        BatasKanan_snail = temp;
    }

    // 👉 Awalnya ke kanan dulu
    target = BatasKanan_snail;

    UpdateArahVisual();
}

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Gerak ke target X (tinggi tetap)
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(target.position.x, transform.position.y, transform.position.z),
            kecepatan * Time.deltaTime
        );

        // Cek kalau udah sampai target (kiri/kanan)
        if (Mathf.Abs(transform.position.x - target.position.x) < 0.05f)
        {
            // Ganti target
            target = (target == BatasKanan_snail) ? BatasKiri_snail : BatasKanan_snail;
            UpdateArahVisual();
        }
    }

    void UpdateArahVisual()
    {
        Vector3 scale = transform.localScale;

        // Misalnya sprite default-nya menghadap KANAN
        if (target.position.x > transform.position.x)
            scale.x = Mathf.Abs(scale.x); // ke kanan
        else
            scale.x = -Mathf.Abs(scale.x); // ke kiri

        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player pemain = collision.GetComponent<player>();
            if (pemain != null)
            {
                pemain.TambahNyawa(tambahanNyawa);
            }

            Destroy(gameObject);
        }
    }
void OnDrawGizmosSelected()
{
    if (BatasKiri_snail != null && BatasKanan_snail != null)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(BatasKiri_snail.position, BatasKanan_snail.position);
    }
}

}
