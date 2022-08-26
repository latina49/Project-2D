using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public Enemy[] E;
    float HPEnemy;
    public GameObject Slime;
    public SkyFollow Sky;
    public MusicControl music;
    // Start is called before the first frame update
    public void Init()
    {
        music.Init();
        Sky.Init();
        foreach (Enemy e in E)
        {
            e.Init();
        }
    }

    // Update is called once per frame
    public void UpDate()
    {
        Sky.UpDate();
        foreach (Enemy e in E)
        {
            e.UpDate();
        }
    }
    public void FixedUpdateMap()
    {
        foreach (Enemy e in E)
        {
            e.FixedUpdateEnemy(Slime.transform.position, Slime);   
        }
    }


}
