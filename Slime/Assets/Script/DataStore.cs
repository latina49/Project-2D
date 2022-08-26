using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCsharp;
using System.IO;
using UnityEngine.UI;
public class DataStore : MonoBehaviour
{
    public static DataStore instance;
    public PlayerData.Model playerModel;
    public DataMap.Model map;
    public DataMusic.Model music;
    public CharacterControl C;
    public CameraControl Ca;
    public SkyFollow Sky;
    public UiControl Ui;
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        LoadPlayerData();
        LoadPlayerDataMap();
        LoadPlayerDataMusic();
    }
    public void PlayerNewGame()
    {
        AssemblyCsharp.PlayerData player = new AssemblyCsharp.PlayerData();
        player.modle.IDSlime = 0;
        player.modle.HP = 100;
        player.modle.mana = 0;
        player.modle.coin = 0;
        player.modle.score = 0.0f;
        player.modle.location = new Vector3(0, 19.55f, 0);
        player.modle.highScore = DataStore.instance.playerModel.highScore;
        string path = Path.Combine(Application.streamingAssetsPath, "Data.txt");
        File.WriteAllText(path, PlayerData.GetJsonFromModel(player, true));
        LoadPlayerData();
    }

    public void ContrinueGame()
    {
        AssemblyCsharp.PlayerData player = new AssemblyCsharp.PlayerData();
        player.modle.IDSlime = C.IDSlime;
        player.modle.HP = C.curHealth;
        player.modle.mana = C.mana;
        player.modle.coin = C.coin;
        player.modle.location = Ca.LocationCharacter;
        player.modle.score = Ui.distance;
        if (player.modle.score >= DataStore.instance.playerModel.highScore)
            player.modle.highScore = player.modle.score;
        else
            player.modle.highScore = DataStore.instance.playerModel.highScore;
        string path = Path.Combine(Application.streamingAssetsPath, "Data.txt");
        File.WriteAllText(path, PlayerData.GetJsonFromModel(player, true));     
        LoadPlayerData();
    }
    public void PlayerRestart()
    {
        AssemblyCsharp.PlayerData player = new AssemblyCsharp.PlayerData();
        player.modle.IDSlime = 0;
        player.modle.HP = 100;
        player.modle.mana = 0;
        player.modle.coin = 0;
        player.modle.score = 0.0f;
        player.modle.location = new Vector3(0, 19.55f, 0);
        if (Ui.distance >= DataStore.instance.playerModel.highScore)
            player.modle.highScore = Ui.distance;
        else
            player.modle.highScore = DataStore.instance.playerModel.highScore;
        string path = Path.Combine(Application.streamingAssetsPath, "Data.txt");
        File.WriteAllText(path, PlayerData.GetJsonFromModel(player, true));
        LoadPlayerData();
    }
    void LoadPlayerData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Data.txt");
        string data = File.ReadAllText(path);
        playerModel = PlayerData.GetModelFromJson(data);
    }
    //Map
    void LoadPlayerDataMap()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "DataMap.txt");
        string data = File.ReadAllText(path);
        map = DataMap.GetModelFromJson(data);
    }
    public void PlayerNewGameMap()
    {
        AssemblyCsharp.DataMap m = new AssemblyCsharp.DataMap();
        m.modle.locationCamera = new Vector3(0.349999994f, 20.2099991f, -10);
        m.modle.locationBG = new Vector3(3.54000092f, 19.5400028f, 0);
        string path = Path.Combine(Application.streamingAssetsPath, "DataMap.txt");
        File.WriteAllText(path, DataMap.GetJsonFromModel(m, true));
        LoadPlayerDataMap();
    }
    public void ContrinueGameMap()
    {
        AssemblyCsharp.DataMap m = new AssemblyCsharp.DataMap();
        m.modle.locationCamera = Ca.LocationCamera;
        m.modle.locationBG = Sky.LocationSky;
        string path = Path.Combine(Application.streamingAssetsPath, "DataMap.txt");
        File.WriteAllText(path, DataMap.GetJsonFromModel(m, true));
        LoadPlayerDataMap();
    }
    //music
    void LoadPlayerDataMusic()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "DataMusic.txt");
        string data = File.ReadAllText(path);
        music = DataMusic.GetModelFromJson(data);
    }
    public void MusicOn()
    {
        AssemblyCsharp.DataMusic m = new AssemblyCsharp.DataMusic();
        m.modle.checkMusic = true;
        string path = Path.Combine(Application.streamingAssetsPath, "DataMusic.txt");
        File.WriteAllText(path, DataMusic.GetJsonFromModel(m, true));
        LoadPlayerDataMusic();
    }

    public void MusicOff()
    {
        AssemblyCsharp.DataMusic m = new AssemblyCsharp.DataMusic();
        m.modle.checkMusic = false;
        string path = Path.Combine(Application.streamingAssetsPath, "DataMusic.txt");
        File.WriteAllText(path, DataMusic.GetJsonFromModel(m, true));
        LoadPlayerDataMusic();
    }
}
