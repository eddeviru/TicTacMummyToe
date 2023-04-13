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
