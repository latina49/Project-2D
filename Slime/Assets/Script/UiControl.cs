using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiControl : MonoBehaviour
{
    public CharacterControl Player;
    public static UiControl instance;
    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private Text coinText;
    public Image Menu;
    public Image gameOver;
    public float distance = 0;

    public void UpDate()
    {
        if (DataStore.instance.playerModel.score >= Player.transform.position.x && Player.transform.position.x >= distance)
        {
            distance = DataStore.instance.playerModel.score;
        }
        if (Player.transform.position.x >= distance)
            distance = Player.transform.position.x;
        distanceText.text = distance.ToString("F1") + " meters";
        coinText.text = Player.coin.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuOn();
        }
        if (Player.curHealth <= 0)
        {
            gameOver.gameObject.SetActive(true);
        }
        else
            gameOver.gameObject.SetActive(false);
    }

    public void FixedUpdateSlime()
    {

    }
    public void MenuOff()
    {
        Menu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void MenuOn()
    {
        Menu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
