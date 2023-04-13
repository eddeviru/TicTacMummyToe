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
    }
}