using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseCharacter : MonoBehaviour
{
    public delegate void CallBack(GameObject ID);
    public GameObject Choose_Slime;
    public Button[] Choose;
    public GameObject[] Slime;
    public CallBack callback;
    public CharacterControl C;
    // Start is called before the first frame update
    public void OnChooseClick(int s)
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == s)
            {
                Slime[i].SetActive(true);
                callback(Slime[i]);
                C.IDSlime = i;
            }    
            else
                Slime[i].SetActive(false);
        }
    }
    public void Init()
    {
        Choose[0].onClick.AddListener(() => OnChooseClick(0));
        Choose[1].onClick.AddListener(() => OnChooseClick(1));
        Choose[2].onClick.AddListener(() => OnChooseClick(2));
        Choose[3].onClick.AddListener(() => OnChooseClick(3));
        Choose[4].onClick.AddListener(() => OnChooseClick(4));
        Choose[5].onClick.AddListener(() => OnChooseClick(5));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChooseSlime()
    {      
        if (!Choose_Slime.activeSelf)
        {
            Choose_Slime.SetActive(true);
        }
        else
        {
            Choose_Slime.SetActive(false);
        }
    }

}
