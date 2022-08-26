using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public CharacterControl C;
    public ChooseCharacter CSlime;
    public MapControl MAP;
    public UiControl Ui;
    public CameraControl Ca;
    // Start is called before the first frame update
    void Start()
    {
        C.Init();
        Ca.Init();
        if (DataStore.instance.playerModel.score == 0)
        {
            CSlime.Choose_Slime.SetActive(true);
            C.coin = 10;
            gameIsPaused = true;
            PauseGame();
        }
        CSlime.Init();
        CSlime.callback += ChooseSlimeCallBack;
        if (C.transform.position.x > 0)
        {
            CSlime.OnChooseClick(C.IDSlime);
        }
        
        MAP.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && C.coin>=10 && C.animator != null)
        {
            CSlime.ChooseSlime();
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
        MAP.UpDate();
        Ui.UpDate();
    }

    private void FixedUpdate()
    {
        C.FixedUpdateSlime();
        MAP.FixedUpdateMap();

    }

    private void LateUpdate()
    {
        Ca.LateUpDate();
    }

    public void ChooseSlimeCallBack(GameObject Slime)
    {
        C.UpDateAnimator(Slime);
    }
    public static bool gameIsPaused;
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
