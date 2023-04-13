using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelTienda : MonoBehaviour
{
    public Sprite[] Content;
    public Sprite[] commercal;
    public Image pantallaCommer;
    private float tmerCommercal;
    private int currentComm;
    private GameObject[] Container;
    public GameObject PantallaGold;
    public GameObject PantallaFailed;
    private int currentContainer;
    private Returno tr;
    private Controlador controlPasaje;
    //private AuthManager aMan;
    public TextMeshProUGUI totalGold;
    private int controlBugs;
    public string[] Descrpton;
    public int[] valor1;
    public string[] HowToUseText;
    public GameObject btnPfb;
    public Transform refer;
    public GameObject BtnAds;
    public GameObject[] ptnl;
    public GameObject AdsGO;
    //private IniiAd ObjAds;

    [Header ("GoldenBugs")]
    public GameObject pntGoldBg;
    public Image imgToGold;
    public TextMeshProUGUI textoDsc;
    public Sprite[] goldM;
    public GameObject[] cards;
    public Sprite[] cardsImg;

    public TextMeshProUGUI GoldBgs;

    private void Awake()
    {
        //if (GameObject.FindGameObjectWithTag("Ads") != null)
        //{
        //    ObjAds = GameObject.FindGameObjectWithTag("Ads").GetComponent<IniiAd>();
        //    ObjAds.inInicio = true;
        //    ObjAds.inDuelo = false;
        //    ObjAds._showAdButton = BtnAds.GetComponent<Button>();
        //    ObjAds.SearchAssets();
        //}

        //if (GameObject.FindGameObjectWithTag("Ads") == null)
        //{
        //    Instantiate(AdsGO, transform.position, transform.rotation);

        //    ObjAds = GameObject.FindGameObjectWithTag("Ads").GetComponent<IniiAd>();
        //    ObjAds.inInicio = true;
        //    ObjAds.inDuelo = false;
        //    ObjAds._showAdButton = BtnAds.GetComponent<Button>();
        //    ObjAds.SearchAssets();
        //}
        Container = new GameObject[Content.Length];
        PutButton();
        tr = GetComponent<Returno>();
        tr.FadeScreen(0,0);
        gameObject.SetActive(false);
        //aMan = GameObject.FindGameObjectWithTag("pasaje").GetComponent<AuthManager>();

        Image[] imgGGold = pntGoldBg.GetComponentsInChildren<Image>();
        TextMeshProUGUI[] txtGGold = pntGoldBg.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (Image img in imgGGold)
            img.CrossFadeAlpha(0,0, false);

        foreach (TextMeshProUGUI txts in txtGGold)
            txts.CrossFadeAlpha(0,0,false);

        Invoke("setNoActivepntGg", 0.1f);
        XardsUnActive();
    }
        
    private void setNoActivepntGg()
    {
        pntGoldBg.SetActive(false);
    }

    private void Start()
    {
        controlPasaje = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
        //Debug.Log(Container.Length);
        //if (aMan.FirebaseConnected)
        //{
        //    BtnAds.SetActive(true);
        //}
        //if (!aMan.FirebaseConnected)
        //{
        //    BtnAds.SetActive(false);
        //}
    }

    private void Update()
    {
        if (controlPasaje.goldenBugs != controlBugs)
        {
            totalGold.text = controlPasaje.goldenBugs.ToString();
            controlBugs = controlPasaje.goldenBugs;
        }

        tmerCommercal += Time.deltaTime;
        if (tmerCommercal > 0.2f)
        {
            if (currentComm >= commercal.Length)
            {
                currentComm = 0;
            }
            pantallaCommer.sprite = commercal[currentComm];
            currentComm ++;
            tmerCommercal = 0;
        }
    }

    private void PutButton()
    {
        for (int i = 0; i < Content.Length; i++)
        {
            if (valor1[i] != 0)
            {
                GameObject boton = (GameObject)Instantiate(btnPfb, refer.transform.position, refer.transform.rotation);
                boton.transform.SetParent(refer.transform);
                boton.transform.localScale = new Vector3(1, 1, 1);
                boton.GetComponent<BotonComprar>().Item.sprite = Content[i];
                boton.GetComponent<BotonComprar>().description.text = Descrpton[i];
                boton.GetComponent<BotonComprar>().carta = i;
                boton.GetComponent<BotonComprar>().HowToUseT.text = HowToUseText[i];
                boton.GetComponent<BotonComprar>().valorCarta = valor1[i];
                boton.GetComponent<BotonComprar>().NCards = PantallaGold;
                Container[i] = boton;

                if (i == 0)
                {
                    boton.transform.localPosition = new Vector3(0, 0, 0);
                }

                if (i != 0)
                {
                    boton.transform.localPosition = new Vector3(-1180f, 0, 0);
                }

            }
        }
    }

    public void nextCard()
    {
        if (currentContainer < Container.Length - 1)
        {
            StartCoroutine(PaseDelante(currentContainer + 1));
        }
        if (currentContainer >= Container.Length - 1)
        {
            StartCoroutine(PaseDelante(0));
        }
    }

    public void previousCard()
    {
        if (currentContainer > 0)
        {
            StartCoroutine(PaseAtras(currentContainer - 1));
        }
        if (currentContainer <= 0)
        {
            StartCoroutine(PaseAtras(Container.Length - 1));
        }
    }

    private IEnumerator PaseDelante(int pista2)
    {
        moverContenedor(pista2, 0);
        Container[pista2].GetComponent<BotonComprar>().cardPanel = true;
        for (int i = 0; i < Container.Length; i++)
        {
            if (i != pista2)
            {
                moverContenedor(i, -1180f);
                Container[i].GetComponent<BotonComprar>().cardPanel = true;
            }
        }

        yield return new WaitForSeconds(0.01f);
        if (currentContainer < 8)
            currentContainer++;

        if (currentContainer > 7)
            currentContainer = 0;
    }

    private IEnumerator PaseAtras(int pista2)
    {
        moverContenedor(pista2, 0);
        Container[pista2].GetComponent<BotonComprar>().cardPanel = true;
        for (int i = 0; i < Container.Length; i++)
        {
            if (i != pista2)
            {
                moverContenedor(i, -1180f);
                Container[i].GetComponent<BotonComprar>().cardPanel = true;
            }
        }

        yield return new WaitForSeconds(0.01f);
        if (currentContainer > -1)
            currentContainer--;

        if (currentContainer < 0)
            currentContainer = Container.Length - 1;
    }

    //public void ShowAds()
    //{
    //    ObjAds.ShowAd();
    //}

    private void moverContenedor(int baseCarrer, float moveX)
    {
        iTween.MoveTo(Container[baseCarrer], iTween.Hash("x", moveX, "time", 0.5f, "islocal", true));
    }

    public void ActBuyGoldBBtn()
    {
        //  Activar pantalla BuyGold
        iTween.MoveTo(ptnl[0], iTween.Hash("x", 2378f, "time", 0.6f, "islocal", true));
        iTween.MoveTo(ptnl[1], iTween.Hash("x", 3.6f, "time", 0.6f, "islocal", true));
        foreach (GameObject ctn in Container)
            ctn.GetComponent<BotonComprar>().cardPanel = false;
    }

    public void ActBuyCardBtn()
    {
        //  Activar pantalla Cartas
        iTween.MoveTo(ptnl[1], iTween.Hash("x", 2378f, "time", 0.6f, "islocal", true));
        iTween.MoveTo(ptnl[0], iTween.Hash("x", 3.6f, "time", 0.6f, "islocal", true));
        foreach (GameObject ctn in Container)
            ctn.GetComponent<BotonComprar>().cardPanel = true;
    }

    public void XardsUnActive()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetActive(false);
        }
    }

    public void RouletteCard(int timesRoulette)
    {
        for (int i = 0; i < timesRoulette; i++)
        {
            int cardN = Random.Range(0, cardsImg.Length);
            cards[i].SetActive(true);
            cards[i].GetComponent<CartaGanadora>().SetCromo(cardsImg[cardN]);
            if (cardN == 0)
            {
                controlPasaje.ObstaculoP1000 += 1;
            }
            if (cardN == 1)
            {
                controlPasaje.ObstaculoP2000 += 1;
            }
            if (cardN == 2)
            {
                controlPasaje.IntercambioAllFichas += 1;
            }
            if (cardN == 3)
            {
                controlPasaje.CambioMummyFichas += 1;
            }
            if (cardN == 4)
            {
                controlPasaje.FichaFantasma += 1;
            }
            if (cardN == 5)
            {
                controlPasaje.OffFicha += 1;
            }
            if (cardN == 6)
            {
                controlPasaje.OffPanel += 1;
            }
            if (cardN == 7)
            {
                controlPasaje.Cambio1Ficha += 1;
            }
        }
    }
}