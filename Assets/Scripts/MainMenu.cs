using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject Main;
    public GameObject Settings;
    public GameObject Shop;
    public GameObject Credits;
    public GameObject Choose;
    public GameObject MissionsCPI;
    public GameObject MissionsJihad;

    public TMP_Text MusicOnOfftxt;
    public AudioSource music;

    public TMP_Text Realistictxt;

    public GameObject APC104;
    public GameObject APC65;
    public GameObject Scout;
    public GameObject Deliverer;
    public GameObject Bastard;
    public GameObject Stalin;
    public GameObject Zooker;

    MainMenuState state;

    private void Start()
    {
        state = MainMenuState.Main;

        CheckSettings();

        SetupGamedata();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (state)
            {
                case MainMenuState.Main:
                    Exit();
                    break;
                case MainMenuState.Settings:
                    CloseSettings();
                    break;
                case MainMenuState.Shop:
                    CloseShop();
                    break;
                case MainMenuState.Credits:
                    CloseCredits();
                    break;
                case MainMenuState.Select:
                    CloseChoose();
                    break;
                case MainMenuState.Mission:
                    CloseCPI();
                    CloseJihad();
                    break;
            }
        }
    }

    //Exit
    public void Exit()
    {
        Application.Quit();
    }
    //Main
    public void HideMain()
    {
        Main.SetActive(false);
    }
    public void OpenMain()
    {
        Main.SetActive(true);
        state = MainMenuState.Main;
    }
    //Choose mission
    public void OpenChoose()
    {
        HideMain();
        Choose.SetActive(true);
        state = MainMenuState.Select;
    }
    public void CloseChoose()
    {
        Choose.SetActive(false);
        OpenMain();
    }
    //Select Mission
    public void OpenCPI()
    {
        MissionsCPI.SetActive(true);
        state = MainMenuState.Mission;
    }
    public void OpenJihad()
    {
        MissionsJihad.SetActive(true);
        state = MainMenuState.Mission;
    }
    public void CloseCPI()
    {
        MissionsCPI.SetActive(false);
        state = MainMenuState.Select;
    }
    public void CloseJihad()
    {
        MissionsJihad.SetActive(false);
        state = MainMenuState.Select;
    }
    //Settings
    public void OpenSettings()
    {
        HideMain();
        Settings.SetActive(true);
        state = MainMenuState.Settings;
    }
    public void CloseSettings()
    {
        Settings.SetActive(false);
        OpenMain();
    }
    //Shop
    public void OpenShop()
    {
        HideMain();
        Shop.SetActive(true);
        state = MainMenuState.Shop;
    }
    public void CloseShop()
    {
        Shop.SetActive(false);
        OpenMain();
    }
    //Credits
    public void OpenCredits()
    {
        HideMain();
        Credits.SetActive(true);
        state = MainMenuState.Credits;
    }
    public void CloseCredits()
    {
        Credits.SetActive(false);
        OpenMain();
    }

    //Load Scene
    public void SceneOpen(int Mission)
    {
        if (Mission <= 3)
        {
            if (GameData.Instance.SelectedCPI == 1)
            {
                GameData.Instance.PlayerVehicleToSpawn = APC104;
            }
            else
            {
                GameData.Instance.PlayerVehicleToSpawn = APC65;
            }
        }
        else
        {
            switch (GameData.Instance.SelectedJihad)
            {
                case 3:
                    GameData.Instance.PlayerVehicleToSpawn = Scout;
                    break;
                case 4:
                    GameData.Instance.PlayerVehicleToSpawn = Deliverer;
                    break;
                case 5:
                    GameData.Instance.PlayerVehicleToSpawn = Bastard;
                    break;
                case 6:
                    GameData.Instance.PlayerVehicleToSpawn = Stalin;
                    break;
                case 7:
                    GameData.Instance.PlayerVehicleToSpawn = Zooker;
                    break;
            }
        }
            
        SceneManager.LoadScene(Mission);
    }


    //Settings
    public void SetMusic()
    {
        if (!GameData.Instance.MusicOn)
        {
            GameData.Instance.MusicOn = true;
            PlayerPrefs.SetInt("Music", 0);
            MusicOnOfftxt.text = "On";
            music.Play();
        }
        else
        {
            GameData.Instance.MusicOn = false;
            PlayerPrefs.SetInt("Music", 1);
            MusicOnOfftxt.text = "Off";
            music.Stop();
        }
        PlayerPrefs.Save();
    }
    public void SetRealistic()
    {
        if (!GameData.Instance.RealisticTurret)
        {
            GameData.Instance.RealisticTurret = true;
            PlayerPrefs.SetInt("Realistic", 0);
            Realistictxt.text = "On";
        }
        else
        {
            GameData.Instance.RealisticTurret = false;
            PlayerPrefs.SetInt("Realistic", 1);
            Realistictxt.text = "Off";
        }
        PlayerPrefs.Save();
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        Exit();
    }


    void CheckSettings()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            GameData.Instance.MusicOn = true;
            MusicOnOfftxt.text = "On";
            music.Play();
        }
        else
        {
            GameData.Instance.MusicOn = false;
            MusicOnOfftxt.text = "Off";
            music.Stop();
        }
        if (PlayerPrefs.GetInt("Realistic") == 0)
        {
            GameData.Instance.RealisticTurret = true;
            Realistictxt.text = "On";
        }
        else
        {
            GameData.Instance.RealisticTurret = false;
            Realistictxt.text = "Off";
        }
    }

    void SetupGamedata()
    {
        if (PlayerPrefs.HasKey("SelectedCPI") && PlayerPrefs.HasKey("SelectedJihad"))
        {
            GameData.Instance.SelectedCPI = PlayerPrefs.GetInt("SelectedCPI");
            GameData.Instance.SelectedJihad = PlayerPrefs.GetInt("SelectedJihad");
        }
        else
        {
            PlayerPrefs.SetInt("SelectedCPI", 2);
            PlayerPrefs.SetInt("SelectedJihad", 3);
            GameData.Instance.SelectedCPI = 2;
            GameData.Instance.SelectedJihad = 3;
        }
        if (PlayerPrefs.HasKey("BoughtAPC104"))
        {
            GameData.Instance.Bought = new List<int> { };
            GameData.Instance.Bought.Add(PlayerPrefs.GetInt("BoughtAPC104"));
            GameData.Instance.Bought.Add(PlayerPrefs.GetInt("BoughtAPC65"));
            GameData.Instance.Bought.Add(PlayerPrefs.GetInt("BoughtScout"));
            GameData.Instance.Bought.Add(PlayerPrefs.GetInt("BoughtDeliverer"));
            GameData.Instance.Bought.Add(PlayerPrefs.GetInt("BoughtBastard"));
            GameData.Instance.Bought.Add(PlayerPrefs.GetInt("BoughtStalin"));
            GameData.Instance.Bought.Add(PlayerPrefs.GetInt("BoughtZooker"));
        }
        else
        {
            PlayerPrefs.SetInt("BoughtAPC104", 0);
            PlayerPrefs.SetInt("BoughtAPC65", 1);
            PlayerPrefs.SetInt("BoughtScout", 1);
            PlayerPrefs.SetInt("BoughtDeliverer", 0);
            PlayerPrefs.SetInt("BoughtBastard", 0);
            PlayerPrefs.SetInt("BoughtStalin", 0);
            PlayerPrefs.SetInt("BoughtZooker", 0);
            GameData.Instance.Bought = new List<int> { 0,1,1,0,0,0,0 };
        }
        if (PlayerPrefs.HasKey("Money"))
        {
            GameData.Instance.Money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            PlayerPrefs.SetInt("Money", 0);
            GameData.Instance.Money = 0;
        }


        PlayerPrefs.Save();
    }
}
public enum MainMenuState
{
    none,
    Main,
    Settings,
    Shop,
    Credits,
    Select,
    Mission
}