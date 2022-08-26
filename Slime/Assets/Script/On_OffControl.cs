using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class On_OffControl : MonoBehaviour
{
    public Image Off;
    public Button On;
    public MusicControl ms;
    [SerializeField]
    private Text distanceText;
    void Start()
    {
        ms.Init();
        distanceText.text = DataStore.instance.playerModel.highScore.ToString("F1");
        if (DataStore.instance.playerModel.score <= 0)
        {
            Off.gameObject.SetActive(true);
            On.gameObject.SetActive(false);
        }
        else
        {
            On.gameObject.SetActive(true);
            Off.gameObject.SetActive(false);
        }
        
    }

}
