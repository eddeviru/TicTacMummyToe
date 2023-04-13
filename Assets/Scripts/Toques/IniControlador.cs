using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IniControlador : MonoBehaviour
{
    public GameObject passage;
    public GameObject corredor;
    public TextMeshProUGUI textNternet;
    private float pasoErr;
    private bool pasoRunnerPasaje;
    [SerializeField]
    private GameObject pasajeCon;
    [SerializeField]
    //private DatabaseManager RunnerCon;
    public string userID = "xx-1";
    public string screenTo;
    private bool checkDataTut;
    public string[] nombreDePrueba;
    public TextMeshProUGUI textVers;

    private void Start()
    {
        StartCoroutine(PasoAPaso());
    }

    private IEnumerator PasoAPaso()
    {
        if (pasajeCon == null)
        {
            checkPasajeroRunner();
            
        }

        yield return new WaitForSeconds(0.2f);

        SetName();
        textVers.text = pasajeCon.GetComponent<Controlador>().versioTxt;

        yield return new WaitForSeconds(0.2f);

        //if (pasajeCon.GetComponent<AuthManager>().FirebaseConnected)
        //{
        //    RunnerCon.SetDataBase();

        //    yield return new WaitForSeconds(0.5f);
            
        //    //Debug.Log("pasoFirebase");
        //    userID = pasajeCon.GetComponent<AuthManager>().userUID;
        //    pasajeCon.GetComponent<Controlador>().LoadDataS();

        //    yield return new WaitUntil(predicate: () => pasajeCon.GetComponent<AuthManager>().pasodbi);

        //    yield return new WaitForSeconds(2f);
            
        //    RunnerCon.OffDataGame();
        //    ChecaData();

        //    yield return new WaitUntil(predicate: () => checkDataTut);
        //    //Go to Game
        //    GoScene(screenTo);
        //}


        //if (!pasajeCon.GetComponent<AuthManager>().FirebaseConnected)
        //{
            textNternet.text = "No internet connection";
            ChecaData();

            yield return new WaitUntil(predicate: () => checkDataTut);
            //Go to Game
            GoScene(screenTo);
        //}
    }
    private void SetName()
    {
        int valNombre = Random.Range(0, nombreDePrueba.Length);
        pasajeCon.GetComponent<Controlador>().NamePlayer = nombreDePrueba[valNombre];
    }
    private void checkPasajeroRunner()
    {
        if (pasajeCon != null)
        {
            pasoRunnerPasaje = true;
            return;
        }

        if (GameObject.FindGameObjectWithTag("pasaje") != null || pasajeCon == null)
        {
            pasajeCon = GameObject.FindGameObjectWithTag("pasaje");
        }


        if (pasajeCon == null && GameObject.FindGameObjectWithTag("pasaje") == null)
        {
            GameObject GoPs = Instantiate(passage, new Vector3(0, 0, 0), transform.rotation);
            pasajeCon = GoPs;
        }

        if (pasajeCon == null)
        {
            //error
            pasoRunnerPasaje = false;
        }
    }

    private void ChecaData()
    {
        //if (!RunnerCon.setError)
        //{
            if (pasajeCon.GetComponent<Controlador>().sunPointsTotal > 0)
            {
                pasajeCon.GetComponent<Controlador>().tutorial = false;
                screenTo = "BeginScene";
                //RunnerCon.OffDataGame();
                checkDataTut = true;
                return;
            }
            if (pasajeCon.GetComponent<Controlador>().sunPointsTotal <= 0)
            {
                pasajeCon.GetComponent<Controlador>().SetPoston();
                pasajeCon.GetComponent<Controlador>().tutorial = true;
                screenTo = "DueloScene";
                //RunnerCon.OffDataGame();
                checkDataTut = true;
            }
        //}
        else
        {
            pasajeCon.GetComponent<Controlador>().OnError();
        }
    }

    private void GoScene(string scen)
    {
        SceneManager.LoadScene(scen);
        //Debug.Log("completo, llama " + scen);
    }

    private void Update()
    {
        if (!pasoRunnerPasaje && pasajeCon == null)
        {
            pasoErr += Time.deltaTime;

            if (pasoErr > 10)
            {
                GameObject.Find("ErrorGame").transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

}
