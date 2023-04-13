using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotonComprar : MonoBehaviour
{
    private Controlador pasajeControl;
    public Image Item;
    public TextMeshProUGUI description;
    public TextMeshProUGUI TextoBoton1;
    public GameObject Cross, Circle, Sacrificio, Forbidden;
    public TextMeshProUGUI CardPrice;
    public TextMeshProUGUI HowToUseT;
    public TextMeshProUGUI QuantityCardN;
    public TextMeshProUGUI ValorFinalN;
    public GameObject[] Spots;
    public GameObject[] fondoLnea;
    public GameObject Puntero;
    private Vector2 punteroPos;
    public Sprite punteroLevantado;
    public Sprite punteroTouch;
    public GameObject NCards;
    private int cantidadCards;
    public int valorCarta;
    private float timerAn;
    public int carta;
    public bool animante;
    private string NombreCartaOk;
    private bool punteroOn;
    private bool punteroMove;
    private bool punteroAccion;
    private bool consecuencia;
    private GameObject GOHelper;
    private GameObject GOHelper2;
    private GameObject[] GOHelperGen;
    private GameObject[] GOHelperGen2;
    public string txtAct;
    public string actualCurrency;

    public bool cardPanel;

    public GameObject FXCompra;

    private void Start()
    {
        punteroPos = new Vector2 (-0.9f, 4.3f);
        cardPanel = true;
        cantidadCards = 1;
        SetValueBtn();
        foreach(GameObject fnd in fondoLnea)
            fnd.GetComponent<Image>().CrossFadeAlpha(0, 0, false);
        pasajeControl = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        Puntero.GetComponent<Image>().CrossFadeAlpha(0, 0, false);

        if (carta == 0)
        {
            //Remover plataforma
            NombreCartaOk = " Off Platform ";
            for (int i = 0; i < Spots.Length; i++)
            {
                if(i == 1 || i == 5 || i == 3)
                {
                    GameObject fichaCircle = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCircle.transform.SetParent(Spots[i].transform);
                    fichaCircle.transform.localScale = new Vector3(1, 1, 1);
                }
                if (i == 0 || i == 2 || i == 4)
                {
                    GameObject fichaCross = (GameObject)Instantiate(Cross, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCross.transform.SetParent(Spots[i].transform);
                    fichaCross.transform.localScale = new Vector3(1, 1, 1);
                }
                if (i == 4)
                {
                    GameObject forbd = (GameObject)Instantiate(Forbidden, Spots[i].transform.position, Spots[i].transform.rotation);
                    forbd.transform.SetParent(Spots[i].transform);
                    forbd.transform.localScale = new Vector3(1, 1, 1);
                    forbd.GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    GOHelper = forbd;
                }
            }
            CardPrice.text = txtAct + pasajeControl.OffPanel;
        }
        if (carta == 1)
        {
            //Remover ficha
            NombreCartaOk = " Off mark ";
            for (int i = 0; i < Spots.Length; i++)
            {
                if (i == 1 || i == 4)
                {
                    GameObject fichaCircle = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCircle.transform.SetParent(Spots[i].transform);
                    fichaCircle.transform.localScale = new Vector3(1, 1, 1);
                }
                if (i == 2 || i == 6 || i == 8)
                {
                    GameObject fichaCross = (GameObject)Instantiate(Cross, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCross.transform.SetParent(Spots[i].transform);
                    fichaCross.transform.localScale = new Vector3(1, 1, 1);
                    if (i == 8)
                    {
                        GOHelper = fichaCross;
                    }
                }
            }
            CardPrice.text = txtAct + pasajeControl.OffFicha;
        }
        if (carta == 2)
        {
            //Cambia 1 ficha
            NombreCartaOk = " Switch mark ";
            for (int i = 0; i < Spots.Length; i++)
            {
                if (i == 1 || i == 4 || i == 5 || i == 2)
                {
                    GameObject fichaCircle = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCircle.transform.SetParent(Spots[i].transform);
                    fichaCircle.transform.localScale = new Vector3(1, 1, 1);
                    
                    if (i == 2)
                    {
                        GOHelper = fichaCircle;
                        GOHelper.GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                    
                }
                if (i == 3 || i == 2 || i == 7)
                {
                    GameObject fichaCross = (GameObject)Instantiate(Cross, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCross.transform.SetParent(Spots[i].transform);
                    fichaCross.transform.localScale = new Vector3(1, 1, 1);
                    if (i == 2)
                    {
                        GOHelper2 = fichaCross;
                        GOHelper2.GetComponent<Image>().CrossFadeAlpha(1, 0, false);
                    }
                }
            }
            CardPrice.text = txtAct + pasajeControl.Cambio1Ficha;
        }
        if (carta == 3)
        {
            //cambia mummy fichas
            NombreCartaOk = " Switch mummy marks ";
            GOHelperGen = new GameObject[3];
            GOHelperGen2 = new GameObject[3];
            for (int i = 0; i < Spots.Length; i++)
            {
                if (i == 4 || i == 7)
                {
                    GameObject fichaCircle = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCircle.transform.SetParent(Spots[i].transform);
                    fichaCircle.transform.localScale = new Vector3(1, 1, 1);
                }
                if (i == 0 || i == 2 || i == 3)
                {
                    GameObject fichaCross = (GameObject)Instantiate(Cross, Spots[i].transform.position, Spots[i].transform.rotation);
                    GameObject fichaCircle2 = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);

                    if (i == 0)
                    {
                        GOHelperGen[0] = fichaCross;
                        GOHelperGen[0].transform.SetParent(Spots[i].transform);
                        GOHelperGen[0].transform.localScale = new Vector3(1, 1, 1);

                        GOHelperGen2[0] = fichaCircle2;
                        GOHelperGen2[0].transform.SetParent(Spots[i].transform);
                        GOHelperGen2[0].transform.localScale = new Vector3(1, 1, 1);

                        GOHelperGen2[0].GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                    if (i == 2)
                    {
                        GOHelperGen[1] = fichaCross;
                        GOHelperGen[1].transform.SetParent(Spots[i].transform);
                        GOHelperGen[1].transform.localScale = new Vector3(1, 1, 1);
                        GOHelperGen2[1] = fichaCircle2;
                        GOHelperGen2[1].transform.SetParent(Spots[i].transform);
                        GOHelperGen2[1].transform.localScale = new Vector3(1, 1, 1);

                        GOHelperGen2[1].GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                    if (i == 3)
                    {
                        GOHelperGen[2] = fichaCross;
                        GOHelperGen[2].transform.SetParent(Spots[i].transform);
                        GOHelperGen[2].transform.localScale = new Vector3(1, 1, 1);
                        GOHelperGen2[2] = fichaCircle2;
                        GOHelperGen2[2].transform.SetParent(Spots[i].transform);
                        GOHelperGen2[2].transform.localScale = new Vector3(1, 1, 1);

                        GOHelperGen2[2].GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                }
            }
            CardPrice.text = txtAct + pasajeControl.CambioMummyFichas;
        }
        if (carta == 4)
        {
            //ficha fantasma
            NombreCartaOk = " Phantom mark ";
            Spots[9].SetActive(true);
            for (int i = 0; i < Spots.Length; i++)
            {
                if (i == 3 || i == 4 || i == 6 || i == 9)
                {
                    GameObject fichaCircle = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCircle.transform.SetParent(Spots[i].transform);
                    fichaCircle.transform.localScale = new Vector3(1, 1, 1);
                    if (i == 9)
                    {
                        GOHelper = fichaCircle;
                        GOHelper.GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                        GOHelper.transform.localPosition = new Vector3(-37.3f, 38.6f, 0);
                    }
                }
                if (i == 0 || i == 2 || i == 8)
                {
                    GameObject fichaCross = (GameObject)Instantiate(Cross, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCross.transform.SetParent(Spots[i].transform);
                    fichaCross.transform.localScale = new Vector3(1, 1, 1);
                    
                }
            }
            CardPrice.text = txtAct + pasajeControl.FichaFantasma;
        }
        if (carta == 5)
        {
            //Obstaculo + 1000
            NombreCartaOk = " Offering + 1000 Sun Points ";
            for (int i = 0; i < Spots.Length; i++)
            {
                if (i == 5)
                {
                    GameObject fichaCircle = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCircle.transform.SetParent(Spots[i].transform);
                    fichaCircle.transform.localScale = new Vector3(1, 1, 1);
                }
                if (i == 7 || i == 2)
                {
                    GameObject fichaCross = (GameObject)Instantiate(Cross, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCross.transform.SetParent(Spots[i].transform);
                    fichaCross.transform.localScale = new Vector3(1, 1, 1);
                }
                if (i == 4)
                {
                    GameObject sacr = (GameObject)Instantiate(Sacrificio, Spots[i].transform.position, Spots[i].transform.rotation);
                    sacr.transform.SetParent(Spots[i].transform);
                    sacr.transform.localScale = new Vector3(1, 1, 1);
                    GOHelper = sacr;
                    GOHelper.GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                }
            }
            CardPrice.text = txtAct + pasajeControl.ObstaculoP1000;
        }
        if (carta == 6)
        {
            //obstaculo + 2000
            NombreCartaOk = " Offering + 2000 Sun Points ";
            for (int i = 0; i < Spots.Length; i++)
            {
                if (i == 8)
                {
                    GameObject fichaCircle = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCircle.transform.SetParent(Spots[i].transform);
                    fichaCircle.transform.localScale = new Vector3(1, 1, 1);
                }
                if (i == 6 || i == 3)
                {
                    GameObject fichaCross = (GameObject)Instantiate(Cross, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCross.transform.SetParent(Spots[i].transform);
                    fichaCross.transform.localScale = new Vector3(1, 1, 1);
                }
                if (i == 0)
                {
                    GameObject sacr = (GameObject)Instantiate(Sacrificio, Spots[i].transform.position, Spots[i].transform.rotation);
                    sacr.transform.SetParent(Spots[i].transform);
                    sacr.transform.localScale = new Vector3(1, 1, 1);
                    GOHelper = sacr;
                    GOHelper.GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                }
            }
            CardPrice.text = txtAct + pasajeControl.ObstaculoP2000;
        }
        if (carta == 7)
        {
            //Cambiar todsa las fichas
            NombreCartaOk = " Switch all marks ";
            GOHelperGen = new GameObject[6];
            GOHelperGen2 = new GameObject[6];
            for (int i = 0; i < Spots.Length; i++)
            {
                if (i == 4 || i == 6 || i == 7)
                {
                    GameObject fichaCircle = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCircle.transform.SetParent(Spots[i].transform);
                    fichaCircle.transform.localScale = new Vector3(1, 1, 1);

                    GameObject fichaCross = (GameObject)Instantiate(Cross, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCross.transform.SetParent(Spots[i].transform);
                    fichaCross.transform.localScale = new Vector3(1, 1, 1);

                    if (i == 4)
                    {
                        GOHelperGen[0] = fichaCircle;
                        GOHelperGen2[0] = fichaCross;
                        GOHelperGen2[0].GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                    if (i == 6)
                    {
                        GOHelperGen[1] = fichaCircle;
                        GOHelperGen2[1] = fichaCross;
                        GOHelperGen2[1].GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                    if (i == 7)
                    {
                        GOHelperGen[2] = fichaCircle;
                        GOHelperGen2[2] = fichaCross;
                        GOHelperGen2[2].GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                }
                if (i == 0 || i == 2 || i == 8 )
                {
                    GameObject fichaCross = (GameObject)Instantiate(Cross, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCross.transform.SetParent(Spots[i].transform);
                    fichaCross.transform.localScale = new Vector3(1, 1, 1);

                    GameObject fichaCircle = (GameObject)Instantiate(Circle, Spots[i].transform.position, Spots[i].transform.rotation);
                    fichaCircle.transform.SetParent(Spots[i].transform);
                    fichaCircle.transform.localScale = new Vector3(1, 1, 1);

                    if (i == 0)
                    {
                        GOHelperGen2[3] = fichaCircle;
                        GOHelperGen[3] = fichaCross;
                        GOHelperGen[3].GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                    if (i == 2)
                    {
                        GOHelperGen2[4] = fichaCircle;
                        GOHelperGen[4] = fichaCross;
                        GOHelperGen[4].GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                    if (i == 8)
                    {
                        GOHelperGen2[5] = fichaCircle;
                        GOHelperGen[5] = fichaCross;
                        GOHelperGen[5].GetComponent<Image>().CrossFadeAlpha(0, 0, false);
                    }
                }
            }
            CardPrice.text = txtAct + pasajeControl.IntercambioAllFichas;
        }
    }

    private void Update()
    {
        if (gameObject.transform.localPosition.x == 0 && cardPanel)
        {
            animante = true;
        }

        if (gameObject.transform.localPosition.x != 0 || !cardPanel)
        {
            animante = false;
            AlphSprite(0, Puntero);
            iTween.Stop(Puntero);
        }

        if (animante)
        {
            if (carta == 0)
            {
                //Remover plataforma
                
                timerAn += Time.deltaTime;
                if (timerAn > 0.3f && !punteroOn)
                {
                    AlphSprite(1, Puntero);
                    punteroOn = true;
                }
                if (timerAn > 0.2f && !punteroMove)
                {
                    punteroMovimiento(Spots[4].transform.position.x, Spots[4].transform.position.y);
                    punteroMove = true;
                }
                if (timerAn > 1.5f && !punteroAccion)
                {
                    punteroAct(punteroTouch);
                    AlphSprite(0, Puntero);
                    AlphSprite(0, Spots[4]);
                    AlphSprite(0, Spots[4].GetComponentInChildren<Image>().gameObject);
                    AlphSprite(1, GOHelper);
                    punteroAccion = true;
                }
                if (timerAn > 2.5f && !consecuencia)
                {
                    punteroMovimiento(punteroPos.x, punteroPos.y);
                    consecuencia = true;
                }
                if (timerAn > 3f)
                {
                    punteroAct(punteroLevantado);
                    if (timerAn > 4.5f)
                    {
                        consecuencia = punteroAccion = punteroMove = punteroOn = false;
                        AlphSprite(1, Spots[4]);
                        AlphSprite(0, GOHelper);
                        AlphSprite(1, Puntero);
                        timerAn = 0;
                    }
                }
            }
            if (carta == 1)
            {
                //Remover ficha
                timerAn += Time.deltaTime;
                if (timerAn > 0.3f && !punteroOn)
                {
                    AlphSprite(1, Puntero);
                    punteroOn = true;
                }
                if (timerAn > 0.2f && !punteroMove)
                {
                    punteroMovimiento(Spots[8].transform.position.x, Spots[8].transform.position.y);
                    punteroMove = true;
                }
                if (timerAn > 1.5f && !punteroAccion)
                {
                    punteroAct(punteroTouch);
                    AlphSprite(0, Puntero);
                    AlphSprite(0, GOHelper);
                    punteroAccion = true;
                }
                if (timerAn > 2.5f && !consecuencia)
                {
                    punteroMovimiento(punteroPos.x, punteroPos.y);
                    consecuencia = true;
                }
                if (timerAn > 3f)
                {
                    punteroAct(punteroLevantado);
                    AlphSprite(1, GOHelper);
                    if (timerAn > 4f)
                    {
                        consecuencia = punteroAccion = punteroMove = punteroOn = false;
                        AlphSprite(1, Puntero);
                        timerAn = 0;
                    }
                }
            }
            if (carta == 2)
            {
                //Cambia 1 ficha
                timerAn += Time.deltaTime;
                if (timerAn > 0.3f && !punteroOn)
                {
                    AlphSprite(1, Puntero);
                    punteroOn = true;
                }
                if (timerAn > 0.2f && !punteroMove)
                {
                    punteroMovimiento(Spots[2].transform.position.x, Spots[2].transform.position.y);
                    punteroMove = true;
                }
                if (timerAn > 1.5f && !punteroAccion)
                {
                    punteroAct(punteroTouch);
                    AlphSprite(0, Puntero);
                    AlphSprite(1, GOHelper);
                    AlphSprite(0, GOHelper2);
                    punteroAccion = true;
                }
                if (timerAn > 2.5f && !consecuencia)
                {
                    punteroMovimiento(punteroPos.x, punteroPos.y);
                    consecuencia = true;
                }
                if (timerAn > 3f)
                {
                    punteroAct(punteroLevantado);
                    AlphSprite(0, GOHelper);
                    AlphSprite(1, GOHelper2);
                    if (timerAn > 4f)
                    {
                        consecuencia = punteroAccion = punteroMove = punteroOn = false;
                        AlphSprite(1, Puntero);
                        timerAn = 0;
                    }
                }
            }
            if (carta == 3)
            {
                //cambia mummy fichas
                timerAn += Time.deltaTime;
                if (timerAn > 0.3f && !punteroOn)
                {
                    punteroOn = true;
                }
                if (timerAn > 0.2f && !punteroMove)
                {
                    for (int i = 0; i < GOHelperGen.Length; i++)
                    {
                        AlphSprite(0, GOHelperGen[i]);
                        AlphSprite(1, GOHelperGen2[i]);
                    }
                    punteroMove = true;
                }
                if (timerAn > 1.5f && !punteroAccion)
                {
                    for (int i = 0; i < GOHelperGen2.Length; i++)
                    {
                        AlphSprite(0, GOHelperGen2[i]);
                        AlphSprite(1, GOHelperGen[i]);
                    }
                    punteroAccion = true;
                }
                if (timerAn > 2f && !consecuencia)
                {
                    consecuencia = true;
                }
                if (timerAn > 2.5f)
                {

                    if (timerAn > 3f)
                    {
                        consecuencia = punteroAccion = punteroMove = punteroOn = false;
                        timerAn = 0;
                    }
                }
            }
            if (carta == 4)
            {
                //ficha fantasma
                timerAn += Time.deltaTime;
                if (timerAn > 0.3f && !punteroOn)
                {
                    AlphSprite(1, Puntero);
                    punteroOn = true;
                }
                if (timerAn > 0.2f && !punteroMove)
                {
                    punteroMovimiento(Spots[9].transform.position.x, Spots[9].transform.position.y);
                    punteroMove = true;
                }
                if (timerAn > 1.5f && !punteroAccion)
                {
                    punteroAct(punteroTouch);
                    AlphSprite(0, Puntero);
                    AlphSprite(1, GOHelper);
                    AlphSprite(1, fondoLnea[0]);
                    AlphSprite(1, fondoLnea[1]);
                    punteroAccion = true;
                }
                if (timerAn > 2.5f && !consecuencia)
                {
                    punteroMovimiento(punteroPos.x, punteroPos.y);
                    consecuencia = true;
                }
                if (timerAn > 3f)
                {
                    punteroAct(punteroLevantado);
                    AlphSprite(0, GOHelper);
                    AlphSprite(0, fondoLnea[0]);
                    AlphSprite(0, fondoLnea[1]);
                    if (timerAn > 4.5f)
                    {
                        consecuencia = punteroAccion = punteroMove = punteroOn = false;
                        AlphSprite(1, Puntero);
                        timerAn = 0;
                    }
                }
            }
            if (carta == 5)
            {
                //Obstaculo + 1000
                timerAn += Time.deltaTime;
                if (timerAn > 0.3f && !punteroOn)
                {
                    AlphSprite(1, Puntero);
                    punteroOn = true;
                }
                if (timerAn > 0.2f && !punteroMove)
                {
                    punteroMovimiento(Spots[4].transform.position.x, Spots[4].transform.position.y);
                    punteroMove = true;
                }
                if (timerAn > 1.5f && !punteroAccion)
                {
                    punteroAct(punteroTouch);
                    AlphSprite(0, Puntero);
                    AlphSprite(1, GOHelper);
                    punteroAccion = true;
                }
                if (timerAn > 2.5f && !consecuencia)
                {
                    punteroMovimiento(punteroPos.x, punteroPos.y);
                    consecuencia = true;
                }
                if (timerAn > 3f)
                {
                    punteroAct(punteroLevantado);
                    AlphSprite(0, GOHelper);
                    if (timerAn > 4.5f)
                    {
                        consecuencia = punteroAccion = punteroMove = punteroOn = false;
                        AlphSprite(1, Puntero);
                        timerAn = 0;
                    }
                }
            }
            if (carta == 6)
            {
                //obstaculo + 2000
                timerAn += Time.deltaTime;
                if (timerAn > 0.3f && !punteroOn)
                {
                    AlphSprite(1, Puntero);
                    punteroOn = true;
                }
                if (timerAn > 0.2f && !punteroMove)
                {
                    punteroMovimiento(Spots[0].transform.position.x, Spots[0].transform.position.y);
                    punteroMove = true;
                }
                if (timerAn > 1.5f && !punteroAccion)
                {
                    punteroAct(punteroTouch);
                    AlphSprite(0, Puntero);
                    AlphSprite(1, GOHelper);
                    punteroAccion = true;
                }
                if (timerAn > 2.5f && !consecuencia)
                {
                    punteroMovimiento(punteroPos.x, punteroPos.y);
                    consecuencia = true;
                }
                if (timerAn > 3f)
                {
                    punteroAct(punteroLevantado);
                    AlphSprite(0, GOHelper);
                    if (timerAn > 4.5f)
                    {
                        consecuencia = punteroAccion = punteroMove = punteroOn = false;
                        AlphSprite(1, Puntero);
                        timerAn = 0;
                    }
                }
            }
            if (carta == 7)
            {
                //Intercambia todas las fichas
                timerAn += Time.deltaTime;
                timerAn += Time.deltaTime;
                if (timerAn > 0.3f && !punteroOn)
                {
                    punteroOn = true;
                }
                if (timerAn > 0.2f && !punteroMove)
                {
                    for (int i = 0; i < GOHelperGen.Length; i++)
                    {
                        AlphSprite(0, GOHelperGen[i]);
                        AlphSprite(1, GOHelperGen2[i]);
                    }
                    punteroMove = true;
                }
                if (timerAn > 1.5f && !punteroAccion)
                {
                    punteroAccion = true;
                }
                if (timerAn > 3f && !consecuencia)
                {
                    for (int i = 0; i < GOHelperGen.Length; i++)
                    {
                        AlphSprite(0, GOHelperGen2[i]);
                        AlphSprite(1, GOHelperGen[i]);
                    }
                    consecuencia = true;
                }
                if (timerAn > 4.5f)
                {
                    if (timerAn > 5f)
                    {
                        consecuencia = punteroAccion = punteroMove = punteroOn = false;
                        timerAn = 0;
                    }
                }
            }
        }
        
    }

    public void SetCompra()
    {
        if (pasajeControl.goldenBugs >= cantidadCards * valorCarta)
        {
            float percentGana = Random.Range(0, 100);
            int CantGanaGoldenBugs = Random.Range(4, 15);
            FXCompra.GetComponent<ParticleSystem>().Play();
            ShakeCamera(0.2f, 1f);

            if (percentGana < 30)
            {
                pasajeControl.goldenBugs = pasajeControl.goldenBugs - (cantidadCards * valorCarta) + CantGanaGoldenBugs;
                SetRecompensa(CantGanaGoldenBugs, cantidadCards, NombreCartaOk, Item.sprite);
            }
            else
            {
                pasajeControl.goldenBugs = pasajeControl.goldenBugs - cantidadCards * valorCarta;
                SetRecompensa(0, cantidadCards, NombreCartaOk, Item.sprite);
            }

            if (carta == 0)
            {
                //Remover plataforma
                pasajeControl.OffPanel = pasajeControl.OffPanel + cantidadCards;

                CardPrice.text = txtAct + pasajeControl.OffPanel;
            }
            if (carta == 1)
            {
                //Remover ficha
                pasajeControl.OffFicha = pasajeControl.OffFicha + cantidadCards;

                CardPrice.text = txtAct + pasajeControl.OffFicha;
            }
            if (carta == 2)
            {
                //Cambia 1 ficha
                pasajeControl.Cambio1Ficha = pasajeControl.Cambio1Ficha + cantidadCards;

                CardPrice.text = txtAct + pasajeControl.Cambio1Ficha;
            }
            if (carta == 3)
            {
                //cambia mummy fichas
                pasajeControl.CambioMummyFichas = pasajeControl.CambioMummyFichas + cantidadCards;

                CardPrice.text = txtAct + pasajeControl.CambioMummyFichas;
            }
            if (carta == 4)
            {
                //ficha fantasma
                pasajeControl.FichaFantasma = pasajeControl.FichaFantasma + cantidadCards;

                CardPrice.text = txtAct + pasajeControl.FichaFantasma;
            }
            if (carta == 5)
            {
                //Obstaculo + 1000
                pasajeControl.ObstaculoP1000 = pasajeControl.ObstaculoP1000 + cantidadCards;

                CardPrice.text = txtAct + pasajeControl.ObstaculoP1000;
            }
            if (carta == 6)
            {
                //obstaculo + 2000
                pasajeControl.ObstaculoP2000 = pasajeControl.ObstaculoP2000 + cantidadCards;

                CardPrice.text = txtAct + pasajeControl.ObstaculoP2000;
            }
            if (carta == 7)
            {
                //Cambiar todsa las fichas
                pasajeControl.IntercambioAllFichas = pasajeControl.IntercambioAllFichas + cantidadCards;

                CardPrice.text = txtAct + pasajeControl.IntercambioAllFichas;
            }

            pasajeControl.SaveDataS();

            cantidadCards = 1;
            SetValueBtn();
        }
    }

    public void ShakeCamera(float terremoto, float timeP)
    {
        GameObject cameraM = GameObject.FindGameObjectWithTag("MainCamera");
        iTween.ShakePosition(cameraM, iTween.Hash("x", terremoto, "time", timeP));
    }

    private void AlphSprite(float alph, GameObject vict)
    {
        vict.GetComponent<Image>().CrossFadeAlpha(alph, 0.5f, false);
    }

    private void punteroMovimiento(float xCord, float yCord)
    {
        iTween.MoveTo(Puntero, iTween.Hash("x", xCord, "y", yCord, "time", 0.8f, "islocal", false));
    }

    private void punteroAct(Sprite actualPuntero)
    {
        Puntero.GetComponent<Image>().sprite = actualPuntero;
    }

    private void SetRecompensa(int regalo, int cartasCantidad, string nombreCarta, Sprite cartaIc)
    {
        NCards.SetActive(true);

        NCards.GetComponent<Returno>().Comenzar();
        if (regalo < 2)
        {
            //no regalo
            NCards.GetComponent<NuevasCards>().SetTextos("You buy " + cartasCantidad.ToString() + nombreCarta + "cards.", pasajeControl.goldenBugs.ToString(), cartaIc);
        }
        if (regalo > 0)
        {
            //regalo ok
            NCards.GetComponent<NuevasCards>().SetTextos("You buy " + cartasCantidad.ToString() + nombreCarta + "cards" + " and recieve " + regalo + " Golden Beetles as a gift.", pasajeControl.goldenBugs.ToString(), cartaIc);
        }
        
    }

    public void sumaItem()
    {
        if (cantidadCards * valorCarta < pasajeControl.goldenBugs - valorCarta)
        {
            cantidadCards++;
            SetValueBtn();
        }
    }

    public void restaItem()
    {
        if (cantidadCards > 1)
        {
            cantidadCards--;
            SetValueBtn();
        }
    }

    private void SetValueBtn()
    {
        QuantityCardN.text = cantidadCards.ToString();
        ValorFinalN.text = "Buy " + cantidadCards + " cards for " + cantidadCards * valorCarta + " \n " + actualCurrency;
    }

}
