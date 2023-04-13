using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Conf : MonoBehaviour
{
    private Returno tr;
    private Controlador Control;
    private GameObject panelVolum;
    public Slider barraSonido;
    private float pasoV;
    public AudioMixer MasterdSonido;

    private void Awake()
    {
        panelVolum = GameObject.FindGameObjectWithTag("Botonera");
        BusquedaPasaje();

        if (!PlayerPrefs.HasKey("Volume"))
        {
            pasoV = Control.volumeSoundInGame;
        }

        if (PlayerPrefs.HasKey("Volume"))
        {
            pasoV = PlayerPrefs.GetFloat("Volume");
        }
        
        tr = GetComponent<Returno>();
        tr.FadeScreen(0, 0);
        OnVolumeChange(pasoV);
        barraSonido.value = pasoV;
        gameObject.SetActive(false);
    }

    public void OnVolumeChange(float value)
    {
        MasterdSonido.SetFloat("MasterVol", value);
        panelVolum.GetComponent<IniThing>().volumeM = value;
        PlayerPrefs.SetFloat("Volume", value);
        if (Control.volumeSoundInGame != value)
        {
            Control.volumeSoundInGame = value;
        }
    }

    private void BusquedaPasaje()
    {
        Control = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Controlador>();
    }
}