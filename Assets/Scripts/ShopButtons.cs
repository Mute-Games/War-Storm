using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtons : MonoBehaviour
{
    public int VehicleIndex;
    public TMP_Text txt;
    bool IsBought;
    public int Price;

    bool PoorRunning;

    bool didFirstFrame;

    // Start is called before the first frame update
    void Start()
    {
        didFirstFrame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!didFirstFrame)
        {
            if (GameData.Instance.Bought[VehicleIndex - 1] == 1)
            {
                IsBought = true;
            }
            else
            {
                IsBought = false;
            }
            didFirstFrame = true;
            Debug.Log("" + GameData.Instance.Bought[VehicleIndex - 1]);
        }

        if (IsBought)
        {
            if (GameData.Instance.SelectedCPI == VehicleIndex || GameData.Instance.SelectedJihad == VehicleIndex)
            {
                txt.text = "Selected";
            }
            else
            {
                txt.text = "";
            }
        }
        else
        {
            if (!PoorRunning) txt.text = "$" + Price;
        }
    }

    public void ClickOnButton()
    {
        if (IsBought)
        {
            if (VehicleIndex < 3)
            {
                GameData.Instance.SelectedCPI = VehicleIndex;
                PlayerPrefs.SetInt("SelectedCPI", VehicleIndex);

            }
            else
            {
                GameData.Instance.SelectedJihad = VehicleIndex;
                PlayerPrefs.SetInt("SelectedJihad", VehicleIndex);
            }
        }
        else
        {
            if (GameData.Instance.Money >= Price)
            {
                string add = "";
                switch (VehicleIndex)
                {
                    case 1:
                        add = "APC104";
                        break;
                    case 2:
                        add = "APC65";
                        break;
                    case 3:
                        add = "Scout";
                        break;
                    case 4:
                        add = "Deliverer";
                        break;
                    case 5:
                        add = "Bastard";
                        break;
                    case 6:
                        add = "Stalin";
                        break;
                    case 7:
                        add = "Zooker";
                        break;
                }
                GameData.Instance.AddMoney(-Price);
                IsBought = true;

                GameData.Instance.Bought[VehicleIndex - 1] = 1;
                PlayerPrefs.SetInt("Bought" + add, 1);
            }
            else
            {
                PoorRunning = true;
                StartCoroutine(Poor());
            }
        }
        PlayerPrefs.Save();
    }

    IEnumerator Poor()
    {
        txt.text = "TOO FUCKING POOR LMAO";
        yield return new WaitForSeconds(2f);
        txt.text = "$" + Price;
        PoorRunning = false;
        yield return null;
    }
}
