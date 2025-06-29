using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class kumpulansuara : MonoBehaviour
{
    public static kumpulansuara instance;

    public AudioClip[] Clip;

    public AudioSource source_sfx;
    public AudioSource source_bgn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Panggil_sfx(int id)
    {
        if (id >= 0 && id < Clip.Length && Clip[id] != null)
        {
            source_sfx.PlayOneShot(Clip[id]);
        }
        else
        {
            Debug.LogWarning("Clip SFX dengan ID " + id + " tidak tersedia atau null.");
        }
    }
}
