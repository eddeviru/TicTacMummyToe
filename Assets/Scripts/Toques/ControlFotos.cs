using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlFotos : MonoBehaviour
{
    private Controlador controlPasaje;
    public Sprite FotoSpr;
    public Image Miniatura;
    public GameObject Cortina;
    public int puntosPerFoto;
    public TextMeshProUGUI PuntosFaltantes;
    private Button BotonLocal;
    public string fraseFoto;
    public Vector2 medida;
    private GameObject FotoCarcaza;

    private void Awake()
    {
        controlPasaje = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        BotonLocal = GetComponent<Button>();
    }

    private void Start()
    {
        FotoCarcaza = GameObject.FindGameObjectWithTag("AlbumPhoto");

        PuntosFaltantes.text = puntosPerFoto.ToString();
        Miniatura.sprite = FotoSpr;
        if (controlPasaje.sunPointsMonth >= puntosPerFoto)
        {
            BotonLocal.enabled = true;
            Cortina.SetActive(false);
        }

        if (controlPasaje.sunPointsMonth <= puntosPerFoto)
        {
            BotonLocal.enabled = false;
        }
    }

    public void AbrirFoto()
    {
        FotoCarcaza.GetComponent<PhotoAlbum>().AbrirFoto(FotoSpr, fraseFoto, medida);
    }
}
