using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {


    [SerializeField]
    InputField InitCooldown;
    [SerializeField]
    InputField matchTime;

    [SerializeField]
    Slider BarraVolume;

    [SerializeField]
    GameObject Menu;
    [SerializeField]
    GameObject menuPrincipal;
    [SerializeField]
    GameObject Loading;

    float VOLUME;

    private void Start()
    {
        BarraVolume.minValue = 0;
        BarraVolume.maxValue = 1;

        if (PlayerPrefs.HasKey("VOLUME"))
        {
            VOLUME = PlayerPrefs.GetFloat("VOLUME");
            BarraVolume.value = VOLUME;
        }
        else
        {
            PlayerPrefs.SetFloat("VOLUME", 1);
            BarraVolume.value = 1;
        }
    }

    private void Update()
    {
        AudioListener.volume = VOLUME;
       
    }

    public void setMatchTime()
    {
        CanvasController.gameTime = float.Parse(matchTime.text);
    }

    public void setInitCooldown()
    {        
        CanvasController.startTime = float.Parse(InitCooldown.text);
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("VOLUME", BarraVolume.value);        
    }

    void applyPrefs()
    {
        VOLUME = PlayerPrefs.GetFloat("VOLUME");        
    }


    public void Play()
    {
        Loading.SetActive(true);
        menuPrincipal.SetActive(false);
    }

    public void openMenu()
    {
        Menu.gameObject.SetActive(true);
    }

    public void closeMenu()
    {
        Menu.gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
