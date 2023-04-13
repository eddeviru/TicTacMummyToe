using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class IniThing : MonoBehaviour
{
    private Controlador Control;
    public GameObject[] Botones;
    public Vector3[] PosFin;
    private Vector3[] PosIni;
    public GameObject[] Screens;
    public GameObject FondoNegro;
    public GameObject Borde;
    public AudioClip[] botonClick;
    private AudioSource BafleSource;
    public float volumeM;
    public string[] bonusScene;
    public TextMeshProUGUI textVers;

    private void Awake()
    {
        // pos cam frente

        StartIssues();

        Control = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        textVers.text = Control.versioTxt;
        PosIni = new Vector3[Botones.Length];

        for (int i = 0; i < Botones.Length; i++)
        {
            PosIni[i] = Botones[i].transform.localPosition;

            Botones[i].transform.localPosition = new Vector3(PosIni[i].x + PosFin[i].x, PosIni[i].y + PosFin[i].y, PosIni[i].z + PosFin[i].z);
        }

        Botones[0].GetComponentInParent<HorizontalLayoutGroup>().enabled = true;
    }

    private void Start()
    {
        int pardJ = Random.Range (3, 4);
        if (Control.PartidasJugadas >= pardJ)
        {
            SetSzeBonsBtn(1);
        }
        if (Control.PartidasJugadas < pardJ)
        {
            SetSzeBonsBtn(0);
        }

        cargarData();
        BafleSource = GetComponent<AudioSource>();
        Image[] images = FondoNegro.GetComponentsInChildren<Image>();
        foreach (Image imgs in images)
            imgs.CrossFadeAlpha(0, 0f, false);

        FondoNegro.SetActive(false);
        Borde.SetActive(false);
        volverIni();
        textVers.text = Control.versioTxt;
    }

    public void SetSzeBonsBtn(float sze)
    {
        Botones[7].transform.localScale = new Vector3(sze, sze, sze);
    }

    private void StartIssues()
    {
        List<Image> imagenes = new List<Image>();
        List<TextMeshProUGUI> textos = new List<TextMeshProUGUI>();

        for (int i = 0; i < Screens.Length; i++)
        {
            Image[] imgScr = Screens[i].GetComponentsInChildren<Image>();
            TextMeshProUGUI[] txtScr = Screens[i].GetComponentsInChildren<TextMeshProUGUI>();
            foreach (Image iSc in imgScr)
                imagenes.Add(iSc);

            foreach (TextMeshProUGUI tSc in txtScr)
                textos.Add(tSc);
        }

        foreach (Image imgF in imagenes)
            imgF.CrossFadeAlpha(0, 0, false);

        foreach (TextMeshProUGUI txtF in textos)
            txtF.CrossFadeAlpha(0, 0, false);

    }

    public void ShowLeaderBoard()
    {
        BotonesOff(Botones, PosFin);
        SonidoClick();
        Screens[0].SetActive(true);
        Screens[0].transform.localScale = new Vector3(1,1,1);
        Screens[0].GetComponent<Returno>().Comenzar();
        //Screens[0].GetComponent<LeaderBoard>().setDataLeaderBoard();
        NegroCortinaEntrada();
    }

    public void ShowSelfieAlbum()
    {
        BotonesOff(Botones, PosFin);
        SonidoClick();
        Screens[2].SetActive(true);
        Screens[2].transform.localScale = new Vector3(1, 1, 1);
        Screens[2].GetComponent<Returno>().Comenzar();
        NegroCortinaEntrada();
    }

    public void PlayGame()
    {
        BotonesOff(Botones, PosFin);
        SonidoClick();
        cargarData();
        Control.SetPoston();

        Invoke("LoadGame", 0.8f);
        GameObject.FindGameObjectWithTag("Bafle").GetComponent<Tunes>().StopSong();
    }

    public void OpenShop()
    {
        BotonesOff(Botones, PosFin);
        SonidoClick();
        NegroCortinaEntrada();
        Screens[1].SetActive(true);
        Screens[1].transform.localScale = new Vector3(1, 1, 1);
        Screens[1].GetComponent<Returno>().Comenzar();
    }

    public void OpenConf()
    {
        BotonesOff(Botones, PosFin);
        SonidoClick();
        NegroCortinaEntrada();
        Screens[3].SetActive(true);
        Screens[3].transform.localScale = new Vector3(1, 1, 1);
        Screens[3].GetComponent<Conf>().OnVolumeChange(Control.volumeSoundInGame);
        Screens[3].GetComponent<Returno>().Comenzar();
    }

    public void OpenDeleteAccount()
    {
        SonidoClick();
        NegroCortinaEntrada();
        Screens[6].SetActive(true);
        Screens[6].transform.localScale = new Vector3(1, 1, 1);
        Screens[6].GetComponent<Returno>().Comenzar();
    }

    private void BotonesOff(GameObject[] BotonV, Vector3[] newPos)
    {
        for (int i = 0; i < BotonV.Length; i++)
        {
            TweenGameObject(BotonV[i], newPos[i].x, newPos[i].y);
        }
    }

    public void volverIni()
    {
        BotonesOff(Botones, PosIni);
        SonidoClick();
        NegroCortinaSalida();
        foreach (GameObject scn in Screens)
            scn.GetComponent<Returno>().Retorno();
    }

    public void volverShop()
    {
        SonidoClick();
        Screens[4].GetComponent<Returno>().Retorno();
        Screens[5].GetComponent<Returno>().Retorno();
    }

    public void volverConf()
    {
        SonidoClick();
        Screens[6].GetComponent<Returno>().Retorno();
    }

    public void BonusRound()
    {
        BotonesOff(Botones, PosFin);
        SonidoClick();
        cargarData();
        Control.SetPoston();

        Invoke("BonusScene", 0.8f);
        GameObject.FindGameObjectWithTag("Bafle").GetComponent<Tunes>().StopSong();
    }

    private void BonusScene()
    {
        int scenaEle = Random.Range(0, bonusScene.Length);

        SceneManager.LoadScene(bonusScene[scenaEle]);
    }

    private void TweenGameObject(GameObject vict, float xPos, float yPos)
    {
        iTween.MoveTo(vict, iTween.Hash("x", xPos, "y", yPos, "time", 0.8f, "islocal", true));
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("DueloScene");
        
    }

    public void SonidoClick()
    {
        int elex = Random.Range(0, botonClick.Length);

        BafleSource.PlayOneShot(botonClick[elex]);
    }

    private void cargarData()
    {
        Control.volumeSoundInGame = volumeM;
        //Control.SaveDataS();
    }

    private void NegroCortinaSalida()
    {
        Image[] images = FondoNegro.GetComponentsInChildren<Image>();
        foreach (Image imgs in images)
            imgs.CrossFadeAlpha(0, 0.5f, false);

        Image bordImg = Borde.GetComponent<Image>();
        bordImg.CrossFadeAlpha(0, 0.5f, false);

        Invoke("setFalseCortina", 0.5f);
    }

    private void setFalseCortina()
    {
        FondoNegro.SetActive(false);
        Borde.SetActive(false);
    }

    private void NegroCortinaEntrada()
    {
        FondoNegro.SetActive(true);
        Borde.SetActive(true);

        Image[] images = FondoNegro.GetComponentsInChildren<Image>();
        foreach (Image imgs in images)
            imgs.CrossFadeAlpha(1, 0.5f, false);

        Image bordImg = Borde.GetComponent<Image>();
        bordImg.CrossFadeAlpha(1, 0.5f, false);
    }
}
