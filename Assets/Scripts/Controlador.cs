using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    public int IntercambioAllFichas, ObstaculoP1000, ObstaculoP2000, FichaFantasma, CambioMummyFichas, Cambio1Ficha, OffFicha, OffPanel;
    private int IntercambioAllFichasCon, ObstaculoP1000Con, ObstaculoP2000Con, FichaFantasmaCon, CambioMummyFichasCon, Cambio1FichaCon, OffFichaCon, OffPanelCon;
    public int goldenBugs, sunPointsTotal, sunPointsMonth;
    private int goldenBugsCon, sunPointsTotalCon, sunPointsMonthCon;
    //private int controlB;
    public string NamePlayer;
    private string NamePlayerCon;
    public float volumeSoundInGame;
    public bool tutorial;
    public GameObject pantallaError;
    public bool anonym;
    public string usurioMuestra;

    //pagodelaluz
    public int GBex3, GBex2k, GBex5k;
    private int GBex3Con, GBex2kCon, GBex5kCon;

    public List<PlayerPosF> ListaFinal = new List<PlayerPosF>();
    public int limiteAnt, limiteFin;
    public bool checkLeaderBoard;
    public bool listaNueva;
    public float timerLeaderBoard;

    //private AuthManager aM;
    //private DatabaseManager RunnerCon;
    public int[] Foto;
    public int puestoJuegador;

    public int PartidasJugadas;
    private Vector3 posMarco, posCam, rotMarco, rotCam;

    public Vector3 camPos;
    public Vector3 camRot;

    public Vector3 MarcoPos;
    public Vector3 MarcoRot;
    public string versioTxt;

    private void Awake()
    {
        versioTxt = "v" + Application.version;
        DontDestroyOnLoad(this.gameObject);
        //aM = GetComponent<AuthManager>();
        //controlB = 1;

        posMarco = new Vector3(-0.974f, 2.83f, 50.7769f);
        posCam = new Vector3(-0.97f, 5.19f, 53.49f);
        rotMarco = new Vector3(-99f, 0, 0);
        rotCam = new Vector3(0, 180f, 0);
    }

    private void Update()
    {
        //if (aM.pasoLogeo)
        //{
        //    timerLeaderBoard += Time.unscaledDeltaTime;
        //    if (timerLeaderBoard > 300)
        //    {
        //        checkLeaderBoard = true;
        //        listaNueva = true;
        //    }
        //}
    }

    public void SetData(CardData cardData)
    {
        IntercambioAllFichas = cardData.IntercambioAllFichas;
        ObstaculoP1000 = cardData.ObstaculoP1000;
        ObstaculoP2000 = cardData.ObstaculoP2000;
        FichaFantasma = cardData.FichaFantasma;
        CambioMummyFichas = cardData.CambioMummyFichas;
        Cambio1Ficha = cardData.Cambio1Ficha;
        OffFicha = cardData.OffFicha;
        OffPanel = cardData.OffPanel;
        volumeSoundInGame = cardData.Volumen;

        IntercambioAllFichasCon = cardData.IntercambioAllFichas;
        ObstaculoP1000Con = cardData.ObstaculoP1000;
        ObstaculoP2000Con = cardData.ObstaculoP2000;
        FichaFantasmaCon = cardData.FichaFantasma;
        CambioMummyFichasCon = cardData.CambioMummyFichas;
        Cambio1FichaCon = cardData.Cambio1Ficha;
        OffFichaCon = cardData.OffFicha;
        OffPanelCon = cardData.OffPanel;
    }

    public void SetScore(ScoreData scoreData)
    {
        sunPointsTotal = scoreData.sunPointsTotal;
        sunPointsMonth = scoreData.sunPointsMonth;
        NamePlayer = scoreData.NamePlayer;
        goldenBugs = scoreData.goldenBugs;

        sunPointsTotalCon = scoreData.sunPointsTotal;
        sunPointsMonthCon = scoreData.sunPointsMonth;
        NamePlayerCon = scoreData.NamePlayer;
        goldenBugsCon = scoreData.goldenBugs;        
    }

    public void GetData(CardData cardData)
    {
        cardData.IntercambioAllFichas = IntercambioAllFichas;
        cardData.ObstaculoP1000 = ObstaculoP1000;
        cardData.ObstaculoP2000 = ObstaculoP2000;
        cardData.FichaFantasma = FichaFantasma;
        cardData.CambioMummyFichas = CambioMummyFichas;
        cardData.Cambio1Ficha = Cambio1Ficha;
        cardData.OffFicha = OffFicha;
        cardData.OffPanel = OffPanel;
        cardData.Volumen = volumeSoundInGame;
    }

    public void SetLaLuz(laLuz laluz)
    {
        GBex3 = laluz.GBx3;
        GBex2k = laluz.GBx2k;
        GBex5k = laluz.GBx5k;

        GBex3Con = laluz.GBx3;
        GBex2kCon = laluz.GBx2k;
        GBex5kCon = laluz.GBx5k;
    }

    public void GetScore(ScoreData scoreData)
    {
        scoreData.sunPointsTotal = sunPointsTotal;
        scoreData.sunPointsMonth = sunPointsMonth;
        scoreData.NamePlayer = NamePlayer;
        scoreData.goldenBugs = goldenBugs;
    }

    public void setLimits(int primerLimit, int sujetoi)
    {
        if (sujetoi < 20)
        {
            limiteAnt = 0;
            limiteFin = 19;
        }
        if (sujetoi > 9 && sujetoi < primerLimit - 20)
        {
            limiteAnt = sujetoi - 10;
            limiteFin = sujetoi + 10;
        }

        if (sujetoi > primerLimit - 20)
        {
            limiteAnt = primerLimit - 20;
            limiteFin = primerLimit;
        }
        //Debug.Log("limite atras " + limiteAnt + " sujeto " + sujetoi + " limite final " + limiteFin);
    }

    private void BusquedaDePantalla()
    {
        pantallaError = GameObject.Find("ErrorGame");
    }
    public void LoadDataS()
    {
        //RunnerCon = GameObject.FindGameObjectWithTag("corredor").GetComponent<DatabaseManager>();
        //OnDataRecieved();
    }

    //public void UnsubscribeData()
    //{
    //    RunnerCon = GameObject.FindGameObjectWithTag("corredor").GetComponent<DatabaseManager>();
    //    RunnerCon.OffDataGame();
    //}
    //private void OnDataRecieved()
    //{
    //    RunnerCon.userid = aM.userUID;
    //    RunnerCon.GetDataGame();
    //    //Debug.Log("sacando datos");
    //}
    public void SaveDataS()
    {
        //Debug.Log("Salva datos en controaldor   ");
        //DatabaseManager database = GameObject.FindGameObjectWithTag("corredor").GetComponent<DatabaseManager>();

        //database.SetDataBase();

        //    CardData datosCartas = new CardData(IntercambioAllFichas, ObstaculoP1000, ObstaculoP2000, FichaFantasma, CambioMummyFichas, Cambio1Ficha, OffFicha, OffPanel, volumeSoundInGame);
        //    ScoreData datosScore = new ScoreData(sunPointsTotal, sunPointsMonth, NamePlayer, goldenBugs);
        //    laLuz datosLaLu = new laLuz(GBex3, GBex2k, GBex5k);
        //    string pers = aM.userUID;

        //    if (IntercambioAllFichas != IntercambioAllFichasCon || ObstaculoP1000 != ObstaculoP1000Con || ObstaculoP2000 != ObstaculoP2000Con || FichaFantasma != FichaFantasmaCon || CambioMummyFichas != CambioMummyFichasCon || Cambio1Ficha != Cambio1FichaCon || OffFicha != OffFichaCon || OffPanel != OffPanelCon)
        //    {
        //        database.SetDataGame(datosCartas, pers);
        //    }

        //    if (goldenBugs != goldenBugsCon || sunPointsMonth != sunPointsMonthCon || sunPointsTotal != sunPointsTotalCon || NamePlayer != NamePlayerCon)
        //    {
        //        database.SetScoreGame(datosScore, pers);
        //    }

        //    if (GBex3 != GBex3Con || GBex2k != GBex2kCon || GBex5k != GBex5kCon)
        //    {
        //        database.SetLaLuzGame(datosLaLu, pers);
        //    }
    }

    public void SetPoston()
    {
        camPos = posCam;
        camRot = rotCam;
        MarcoPos = posMarco;
        MarcoRot = rotMarco;
    }

    public void OnError()
    {
        BusquedaDePantalla();
        Debug.Log("Something Happen!");
        pantallaError.transform.localScale = new Vector3(1, 1, 1);
    }

}
