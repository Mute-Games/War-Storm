using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Display : MonoBehaviour
{
    public SortOfDisplay sortOfDisplay;
    TMP_Text txt;

    bool AddedMoney;
    public int moneyToAdd;

    public GameObject End;

    void Start()
    {
        GameData.Instance.CountEnemies();
        txt = GetComponent<TMP_Text>();
        AddedMoney = false;
    }

    void Update()
    {
        switch (sortOfDisplay)
        {
            case SortOfDisplay.None:
                txt.text = "No Data Selected";
                break;
            case SortOfDisplay.Speed:
                txt.text = "Speed: " + Mathf.Round(GameData.Instance.Speed * 3.6f) + "km/h";
                break;
            case SortOfDisplay.Turn:
                if (Mathf.Abs(GameData.Instance.Turn) < 1)
                {
                    txt.text = "Turn: 0";
                }
                else
                {
                    txt.text = "Turn: " + Mathf.Round(GameData.Instance.Turn * 100) / 100;
                }
                break;
            case SortOfDisplay.Ammo:
                txt.text = "Ammo: " + GameData.Instance.Ammo;
                break;
            case SortOfDisplay.Health:
                txt.text = "Health: " + GameData.Instance.PlayerHealth;
                break;
            case SortOfDisplay.Enemies:
                GameData.Instance.CountEnemies();
                txt.text = "Enemies left: " + GameData.Instance.AmountOfEnemies;
                break;
            case SortOfDisplay.Victory:
                if (GameData.Instance.AmountOfEnemies == 0)
                {
                    txt.text = "You win";
                    End.SetActive(true);
                    Cursor.lockState = CursorLockMode.Confined;
                    if (!AddedMoney)
                    {
                        GameData.Instance.AddMoney(moneyToAdd);
                        AddedMoney = true;
                    }
                }
                else if (GameData.Instance.PlayerHealth == 0)
                {
                    txt.text = "You died";
                    End.SetActive(true);
                    Cursor.lockState = CursorLockMode.Confined;
                }
                else
                {
                    txt.text = "";
                    End.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;
            case SortOfDisplay.Money:
                txt.text = "$" + GameData.Instance.Money;
                break;
        }
    }


    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
public enum SortOfDisplay{
    None,
    Speed,
    Turn,
    Ammo,
    Health,
    Enemies,
    Victory,
    Money
}
