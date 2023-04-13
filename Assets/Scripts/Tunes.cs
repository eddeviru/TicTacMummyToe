using UnityEngine;
using UnityEngine.Audio;

public class Tunes : MonoBehaviour
{
    public AudioClip[] songs;
    private AudioSource bafle;

    public AudioMixer MasterdSonido;
    private bool pasod;
    private float sonOff;
    public GameObject volD;

    private void Start()
    {
        bafle = GetComponent<AudioSource>();
        volD.transform.position = new Vector3(0, 1f, 0);
        Invoke("tocaSonata", 0.7f);
    }

    private void tocaSonata()
    {
        int oportunidades = Random.Range(0, songs.Length);
        bafle.clip = songs[oportunidades];
        bafle.Play();
    }

    public void StopSong()
    {
        iTween.MoveTo(volD, iTween.Hash("y", 0f, "time", 0.7f));
        pasod = true;
    }

    private void Update()
    {
        if (pasod)
        {
            bafle.volume = volD.transform.position.y;
        }
    }
}
