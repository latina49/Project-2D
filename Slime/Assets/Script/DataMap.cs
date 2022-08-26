using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace AssemblyCsharp
{
    public class DataMap
    {
        public Model modle = new Model();
        [Serializable]
        public class Model
        {
            public Vector3 locationBG;
            public Vector3 locationCamera;
        }

        public static Model GetModelFromJson(string respense)
        {
            DataMap m = JsonUtility.FromJson<DataMap>(respense);
            return m.modle;
        }
        public static string GetJsonFromModel(DataMap model, bool pretty)
        {
            return JsonUtility.ToJson(model, pretty);
        }
    }
}

