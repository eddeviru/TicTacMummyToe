using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LeaderBoard : MonoBehaviour
{
    private Controlador controlP;
    //private AuthManager aMan;    
   //private DatabaseManager dataB;
    public GameObject Person;
    public GameObject referencia;
    public GameObject advce;

    private void Start()
    {
        controlP = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        //dataB = GameObject.FindGameObjectWithTag("corredor").GetComponent<DatabaseManager>();

        ChecNtnernet();
    }

    public void ChecNtnernet()
    {
        //aMan = GameObject.FindGameObjectWithTag("pasaje").GetComponent<AuthManager>();

        //if (aMan.FirebaseConnected)
        //{
        //    SacarLsta();
        //    advce.SetActive(false);
        //}
        //if (!aMan.FirebaseConnected)
        //{
        //    advce.SetActive(true);
        //}
    }

    private void SacarLsta()
    {
        //if (controlP.checkLeaderBoard && controlP.listaNueva)
        //{
        //    StartCoroutine(onDataB());
        //    return;
        //}

        OrganizarJug();
    }

    //private IEnumerator onDataB()
    //{
    //    dataB.SetDataLeaderBoard();

    //    yield return new WaitUntil(predicate: () => dataB.PuedeApagar == true);

    //    dataB.OffDataLeaderBoard();
    //    setdatosPl();
    //}
    //26038
    private void OrganizarJug()
    {
        for (int i = 0; i < controlP.ListaFinal.Count; i++)
        {
            if (controlP.ListaFinal[i].playerName == controlP.NamePlayer)
            {
                if (controlP.ListaFinal[i].sunPointMonth != controlP.sunPointsMonth)
                {
                    //Reorganizar lista
                    controlP.ListaFinal[i] = new PlayerPosF(controlP.NamePlayer, controlP.sunPointsMonth, controlP.sunPointsTotal, i);
                    Debug.Log("Organ;zar jugadores");
                    organizarLista();
                    return;
                }
            }
        }
        setdatosPl();
    }

    private void organizarLista()
    {
        controlP.ListaFinal.Sort((x, y) => x.sunPointMonth.CompareTo(y.sunPointMonth));
        controlP.ListaFinal.Reverse();
        setdatosPl();
    }

    private void setdatosPl()
    {
        for (int i = controlP.limiteAnt; i < controlP.limiteFin; i++)
        {
            GameObject person = Instantiate(Person, new Vector3(0, 0, 0), transform.rotation);
            person.transform.SetParent(referencia.transform);
            person.transform.localScale = new Vector3(1, 1, 1);
            person.transform.localPosition = new Vector3(0, i * -255f, 0);
            person.GetComponent<SetDataPlayers>().position = i + 1;
            person.GetComponent<SetDataPlayers>().namePl = controlP.ListaFinal[i].playerName;
            person.GetComponent<SetDataPlayers>().sunPointM = controlP.ListaFinal[i].sunPointMonth;
            person.GetComponent<SetDataPlayers>().sunPointT = controlP.ListaFinal[i].sunPointTotal;
            person.GetComponent<SetDataPlayers>().setData();
            if (controlP.NamePlayer == controlP.ListaFinal[i].playerName && controlP.sunPointsMonth == controlP.ListaFinal[i].sunPointMonth && controlP.sunPointsTotal == controlP.ListaFinal[i].sunPointTotal)
            {
                person.GetComponent<SetDataPlayers>().PlayerFlag.SetActive(true);
            }
        }
        controlP.listaNueva = false;
        controlP.checkLeaderBoard = false;
        //dataB.PuedeApagar = false;
    }
}