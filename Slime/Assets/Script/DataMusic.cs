using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace AssemblyCsharp
{
    public class DataMusic
    {
        
        public Model modle = new Model();
        [Serializable]
        public class Model
        {
            public bool checkMusic;
        }

        public static Model GetModelFromJson(string respense)
        {
            DataMusic m = JsonUtility.FromJson<DataMusic>(respense);
            return m.modle;
        }
        public static string GetJsonFromModel(DataMusic model, bool pretty)
        {
            return JsonUtility.ToJson(model, pretty);
        }
    }
}


