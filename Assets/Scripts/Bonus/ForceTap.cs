using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForceTap : MonoBehaviour
{
    private Controlador control;
    public GameObject AudBtn;
    private AudioSource audSour;
    private bool canPressBtn;
    public Image gold;
    public Sprite[] fnalMage;

    public TextMeshProUGUI timerIniTxt;
    public TextMeshProUGUI timerJuegotxt;
    public TextMeshProUGUI advce;

    public TextMeshProUGUI TextFnal;
    public TextMeshProUGUI GoldBeetles;

    private float timerIni;
    private float timerJuego;
    private float iniciarGame;
    private bool premio;
    public GameObject LastScreen;
    public GameObject botonTap;
    public float tempo;
    private bool SetFnal;
    private int premioN;
    private int puntosN;
    private bool setPrem;
    private bool salvaData;

    //character
    public GameObject jnt;
    public GameObject[] bloque;
    public Image medidor;
    public GameObject MedLimite;
    public AudioSource fxSuccess;
    public SuenaSqueak sS;

    //force system
    private float fuerzaAct;
    private float fuerzaNecesar;
    private float menosF;
    private bool golpeAn;

    private void Awake()
    {
        control = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        audSour = AudBtn.GetComponent<AudioSource>();
        LastScreen.GetComponent<Returno>().FadeScreen(0, 0);
        LastScreen.SetActive(false);
        salvaData = false;
        botonTap.SetActive(true);
        timerIni = 3;
        timerJuego = tempo;
        SetFnal = false;
        control.PartidasJugadas = 0;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        SetAnim(jnt, "jntConcentra");
        SetiniData();
        Invoke("SetBarra", 3f);
    }

    private void SetiniData()
    {
        fuerzaNecesar = Random.Range(40, 80);
        menosF = Random.Range(15, 35);
        fuerzaAct = 0.2f;
    }

    private void Update()
    {
        iniciarGame += Time.deltaTime;

        if (iniciarGame >= 1.5f)
        {
            timerIni -= Time.deltaTime;
        }

        timerIniTxt.text = timerIni.ToString("N0");

        if (timerIni <= 0)
        {
            if (timerJuego < tempo)
            {
                botonTap.SetActive(false);
                canPressBtn = true;
            }

            if (timerJuego > 0)
            {
                if (fuerzaAct > 0)
                {
                    fuerzaAct -= Time.deltaTime * menosF;
                    medidor.fillAmount = fuerzaAct / 100;
                }
            }


            timerIni = 0;
            timerIniTxt.CrossFadeAlpha(0f, 0.2f, false);
            iTween.ScaleTo(timerIniTxt.gameObject, new Vector3(0, 0, 0), 0.2f);

            timerJuego -= Time.deltaTime;
            timerJuegotxt.text = timerJuego.ToString("N0");

            if (timerJuego <= 0.2)
            {
                botonTap.SetActive(true);
                canPressBtn = false;
                timerJuego = 0;
                if (!golpeAn)
                {
                    golpeFnal();
                }
            }

            if (timerJuego <= 0f && !SetFnal)
            {
                timerJuego = 0;
                AudBtn.SetActive(false);
                Invoke("SetLoadScreen", 3f);
                botonTap.SetActive(true);
            }

        }
    }

    private void golpeFnal()
    {
        if (fuerzaAct >= fuerzaNecesar)
        {
            SetAnim(jnt, "jntSmash");
            //sound
            Invoke("CameraSuccess", 0.1f);
            fxSuccess.Play();
            ladrllosOff();
        }

        if (fuerzaAct < fuerzaNecesar)
        {
            SetAnim(jnt, "jntfall");
            //Debug.Log("fa;l");
            //sound
            sS.canShot = true;
        }
        golpeAn = true;
    }

    public void SetForce()
    {
        if (fuerzaAct < 101f)
        {
            int varF = Random.Range(3, 5);
            fuerzaAct = fuerzaAct + varF;
        }
        //Debug.Log("barra " + fuerzaAct + " y debe llegar a " + fuerzaNecesar);
    }

    private void ladrllosOff()
    {
        foreach (GameObject ldr in bloque)
            ldr.AddComponent<Rigidbody>();

        iTween.MoveTo(bloque[0], iTween.Hash("x", bloque[0].transform.localPosition.x + 0.5f, "time", 0.3f, "islocal", true));
        iTween.MoveTo(bloque[1], iTween.Hash("x", bloque[1].transform.localPosition.x - 0.5f, "time", 0.3f, "islocal", true));
    }

    // Anmacones

    private void SetAnim(GameObject victima, string accon)
    {
        victima.GetComponent<AnimationMJ>().ChangeAnimationState(accon);
    }

    private void SetBarra()
    {
        iTween.MoveTo(MedLimite, iTween.Hash("y", fuerzaNecesar, "time", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInOutSine));
    }

    private void SetLoadScreen()
    {
        //control.PartidasJugadas = 0;
        LastScreen.SetActive(true);
        LastScreen.GetComponent<Returno>().Comenzar();
        SetFnal = true;
        timerJuegotxt.CrossFadeAlpha(0, 0.8f, false);
        advce.CrossFadeAlpha(0, 0.8f, false);

        if (!setPrem)
        {
            premioN = Random.Range(30, 45);
            puntosN = Random.Range(30, 100);
            setPrem = true;
        }

        SetPremo();

        if (premio)
        {
            int valor = control.goldenBugs + premioN;
            int puntos = control.sunPointsMonth + puntosN;
            TextFnal.text = "<size=150%> Congrats! " +
                "<size=100%>You get " + premioN + " Golden Beetles and " + premioN + " Sun Points.";
            GoldBeetles.text = valor.ToString();
            gold.sprite = fnalMage[0];
            if (!salvaData)
            {
                control.goldenBugs = valor;
                salvaData = true;
            }
        }

        if (!premio)
        {
            TextFnal.text = "<size=150%> Wow!" +
                " <size=100%>Next time tap faster and broke that brick.";
            GoldBeetles.text = control.goldenBugs.ToString();
            gold.sprite = fnalMage[1];
        }
    }

    private void SetPremo()
    {
        if (fuerzaAct >= fuerzaNecesar)
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

    private void CameraSuccess()
    {
        float shakeF = Random.Range(0.05f, 0.12f);
        float shakeM = Random.Range(0.5f, 1.2f);
        ShakeCamera(shakeF, shakeM);
    }

    public void BtnPressAc()
    {
        if (canPressBtn)
        {
            SetForce();
            audSour.Play();
        }
    }

    public void ShakeCamera(float terremoto, float timeP)
    {
        iTween.ShakePosition(gameObject, iTween.Hash("x", terremoto, "time", timeP));
    } 
}
