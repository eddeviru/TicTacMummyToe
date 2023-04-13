using System.Collections;
using UnityEngine;

public class Logica : MonoBehaviour
{
    #region variables
    public GameObject Empaques;
    private ControladorAereo cA;
    private ControlAnimation cAn;
    public ControlPuntos cP;
    private Controlador PasajeControl;
    private bool canPlay;
    private bool GetFicha;
    private float playPrs;
    private int[] plGan = new int[3];
    private Casilla[] Botones;
    private ControlPuntos[] CtrlPuntos;
    private GameObject[] Stones;
    public GameObject[] bases;
    private float echarBola;
    private bool exevuteMummyAction;
    private float setFicha;
    private int cant;
    private bool checkSpaces;
    public bool changeTurn;
    public int intentos;
    public bool turnoMomia, turnoJanet;
    public bool ganoMummy, ganoJanet, empate, menuFicha;
    public bool removePlace, retrocesoMark, changeMark, changeAllMarks, ghostMark, offerx1000, offerx2000, intAllMarks, normalTurn;
    public GameObject BotnFicha;
    public Vector3 posBtnFicha;
    private bool setManos;
    private float timerdesappear;
    private float timerObstaculo;
    public Material OffPhantomM;
    public Material NorPhantomM;
    public bool camboMat;
    public bool tomeJanet;
    public bool tomeMummy;
    public bool rage;
    public float timeToStart;

    private int pacienciaMummy;
    private bool changeMarksMummy;
    private bool intMarksMummy;
    private bool offPMarksMummy;
    private bool cleanBrd;
    private float timerRage;
    private bool rageSo;
    private bool setLoteria;

    public bool paraTut = false;
    private float loteria;

    private bool camZoom;
    public bool canMove;
    public bool canGo;

    public enum Seed { EMPTY, JANET, MUMMY, INUTILIZABLE };

    Seed Turn;

    public Seed[] ControlSp = new Seed[21];
    #endregion

    private void Awake()
    {
        posBtnFicha = BotnFicha.transform.localPosition;
        CtrlPuntos = new ControlPuntos[bases.Length];
        Botones = new Casilla[bases.Length];
        Stones = GameObject.FindGameObjectsWithTag("Piedras");

        for (int i = 0; i < bases.Length; i++)
        {
            GameObject empaque = (GameObject)Instantiate(Empaques, bases[i].transform.position, bases[i].transform.rotation);
            Botones[i] = bases[i].GetComponentInChildren<Casilla>();
            CtrlPuntos[i] = empaque.GetComponent<ControlPuntos>();
            if (Botones[i].BaseExtra)
            {
                CtrlPuntos[i].putObst = true;
            }

            CtrlPuntos[i].Piedras = Stones;
            CtrlPuntos[i].ControlMesa = i;
            Botones[i].Botonazo = empaque;
            empaque.transform.SetParent(bases[i].transform);
        }
        PasajeControl = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();

        normalTurn = true;
    }
    private void Start()
    {
        cA = GameObject.FindGameObjectWithTag("ControlAereo").GetComponent<ControladorAereo>();
        cAn = GetComponent<ControlAnimation>();
        cAn.posiblyToasty = false;

        if (!cAn.tutorial)
        {
            Invoke("SetBoard", timeToStart + 0.5f);
        }
    }
    public void SetBoard()
    {
        cAn.posiblyToasty = false;
        for (int i = 0; i < 21; i++)
        {
            ControlSp[i] = Seed.EMPTY;
            Botones[i].result = 0;
            CtrlPuntos[i].setContraMark(CtrlPuntos[i].Cross, 0, 0);
            CtrlPuntos[i].setContraMark(CtrlPuntos[i].Nought, 0, 0);
        }

        int TurnoPrimero = Random.Range(0, 2);

        if (TurnoPrimero == 0)
        {
            Turn = Seed.MUMMY;
        }

        if (TurnoPrimero == 1)
        {
            Turn = Seed.JANET;
        }

        if (Turn == Seed.MUMMY)
        {
            SetAutoMove(ControlSp);
        }

        removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
        changeTurn = true;
        normalTurn = true;
        camboMat = true;
        exevuteMummyAction = true;
        GetFicha = true;
        intentos = 0;

        setManos = true;
        //Debug.Log("Turno de " + Turn);
        ganoJanet = false;
        ganoMummy = false;

        canPlay = true;

    }

    public void SetTutorial()
    {
        for (int i = 0; i < 21; i++)
        {
            if (ControlSp[i] != Seed.INUTILIZABLE && ControlSp[i] != Seed.MUMMY && ControlSp[i] != Seed.JANET)
            {
                ControlSp[i] = Seed.EMPTY;
                Botones[i].result = 0;
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Cross, 0, 0);
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Nought, 0, 0);
            }
            if (i >= 8)
            {
                break;
            }
        }

        Turn = Seed.JANET;
        intentos = 1;
        changeTurn = true;
        setManos = true;
        //Debug.Log("Turno de " + Turn);
        camboMat = true;
        exevuteMummyAction = true;
        ganoJanet = false;
        ganoMummy = false;
        GetFicha = true;
        canPlay = true;

    }

    public void SetTutorialGhMark()
    {
        for (int i = 0; i < 21; i++)
        {
            if (i > 8)
            {
                ControlSp[i] = Seed.EMPTY;
                Botones[i].result = 0;
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Cross, 0, 0);
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Nought, 0, 0);
                if (i >= 21)
                {
                    break;
                }
            }
        }
        Turn = Seed.JANET;
        intentos = 1;
        changeTurn = true;
        setManos = true;
        //Debug.Log("Turno de " + Turn);
        camboMat = true;
        exevuteMummyAction = true;
        ganoJanet = false;
        ganoMummy = false;
        GetFicha = true;
        canPlay = true;
    }

    public void SetOffPanel()
    {
        Turn = Seed.EMPTY;
        SetFalseBoxes(false);
    }

    private void Update()
    {
        //Debug.Log("turno de " + Turn);
        if (canPlay)
        {
            if (GetFicha && !rage)
            {
                playPrs += Time.deltaTime;
                if (playPrs >= 0.05f)
                {
                    if (cP != null)
                    {
                        if (cP.obstaculo)
                        {
                            cP.matDss = true;
                            cP.obstaculo = false;
                            ShakeCamera(0.09f, 5f);
                            cP = null;
                            canGo = true;
                            playPrs = 0;
                        }
                    }

                    if (playPrs > 0.2f && Turn == Seed.JANET)
                    {
                        playPrs = 0;
                        GetFicha = false;
                    }
                    if (playPrs > 0.1f && Turn == Seed.MUMMY)
                    {
                        canGo = true;
                    }
                }
                if (Turn == Seed.MUMMY && canGo)
                {
                    iTween.MoveTo(BotnFicha, iTween.Hash("y", -4774f, "islocal", true, "time", 0.8f));
                    echarBola += Time.deltaTime;
                    turnoMomia = true;
                    turnoJanet = false;
                    if (echarBola > 0.1f && cant < 13)
                    {
                        //set enemy ficha
                        setLoteria = true;
                        SetFichaEnemiga();
                        Debug.Log("set ficha enemiga");
                        playPrs = 0;
                    }
                }
                if (cant > 12 && Turn == Seed.MUMMY && canGo)
                {
                    SetAutoMove(ControlSp);
                    playPrs = 0;
                }
                if (Turn == Seed.JANET)
                {
                    turnoJanet = true;
                    turnoMomia = false;
                }
                if (checkSpaces)
                {
                    CheckEmptySpaces();
                }
                if (changeTurn)
                {
                    ChangeController();
                }
            }
            if (ghostMark && Turn == Seed.JANET && camboMat)
            {
                SearchPhantomBases(NorPhantomM, true);
            }

            if (!ghostMark && camboMat)
            {
                SearchPhantomBases(OffPhantomM, false);
            }

            if (Turn == Seed.JANET && !menuFicha)
            {
                timerObstaculo += Time.deltaTime;
                iTween.MoveTo(BotnFicha, iTween.Hash("y", posBtnFicha.y, "islocal", true, "time", 0.8f));
                if (timerObstaculo > 0.03f)
                {
                    ChangeController();
                    cA.Obstaculo.SetActive(false);
                    timerObstaculo = 0;
                }
            }

            if (menuFicha)
            {
                timerObstaculo = 0;
            }
            if (setManos && removePlace)
            {
                SenalaMummyMarks(true, 1, Seed.MUMMY);
                SenalaMummyMarks(true, 1, Seed.EMPTY);
                setManos = false;
            }
            if (setManos && retrocesoMark || setManos && changeMark)
            {
                SenalaMummyMarks(true, 1, Seed.MUMMY);
                setManos = false;
            }

            if (setManos && offerx1000 || setManos && offerx2000)
            {
                SenalaMummyMarks(true, 1, Seed.EMPTY);
                setManos = false;
            }

            if (!setManos && !removePlace && !retrocesoMark && !changeMark && !offerx1000 && !offerx2000)
            {
                timerdesappear += Time.deltaTime;
                if (timerdesappear > 1)
                {
                    BasesABajar(false, 0);
                    setManos = true;
                    timerdesappear = 0;
                }
            }
        }

        if (ganoJanet || ganoMummy || empate)
        {
            SearchPhantomBases(OffPhantomM, false);
            iTween.MoveTo(BotnFicha, iTween.Hash("y", -4774f, "islocal", true, "time", 0.8f));
        }

        if (Turn == Seed.MUMMY && changeMarksMummy)
        {
            timerRage += Time.deltaTime;
            if (timerRage < 0.2f)
            {
                rage = true;
                if (!rageSo)
                {
                    SoundRage();
                    rageSo = true;
                }
            }


            if (timerRage > 1.2f)
            {
                CambioAllFchas();

                if (!PartidaGanada(Turn) && !tie())
                {
                    rage = true;
                    tomeJanet = true;
                    intentos = 1;
                    exevuteMummyAction = true;
                    GetFicha = true;
                    removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                    changeTurn = true;
                    normalTurn = true;
                    changeMarksMummy = false;
                    ChangeController();
                    Turn = Seed.JANET;
                }
                if (PartidaGanada(Turn))
                {
                    Turn = Seed.EMPTY;
                    Debug.Log("Gano Mummy");
                    ganoMummy = true;
                    StartCoroutine(SetBotones());
                    GetFicha = false;
                    canPlay = false;
                    normalTurn = false;
                    changeMarksMummy = false;
                }
                if (PartidaGanadaFFantasma())
                {
                    Turn = Seed.EMPTY;
                    Debug.Log("Gano Janet");
                    ganoJanet = true;
                    StartCoroutine(SetBotones());
                    GetFicha = false;
                    canPlay = false;
                    normalTurn = false;
                    changeMarksMummy = false;
                }
                if (tie())
                {
                    Turn = Seed.EMPTY;
                    empate = true;
                    // dejar intocable el teclado
                    GetFicha = false;
                    canPlay = false;
                    normalTurn = false;
                    changeMarksMummy = false;
                }
                rageSo = false;
                timerRage = 0;
            }
        }

        if (Turn == Seed.MUMMY && intMarksMummy)
        {
            timerRage += Time.deltaTime;
            if (timerRage < 0.2f)
            {
                rage = true;
                if (!rageSo)
                {
                    SoundRage();
                    rageSo = true;
                }
            }

            if (timerRage > 2f)
            {

                intercambioAllFchas();

                if (!PartidaGanada(Turn) && !tie())
                {
                    //rage = true;
                    tomeJanet = true;
                    exevuteMummyAction = true;
                    intentos = 1;
                    GetFicha = true;
                    removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                    changeTurn = true;
                    normalTurn = true;
                    ChangeController();
                    Turn = Seed.JANET;
                }
                if (PartidaGanada(Turn))
                {
                    Turn = Seed.EMPTY;
                    Debug.Log("Gano Mummy");
                    ganoMummy = true;
                    StartCoroutine(SetBotones());
                    GetFicha = false;
                    canPlay = false;
                    normalTurn = false;
                }
                if (PartidaGanadaFFantasma())
                {
                    Turn = Seed.EMPTY;
                    Debug.Log("Gano Janet");
                    ganoJanet = true;
                    StartCoroutine(SetBotones());
                    GetFicha = false;
                    canPlay = false;
                    normalTurn = false;
                }
                if (tie())
                {
                    Turn = Seed.EMPTY;
                    empate = true;
                    // dejar intocable el teclado
                    GetFicha = false;
                    canPlay = false;
                    normalTurn = false;
                }
                intMarksMummy = false;
                rageSo = false;
                timerRage = 0;
            }
        }

        if (Turn == Seed.MUMMY && offPMarksMummy)
        {
            timerRage += Time.deltaTime;
            if (timerRage < 0.2f)
            {
                rage = true;
                if (!rageSo)
                {
                    SoundRage();
                    rageSo = true;
                }
            }

            if (timerRage > 2f)
            {

                SetOffFichasFantasma();

                if (!PartidaGanada(Turn) && !tie())
                {
                    //rage = true;
                    tomeJanet = true;
                    exevuteMummyAction = true;
                    intentos = 1;
                    GetFicha = true;
                    changeTurn = true;
                    normalTurn = true;
                    removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                    ChangeController();
                    Turn = Seed.JANET;
                }
                if (PartidaGanada(Turn))
                {
                    Turn = Seed.EMPTY;
                    Debug.Log("Gano Mummy");
                    ganoMummy = true;
                    StartCoroutine(SetBotones());
                    GetFicha = false;
                    canPlay = false;
                    normalTurn = false;
                }
                if (PartidaGanadaFFantasma())
                {
                    Turn = Seed.EMPTY;
                    Debug.Log("Gano Janet");
                    ganoJanet = true;
                    StartCoroutine(SetBotones());
                    GetFicha = false;
                    canPlay = false;
                    normalTurn = false;
                }
                if (tie())
                {
                    Turn = Seed.EMPTY;
                    empate = true;
                    // dejar intocable el teclado
                    GetFicha = false;
                    canPlay = false;
                    normalTurn = false;
                }
                offPMarksMummy = false;
                rageSo = false;
                timerRage = 0;
            }
        }

        if (Turn == Seed.MUMMY && cleanBrd)
        {
            timerRage += Time.deltaTime;
            if (timerRage < 0.2f)
            {
                rage = true;
                if (!rageSo)
                {
                    SoundRage();
                    rageSo = true;
                }
            }

            if (timerRage > 2f)
            {
                CleanBoard();
                intentos = 1;
                exevuteMummyAction = true;
                GetFicha = true;
                removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                cleanBrd = false;
                changeTurn = true;
                normalTurn = true;
                Turn = Seed.JANET;
                rageSo = false;
                timerRage = 0;
            }
        }

        // Cambiar Marcas
        if (Turn == Seed.JANET && changeAllMarks && canPlay && !normalTurn)
        {
            CambioAllFchas();
            cAn.posiblyToasty = true;
            if (!PartidaGanada(Turn) && !tie())
            {
                rage = true;
                tomeJanet = true;
                exevuteMummyAction = true;
                GetFicha = true;
                changeTurn = true;
                normalTurn = true;
                removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                PasajeControl.CambioMummyFichas--;
                intentos = 0;
                pacienciaMummy++;
                Turn = Seed.MUMMY;
            }
            if (PartidaGanada(Seed.MUMMY))
            {
                Turn = Seed.EMPTY;
                Debug.Log("Gano Mummy");
                ganoMummy = true;
                StartCoroutine(SetBotones());
                GetFicha = false;
                canPlay = false;
                normalTurn = false;
                PasajeControl.CambioMummyFichas--;
                removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            }
            if (PartidaGanadaFFantasma())
            {
                Turn = Seed.EMPTY;
                Debug.Log("Gano Janet");
                ganoJanet = true;
                StartCoroutine(SetBotones());
                GetFicha = false;
                canPlay = false;
                normalTurn = false;
                PasajeControl.CambioMummyFichas--;
                removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            }
            if (tie())
            {
                Turn = Seed.EMPTY;
                empate = true;
                // dejar intocable el teclado
                GetFicha = false;
                canPlay = false;
                normalTurn = false;
                PasajeControl.CambioMummyFichas--;
                removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            }
        }
        // Intercambiar Marcas
        if (Turn == Seed.JANET && intAllMarks && canPlay && !normalTurn)
        {
            tomeJanet = true;
            rage = true;
            intercambioAllFchas();
            cAn.posiblyToasty = true;
            exevuteMummyAction = true;
            GetFicha = true;
            changeTurn = true;
            intentos = 0;
            PasajeControl.IntercambioAllFichas--;
            removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            normalTurn = true;
            pacienciaMummy++;
            Turn = Seed.MUMMY;
            if (PartidaGanadaFFantasma())
            {
                Turn = Seed.EMPTY;
                Debug.Log("Gano Janet");
                ganoJanet = true;
                StartCoroutine(SetBotones());
                GetFicha = false;
                canPlay = false;
                normalTurn = false;
                removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            }
            if (tie())
            {
                Turn = Seed.EMPTY;
                empate = true;
                // dejar intocable el teclado
                GetFicha = false;
                canPlay = false;
                normalTurn = false;
                removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            }
        }
        //Debug.Log("cant " + cant);
        //Debug.Log("CanPlay " + canPlay);
        //Debug.Log("GetFicha " + GetFicha);
        //Debug.Log("changeTurn " + changeTurn);

        if (!cAn.tutorial)
        {
            if (Turn == Seed.EMPTY)
            {
                BotnFicha.SetActive(false);
            }
            if (Turn == Seed.JANET)
            {
                BotnFicha.SetActive(true);
            }
        }
    }
    private void FixedUpdate()
    {
        if (canPlay)
        {
            if (cAn.camP.transform.localPosition != cAn.camPos)
            {
                camZoom = true;
            }
            if (cAn.camP.transform.localPosition == cAn.camPos)
            {
                camZoom = false;
            }
            if (Turn == Seed.MUMMY || camZoom)
            {
                SetFalseBoxes(false);
                normalTurn = true;
                removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            }
            if (Turn == Seed.JANET && !camZoom)
            {
                SetFalseBoxes(true);
            }
            Debug.Log("ficha changeMummyMarks " + changeAllMarks);
        }
        else
        {
            SetFalseBoxes(false);
            turnoJanet = false;
            turnoMomia = false;
        }
    }
    public void Spawn(int id)
    {
        if (Turn == Seed.JANET && canMove)
        {
            if (canPlay && !GetFicha)
            {
                // Turno Normal
                if (normalTurn && !removePlace && !retrocesoMark && !changeAllMarks && !intAllMarks && !changeMark && !ghostMark && !offerx1000 && !offerx2000 && Botones[id].result == 0 && ControlSp[id] == Seed.EMPTY && id < 9)
                {
                    tomeJanet = true;
                    removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                    normalTurn = true;
                    if (!cP.obstaculo)
                    {
                        ControlSp[id] = Turn;
                        FichaSetUP(cP.Nought, 1.44f, 1);
                        Botones[id].result = 1;
                        cP.playEfx(cP.fxs[5]);
                        //caja.enabled = false;
                    }
                    if (PartidaGanadaFFantasma())
                    {
                        Turn = Seed.EMPTY;
                        Debug.Log("Gano Janet");
                        ganoJanet = true;
                        normalTurn = false;
                        canPlay = false;
                        GetFicha = false;
                        camboMat = true;
                        StartCoroutine(SetBotones());
                    }
                    else
                    {
                        changeTurn = true;
                        normalTurn = true;
                        pacienciaMummy++;
                        Turn = Seed.MUMMY;
                        camboMat = true;
                        exevuteMummyAction = true;
                        GetFicha = true;
                        intentos = 0;
                        return;
                    }
                }
                // remover base
                if (removePlace && !cP.obstaculo && !normalTurn && !retrocesoMark && !intAllMarks && !changeMark && !ghostMark && !offerx1000 && !offerx2000 && id < 9)
                {
                    OffStageBase(id);
                    Botones[id].result = 1;
                    RandomPremios(10, id, 15, 50);
                    bases[id].GetComponentInChildren<BoxCollider>().enabled = false;
                    BasesABajar(true, 0);
                    cP.playEfx(cP.fxs[7]);
                    tomeJanet = true;
                    PossiblyMummyRage();
                    cAn.posiblyToasty = true;
                    pacienciaMummy++;
                    if (tie())
                    {
                        Turn = Seed.EMPTY;
                        empate = true;
                        // dejar intocable el teclado
                        GetFicha = false;
                        canPlay = false;
                        normalTurn = false;
                        camboMat = true;
                    }
                    else
                    {
                        removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                        changeTurn = true;
                        normalTurn = true;
                        Turn = Seed.MUMMY;
                        setManos = false;
                        exevuteMummyAction = true;
                        GetFicha = true;
                        PasajeControl.OffPanel--;
                        camboMat = true;
                        intentos = 0;
                        return;
                    }
                }
                // quitar 1 ficha
                if (retrocesoMark && !cP.obstaculo && ControlSp[id] != Seed.EMPTY && ControlSp[id] != Seed.INUTILIZABLE && !normalTurn && !removePlace && !changeAllMarks && !intAllMarks && !changeMark && !ghostMark && !offerx1000 && !offerx2000 && id < 9)
                {
                    cP.setContraMark(cP.Cross, 0, 0);
                    cP.setContraMark(cP.Nought, 0, 0);
                    RandomPremios(20, id, 3, 15);
                    PossiblyMummyRage();
                    pacienciaMummy++;
                    BasesABajar(true, 0);
                    Botones[id].result = 0;
                    ControlSp[id] = Seed.EMPTY;
                    cP.playEfx(cP.fxs[2]);
                    tomeJanet = true;
                    removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                    changeTurn = true;
                    normalTurn = true;
                    Turn = Seed.MUMMY;
                    exevuteMummyAction = true;
                    GetFicha = true;
                    PasajeControl.OffFicha--;
                    camboMat = true;
                    intentos = 0;
                    return;
                }
                // cambiar ficha Mummy
                if (!normalTurn && !removePlace && !retrocesoMark && !changeAllMarks && !intAllMarks && changeMark && !ghostMark && !offerx1000 && !offerx2000 && ControlSp[id] != Seed.EMPTY && ControlSp[id] != Seed.INUTILIZABLE && ControlSp[id] != Seed.MUMMY && Botones[id].result != 0 && id < 9)
                {
                    if (ControlSp[id] == Seed.JANET)
                    {
                        cP.setContraMark(cP.Cross, 1.44f, 1);
                        cP.setContraMark(cP.Nought, 0, 0);
                        ControlSp[id] = Seed.MUMMY;
                        Botones[id].result = 1;
                        RandomPremios(10, id, 15, 30);
                        cP.playEfx(cP.fxs[2]);
                        cP.playEfxB();
                    }

                    BasesABajar(true, 0);
                    tomeJanet = true;

                    if (PartidaGanada(Seed.MUMMY))
                    {
                        Turn = Seed.EMPTY;
                        Debug.Log("Gano Mummy");
                        changeMark = false;
                        ganoMummy = true;
                        ganoJanet = false;
                        normalTurn = false;
                        canPlay = false;
                        GetFicha = false;
                        camboMat = true;
                        PasajeControl.Cambio1Ficha--;
                        StartCoroutine(SetBotones());
                    }

                    else
                    {
                        pacienciaMummy++;
                        removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                        changeTurn = true;
                        normalTurn = true;
                        Turn = Seed.MUMMY;
                        exevuteMummyAction = true;
                        GetFicha = true;
                        PasajeControl.Cambio1Ficha--;
                        camboMat = true;
                        intentos = 0;
                        return;
                    }

                }
                // cambiar ficha Janet
                if (!normalTurn && !removePlace && !retrocesoMark && !changeAllMarks && !intAllMarks && changeMark && !ghostMark && !offerx1000 && !offerx2000 && ControlSp[id] != Seed.EMPTY && ControlSp[id] != Seed.INUTILIZABLE && ControlSp[id] != Seed.JANET && Botones[id].result != 0 && id < 9)
                {
                    if (ControlSp[id] == Seed.MUMMY)
                    {
                        cP.setContraMark(cP.Nought, 1.44f, 1);
                        cP.setContraMark(cP.Cross, 0, 0);
                        Botones[id].result = 1;
                        RandomPremios(10, id, 15, 30);
                        cAn.posiblyToasty = true;
                        PossiblyMummyRage();
                        ControlSp[id] = Seed.JANET;
                        cP.playEfx(cP.fxs[2]);
                        cP.playEfxB();
                    }

                    BasesABajar(true, 0);
                    tomeJanet = true;

                    if (PartidaGanadaFFantasma())
                    {
                        Turn = Seed.EMPTY;
                        Debug.Log("Gano Janet");
                        changeMark = false;
                        ganoJanet = true;
                        ganoMummy = false;
                        normalTurn = false;
                        canPlay = false;
                        GetFicha = false;
                        camboMat = true;
                        PasajeControl.Cambio1Ficha--;
                        StartCoroutine(SetBotones());
                    }
                    else
                    {
                        pacienciaMummy++;
                        removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                        changeTurn = true;
                        normalTurn = true;
                        Turn = Seed.MUMMY;
                        exevuteMummyAction = true;
                        GetFicha = true;
                        PasajeControl.Cambio1Ficha--;
                        camboMat = true;
                        intentos = 0;
                        return;
                    }

                }
                // ficha fantasma
                if (!normalTurn && !removePlace && !retrocesoMark && !changeAllMarks && !intAllMarks && !changeMark && ghostMark && !offerx1000 && !offerx2000 && Botones[id].result < 1 && id > 8)
                {
                    ControlSp[id] = Turn;
                    FichaSetUP(cP.Nought, 1.44f, 1);
                    RandomPremios(100, id, 5, 20);
                    cP.Nought.GetComponent<MeshRenderer>().material = NorPhantomM;
                    Botones[id].result = 1;
                    tomeJanet = true;
                    PossiblyMummyRage(); ;
                    PasajeControl.FichaFantasma--;
                    cP.playEfx(cP.fxs[2]);
                    cP.playEfxB();

                    if (PartidaGanadaFFantasma())
                    {
                        Turn = Seed.EMPTY;
                        Debug.Log("Gano Janet");
                        ganoJanet = true;
                        StartCoroutine(SetBotones());
                        GetFicha = false;
                        canPlay = false;
                        normalTurn = false;
                        camboMat = true;
                    }
                    else
                    {
                        pacienciaMummy = pacienciaMummy + 3;
                        removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                        changeTurn = true;
                        normalTurn = true;
                        Turn = Seed.MUMMY;
                        camboMat = true;
                        exevuteMummyAction = true;
                        GetFicha = true;
                        intentos = 0;
                        return;
                    }
                }
                // ofrenda x 1000
                if (!normalTurn && !removePlace && !retrocesoMark && !changeAllMarks && !intAllMarks && !changeMark && !ghostMark && offerx1000 && !offerx2000 && ControlSp[id] == Seed.EMPTY && id < 9)
                {
                    if (!CtrlPuntos[id].obstaculo)
                    {
                        CtrlPuntos[id].SetIconOn(100, 0);
                        CtrlPuntos[id].AnimText(20, 1000, true);
                        BasesABajar(true, 0);
                        tomeJanet = true;
                        removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                        changeTurn = true;
                        normalTurn = true;
                        Turn = Seed.MUMMY;
                        // menos carta
                        cP = null;
                        exevuteMummyAction = true;
                        GetFicha = true;
                        PasajeControl.ObstaculoP1000--;
                        camboMat = true;
                        intentos = 0;
                        return;
                    }
                }
                // ofrenda x 2000
                if (!normalTurn && !removePlace && !retrocesoMark && !changeAllMarks && !intAllMarks && !changeMark && !ghostMark && !offerx1000 && offerx2000 && ControlSp[id] == Seed.EMPTY && id < 9)
                {
                    if (!CtrlPuntos[id].obstaculo)
                    {
                        CtrlPuntos[id].SetIconOn(100, 0);
                        CtrlPuntos[id].AnimText(45, 2000, true);
                        BasesABajar(true, 0);
                        tomeJanet = true;
                        removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
                        changeTurn = true;
                        normalTurn = true;
                        Turn = Seed.MUMMY;
                        // menos carta
                        cP = null;
                        exevuteMummyAction = true;
                        GetFicha = true;
                        PasajeControl.ObstaculoP2000--;
                        camboMat = true;
                        intentos = 0;
                        return;
                    }
                }
            }
            if (tie())
            {
                Turn = Seed.EMPTY;
                empate = true;
                // dejar intocable el teclado
                GetFicha = false;
                canPlay = false;
                camboMat = true;
            }
        }
        else if (Turn == Seed.MUMMY && intentos == 0)
        {
            if (canPlay)
            {
                if (normalTurn)
                {
                    tomeMummy = true;
                    if (!cP.obstaculo)
                    {
                        ControlSp[id] = Turn;
                        FichaSetUP(cP.Cross, 1.44f, 1);
                        cP.playEfx(cP.fxs[6]);
                        Botones[id].result = 1;
                        //caja.enabled = false;
                    }
                    if (PartidaGanada(Seed.MUMMY))
                    {
                        Turn = Seed.EMPTY;
                        Debug.Log("Gano Mummy");
                        ganoMummy = true;
                        normalTurn = false;
                        canPlay = false;
                        GetFicha = false;
                        StartCoroutine(SetBotones());
                    }
                    else
                    {
                        Turn = Seed.JANET;
                        GetFicha = true;
                        changeTurn = true;
                        camboMat = false;
                    }
                }
                if (tie())
                {
                    Turn = Seed.EMPTY;
                    empate = true;
                    // dejar intocable el teclado
                    GetFicha = false;
                    canPlay = false;
                }
            }
        }
    }
    public void pasoObstaculos(int id)
    {
        if (Turn == Seed.JANET)
        {
            // Turno Normal
            if (normalTurn && !removePlace && !retrocesoMark && !changeAllMarks && !intAllMarks && !changeMark && !ghostMark && !offerx1000 && !offerx2000 && Botones[id].result == 0 && ControlSp[id] == Seed.EMPTY && id < 9)
            {
                cP.ArenasEfx = true;
                cP.pasoPuntaje = true;
                cP.matDss = true;
                cP.obstaculo = false;
                pacienciaMummy++;
                //Debug.Log("turno de " + Turn);
                camboMat = true;
                exevuteMummyAction = true;
                GetFicha = true;
                changeTurn = true;
                intentos = 0;
                Turn = Seed.MUMMY;
                return;
            }
        }
        if (Turn == Seed.MUMMY && intentos == 0)
        {
            if (normalTurn)
            {
                cP.ArenasEfx = true;
                pacienciaMummy++;
                Turn = Seed.JANET;
                GetFicha = true;
                changeTurn = true;
                camboMat = false;
            }
        }
    }

    private void PossiblyMummyRage()
    {
        float loteria = Random.Range(0, 100);

        if (loteria > 70)
        {
            rage = true;
        }
        else
        {
            rage = false;
        }
    }

    public void SoundRage()
    {
        GameObject.FindGameObjectWithTag("Momia").GetComponent<AudioSource>().Play();
    }
    private void CambioAllFchas()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Turn == Seed.MUMMY)
            {
                if (ControlSp[i] == Seed.JANET)
                {
                    CtrlPuntos[i].setContraMark(CtrlPuntos[i].Cross, 1.44f, 1);
                    CtrlPuntos[i].setContraMark(CtrlPuntos[i].Nought, 0f, 0);
                    ControlSp[i] = Seed.MUMMY;
                }
            }

            if (Turn == Seed.JANET)
            {
                if (ControlSp[i] == Seed.MUMMY)
                {
                    CtrlPuntos[i].setContraMark(CtrlPuntos[i].Nought, 1.44f, 1);
                    CtrlPuntos[i].setContraMark(CtrlPuntos[i].Cross, 0f, 0);
                    RandomPremios(5, i, 7, 20);
                    ControlSp[i] = Seed.JANET;
                }
            }
        }
        sonidofichas();
    }
    private void intercambioAllFchas()
    {
        int[] dataM = new int[9];
        int[] dataJ = new int[9];

        for (int i = 0; i < 9; i++)
        {
            if (ControlSp[i] == Seed.MUMMY)
            {
                dataM[i] = i;
            }
            if (ControlSp[i] == Seed.JANET)
            {
                dataJ[i] = i;
            }

            if (i == dataM[i])
            {
                if (Turn == Seed.JANET)
                {
                    RandomPremios(5, i, 3, 15);
                }
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Nought, 1.44f, 1);
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Cross, 0f, 0);
                ControlSp[i] = Seed.JANET;
            }

            if (i == dataJ[i])
            {
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Cross, 1.44f, 1);
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Nought, 0f, 0);
                ControlSp[i] = Seed.MUMMY;
            }

        }
        sonidofichas();
    }
    private void sonidofichas()
    {
        ControlPuntos vict = GameObject.FindGameObjectWithTag("Empaque").GetComponent<ControlPuntos>();

        vict.playEfx(vict.fxs[2]);
        vict.playEfxB();
    }
    public void SobreCasilla()
    {
        if (!cP.obstaculo)
        {
            if (Turn == Seed.JANET && normalTurn)
            {
                PasoEncima(cP.NoughtTr);
            }

            else if (Turn == Seed.MUMMY)
            {
                PasoEncima(cP.CrossTr);
            }
        }
    }
    private void FichaSetUP(GameObject ficha, float position, float scala)
    {
        iTween.ScaleTo(cP.PisoBrillante, new Vector3(1.03f, 1f, 1.11f), 0.5f);
        iTween.MoveTo(ficha, iTween.Hash("y", position, "time", 1f, "islocal", true));
        iTween.ScaleTo(ficha, iTween.Hash("scale", new Vector3(scala, scala, scala), "time", 1f, "islocal", true));
        cP.Luciernagas.Play();
        cP.stonesFicha.Play();
        cP.AudioFXB.Play();
        cP.vfxStay[5].Play();
        ShakeCamera(0.2f, 1f);
    }
    public void OffStageBase(int baseV)
    {
        Botones[baseV].result = 1;
        Botones[baseV].enabled = false;
        iTween.ShakePosition(bases[baseV], new Vector3(0.2f, 0f, 0.2f), 2f);
        bases[baseV].GetComponentInParent<Contenedor>().caePlt();
        ControlSp[baseV] = Seed.INUTILIZABLE;
    }
    private void BasesABajar(bool activo, float scala)
    {
        for (int i = 0; i < 9; i++)
        {
            CtrlPuntos[i].manoMummy.SetActive(activo);
            iTween.ScaleTo(CtrlPuntos[i].manoMummy, new Vector3(scala, scala, scala), 0.8f);
        }
    }
    private void SenalaMummyMarks(bool activo, float scala, Seed lugarAsenalar)
    {
        for (int i = 0; i < 9; i++)
        {
            if (ControlSp[i] == lugarAsenalar && !CtrlPuntos[i].obstaculo)
            {
                CtrlPuntos[i].manoMummy.SetActive(activo);
                iTween.ScaleTo(CtrlPuntos[i].manoMummy, new Vector3(scala, scala, scala), 0.8f);
            }
        }
    }
    public void PasoEncima(GameObject Ficha)
    {
        Ficha.SetActive(true);
        cP.efxEcima = true;
    }
    public void ShakeCamera(float terremoto, float timeP)
    {
        GameObject cameraM = GameObject.FindGameObjectWithTag("MainCamera");
        iTween.ShakePosition(cameraM, iTween.Hash("x", terremoto, "time", timeP));
    }
    bool Vacios()
    {
        bool empty = false;
        for (int i = 0; i < 9; i++)
        {
            if (ControlSp[i] == Seed.EMPTY)
            {
                empty = true;
                break;
            }
        }
        return empty;
    }
    bool PartidaGanada(Seed CurPl)
    {
        checkSpaces = true;
        bool gano = false;

        int[,] listo = new int[8, 3] { {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
                                        {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
                                        {0, 4, 8}, {2, 4, 6} };
        for (int i = 0; i < 8; i++)
        {
            if (ControlSp[listo[i, 0]] == CurPl && ControlSp[listo[i, 1]] == CurPl && ControlSp[listo[i, 2]] == CurPl)
            {
                plGan[0] = listo[i, 0];
                plGan[1] = listo[i, 1];
                plGan[2] = listo[i, 2];
                gano = true;
                //Debug.Log("Ficha I " + plGan[0] + " Ficha II " + plGan[1] + " Ficha III " + plGan[2]);
                break;
            }
        }

        return gano;
    }
    bool PartidaGanadaFFantasma()
    {
        checkSpaces = true;
        bool gano = false;

        int[,] listo = new int[20, 3] { {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
                                        {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
                                        {0, 4, 8}, {2, 4, 6}, { 12, 0, 1 },
                                        { 13, 3, 4 }, { 14, 6, 7 }, { 9, 0, 3 },
                                        { 10, 1, 4 }, { 11, 2, 5 }, { 15, 6, 3 },
                                        { 16, 7, 4 }, { 17, 8, 5 }, { 18, 2, 1 },
                                        { 19, 5, 4 }, { 20, 8, 7 } };
        for (int i = 0; i < 20; i++)
        {
            if (ControlSp[listo[i, 0]] == Seed.JANET && ControlSp[listo[i, 1]] == Seed.JANET && ControlSp[listo[i, 2]] == Seed.JANET)
            {
                plGan[0] = listo[i, 0];
                plGan[1] = listo[i, 1];
                plGan[2] = listo[i, 2];
                gano = true;
                //Debug.Log("Ficha I " + plGan[0] + " Ficha II " + plGan[1] + " Ficha III " + plGan[2]);
                break;
            }
        }

        return gano;
    }
    bool tie()
    {
        bool JanetWon, MummyWon, anyEmpty;

        JanetWon = PartidaGanada(Seed.JANET);
        MummyWon = PartidaGanada(Seed.MUMMY);
        anyEmpty = Vacios();

        bool tie = false;

        if (!JanetWon && !MummyWon && !anyEmpty)
            tie = true;

        return tie;
    }
    private IEnumerator SetBotones()
    {
        yield return new WaitForSeconds(1f);
        // Set vfx to normal
        for (int i = 0; i < CtrlPuntos.Length; i++)
        {
            CtrlPuntos[i].CanPlay = false;
        }
        yield return new WaitForSeconds(0.5f);
        // light winners platforms
        for (int i = 0; i < plGan.Length; i++)
        {
            iTween.ScaleTo(CtrlPuntos[plGan[i]].PisoBrillante, new Vector3(1.03f, 1f, 1.11f), 0.7f);
        }
    }
    public void ChangeController()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Turn == Seed.MUMMY)
            {
                Botones[i].GetComponent<BoxCollider>().enabled = false;
                turnoMomia = true;
            }

            if (Turn == Seed.JANET)
            {
                Botones[i].GetComponent<BoxCollider>().enabled = true;
                turnoJanet = true;
            }
        }
    }
    public void SetFalseBoxes(bool estado)
    {
        for (int i = 0; i < 9; i++)
        {
            Botones[i].GetComponent<BoxCollider>().enabled = estado;
        }
    }
    public void SearchPhantomBases(Material mat, bool stateOn)
    {
        for (int i = 0; i < 21; i++)
        {
            if (i > 8 && camboMat)
            {
                bases[i].GetComponent<MeshRenderer>().material = mat;
                Botones[i].GetComponent<BoxCollider>().enabled = stateOn;
            }
        }
        camboMat = false;
    }
    private void SetAutoMove(Seed[] board)
    {
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == Seed.EMPTY)
            {
                setFicha = Random.Range(0f, 100f);

                if (setFicha > 30f)
                {
                    if (exevuteMummyAction)
                    {
                        //Debug.Log("este es " + i + " loteria: " + setFicha + " Caja Nombre " + Botones[i].name);
                        Botones[i].oprimirBtn();
                        playPrs = 0;
                        echarBola = 0;
                        cant = 0;
                        setFicha = 0;
                        exevuteMummyAction = false;
                        paraTut = true;
                        break;
                    }
                }
                ControlSp[i] = Seed.EMPTY;
            }
        }
    }
    int minimaxEmpty(Seed curPl, Seed[] board, int alpha, int beta)
    {
        if (tie())
            return 0;

        if (PartidaGanada(Seed.MUMMY))
            return +1;

        if (PartidaGanada(Seed.JANET))
            return -1;

        int score;

        if (curPl == Seed.MUMMY)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == Seed.EMPTY && !CtrlPuntos[i].obstaculo)
                {
                    board[i] = Seed.MUMMY;
                    score = minimaxEmpty(Seed.JANET, board, alpha, beta);
                    board[i] = Seed.EMPTY;

                    if (score > alpha)
                        alpha = score;

                    if (alpha > beta)
                        break;
                }
            }
            return alpha;
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == Seed.EMPTY && !CtrlPuntos[i].obstaculo)
                {
                    board[i] = Seed.JANET;
                    score = minimaxEmpty(Seed.MUMMY, board, alpha, beta);
                    board[i] = Seed.EMPTY;

                    if (score < beta)
                        beta = score;

                    if (alpha > beta)
                        break;
                }
            }
            return beta;
        }
    }
    int minimaxObstacles(Seed curPl, Seed[] board, int alpha, int beta)
    {
        if (tie())
            return 0;

        if (PartidaGanada(Seed.MUMMY))
            return +1;

        if (PartidaGanada(Seed.JANET))
            return -1;

        int score;

        if (curPl == Seed.MUMMY)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == Seed.EMPTY && CtrlPuntos[i].obstaculo)
                {
                    board[i] = Seed.MUMMY;
                    score = minimaxObstacles(Seed.JANET, board, alpha, beta);
                    board[i] = Seed.EMPTY;

                    if (score > alpha)
                        alpha = score;

                    if (alpha > beta)
                        break;
                }
            }
            return alpha;
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == Seed.EMPTY && CtrlPuntos[i].obstaculo)
                {
                    board[i] = Seed.JANET;
                    score = minimaxObstacles(Seed.MUMMY, board, alpha, beta);
                    board[i] = Seed.EMPTY;

                    if (score < beta)
                        beta = score;

                    if (alpha > beta)
                        break;
                }
            }
            return beta;
        }
    }
    private void SetFichaEnemiga()
    {
        if (setLoteria)
        {
            loteria = Random.Range(0, 100);
            setLoteria = false;
        }

        if (loteria > 96 && pacienciaMummy > 3 && !changeMarksMummy && !intMarksMummy && !offPMarksMummy && !cAn.tutorial)
        {
            changeMarksMummy = true;
            pacienciaMummy = 0;
            GetFicha = false;
            playPrs = 0;
            echarBola = 0;
            cAn.aviso("Change Marks to Me!", cAn.Rojo);
            loteria = 50;
            removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            canGo = false;
            return;
        }

        if (loteria > 86 && loteria < 90 && pacienciaMummy > 3 && !changeMarksMummy && !intMarksMummy && !offPMarksMummy && !cleanBrd && !cAn.tutorial)
        {
            cAn.aviso("Change All Marks!", cAn.Rojo);
            intMarksMummy = true;
            pacienciaMummy = 0;
            GetFicha = false;
            playPrs = 0;
            echarBola = 0;
            loteria = 50;
            removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            canGo = false;
            return;
        }

        if (loteria > 15 && loteria < 18 && pacienciaMummy > 1 && !changeMarksMummy && !intMarksMummy && !offPMarksMummy && !cleanBrd && searchFF() && !cAn.tutorial)
        {
            cAn.aviso("Goodbye Phantom Marks!", cAn.Rojo);
            offPMarksMummy = true;
            pacienciaMummy = 0;
            GetFicha = false;
            playPrs = 0;
            echarBola = 0;
            removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            canGo = false;
            return;
        }

        if (loteria > 0 && loteria < 4 && pacienciaMummy > 1 && !changeMarksMummy && !intMarksMummy && !offPMarksMummy && !cleanBrd && !cAn.tutorial)
        {
            cAn.aviso("Clean All Marks!", cAn.Rojo);
            cleanBrd = true;
            pacienciaMummy = 0;
            GetFicha = false;
            playPrs = 0;
            echarBola = 0;
            loteria = 50;
            removePlace = retrocesoMark = intAllMarks = changeAllMarks = changeMark = ghostMark = offerx1000 = offerx2000 = false;
            canGo = false;
            return;
        }

        else
        {
            int bestScore = -1, bestpos = -1, score;

            for (int i = 0; i < 9; i++)
            {
                if (ControlSp[i] == Seed.EMPTY)
                {
                    ControlSp[i] = Seed.MUMMY;

                    if (CtrlPuntos[i].obstaculo)
                    {
                        score = minimaxObstacles(Seed.JANET, ControlSp, -1000, +1000);
                        ControlSp[i] = Seed.EMPTY;
                        if (bestScore < score)
                        {
                            bestScore = score;
                            bestpos = i;
                        }

                        /*if (bestpos > -1)
                        {
                            return;
                        }*/
                    }

                    if (bestpos < 0)
                    {
                        if (!CtrlPuntos[i].obstaculo)
                        {
                            score = minimaxEmpty(Seed.JANET, ControlSp, -1000, +1000);
                            ControlSp[i] = Seed.EMPTY;
                            if (bestScore < score)
                            {
                                bestScore = score;
                                bestpos = i;
                            }
                        }
                        cant++;
                    }
                }
                ///Debug.Log("bestpos " + bestpos);

                if (bestpos > -1)
                {
                    if (exevuteMummyAction && intentos == 0)
                    {
                        GetFicha = false;
                        exevuteMummyAction = false;
                        playPrs = 0;
                        echarBola = 0;
                        paraTut = true;
                        StartCoroutine(actMummy(bestpos));
                        canGo = false;
                    }
                }
            }
        }

        if (cant > 9)
        {
            exevuteMummyAction = true;
        }
    }
    private IEnumerator actMummy(int pos)
    {
        yield return new WaitForSeconds(0.05f);
        Botones[pos].oprimirBtn();
    }
    private void SetOffFichasFantasma()
    {
        if (Turn == Seed.MUMMY)
        {
            for (int i = 0; i < 21; i++)
            {
                if (i > 8)
                {
                    if (ControlSp[i] == Seed.JANET)
                    {
                        ControlSp[i] = Seed.EMPTY;
                        CtrlPuntos[i].setContraMark(CtrlPuntos[i].Cross, 0, 0);
                        CtrlPuntos[i].setContraMark(CtrlPuntos[i].Nought, 0, 0);
                        if (i >= 21)
                        {
                            break;
                        }
                    }
                }
            }
            ControlPuntos plt = GameObject.FindGameObjectWithTag("Empaque").GetComponent<ControlPuntos>();
            plt.playEfx(plt.fxs[2]);
            plt.playEfxB();
        }
    }
    private bool searchFF()
    {
        bool fichason = false;
        for (int i = 0; i < 21; i++)
        {
            if (i > 8)
            {
                if (ControlSp[i] == Seed.JANET)
                {
                    fichason = true;
                    break;
                }
            }
        }

        return fichason;
    }
    private void CheckEmptySpaces()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Botones[i].result == 0)
            {
                ControlSp[i] = Seed.EMPTY;
                //Debug.Log("Check Spaces");
            }
        }
        checkSpaces = false;
    }
    private void CleanBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            if (ControlSp[i] != Seed.INUTILIZABLE)
            {
                ControlSp[i] = Seed.EMPTY;
                Botones[i].result = 0;
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Cross, 0, 0);
                CtrlPuntos[i].setContraMark(CtrlPuntos[i].Nought, 0, 0);
                if (i >= 9)
                {
                    break;
                }
            }
        }
        ControlPuntos plt = GameObject.FindGameObjectWithTag("Empaque").GetComponent<ControlPuntos>();
        plt.playEfx(plt.fxs[2]);
        plt.playEfxB();
    }
    private void RandomPremios(float limitGift, int idP, int limiteGB, int limiteSP)
    {
        float randomGift = Random.Range(0, 100);

        if (randomGift < limitGift)
        {
            int regaloGB = Random.Range(1, limiteGB);
            int regaloSP = Random.Range(10, limiteSP);

            CtrlPuntos[idP].AnimText(regaloGB, regaloSP, true);
        }
    }
    public void RequestPermss(int id)
    {
        if (canMove)
        {
            Botones[id].SetActns();
        }
    }
}
