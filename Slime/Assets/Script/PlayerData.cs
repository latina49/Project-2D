using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace AssemblyCsharp
{
    public class PlayerData
    {
        public Model modle = new Model();
        [Serializable]
        public class Model
        {
            public int IDSlime;
            public float HP;
            public float mana;
            public int coin;
            public float score;
            public float highScore;
            public Vector3 location;         
        }

        public static Model GetModelFromJson(string respense)
        {
            PlayerData md = JsonUtility.FromJson<PlayerData>(respense);
            return md.modle;
        }
        public static string GetJsonFromModel(PlayerData model, bool pretty)
        {
            return JsonUtility.ToJson(model, pretty);
        }
    }
}
