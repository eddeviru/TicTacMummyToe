using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Botones : MonoBehaviour
{
    public bool[] cartas;
    public int cartasInv;
    public int idCard;
    public Image Victima;
    public Sprite intAllF, ObsP2000, ObsP1000, fFants, camAllF, cam1F, offF, OffPn, noCard;
    private Logica logic;
    private ControladorAereo cA;
    private ControlAnimation cAn;
    public TextMeshProUGUI[] Textos;
    public GameObject Boton;
    public int tamanoCard;
    private string cardSingle;
    public bool disponible, checkeo;
    public GameObject Contenedor;


    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logica").GetComponent<Logica>();
        cAn = GameObject.FindGameObjectWithTag("Logica").GetComponent<ControlAnimation>();
        cA = GetComponentInParent<ControladorAereo>();

    }

    public void Botonazo()
    {
        //Intercambia Todas las marcas
        if (cartas[0])
        {
            logic.intAllMarks = true;
            Boton.SetActive(false);
            logic.removePlace = logic.retrocesoMark = logic.changeMark = logic.changeAllMarks = logic.ghostMark = logic.offerx1000 = logic.offerx2000 = logic.normalTurn = false;
            cA.SetGame(false);
        }
        // ofrenda x 1000
        if (cartas[1])
        {
            logic.offerx1000 = true;
            Boton.SetActive(false);
            logic.removePlace = logic.retrocesoMark = logic.changeMark = logic.changeAllMarks = logic.ghostMark = logic.offerx2000 = logic.intAllMarks = logic.normalTurn = false;
            cA.SetGame(false);
        }
        //ofrenda x 2000
        if (cartas[2])
        {
            logic.offerx2000 = true;
            Boton.SetActive(false);
            logic.removePlace = logic.retrocesoMark = logic.changeMark = logic.changeAllMarks = logic.ghostMark = logic.offerx1000 = logic.intAllMarks = logic.normalTurn = false;
            cA.SetGame(false);
        }
        // ficha fantasma
        if (cartas[3])
        {
            logic.ghostMark = true;
            Boton.SetActive(false);
            logic.removePlace = logic.retrocesoMark = logic.changeMark = logic.changeAllMarks = logic.offerx1000 = logic.offerx2000 = logic.intAllMarks = logic.normalTurn = false;
            logic.camboMat = true;
            cA.SetGame(false);
        }
        //cambiar todas las cartas enemigas
        if (cartas[4])
        {
            logic.changeAllMarks = true;
            Boton.SetActive(false);
            logic.removePlace = logic.retrocesoMark = logic.changeMark = logic.ghostMark = logic.offerx1000 = logic.offerx2000 = logic.intAllMarks = logic.normalTurn = false;
            cA.SetGame(false);
        }
        //cambiar una marca
        if (cartas[5])
        {
            logic.changeMark = true;
            Boton.SetActive(false);
            logic.removePlace = logic.retrocesoMark = logic.changeAllMarks = logic.ghostMark = logic.offerx1000 = logic.offerx2000 = logic.intAllMarks = logic.normalTurn = false;
            cA.SetGame(false);
        }
        //remover una ficha
        if (cartas[6])
        {
            logic.retrocesoMark = true;
            Boton.SetActive(false);
            logic.removePlace = logic.changeMark = logic.changeAllMarks = logic.ghostMark = logic.offerx1000 = logic.offerx2000 = logic.intAllMarks = logic.normalTurn = false;
            cA.SetGame(false);
        }
        //remover una base
        if (cartas[7])
        {
            logic.removePlace = true;
            Boton.SetActive(false);
            logic.retrocesoMark = logic.changeMark = logic.changeAllMarks = logic.ghostMark = logic.offerx1000 = logic.offerx2000 = logic.intAllMarks = logic.normalTurn = false;
            cA.SetGame(false);
        }
        cA.SonidoClick();
    }
    private void Update()
    {
        if (cAn.tutorial)
        {
            if (idCard != 3)
            {
                Contenedor.SetActive(false);
            }
        }

        if (tamanoCard != cartasInv)
        {
            if (cartasInv <= 0)
            {
                //desabilitar boton
                Textos[0].text = "x" + cartasInv.ToString() + cardSingle;

                Boton.GetComponent<Button>().enabled = false;
            }
            if (cartasInv > 1)
            {
                cardSingle = " Cards";
                Textos[0].text = "x" + cartasInv.ToString() + cardSingle;
                Boton.GetComponent<Button>().enabled = true;
                //Debug.Log("ok, mayor a 1");
            }
            if (cartasInv == 1)
            {
                cardSingle = " Card";
                Textos[0].text = "x" + cartasInv.ToString() + cardSingle;
                Boton.GetComponent<Button>().enabled = true;
                //Debug.Log("ok, igual a 1");
            }
            tamanoCard = cartasInv;
        }

        if (checkeo)
        {
            cartasInv = cA.Cards[idCard];
            SetTextos();
            checkeo = false;
        }

    }

    public void SetTextos()
    {

        if (cartas[0] && cartasInv > 0)
        {
            Victima.sprite = intAllF;
            Textos[2].text = "Change all marks on the board";
        }
        if (cartas[1] && cartasInv > 0)
        {
            Victima.sprite = ObsP1000;
            Textos[2].text = "Put an offering and recieve +1000 Points & +20 Gold Bugs";
        }
        if (cartas[2] && cartasInv > 0)
        {
            Victima.sprite = ObsP2000;
            Textos[2].text = "Put an offering and recieve +2000 Points & +45 Gold Bugs";
        }
        if (cartas[3] && cartasInv > 0)
        {
            Victima.sprite = fFants;
            Textos[2].text = "Put a ghost mark outside the board, not apply for diagonal row";
        }
        if (cartas[4] && cartasInv > 0)
        {
            Victima.sprite = camAllF;
            Textos[2].text = "Change all Mummy marks for yours";
        }
        if (cartas[5] && cartasInv > 0)
        {
            Victima.sprite = cam1F;
            Textos[2].text = "Change a Mummy mark for one of yours";
        }
        if (cartas[6] && cartasInv > 0)
        {
            Victima.sprite = offF;
            Textos[2].text = "Remove a Mummy mark";
        }
        if (cartas[7] && cartasInv > 0)
        {
            Victima.sprite = OffPn;
            Textos[2].text = "Remove a spot in the board";
        }
        if (cartasInv <= 0)
        {
            Victima.sprite = noCard;
            Textos[2].text = "No more cards. Go to the shop and get new ones";
        }
    }
}
