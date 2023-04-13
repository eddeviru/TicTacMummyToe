using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ControladorAereo : MonoBehaviour
{
    private Logica Logic;
    public GameObject BotonCarta;
    public Transform referencia;
    private Controlador ControlCards;
    public int[] Cards = new int[8];
    private int fila;
    public GameObject MenuBotones;
    public GameObject Obstaculo;
    public GameObject PantallaOsc;
    public GameObject Borde;
    public GameObject PanelJuego;
    private bool CambioPantalla;
    private float timerPantalla;
    private GameObject objADes, objAAct;
    public AudioClip[] botonClick;
    private AudioSource BafleSource;
    public Botones[] botonesDisp;

    private void Awake()
    {
        ControlCards = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        botonesDisp = new Botones[Cards.Length];
        setCards();
    }

    private void Start()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            GameObject carta = (GameObject)Instantiate(BotonCarta, referencia.transform.position, referencia.transform.rotation);
            botonesDisp[i] = carta.GetComponent<Botones>();
            botonesDisp[i].cartas = new bool[8];
            botonesDisp[i].idCard = i;
            carta.transform.SetParent(referencia.transform);
            carta.transform.localPosition = new Vector3(0, fila * 265f, 0);
            carta.transform.localScale = new Vector3(1, 1, 1);
            botonesDisp[i].cartasInv = Cards[i];
            botonesDisp[i].cartas[i] = true;
            botonesDisp[i].disponible = true;
            botonesDisp[i].Boton.SetActive(true);
            botonesDisp[i].SetTextos();
            fila++;
        }
        BafleSource = GetComponent<AudioSource>();
        Logic = GameObject.FindGameObjectWithTag("Logica").GetComponent<Logica>();
        SetGame(true);
    }


    private void Update()
    {
        if (CambioPantalla)
        {
            timerPantalla += Time.deltaTime;
            if (timerPantalla < 1f)
            {
                objAAct.SetActive(true);

            }
            if (timerPantalla > 0.7f)
            {
                pantAction();
            }
        }
    }

    private void setCards()
    {
        Cards[0] = ControlCards.IntercambioAllFichas;
        Cards[1] = ControlCards.ObstaculoP1000;
        Cards[2] = ControlCards.ObstaculoP2000;
        Cards[3] = ControlCards.FichaFantasma;
        Cards[4] = ControlCards.CambioMummyFichas;
        Cards[5] = ControlCards.Cambio1Ficha;
        Cards[6] = ControlCards.OffFicha;
        Cards[7] = ControlCards.OffPanel;
    }

    public void SetMenu()
    {
        SonidoClick();
        Logic.menuFicha = true;
        Obstaculo.SetActive(true);
        PantallaOsc.SetActive(true);
        Borde.SetActive(false);
        objAAct = MenuBotones;
        objADes = PanelJuego;
        CambioPantalla = true;

        FadingAssets(1, MenuBotones, 0);
        FadingAssets(0, PanelJuego, 1);
        Logic.removePlace = Logic.retrocesoMark = Logic.changeMark = Logic.changeAllMarks = Logic.ghostMark = Logic.offerx1000 = Logic.offerx2000 = Logic.intAllMarks = Logic.normalTurn = false;

        Logic.SearchPhantomBases(Logic.OffPhantomM, false);
        Invoke("CheckNewCards", 0.1f);
    }

    public void CheckNewCards()
    {
        setCards();

        for (int i = 0; i < botonesDisp.Length; i++)
        {
            if (Cards[i] <= 0)
            {
                botonesDisp[i].Contenedor.SetActive(false);
                botonesDisp[i].gameObject.transform.localScale = new Vector3(0, 0, 0);
            }
            if (Cards[i] >= 1)
            {
                botonesDisp[i].Boton.SetActive(true);
                botonesDisp[i].Contenedor.SetActive(true);
                botonesDisp[i].gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        StartCoroutine(CheckSpaces(botonesDisp[0].gameObject));
        checkcards();
    }


    private void checkcards()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            if (botonesDisp[i] != null)
            {
                if (botonesDisp[i].disponible)
                {
                    botonesDisp[i].checkeo = true;
                    botonesDisp[i].tamanoCard = -10;
                }
            }
        }
    }

    private IEnumerator CheckSpaces(GameObject vic)
    {
        vic.GetComponentInParent<VerticalLayoutGroup>().spacing = 10.5f;

        yield return new WaitForSeconds(0.1f);

        vic.GetComponentInParent<VerticalLayoutGroup>().spacing = 10f;
    }

    public void SetGame(bool normalT)
    {
        SonidoClick();
        Logic.menuFicha = false;
        FadingAssets(0, MenuBotones, 1);
        FadingAssets(1, PanelJuego, 0);
        objAAct = PanelJuego;
        objADes = MenuBotones;
        PantallaOsc.SetActive(false);
        Borde.SetActive(false);
        if (normalT)
        {
            Logic.normalTurn = true;
            Logic.removePlace = Logic.retrocesoMark = Logic.changeMark = Logic.changeAllMarks = Logic.ghostMark = Logic.offerx1000 = Logic.offerx2000 = Logic.intAllMarks = false;
            Logic.SearchPhantomBases(Logic.OffPhantomM, false);
        }
        if (!normalT)
        {
            Logic.normalTurn = false;
        }
        CambioPantalla = true;
    }

    private void pantAction()
    {
        objADes.SetActive(false);
        timerPantalla = 0;
        CambioPantalla = false;
    }

    private void FadingAssets(float aloha, GameObject victima, float NegAloha)
    {
        TextMeshProUGUI[] Txts = victima.GetComponentsInChildren<TextMeshProUGUI>();
        Image[] Imgs = victima.GetComponentsInChildren<Image>();
        for (int i = 0; i < Txts.Length; i++)
        {
            Txts[i].CrossFadeAlpha(NegAloha, 0, false);
        }

        for (int i = 0; i < Imgs.Length; i++)
        {
            Imgs[i].CrossFadeAlpha(NegAloha, 0, false);
        }

        float dur = 0.7f;

        for (int i = 0; i < Txts.Length; i++)
        {
            Txts[i].CrossFadeAlpha(aloha, dur, false);
        }

        for (int i = 0; i < Imgs.Length; i++)
        {
            Imgs[i].CrossFadeAlpha(aloha, dur, false);
        }
    }

    public void SonidoClick()
    {
        int elex = Random.Range(0, botonClick.Length);

        BafleSource.PlayOneShot(botonClick[elex]);
    }
}
