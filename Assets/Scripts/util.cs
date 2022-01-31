using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util2 : MonoBehaviour
{
    public static Vector3 ToV3(string raw)
    {
        string r2 = raw.Substring(1, raw.Length - 2);
        Debug.Log(r2);
        string r3 = r2.Replace(" ","");
        string[] r4 = r3.Split(',');
        List<float> L = new List<float>();
        foreach (string r in r4) L.Add(float.Parse(r));
        Vector3 res = new Vector3(L[0],L[1],L[2]);
        return res;
    }
}
