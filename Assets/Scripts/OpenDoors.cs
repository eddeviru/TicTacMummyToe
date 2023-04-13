using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public GameObject PuertaDer, PuertaIzq, PuertaDerSh, PuertaIzqSh;
    public GameObject MarcoPuertas;
    public GameObject PuertaContenedor;
    private Controlador control;
    private GameObject camC;
    //Audios
    private AudioSource PuertaSource;

    private void Awake()
    {
        control = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        camC = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Start()
    {
        SetPos();
        PuertaSource = MarcoPuertas.GetComponent<AudioSource>();
        Invoke("AbrirPuertas", 0.8f);
    }

    private void SetPos()
    {
        PuertaContenedor.transform.position = control.MarcoPos;
        camC.transform.position = control.camPos;

        PuertaContenedor.transform.eulerAngles = control.MarcoRot;
        camC.transform.eulerAngles = control.camRot;
    }

    public void CerrarPuerta()
    {
        iTween.MoveTo(MarcoPuertas, iTween.Hash("y", 0, "time", 0.3f, "islocal", true));
        Puertas(PuertaDer, 0f, PuertaDerSh);
        Puertas(PuertaIzq, 0f, PuertaIzqSh);
        PuertaSource.Play();
    }

    public void AbrirPuertas()
    {
        Puertas(PuertaDer, -2.1f, PuertaDerSh);
        Puertas(PuertaIzq, -2.09f, PuertaIzqSh);
        PuertaSource.Play();
        Invoke("MoverMarco", 1.5f);
    }

    private void Puertas(GameObject Puerta, float sitio, GameObject PuertaShake)
    {
        iTween.MoveTo(Puerta, iTween.Hash("x", sitio, "time", 2, "islocal", true));
        iTween.ShakeScale(PuertaShake, iTween.Hash("z", 0.01f, "time", 2));
    }

    private void MoverMarco()
    {
        iTween.MoveTo(MarcoPuertas, iTween.Hash("y", MarcoPuertas.transform.localPosition.z - 2f, "time", 2, "islocal", true));
    }
}
