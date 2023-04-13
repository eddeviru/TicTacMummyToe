using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections;

public class ControlAnimation : MonoBehaviour
{
    private ControlSun cS;
    private ControladorAereo cA;
    public GameObject camP;

    public TextMeshProUGUI TurnoDe;
    public Image[] FondoTurno;
    public GameObject alasTurno;
    private Logica logic;
    private bool AvisoOn;
    private bool setAviso;
    public Color Rojo, Azul;
    public Color Black;
    private Color Janet, Momia;
    private bool offAviso;
    private float timeEFX;
    public string[] turnoJanetTxt;
    public string[] turnoMummyTxt;
    public string[] ganoJanetTxt;
    public string[] ganoMummyTxt;
    private bool ganoTxtb, aviTxtb, setEmpate;
    public GameObject ResultBoard;
    public GameObject[] AvisosGO;
    public Vector3[] posFinAvisosGo;
    private Vector3[] posIniAvisosGO;

    //Animation Janet Mummy
    public GameObject Mummygo;
    public GameObject OjosMummy;
    public GameObject restosMummy;
    public GameObject Janetgo;
    public GameObject polvoMummy;

    //boleanos
    private bool enJuego;
    private bool entersueno;
    private bool sueno1, sueno2, levantarse;
    private float timerParpadeo;
    private bool parpadeo = true;
    private float timerEspera;
    private float timerEsperaMummy;
    private float timerEsperaJanet;
    private float timerDelay;
    private float timerDelayM;
    private float timerPutFichaJ;
    private float timerPutFichaM;
    private GameObject PasajeControl;
    public bool tutorial;
    public float volumeSound;
    public AudioMixer MasterdSonido;

    //Janet Anim Tutorial
    [Header("Textos")]
    public GameObject tutCompleto;
    public GameObject BotonContinuaTut;
    public Image JanetTemplate;
    public Sprite[] Janettmp;
    public GameObject Rusty;
    private Vector3 RustyPos;
    private Vector3 RustyNewPos;
    public bool posiblyToasty;
    public bool returnToasty;
    [SerializeField] [TextArea] private string[] textsTo;
    [SerializeField] private float textSpeed = 0.01f;

    [Header("UI Elements")]
    public GameObject logo1;
    [SerializeField] private TextMeshProUGUI textInfo;
    private int currentText = 0;

    private bool callSetTutorial;

    public Vector3 camPos, marcoPos;
    public Vector3 camRot, marcoRot;


    private void Awake()
    {
        //pos cam above
        camPos = new Vector3(-0.97f, 24.74f, 26.34f);
        camRot = new Vector3(37.996f, 180f, 0);
        marcoPos = new Vector3(-0.973999f, 21.21001f, 25.65479f);
        marcoRot = new Vector3(-136.996f, 0, 0);

        PasajeControl = GameObject.FindGameObjectWithTag("pasaje");
        cS = GetComponent<ControlSun>();
        loadData();
        posIniAvisosGO = new Vector3[AvisosGO.Length];
        RustyPos = Rusty.transform.localPosition;
        RustyNewPos = new Vector3(RustyPos.x + 1414f, RustyPos.y - 2273, 0);

        for (int i = 0; i < AvisosGO.Length; i++)
        {
            posIniAvisosGO[i] = AvisosGO[i].transform.localPosition;

            AvisosGO[i].transform.localPosition = posFinAvisosGo[i];
        }

        Rusty.transform.localPosition = RustyNewPos;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        camP = GameObject.FindGameObjectWithTag("MainCamera");
        cA = GameObject.FindGameObjectWithTag("ControlAereo").GetComponent<ControladorAereo>();
        logic = GetComponent<Logica>();
        for (int i = 0; i < FondoTurno.Length; i++)
        {
            FondoTurno[i].CrossFadeAlpha(0, 0, false);
        }

        TurnoDe.CrossFadeAlpha(0, 0, false);
        alasTurno.GetComponent<Image>().CrossFadeAlpha(0, 0, false);

        Janet = Azul;
        Momia = Rojo;
        ganoTxtb = true;
        aviTxtb = true;
        enJuego = true;
        Invoke("OrPosCam", logic.timeToStart);
        Invoke("colocarAvisos", 2f);
        TutImagesStart(0, 0);
        if (!tutorial)
        {
            destroyTut();
        }
        if (tutorial)
        {
            Invoke("PasoComienzo", 2f);
        }
    }


    private void Update()
    {
        if (tutorial)
        {
            //Debug.Log("textos " + currentText);

            if (currentText == 0)
            {
                iTween.ScaleTo(logic.BotnFicha, iTween.Hash("scale", new Vector3(0, 0, 0), "time", 0.5f, "islocal", true));
            }
            if (currentText == 3 && logic.paraTut)
            {
                logic.SetOffPanel();
                Paso();
            }
            if (currentText == 5 && logic.ghostMark)
            {
                //usar carta fantasma
                logic.SetTutorialGhMark();
                Paso();
            }
            if (currentText == 6 && logic.paraTut)
            {
                logic.SetOffPanel();
                Paso();
            }
        }
        if (logic.changeTurn)
        {
            AvisoOn = true;
            logic.changeTurn = false;
        }
        //Debug.Log("TimeEFX " + timeEFX);
        //Debug.Log("SetAviso " + setAviso);
        if (AvisoOn)
        {
            timeEFX += Time.deltaTime;
            if (!setAviso && timeEFX >= 0.3f && aviTxtb)
            {
                setAviso = true;
                aviTxtb = false;
            }

            if (timeEFX >= 1f && timeEFX <= 1.2f)
            {
                offAviso = true;
            }

            if (timeEFX >= 1.3f)
            {
                AvisoOn = false;
                offAviso = false;
                aviTxtb = true;
                timeEFX = 0;
            }
        }

        if (enJuego)
        {
            if (logic.turnoMomia)
            {
                cA.Obstaculo.SetActive(true);
                logic.canMove = false;
            }
            if (logic.turnoJanet)
            {
                cA.Obstaculo.SetActive(false);
                logic.intentos = 0;
                logic.canMove = true;
            }

            if (logic.turnoJanet && entersueno)
            {
                if (!levantarse)
                {
                    timerEspera += Time.deltaTime;
                }

                if (timerEspera > Random.Range(20f, 30f) && !sueno1 && !sueno2 && !levantarse)
                {
                    sueno1 = true;
                    sueno2 = true;
                    timerEspera = 0;
                }

                if (timerEspera > Random.Range(20f, 50f) && sueno2 && !sueno1 && !levantarse)
                {
                    sueno2 = true;
                    levantarse = true;
                    timerEspera = 0;
                    //Debug.Log("Entrasueño 2");
                }

                if (sueno1 && sueno2 && !levantarse)
                {
                    MummySleep0();
                    float trackW = Mummygo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
                    Invoke("MummySleep1", trackW);
                    //Debug.Log("Entrasueño 1");
                    timerEspera = 0;
                    sueno1 = false;
                }

                if (sueno2 && !sueno1 && levantarse)
                {
                    MummySleep2();
                    parpadeo = false;
                    float trackW = Mummygo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
                    Invoke("MummySleep3", trackW);
                    //Debug.Log("dormido");
                    timerEspera = 0;
                    sueno2 = false;
                }

                //sentarse
                if (logic.turnoMomia && sueno1 || logic.turnoMomia && sueno2 || logic.turnoMomia && levantarse)
                {
                    MummyLevantarse();
                    timerEspera = 0;
                }
            }

            if (logic.turnoMomia && entersueno && sueno1 || logic.turnoMomia && entersueno && sueno2 || logic.turnoMomia && entersueno && levantarse)
            {
                MummyLevantarse();
                timerEspera += Time.deltaTime;

                if (entersueno)
                {
                    entersueno = false;
                    timerEspera = 0;
                }
            }
            //Anim Put Ficha
            if (logic.tomeJanet)
            {
                PutAFicha(Janetgo);

                float timerDelayT = Janetgo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + Janetgo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length / 2;
                timerPutFichaJ += Time.deltaTime;
                if (timerPutFichaJ > timerDelayT)
                {
                    if (levantarse || entersueno)
                    {
                        sueno1 = false;
                        sueno2 = false;
                        entersueno = false;
                        levantarse = false;
                    }
                    logic.tomeJanet = false;
                    timerEspera = 0;
                }
            }

            //Anim Put Ficha
            if (logic.tomeMummy)
            {
                float timerDelayT = Mummygo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.8f;
                PutAFicha(Mummygo);
                timerPutFichaM += Time.deltaTime;
                if (timerPutFichaM > timerDelayT)
                {
                    if (levantarse || entersueno)
                    {
                        sueno1 = false;
                        sueno2 = false;
                        entersueno = false;
                        levantarse = false;
                    }
                    logic.tomeMummy = false;
                    timerEspera = 0;
                }
            }

            //Debug.Log("levantarse " + levantarse);
            //Debug.Log("sueno1 " + sueno1);
            //Debug.Log("sueno2 " + sueno2);
            //Debug.Log("entersueno " + entersueno);

            // AnimEspera
            if (logic.turnoJanet && !logic.turnoMomia && !entersueno && !logic.ganoMummy && !logic.ganoJanet && !logic.tomeJanet && !logic.tomeMummy && !logic.removePlace && !logic.retrocesoMark && !logic.changeAllMarks && !logic.intAllMarks && !logic.changeMark && !logic.ghostMark && !logic.offerx1000 && !logic.offerx2000 && !logic.rage)
            {
                timerEsperaJanet += Time.deltaTime;
                timerEsperaMummy += Time.deltaTime;
                timerEspera += Time.deltaTime;
                parpadeo = true;

                if (levantarse)
                {
                    sueno1 = false;
                    sueno2 = false;
                    entersueno = false;
                    levantarse = false;
                }
                //Debug.Log("idle an");
                if (timerEsperaJanet >= timerDelay)
                {
                    EsperaAm(Mummygo);
                    timerDelay = Janetgo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + Janetgo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length / 2;
                    timerEsperaJanet = 0;
                }
                if (timerEsperaMummy >= timerDelayM)
                {
                    EsperaAm(Janetgo);
                    timerDelayM = Mummygo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + Mummygo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length / 2;
                    timerEsperaMummy = 0;
                }

                if (timerEspera > Random.Range(7f, 12f))
                {
                    entersueno = true;
                    timerEspera = 0;
                }
            }

            //Anim Rage
            if (logic.rage && !entersueno && !sueno1 && !sueno2 && !levantarse && !logic.ganoJanet && !logic.ganoMummy)
            {
                ZoomMummy();
                RagePers(Mummygo);
                logic.rage = false;
                logic.SoundRage();
            }

            if (!logic.rage && camP.transform.localPosition.x != -0.97f)
            {
                Invoke("OrPosCam", 2f);
            }
        }

        //Anim Empate
        if (logic.empate)
        {
            aviso("Draw Game", Black);
            if (!setEmpate)
            {
                int sunPontExtra = Random.Range(38, 72);

                cS.ScoreTotalPts = cS.ScoreTotalPts + sunPontExtra;
                cS.SceneryPts = cS.SceneryPts + sunPontExtra;
                PierdePers(Mummygo);
                PierdePers(Janetgo);
                Invoke("OrPosCam", 2f);
                Invoke("SetResult", 7);
                Invoke("sacarAvisos", 3f);
                enJuego = false;
                setEmpate = true;
            }
        }

        //Anim Gano Janet
        if (logic.ganoJanet && ganoTxtb)
        {
            int turno = Random.Range(0, ganoJanetTxt.Length);
            aviso(ganoJanetTxt[turno], Janet);
            int goldBugsExtra = Random.Range(10, 20);
            int sunPontExtra = Random.Range(340, 447);

            cS.ScoreTotalGB = cS.ScoreTotalGB + goldBugsExtra;
            cS.SceneryGB = cS.SceneryGB + goldBugsExtra;
            cS.ScoreTotalPts = cS.ScoreTotalPts + sunPontExtra;
            cS.SceneryPts = cS.SceneryPts + sunPontExtra;
            GanaPers(Janetgo);
            PierdePers(Mummygo);
            Invoke("OrPosCam", 2f);
            Invoke("SetResult", 4);
            Invoke("sacarAvisos", 3f);
            enJuego = false;
            ganoTxtb = false;
        }

        //Anim Gano Mummy
        if (logic.ganoMummy && ganoTxtb)
        {
            int turno = Random.Range(0, ganoMummyTxt.Length);
            aviso(ganoMummyTxt[turno], Momia);
            int sunPontExtra = Random.Range(2, 7);

            cS.ScoreTotalPts = cS.ScoreTotalPts + sunPontExtra;
            cS.SceneryPts = cS.SceneryPts + sunPontExtra;
            GanaPers(Mummygo);
            PierdePers(Janetgo);
            Invoke("OrPosCam", 2f);
            Invoke("SetResult", 4);
            Invoke("sacarAvisos", 3f);
            enJuego = false;
            ganoTxtb = false;
        }

        if (setAviso && !logic.empate)
        {
            ShowBusiness();
            //Debug.Log("Cambio");
            setAviso = false;
        }

        if (parpadeo && !logic.ganoJanet && !logic.ganoMummy && logic.empate)
        {
            timerParpadeo += Time.deltaTime;
            if (timerParpadeo > 3.5f && timerParpadeo < 3.6f)
            {
                iTween.ScaleTo(OjosMummy, new Vector3(1, 0, 0), 0.5f);
            }
            if (timerParpadeo > 4f)
            {
                iTween.ScaleTo(OjosMummy, new Vector3(1, 1, 1), 0.8f);
                timerParpadeo = 0;
            }
        }

        if (offAviso)
        {
            offAdvice();
        }

        if (posiblyToasty)
        {
            randomToasty();
        }

        if (returnToasty)
        {
            Invoke("lugartoasty", 0.8f);
            returnToasty = false;
        }

    }
    private void colocarAvisos()
    {
        for (int i = 0; i < AvisosGO.Length; i++)
        {
            iTween.MoveTo(AvisosGO[i], iTween.Hash("position", posIniAvisosGO[i], "time", 0.5f, "islocal", true));
        }

    }

    private void sacarAvisos()
    {
        for (int i = 0; i < AvisosGO.Length; i++)
        {
            iTween.MoveTo(AvisosGO[i], iTween.Hash("position", posFinAvisosGo[i], "time", 0.5f, "islocal", true));
        }
    }

    private IEnumerator ShowText()
    {
        if (currentText != 0)
        {
            logo1.SetActive(false);
        }
        if (currentText != 8)
        {
            ActivateFlechas(false);
        }
        if (currentText == 0)
        {
            JanetImg(Janettmp[2]);
            TutImagesStart(1, 0.5f);
            logo1.SetActive(true);

        }
        if (currentText == 1)
        {
            JanetImg(Janettmp[5]);
        }
        if (currentText == 2)
        {
            JanetImg(Janettmp[5]);
        }
        if (currentText == 3)
        {
            JanetImg(Janettmp[4]);
            PasajeControl.GetComponent<Controlador>().FichaFantasma = 2;
            PasajeControl.GetComponent<Controlador>().CambioMummyFichas = 4;
            PasajeControl.GetComponent<Controlador>().Cambio1Ficha = 3;
            PasajeControl.GetComponent<Controlador>().OffFicha = 1;
            PasajeControl.GetComponent<Controlador>().OffPanel = 3;

            if (!callSetTutorial)
            {
                logic.SetTutorial();
                callSetTutorial = true;
            }
        }
        if (currentText == 4)
        {
            JanetImg(Janettmp[6]);
            aviso("Hi, I'll show the turn", Janet);
        }
        if (currentText == 5)
        {
            JanetImg(Janettmp[1]);
            iTween.ScaleTo(logic.BotnFicha, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f, "islocal", true));
            iTween.MoveTo(logic.BotnFicha, iTween.Hash("y", logic.posBtnFicha.y, "islocal", true, "time", 0.8f));
        }
        if (currentText == 6)
        {
            JanetImg(Janettmp[4]);
            iTween.ScaleTo(logic.BotnFicha, iTween.Hash("scale", new Vector3(0, 0, 0), "time", 0.5f, "islocal", true));
        }
        if (currentText == 7)
        {
            JanetImg(Janettmp[4]);
        }
        if (currentText == 8)
        {
            JanetImg(Janettmp[3]);
            ActivateFlechas(true);
        }
        if (currentText == 9)
        {
            JanetImg(Janettmp[0]);
        }
        if (currentText == 10)
        {
            JanetImg(Janettmp[3]);
        }
        if (currentText == 11)
        {
            JanetImg(Janettmp[4]);
        }
        if (currentText == 12)
        {
            JanetImg(Janettmp[4]);
        }

        for (int i = 0; i < textsTo[currentText].Length + 1; i++)
        {
            int j = i;
            textInfo.text = textsTo[currentText].Substring(0, i);
        }
        yield return new WaitForSeconds(textSpeed);
    }

    public void Paso()
    {
        if (currentText < textsTo.Length - 1)
        {
            currentText++;
            StartCoroutine(ShowText());
            StartCoroutine(pasoBtn());
            logic.paraTut = false;
        }

        else
        {
            TutImagesStart(0, 0.5f);
            iTween.ScaleTo(BotonContinuaTut, iTween.Hash("scale", new Vector3(0, 0, 0), "time", 0.5f, "islocal", true, "easetype", iTween.EaseType.easeInElastic));
            Invoke("destroyTut", 0.6f);
            logic.BotnFicha.SetActive(true);
            for (int i = 0; i < cA.Cards.Length; i++)
            {
                if (cA.Cards[i] > 0)
                {
                    cA.botonesDisp[i].gameObject.SetActive(true);
                }
            }
        }
    }

    private IEnumerator pasoBtn()
    {
        BotonContinuaTut.GetComponent<Image>().CrossFadeAlpha(0, 0.2f, false);
        iTween.ScaleTo(BotonContinuaTut, iTween.Hash("scale", new Vector3(0, 0, 0), "time", 0.5f, "islocal", true));
        BotonContinuaTut.GetComponent<Button>().enabled = false;

        yield return new WaitForSeconds(0.3f);

        if (currentText == 3 || currentText == 5 || currentText == 6)
        {
            BotonContinuaTut.SetActive(false);
        }
        if (currentText < 3 || currentText == 4 || currentText > 6)
        {
            BotonContinuaTut.SetActive(true);
            iTween.ScaleTo(BotonContinuaTut, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f, "islocal", true));
            BotonContinuaTut.GetComponent<Image>().CrossFadeAlpha(1, 0.2f, false);
            BotonContinuaTut.GetComponent<Button>().enabled = true;
        }

    }

    public void PasoComienzo()
    {
        StartCoroutine(ShowText());
    }

    private void TutImagesStart(float alp, float tiempo)
    {
        Image[] subd = tutCompleto.GetComponentsInChildren<Image>();
        foreach (Image sd in subd)
            sd.CrossFadeAlpha(alp, tiempo, true);

        textInfo.CrossFadeAlpha(alp, tiempo, true);
    }

    private void destroyTut()
    {
        tutCompleto.gameObject.SetActive(false);
        iTween.ScaleTo(logic.BotnFicha, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f, "islocal", true));
        logic.BotnFicha.GetComponentInChildren<Button>().enabled = true;
        if (tutorial)
        {
            logic.SetTutorial();
            PasajeControl.GetComponent<Controlador>().tutorial = false;
            tutorial = false;
        }
    }

    private void JanetImg(Sprite cara)
    {
        JanetTemplate.sprite = cara;
    }

    private void ActivateFlechas(bool activo)
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Empaque");
        for (int i = 0; i < 9; i++)
        {
            if (bases[i].GetComponent<ControlPuntos>().obstaculo)
            {
                bases[i].GetComponent<ControlPuntos>().Flecha.SetActive(activo);
            }
        }
    }

    private void ShowBusiness()
    {
        if (logic.turnoJanet)
        {
            int turno = Random.Range(0, turnoJanetTxt.Length);
            aviso(turnoJanetTxt[turno], Janet);
        }
        if (logic.turnoMomia)
        {
            int turno = Random.Range(0, turnoMummyTxt.Length);
            aviso(turnoMummyTxt[turno], Momia);
        }
    }

    public void aviso(string turno, Color backg)
    {
        TurnoDe.text = turno;
        FondoTurno[0].color = backg;
        for (int i = 0; i < FondoTurno.Length; i++)
        {
            FondoTurno[i].CrossFadeAlpha(1, 0.3f, false);
        }
        TurnoDe.CrossFadeAlpha(1, 0.3f, false);
        alasTurno.GetComponent<Image>().CrossFadeAlpha(1, 0.3f, false);
    }

    private void offAdvice()
    {
        alasTurno.GetComponent<Image>().CrossFadeAlpha(0, 0.3f, false);
        TurnoDe.CrossFadeAlpha(0, 0.3f, false);
        for (int i = 0; i < FondoTurno.Length; i++)
        {
            FondoTurno[i].CrossFadeAlpha(0, 0.3f, false);
        }
    }

    private void SetResult()
    {
        ResultBoard.SetActive(true);

        TextMeshProUGUI[] textos = ResultBoard.GetComponentsInChildren<TextMeshProUGUI>();
        Image[] imagenes = ResultBoard.GetComponentsInChildren<Image>();

        foreach (TextMeshProUGUI txt in textos)
            txt.CrossFadeAlpha(1, 0.8f, false);

        foreach (Image img in imagenes)
            img.CrossFadeAlpha(1, 0.8f, false);
    }

    #region Camara Movement
    private void ZoomMummy()
    {
        Vector3[] ZoPos = new Vector3[3];
        Vector3[] ZoRot = new Vector3[3];

        ZoPos[0] = new Vector3(-12.27f, 6.34f, 4.56f);
        ZoRot[0] = new Vector3(9.31f, 139.82f, -0.38f);
        ZoPos[1] = new Vector3(-0.76f, 7.57f, 3.57f);
        ZoRot[1] = new Vector3(1.38f, 180f, 0f);
        ZoPos[2] = new Vector3(12.85f, 3.23f, -0.13f);
        ZoRot[2] = new Vector3(-6.1f, 224.1f, 1.3f);
        //Quaternion ZoRot = new Quaternion(0.0f, 0.9f, -0.1f, 0.3f);
        int lotr = Random.Range(0, ZoPos.Length);

        iTween.MoveTo(camP, ZoPos[lotr], 1f);
        iTween.RotateTo(camP, ZoRot[lotr], 1f);
        logic.SetFalseBoxes(false);
    }

    // posición camara origen -0.97f, 5.19f, 53.49f
    // posición Puerta origen -0.975f, 2.83f, 50.77f
    private void OrPosCam()
    {
        //Quaternion orRot = new Quaternion(0.0f, 0.9f, -0.3f, 0.0f);

        iTween.MoveTo(camP, camPos, 1f);
        iTween.RotateTo(camP, camRot, 1f);
        logic.ChangeController();
    }
    #endregion

    #region Sueno Momia
    // sentarse
    private void MummySleep0()
    {
        Mummygo.GetComponent<AnimationMJ>().ChangeAnimationState(Mummygo.GetComponent<AnimationMJ>().dormir[0]);
    }

    //loop sentado
    private void MummySleep1()
    {
        Mummygo.GetComponent<AnimationMJ>().ChangeAnimationState(Mummygo.GetComponent<AnimationMJ>().dormir[1]);
    }

    // se acuesta
    private void MummySleep2()
    {
        Mummygo.GetComponent<AnimationMJ>().ChangeAnimationState(Mummygo.GetComponent<AnimationMJ>().dormir[2]);
    }

    //lopp acostado
    private void MummySleep3()
    {
        Mummygo.GetComponent<AnimationMJ>().ChangeAnimationState(Mummygo.GetComponent<AnimationMJ>().dormir[3]);
        iTween.ScaleTo(OjosMummy, new Vector3(1, 0, 0), 0.8f);
    }

    // levantarse
    private void MummyLevantarse()
    {
        Mummygo.GetComponent<AnimationMJ>().ChangeAnimationState(Mummygo.GetComponent<AnimationMJ>().dormir[4]);
        iTween.ScaleTo(OjosMummy, new Vector3(1, 1, 1), 0.8f);
    }
    #endregion

    #region Animaciones
    private void PutAFicha(GameObject victima)
    {
        victima.GetComponent<AnimationMJ>().ChangeAnimationState(victima.GetComponent<AnimationMJ>().putFAm);
    }

    private void RagePers(GameObject victima)
    {
        victima.GetComponent<AnimationMJ>().ChangeAnimationState(victima.GetComponent<AnimationMJ>().rageAm);
    }

    private void PierdePers(GameObject victima)
    {
        int win = Random.Range(0, victima.GetComponent<AnimationMJ>().dieAm.Length);

        victima.GetComponent<AnimationMJ>().ChangeAnimationState(victima.GetComponent<AnimationMJ>().dieAm[win]);

        if (win == 3 && victima == Mummygo || win == 0 && victima == Mummygo)
        {
            if (win == 3)
                iTween.ScaleTo(restosMummy, new Vector3(1, 1, 1), 1.5f);

            if (win == 0)
                Invoke("DestroyLights", 1.5f);
        }
    }

    private void DestroyLights()
    {
        polvoMummy.SetActive(true);
        Invoke("SetOffMummy", 0.2f);
    }

    private void SetOffMummy()
    {
        Mummygo.SetActive(false);
    }


    private void GanaPers(GameObject victima)
    {
        int win = Random.Range(0, victima.GetComponent<AnimationMJ>().celebAm.Length);

        victima.GetComponent<AnimationMJ>().ChangeAnimationState(victima.GetComponent<AnimationMJ>().celebAm[win]);
    }

    private void EsperaAm(GameObject victima)
    {
        int win = Random.Range(0, victima.GetComponent<AnimationMJ>().idleAm.Length);

        victima.GetComponent<AnimationMJ>().ChangeAnimationState(victima.GetComponent<AnimationMJ>().idleAm[win]);
    }
    #endregion

    private void loadData()
    {
        Controlador cont = PasajeControl.GetComponent<Controlador>();

        tutorial = cont.tutorial;
        volumeSound = cont.volumeSoundInGame;
        cS.ScoreTotalPts = cont.sunPointsMonth;
        cS.ScoreTotalGB = cont.goldenBugs;
        cS.SceneryPts = 0;
        cS.SceneryGB = 0;

        OnVolumeChange(cont.volumeSoundInGame);
    }

    private void randomToasty()
    {
        float posibilidad = Random.Range(0, 100);

        if (posibilidad < 5f)
        {
            //toasty
            iTween.MoveTo(Rusty, iTween.Hash("position", RustyPos, "time", 0.5f, "islocal", true));
            //Rusty.GetComponent<AudioSource>().Play();
            returnToasty = true;
        }
        posiblyToasty = false;
    }

    private void lugartoasty()
    {
        iTween.MoveTo(Rusty, iTween.Hash("position", RustyNewPos, "time", 0.5f, "islocal", true));
    }

    private void OnVolumeChange(float value)
    {
        MasterdSonido.SetFloat("MasterVol", value);
    }
}
