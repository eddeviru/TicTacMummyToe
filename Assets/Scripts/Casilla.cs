using UnityEngine;

public class Casilla : MonoBehaviour
{
    public int id;
    public int result;
    public GameObject Botonazo;
    public ControlPuntos cP;
    private Logica logic;
    public bool BaseExtra;
    public bool juega;
    private float CambioFicha;

    private void Start()
    {
        logic = GetComponentInParent<Logica>();
        cP = Botonazo.GetComponent<ControlPuntos>();

    }

    private void Update()
    {
        if (!juega)
        {
            CambioFicha += Time.deltaTime;
            if (CambioFicha > 0.2f)
            {
                juega = true;
                CambioFicha = 0;
            }
        }
    }

    private void OnTouchDown()
    {
        // activate vfx
        oprimirBtn();
    }

    public void oprimirBtn()
    {
        if (juega)
        {
            logic.cP = cP;
            if (logic.turnoMomia)
            {
                Normales();
                return;
            }
            if (logic.turnoJanet && logic.canMove)
            {
                logic.RequestPermss(id);
            }
        }
    }

    public void SetActns()
    {
        Normales();
    }

    private void Normales()
    {
        if (cP.obstaculo)
        {
            logic.pasoObstaculos(id);
            logic.intentos = 1;
            juega = false;
            return;
        }
        if (!cP.obstaculo)
        {
            logic.Spawn(id);
            logic.intentos = 1;
            juega = false;
        }
    }
}
