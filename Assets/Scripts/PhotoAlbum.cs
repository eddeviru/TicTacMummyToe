using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoAlbum : MonoBehaviour
{
    public GameObject photo;
    public GameObject Foto;
    public Sprite[] FotosAventure;
    public int[] precio;
    public string[] fraseFoto;
    public Vector2[] medidasFrase;
    public RectTransform cascoTexto;
    public TextMeshProUGUI historia;
    public GameObject PolaroidGO;
    public Transform referencia;
    public TextMeshProUGUI sunPoints;
    private Returno rt;

    private void Awake()
    {
        precio = new int[GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>().Foto.Length];

        for (int i = 0; i < precio.Length; i++)
        {
            precio[i] = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>().Foto[i];
        }

        rt = GetComponent<Returno>();
        rt.FadeScreen(0, 0);
        checkFoto(0, 0);
        photo.SetActive(false);
        gameObject.SetActive(false);
        OrganizarFotos();
        SetTextos();
    }
    public void SetTextos()
    {
        sunPoints.text = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>().sunPointsMonth.ToString();
    }
    private void OrganizarFotos()
    {
        
        for (int i = 0; i < FotosAventure.Length; i++)
        {
            GameObject caja = (GameObject)Instantiate(PolaroidGO, referencia.transform.position, referencia.transform.rotation);
            caja.transform.SetParent(referencia);
            
            caja.transform.localScale = new Vector3(1, 1, 1);
            caja.GetComponent<ControlFotos>().puntosPerFoto = precio[i];
            caja.GetComponent<ControlFotos>().FotoSpr = FotosAventure[i];
            caja.GetComponent<ControlFotos>().fraseFoto = fraseFoto[i];
            caja.GetComponent<ControlFotos>().medida = medidasFrase[i];
        }
    }
    private void checkFoto(float alphaA, float tiempo)
    {
        Image[] asse = photo.GetComponentsInChildren<Image>();

        foreach (Image ase in asse)
            ase.CrossFadeAlpha(alphaA, tiempo, false);
    }
    public void cerrarFoto()
    {
        checkFoto(0, 0.8f);
        Invoke("destroirFoto", 0.8f);
    }
    public void AbrirFoto(Sprite fotoreal, string history, Vector2 medidas)
    {
        //Debug.Log("llamada foto");
        photo.SetActive(true);
        photo.transform.localScale = new Vector3(1, 1, 1);
        historia.text = history;
        cascoTexto.sizeDelta = medidas;
        Foto.GetComponent<Image>().sprite = fotoreal;
        checkFoto(1, 0.8f);
    }
    public void destroirFoto()
    {
        photo.SetActive(false);
    }
}
