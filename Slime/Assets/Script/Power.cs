using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public static void Heal(ref float hp)
    {
        if (hp > 80)
            hp = 100;
        else
            hp += 20 ;
    }
    public static void Shield(GameObject shield,ref float checkShield, float check)
    {
        shield.SetActive(true);
        checkShield = check;
    }
    public static void Electro(ref float V_0, ref int ID)
    {
        V_0 = 4;
        ID = 1;
    }
    public static void ATK(ref float ATK)
    {
        ATK *= 2;
    }

    public static void Anemo(Rigidbody2D rigdi, float V_Fly)
    {
        float x = Input.GetAxis("Horizontal");
        rigdi.velocity = new Vector2(x * 2, V_Fly);
        rigdi.transform.eulerAngles = new Vector3(0, x > 0 ? 0 : (x < 0 ? 180 : rigdi.transform.eulerAngles.y), 0);          
    }
}
