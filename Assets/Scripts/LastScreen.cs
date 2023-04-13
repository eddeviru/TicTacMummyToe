using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LastScreen : MonoBehaviour
{
    public TextMeshProUGUI BugsScena;
    public TextMeshProUGUI PtsScena;
    public TextMeshProUGUI TotalBugs;
    public TextMeshProUGUI TotalPts;
    public GameObject Puertas;
    private ControlSun cS;
    private OpenDoors Op;
    private Controlador controlPasaje;
    private ControlAnimation cAn;
    //private AuthManager aMan;
    private float controlScBugs, controlScPts, controlTotBugs, controlTotPts;
    private float coldScBugs, coldScPts, coldTotBugs, coldTotPts;
    private float lerpSpeed;
    public TMP_InputField userNickName;
    public TextMeshProUGUI TextoCard;
    public Image cartaIcon;
    public Sprite[] cartasR;
    private bool sumaPart;
    private bool saveData;

    public GameObject Regalo;

    private void Awake()
    {
        cS = GameObject.FindGameObjectWithTag("Logica").GetComponent<ControlSun>();
        cAn = GameObject.FindGameObjectWithTag("Logica").GetComponent<ControlAnimation>();
        Op = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OpenDoors>();
        controlPasaje = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        userNickName.text = controlPasaje.NamePlayer;
        TextMeshProUGUI[] textos = GetComponentsInChildren<TextMeshProUGUI>();
        Image[] imagenes = GetComponentsInChildren<Image>();

        foreach (TextMeshProUGUI txt in textos)
            txt.CrossFadeAlpha(0,0, false);

        foreach (Image img in imagenes)
            img.CrossFadeAlpha(0,0, false);

        gameObject.SetActive(false);
    }

    private void Start()
    {
        lerpSpeed = 2;
        if (!sumaPart)
        {
            controlPasaje.PartidasJugadas++;
            Debug.Log("partidas jugadas " + controlPasaje.PartidasJugadas);
            sorteoListo();
            sumaPart = true;
        }
    }
    private void Update()
    {
        if (controlScPts != cS.SceneryPts)
        {
            coldScPts = cS.SceneryPts;

            controlScPts = Mathf.Lerp(controlScPts, coldScPts, Time.deltaTime * lerpSpeed);
            PtsScena.text = "<size=70%>In this Match: <size=100%>" + controlScPts.ToString("F0");
        }

        if (controlScBugs != cS.SceneryGB)
        {
            coldScBugs = cS.SceneryGB;

            controlScBugs = Mathf.Lerp(controlScBugs, coldScBugs, Time.deltaTime * lerpSpeed);
            BugsScena.text = "<size=70%>In this Match: <size=100%>" + controlScBugs.ToString("F0");
        }

        if (controlTotPts != cS.ScoreTotalPts)
        {
            coldTotPts = cS.ScoreTotalPts;

            controlTotPts = Mathf.Lerp(controlTotPts, coldTotPts, Time.deltaTime * lerpSpeed);
            TotalPts.text = "<size=70%>Total: <size=100%>" + controlTotPts.ToString("F0");
        }

        if (controlTotBugs != cS.ScoreTotalGB)
        {
            coldTotBugs = cS.ScoreTotalGB;

            controlTotBugs = Mathf.Lerp(controlTotBugs, coldTotBugs, Time.deltaTime * lerpSpeed);
            TotalBugs.text = "<size=70%>Total: <size=100%>" + controlTotBugs.ToString("F0");
        }
    }
    public void ResetScene()
    {
        Puertas.transform.position = cAn.marcoPos;
        iTween.RotateTo(Puertas, cAn.marcoRot, 0f);

        controlPasaje.camPos = cAn.camPos;
        controlPasaje.camRot = cAn.camRot;
        controlPasaje.MarcoPos = cAn.marcoPos;
        controlPasaje.MarcoRot = cAn.marcoRot;

        Op.CerrarPuerta();
        if (!saveData)
        {
            SaveAllData();
        }
        Invoke("LoadGame", 2f);
    }
    public void NextScene()
    {
        Puertas.transform.position = cAn.marcoPos;
        iTween.RotateTo(Puertas, cAn.marcoRot, 0f);

        Op.CerrarPuerta();
        if(!saveData)
        {
            SaveAllData();
        }
        Invoke("LoadMenu", 2f);
        GameObject.FindGameObjectWithTag("Bafle").GetComponent<Tunes>().StopSong();
    }
    private void LoadGame()
    {
        SceneManager.LoadScene("DueloScene");
    }
    private void LoadMenu()
    {
        SceneManager.LoadScene("BeginScene");
    }
    //private void SearchAuthManager()
    //{
    //    aMan = GameObject.FindGameObjectWithTag("pasaje").GetComponent<AuthManager>();
    //}
    private void SaveAllData()
    {
        //SearchAuthManager();

        //if (aMan.FirebaseConnected)
        //{
        //    controlPasaje.goldenBugs = cS.ScoreTotalGB;
        //    controlPasaje.sunPointsMonth = cS.ScoreTotalPts;
        //    controlPasaje.sunPointsTotal = controlPasaje.sunPointsTotal + cS.SceneryPts;
        //    controlPasaje.NamePlayer = userNickName.text;
        //    controlPasaje.tutorial = false;
        //    //Debug.Log("paso save Last Screen");
        //    controlPasaje.SaveDataS();
        //    saveData = true;
        //}
    }
    private void sorteoListo()
    {
        float randosorteo = Random.Range(0, 100);

        if (randosorteo < 10)
        {
            RegaloCartas(true);
        }
        if (randosorteo <= 10)
        {
            //noup
            Debug.Log("no hay regalo");
        }
    }
    private void RegaloCartas(bool Activo)
    {
        if (Activo)
        {
            int ruleta = Random.Range(0, 100);
            if (ruleta > 70)
            {
                Regalo.SetActive(true);
                int card = Random.Range(0, 9);
                if(card == 0 || card == 5)
                {
                    setcarta(0, " a Change a Mummy mark card.");
                    controlPasaje.Cambio1Ficha ++;
                    return;
                }
                if (card == 1 || card == 6)
                {
                    setcarta(1, " a Put a ghost mark card");
                    controlPasaje.FichaFantasma++;
                    return;
                }
                if (card == 2 || card == 7)
                {
                    setcarta(2, " a 1000 Sun Points offering card");
                    controlPasaje.ObstaculoP1000++;
                    return;
                }
                if (card == 3 || card == 8)
                {
                    setcarta(3, " a Remove a Platform card");
                    controlPasaje.OffPanel++;
                    return;
                }
                if (card == 4 || card == 9)
                {
                    setcarta(4, " a Remove a Mummy mark card");
                    controlPasaje.OffFicha++;
                    return;
                }
            }
            else
                Activo = false;
        }
    }
    private void setcarta(int carta, string cartaText)
    {
        Regalo.SetActive(true);
        TextoCard.text = "You found " + cartaText;
        cartaIcon.sprite = cartasR[carta];
    }
}