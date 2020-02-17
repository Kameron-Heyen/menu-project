using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    public float smooth = 1f;

    // Game object
    public GameObject UiPanel;

    // materials
    public Material skyOne;
    public Material skyTwo;
    public Material skyThree;

    // prefabs
    public GameObject tree;
    public GameObject house;
    public GameObject skyScraper;

    public GameObject spear;
    public GameObject sword;
    public GameObject cleaver;

    // Music Assets
    public AudioSource musicSource;
    public AudioSource musicTrack1;
    public AudioSource musicTrack2;
    public AudioSource musicTrack3;

    public Resolution[] resolutions;
    private GameSettings game_settings;
    private GameObject selectedObject;
    private GameObject selectedWeapon;

    private int track = 0;
    private float volume = 0.5f;
    private int skybox = 0;
    private int objInt = 0;
    private int weaponInt = 0;

    void Awake()
    {
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        // rotate panel around controller
        if (e.target.name == "SettingsButton")
        {
            onSettingsButtonClick();
        } else if (e.target.name == "ConfirmButton")
        {
            onConfirmClick();
        } 

         // create objects
        else if (e.target.name == "WeaponsButton")
        {
            OnCreateWeaponClick();
        } else if (e.target.name == "ObjectsButton")
        {
            OnCreateObjectClick();
        }
        
        // music
        else if (e.target.name == "PreviousTrack")
        {
            track = track -1;
            if (track == -1) { track = 2; }
            OnMusicSelection(track);
        } else if (e.target.name == "SkipTrack") {
            track = track+1;
            if (track == 3) { track = 0; }
            OnMusicSelection(track);
        }

        // volume
        else if (e.target.name == "VolumeDown") {
            if (volume > 0.09f) {
                 volume = volume-0.1f;
                 OnMusicVolumeChange(volume);
            }
        } else if (e.target.name == "VolumeUp") {
            if (volume < 0.95f) {
                 volume = volume+0.1f;
                 OnMusicVolumeChange(volume);
            }
        }

        // skybox
        else if (e.target.name == "PreviousSkybox") {
            skybox = skybox - 1;
            if (skybox == -1) { skybox = 2; }
            OnSkyboxSelection(skybox);

        } else if (e.target.name == "NextSkybox") {
            skybox = skybox + 1;
            if (skybox == 3) { skybox = 0; }
            OnSkyboxSelection(skybox);
        }


        // object selection
        else if (e.target.name == "LeftObjectButton") {
            objInt = objInt - 1;
            if (objInt == -1) { objInt = 2; }
            OnObjectSelection(objInt);
        }
        else if (e.target.name == "RightObjectButton") {
            objInt = objInt + 1;
            if (objInt == 3) { objInt = 0; }
            OnObjectSelection(objInt);
        }

        // weapon selection
        else if (e.target.name == "LeftWeaponButton") {
            weaponInt = weaponInt - 1;
            if (weaponInt == -1) { weaponInt = 2; }
            OnWeaponSelection(weaponInt);
        }
        else if (e.target.name == "RightWeaponButton") {
            weaponInt = weaponInt + 1;
            if (weaponInt == 3) { weaponInt = 0; }
            OnWeaponSelection(weaponInt);
        }

       
    }

    void Start()
    {
        selectedObject = tree;
        selectedWeapon = spear;
        game_settings = new GameSettings();
        resolutions = Screen.resolutions;
    }

    void Update()
    {
    }

    public void OnSkyboxSelection(int newSkybox)
    {
        if (newSkybox == 0)
        {
            RenderSettings.skybox = skyOne;
            GameObject.Find("SelectedSkybox").GetComponentInChildren<Text>().text = "Cloudy";
        } else if (newSkybox == 1)
        {
            RenderSettings.skybox = skyTwo;
            GameObject.Find("SelectedSkybox").GetComponentInChildren<Text>().text = "Sunset";
        } else if (newSkybox == 2)
        {
            RenderSettings.skybox = skyThree;
            GameObject.Find("SelectedSkybox").GetComponentInChildren<Text>().text = "Cloudy 2";
        }
    }

    public void OnMusicSelection(int music)
    {
        if (music == 0)
        {
            musicSource.Stop();
            musicSource = musicTrack1;
            musicSource.Play();
            GameObject.Find("SelectedTrack").GetComponentInChildren<Text>().text = "Track 1";
        } else if (music == 1)
        {
            musicSource.Stop();
            musicSource = musicTrack2;
            musicSource.Play();
            GameObject.Find("SelectedTrack").GetComponentInChildren<Text>().text = "Track 2";
        } else if (music == 2)
        {
            musicSource.Stop();
            musicSource = musicTrack3;
            musicSource.Play();
            GameObject.Find("SelectedTrack").GetComponentInChildren<Text>().text = "Track 3";
        }
    }

    public void OnMusicVolumeChange(float newVolume)
    {
        musicSource.volume = newVolume;
        string vol = (newVolume * 100).ToString("0\\%");
        GameObject.Find("CurrentVolume").GetComponentInChildren<Text>().text = vol;
    }

    public void OnObjectSelection(int obj)
    {
        if (obj == 0)
        {
            selectedObject = tree;
            GameObject.Find("SelectedObject").GetComponentInChildren<Text>().text = "Tree";
        }
        if (obj == 1)
        {
            selectedObject = house;
            GameObject.Find("SelectedObject").GetComponentInChildren<Text>().text = "House";
        }
        if (obj == 2)
        {
            selectedObject = skyScraper;
            GameObject.Find("SelectedObject").GetComponentInChildren<Text>().text = "Skyscraper";
        }
    }

    public void OnWeaponSelection(int weapon)
    {
        if (weapon == 0)
        {
            selectedWeapon = spear;
            GameObject.Find("SelectedWeapon").GetComponentInChildren<Text>().text = "Spear";
        }
        if (weapon == 1)
        {
            selectedWeapon = sword;
            GameObject.Find("SelectedWeapon").GetComponentInChildren<Text>().text = "Sword";
        }
        if (weapon == 2)
        {
            selectedWeapon = cleaver;
            GameObject.Find("SelectedWeapon").GetComponentInChildren<Text>().text = "Cleaver";
        }
    }

    public void OnCreateObjectClick()
    {
        Instantiate(selectedObject, transform.position, transform.rotation);
    }

    public void OnCreateWeaponClick()
    {
        Instantiate(selectedWeapon, transform.position, transform.rotation);
    }

    // need to fix to rotate around controller
    public void onConfirmClick()
    {
        transform.RotateAround (transform.parent.position, transform.parent.up, 180f * Time.deltaTime);
    }

    public void onSettingsButtonClick()
    {
        transform.RotateAround (transform.parent.position, transform.parent.up, -180f * Time.deltaTime);
    }
}