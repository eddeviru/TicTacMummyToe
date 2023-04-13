using UnityEngine;
using TMPro;

public class ControlPuntos : MonoBehaviour
{
    private Logica logic;
    private ControlSun cS;
    public bool obstaculo;
    public GameObject[] Iconos;
    public GameObject CrossTr;
    public GameObject NoughtTr;
    public ParticleSystem[] vfxStay;
    private GameObject elegido;
    public float[] dirGiro = new float[4];
    private int loteriaG;
    public bool StopMove;
    public GameObject Cross;
    public GameObject Nought;
    public ParticleSystem Luciernagas;
    public ParticleSystem stonesFicha;
    public GameObject PisoBrillante;
    private ParticleSystem PsoPartcles;
    private float NoiseCont = 30;
    private float timeEn;
    public bool efxEcima;
    private float timeMa;
    public bool matDss;
    private float velGiro;
    public bool CanPlay;
    private int loteria;
    private GameObject[] Arenas;
    public GameObject[] Piedras;
    public TextMeshPro ScoreTxt;
    public TextMeshPro BetleTxt;
    private bool setScoreVisual;
    public bool ArenasEfx;
    public bool pasoPuntaje;
    public GameObject manoMummy;
    public bool putObst;
    private AudioSource AudioFXG;
    public AudioSource AudioFXB;
    public AudioSource AudioFXS;
    public AudioClip[] fxs;
    private bool audioMatDss;

    public GameObject Flecha;
    public GameObject ContenedorPuntaje;
    public int ControlMesa;
    private void Start()
    {
        logic = GetComponentInParent<Logica>();
        cS = GetComponentInParent<ControlSun>();
        PsoPartcles = PisoBrillante.GetComponent<ParticleSystem>();
        float setIcon = UnityEngine.Random.Range(0, 100);
        manoMummy.SetActive(false);
        loteriaG = UnityEngine.Random.Range(0, dirGiro.Length);
        AudioFXG = gameObject.GetComponent<AudioSource>();
        AudioFXB = GameObject.Find("LxBase").GetComponent<AudioSource>();
        AudioFXS = GameObject.Find("LxSacrificio").GetComponent<AudioSource>();
        Arenas = GameObject.FindGameObjectsWithTag("Arena");

        float randomPiso = UnityEngine.Random.Range(50f, 90f);

        if (!putObst)
        {
            SetIconOn(setIcon, randomPiso);
        }

        matDss = false;
        CanPlay = true;
        timeMa = 0;
        //SetRotationBox();
    }
    /*private void SetRotationBox()
    {
        if (ControlMesa < 9)
        {
            ContenedorPuntaje.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1f);
        }
        if (ControlMesa > 8 && ControlMesa < 15 || ControlMesa > 17)
        {
            //Debug.Log(ContenedorPuntaje.transform.localRotation);
            ContenedorPuntaje.transform.localRotation = new Quaternion(-0.7f, 0.0f, 0.0f, 0.7f);
        }
        if (ControlMesa > 14 && ControlMesa < 18)
        {
            //Debug.Log("Rotation world " + ContenedorPuntaje.transform.rotation); 0.0f, 0.0f, 0.0f 1.0f
            //Debug.Log("Rotation Local" + ContenedorPuntaje.transform.localRotation); 0.2f, 0.0f, 1.0f
            ContenedorPuntaje.transform.localRotation = new Quaternion(0.2f, 0.0f, 0.0f, 1.0f);
        }
    }*/
    public void SetIconOn(float sIc, float rnP)
    {
        loteria = UnityEngine.Random.Range(0, Iconos.Length);
        elegido = Iconos[loteria];

        if (sIc >= rnP)
        {
            elegido.SetActive(true);
            //ScoreTxt.text = "+ " + valoresScore[loteria];
            iTween.ScaleTo(PisoBrillante, iTween.Hash("scale", new Vector3(1.03f, 1f, 1f), "time", 1f, "islocal", true));
            Luciernagas.Play();
            iTween.MoveTo(elegido, iTween.Hash("y", 2.28f, "time", 0.5f, "islocal", true));
            AudioFXS.PlayOneShot(fxs[0]);
            CanPlay = true;
            obstaculo = true;
        }
        StopMove = false;
        velGiro = dirGiro[loteriaG];
        MeshRenderer[] MatIconos = elegido.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer icn in MatIconos)
            icn.GetComponent<MeshRenderer>().material.SetFloat("_VelocityDss", 0);

    }
    private void Update()
    {
        if (!StopMove && !putObst)
        {
            elegido.transform.Rotate(Vector3.up * velGiro * Time.deltaTime);
        }

        if (Flecha.activeSelf)
        {
            Flecha.transform.Rotate(Vector3.up * velGiro * Time.deltaTime);
        }

        if (CanPlay)
        {
            if (PisoBrillante.transform.localScale.x == 1.03f)
            {
                iTween.ScaleTo(PisoBrillante, iTween.Hash("scale", new Vector3(0f, 0f, 0f), "time", 1f, "delay", 0.5f));
                Luciernagas.Stop();
            }

            if (efxEcima)
            {
                timeEn += Time.deltaTime;
                if (timeEn >= 0.05f)
                {
                    if (CrossTr.activeSelf)
                    {
                        CrossTr.SetActive(false);
                    }
                    if (NoughtTr.activeSelf)
                    {
                        NoughtTr.SetActive(false);
                    }
                    if (timeEn >= 0.2f)
                    {
                        efxEcima = false;
                    }
                }
            }
        }
        
        if (matDss)
        {
            IconoSetOff(elegido);
            timeMa += Time.deltaTime * 0.1f;

            //Debug.Log(timeMa);
            if (ArenasEfx)
            {
                fallPiedras();
                DisparaEfx();
            }
            if (velGiro != 0)
            {
                //velGiro = dirGiro[loteriaG] / 4f;
                velGiro = Mathf.Lerp(velGiro, 0, Time.deltaTime * 0.8f);
            }
            if (timeMa > 0.1f && timeMa < 0.3f && !setScoreVisual)
            {
                if (pasoPuntaje)
                {
                    AnimText(0,0, false);
                    pasoPuntaje = false;
                }
            }
            if (timeMa > 0.6f && timeMa < 0.65f)
            {
                DetenEfx();

            }
            if (timeMa >= 1)
            {
                timeMa = 0;
                audioMatDss = false;
                matDss = false;
            }
        }
        if (setScoreVisual)
        {
            iTween.ScaleTo(ScoreTxt.gameObject, iTween.Hash("scale", new Vector3(0, 0, 0), "time", 0.5f, "delay", 1f));
            iTween.MoveTo(ScoreTxt.gameObject, iTween.Hash("position", new Vector3(0, 2.99f, 0), "time", 0.1f, "islocal", true, "delay", 1.5f));
            iTween.ScaleTo(BetleTxt.gameObject, iTween.Hash("scale", new Vector3(0, 0, 0), "time", 0.5f, "delay", 1f));
            iTween.MoveTo(BetleTxt.gameObject, iTween.Hash("position", new Vector3(0, 2.99f, 0), "time", 0.1f, "islocal", true, "delay", 1.5f));
            setScoreVisual = false;
        }

    }
    private void IconoSetOff(GameObject Icono)
    {
        // disolve icon
        // _ControllerDss Dissolve Controller = 30
        // _VelocityDss Dissolve Velocity 0 a 1
        
        MeshRenderer[] MatIconos = Icono.GetComponentsInChildren<MeshRenderer>();

        if (NoiseCont == 30)
        {
            NoiseCont = UnityEngine.Random.Range(15, 60);
        }
        
        foreach (MeshRenderer icn in MatIconos)
        {
            icn.material.SetFloat("_ControllerDss", NoiseCont);
            icn.GetComponent<MeshRenderer>().material.SetFloat("_VelocityDss", timeMa);
        }
        if (!audioMatDss)
        {
            playEfx(fxs[1]);
            audioMatDss = true;
        }
        
    }
    public void AnimText(int bugTxtP, int sunTxtP, bool premo)
    {
        if (!premo)
        {
            int bugTxt = Random.Range(4, 12);
            int sunTxt = Random.Range(30, 100);

            if (ScoreTxt.transform.localPosition.y != 2.12f || BetleTxt.transform.localPosition.y != 4.95f)
            {
                ScoreTxt.transform.localPosition = new Vector3(0.56f, 2.12f, 0);
                BetleTxt.transform.localPosition = new Vector3(0.56f, 4.95f, 0);
            }

            if (ScoreTxt.text != "0" || ScoreTxt.text != null)
            {
                iTween.MoveAdd(ScoreTxt.gameObject, iTween.Hash("y", 5f, "time", 1f, "islocal", true));
                iTween.ScaleTo(ScoreTxt.gameObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f));
                ScoreTxt.text = "+ " + sunTxt.ToString();
                cS.ScoreTotalPts = cS.ScoreTotalPts + sunTxt;
                cS.SceneryPts = cS.SceneryPts + sunTxt;
                playEfx(fxs[3]);
            }

            if (BetleTxt.text != "0" || BetleTxt.text != null)
            {
                iTween.MoveAdd(BetleTxt.gameObject, iTween.Hash("y", 5f, "time", 1f, "islocal", true));
                iTween.ScaleTo(BetleTxt.gameObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f));
                BetleTxt.text = "+ " + bugTxt.ToString();
                cS.ScoreTotalGB = cS.ScoreTotalGB + bugTxt;
                cS.SceneryGB = cS.SceneryGB + bugTxt;
            }
            setScoreVisual = true;
        }
        if (premo)
        {
            if (ScoreTxt.transform.localPosition.y != 2.12f || BetleTxt.transform.localPosition.y != 4.95f)
            {
                ScoreTxt.transform.localPosition = new Vector3(0.56f, 2.12f, 0);
                BetleTxt.transform.localPosition = new Vector3(0.56f, 4.95f, 0);
            }

            if (ScoreTxt.text != "0" || ScoreTxt.text != null)
            {
                iTween.MoveAdd(ScoreTxt.gameObject, iTween.Hash("y", 5f, "time", 1f, "islocal", true));
                iTween.ScaleTo(ScoreTxt.gameObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f));
                ScoreTxt.text = "+ " + sunTxtP.ToString();
                cS.ScoreTotalPts = cS.ScoreTotalPts + sunTxtP;
                cS.SceneryPts = cS.SceneryPts + sunTxtP;
                playEfx(fxs[3]);
            }

            if (BetleTxt.text != "0" || BetleTxt.text != null)
            {
                iTween.MoveAdd(BetleTxt.gameObject, iTween.Hash("y", 5f, "time", 1f, "islocal", true));
                iTween.ScaleTo(BetleTxt.gameObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f));
                BetleTxt.text = "+ " + bugTxtP.ToString();
                cS.ScoreTotalGB = cS.ScoreTotalGB + bugTxtP;
                cS.SceneryGB = cS.SceneryGB + bugTxtP;
            }
            setScoreVisual = true;
        }

    }
    void DisparaEfx()
    {
        foreach (ParticleSystem efx in vfxStay)
            efx.Play();

        PsoPartcles.Play();
    }
    void DetenEfx()
    {
        foreach (ParticleSystem efx in vfxStay)
            efx.Stop();

        PsoPartcles.Stop();
    }
    private void fallPiedras()
    {
        float[] suerteP = new float[Piedras.Length];
        float[] suerteA = new float[Arenas.Length];

        for (int i = 0; i < Arenas.Length; i++)
        {
            suerteA[i] = UnityEngine.Random.Range(0f, 10f);
            if (suerteA[i] > 7f)
            {
                Arenas[i].GetComponent<ParticleSystem>().Play();
            }
        }

        for (int j = 0; j < Piedras.Length; j++)
        {
            suerteP[j] = UnityEngine.Random.Range(0f, 10f);
            if (suerteP[j] > 8f)
            {
                Piedras[j].SetActive(true);
            }
        }
        playEfx(fxs[4]);
        ArenasEfx = false;
    }
    public void setContraMark(GameObject ficha, float posicion, float scala)
    {
        iTween.ScaleTo(PisoBrillante, new Vector3(1.03f, 1f, 1.11f), 0.5f);
        PsoPartcles.Play();
        iTween.MoveTo(ficha, iTween.Hash("y", posicion, "time", 1f, "islocal", true));
        iTween.ScaleTo(ficha, iTween.Hash("scale", new Vector3(scala, scala, scala), "time", 1f, "islocal", true));
        Luciernagas.Play();
        stonesFicha.Play();
        logic.ShakeCamera(0.2f, 1f);
        playEfxB();
    }
    public void IniPos(GameObject ficha, float posicion, float scala)
    {
        iTween.MoveTo(ficha, iTween.Hash("y", posicion, "time", 1f, "islocal", true));
        iTween.ScaleTo(ficha, iTween.Hash("scale", new Vector3(scala, scala, scala), "time", 1f, "islocal", true));
    }
    public void playEfx(AudioClip sonido)
    {
        AudioFXG.PlayOneShot(sonido);
    }
    public void playEfxB()
    {
        AudioFXB.Play();
    }
}
