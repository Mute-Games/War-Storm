using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameData
{
    //player stats for display
    public float Speed;
    public float Turn;
    public string Ammo;
    public int PlayerHealth;
    public int AmountOfEnemies;

    public bool MusicOn;
    public bool RealisticTurret;

    public GameObject PlayerVehicleToSpawn;
    public int SelectedCPI;
    public int SelectedJihad;

    public List<int> Bought;

    public int Money;

    //to count enemies
    public void CountEnemies()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        AmountOfEnemies = Enemies.Length;
    }

    //to add money
    public void AddMoney(int addition)
    {
        Money += addition;

        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.Save();
    }


    public static GameData Instance
    {
        get
        {
            if (instance == null) instance = new GameData();
            return instance;
        }
    }
    private static GameData instance;
    private GameData() { }

}

