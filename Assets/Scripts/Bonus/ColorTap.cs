using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorTap : MonoBehaviour
{
    private Controlador control;
    public GameObject flecha;
    public GameObject AudBtn;
    private AudioSource audSour;
    private bool canPressBtn;
    public GameObject[] pacentePq;
    public GameObject[] pacenteGr;
    public Color[] coloresToTap;

    public Image gold;
    public Sprite[] fnalMage;

    private float reboteFlecha;
    private int chance;

    public TextMeshProUGUI timerIniTxt;
    public TextMeshProUGUI timerJuegotxt;
    public TextMeshProUGUI advce;

    public TextMeshProUGUI TextFnal;
    public TextMeshProUGUI GoldBeetles;
    private int[] sizebol;
    private MeshRenderer[] pacChild;
    private MeshRenderer[] pacPqC;

    private float timerIni;
    private float timerJuego;
    private float iniciarGame;
    private int sumaPrem;
    private bool premio;
    public GameObject LastScreen;
    public GameObject botonTap;
    public float tempo;
    private bool SetFnal;
    private int total;
    private int premioN;
    private int puntosN;
    private bool setPrem;
    private bool salvaData;

    private void Awake()
    {
        control = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        LastScreen.GetComponent<Returno>().FadeScreen(0,0);
        LastScreen.SetActive(false);
        int localPac = Random.Range(0, pacentePq.Length);
        salvaData = false;
        pacentePq[localPac].SetActive(true);
        pacenteGr[localPac].SetActive(true);
        audSour = AudBtn.GetComponent<AudioSource>();

        SetDataToTap(pacenteGr[localPac], pacentePq[localPac]);
        botonTap.SetActive(true);

        chance = localPac;
        timerIni = 3;
        timerJuego = tempo;
        SetFnal = false;
        control.PartidasJugadas = 0;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void ChangeColor()
    {
        int sizeMat = pacenteGr[chance].transform.childCount;

        for (int i = 0; i < sizeMat; i++)
        {
            int j = Random.Range(0, coloresToTap.Length);
            if (pacChild[i].material.color != pacPqC[i].material.color && sizebol[i] == 0)
            {
                pacChild[i].material.color = coloresToTap[j];
                pacChild[i].material.SetColor("_EmissionColor", coloresToTap[j]);
            }

            if (pacChild[i].material.color == pacPqC[i].material.color && sizebol[i] == 0)
            {
                pacChild[i].material.color = coloresToTap[j];
                pacChild[i].material.SetColor("_EmissionColor", coloresToTap[j]);
                float shakeF = Random.Range(0.05f, 0.12f);
                float shakeM = Random.Range(0.5f, 1.2f);

                ShakeCamera(shakeF, shakeM);
                pacChild[i].gameObject.GetComponent<ParticleSystem>().Play();
                sizebol[i] = 1;
            }
        }
    }

    public void ShakeCamera(float terremoto, float timeP)
    {
        iTween.ShakePosition(gameObject, iTween.Hash("x", terremoto, "time", timeP));
    }

    private void SetDataToTap(GameObject pac, GameObject pacPq)
    {
        int sizeMat = pac.transform.childCount;
        sizebol = new int[sizeMat];
        pacChild = new MeshRenderer[sizeMat];
        pacPqC =new MeshRenderer[sizeMat];
        total = sizeMat;

        List<int> matMatch = new List<int>();

        for (int i = 0; i < coloresToTap.Length; i++)
        {
            matMatch.Add(i);
        }
        
        pacChild = pac.GetComponentsInChildren<MeshRenderer>();
        pacPqC = pacPq.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < sizeMat; i++)
        {
            int matColor = Random.Range(0, coloresToTap.Length);
            matMatch.Remove(matColor);
            int matColorPq = Random.Range(0, matMatch.Count);
            pacChild[i].material.color = coloresToTap[matColor];
            pacPqC[i].material.color = coloresToTap[matColorPq];
            pacChild[i].material.SetColor("_EmissionColor", coloresToTap[matColor]);
            pacPqC[i].material.SetColor("_EmissionColor", coloresToTap[matColorPq]);
        }
    }

    private void Update()
    {
        reboteFlecha += Time.deltaTime;
        iniciarGame += Time.deltaTime;

        if (iniciarGame >= 1.5f)
        {
            timerIni -= Time.deltaTime;
        }

        timerIniTxt.text = timerIni.ToString("N0");

        if (reboteFlecha >= 1)
        {
            if (reboteFlecha >= 1 && reboteFlecha < 2)
            {
                iTween.MoveTo(flecha, iTween.Hash("x", 2.51f, "time", 1f, "islocal", true, "easetype", iTween.EaseType.easeInOutSine));
            }

            if (reboteFlecha >= 2)
            {
                iTween.MoveTo(flecha, iTween.Hash("x", 1.63f, "time", 1f, "islocal", true, "easetype", iTween.EaseType.easeInOutSine));
                reboteFlecha = 0;
            }
        }
        if (timerIni <= 0)
        {
            if (timerJuego < tempo)
            {
                botonTap.SetActive(false);
                canPressBtn = true;
            }

            timerIni = 0;
            timerIniTxt.CrossFadeAlpha(0f, 0.2f, false);
            iTween.ScaleTo(timerIniTxt.gameObject, new Vector3(0, 0, 0), 0.2f);

            timerJuego -= Time.deltaTime;
            timerJuegotxt.text = timerJuego.ToString("N0");

            if (timerJuego <= 0.2)
            {
                AudBtn.SetActive(false);
                botonTap.SetActive(true);
                canPressBtn = false;
                timerJuego = 0;
            }

            if (timerJuego <= 0f && !SetFnal)
            {
                timerJuego = 0;
                Invoke("SetLoadScreen", 0.8f);
                botonTap.SetActive(true);
            }
            
        }
    }

    private void SetLoadScreen()
    {
        LastScreen.SetActive(true);
        LastScreen.GetComponent<Returno>().Comenzar();
        SetFnal = true;
        flecha.SetActive(false);
        timerJuegotxt.CrossFadeAlpha(0, 0.8f, false);
        advce.CrossFadeAlpha(0, 0.8f, false);

        if (!setPrem)
        {
            premioN = Random.Range(30, 45);
            puntosN = Random.Range(30, 100);
            setPrem = true;
        }
        
        SetPremo();

        /*Debug.Log("sumatora prem " + sumaPrem + " premio disponible: " + premio);

        for (int i = 0; i < sizebol.Length; i++)
        {
            Debug.Log("sizebol " + i + " > " + sizebol[i]);
        }*/

        if (premio)
        {
            int valor = control.goldenBugs + premioN;
            int puntos = control.sunPointsMonth + puntosN;
            TextFnal.text = "<size=150%> Congrats! " +
                "<size=100%>You get " + premioN + " Golden Beetles and " + puntosN + " Sun Points.";
            GoldBeetles.text = valor.ToString();
            gold.sprite = fnalMage[0];
            if (!salvaData)
            {
                control.goldenBugs = valor;
                control.SaveDataS();
                salvaData = true;
            }
        }

        if (!premio)
        {
            TextFnal.text = "<size=150%> Wow!" +
                " <size=100%>That was close, better luck next time.";
            GoldBeetles.text = control.goldenBugs.ToString();
            gold.sprite = fnalMage[1];
        }
    }

    private void SetPremo()
    {
        for (int i = 0; i < sizebol.Length; i++)
        {
            if (sizebol[i] > 0)
            {
                sumaPrem = sumaPrem + sizebol[i];
            }
        }

        if (sumaPrem == total)
        {
            premio = true;
        }
    }

    public void ContnueBtn()
    {
        iTween.MoveTo(GetComponent<OpenDoors>().PuertaDer.GetComponentInParent<AudioSource>().gameObject, iTween.Hash("z", 50.7769f, "time", 0.8f));
        GetComponent<OpenDoors>().CerrarPuerta();
        Invoke("GoScene", 2f);
    }

    private void GoScene()
    {
        SceneManager.LoadScene("BeginScene");
    }

    public void BotonAc()
    {
        if (canPressBtn)
        {
            ChangeColor();
            audSour.Play();
        }
    }
}
