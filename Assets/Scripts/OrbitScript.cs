using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitScript : MonoBehaviour, I_DynamicScript
{
    public GameObject clocktower;
    I_TimeManager clock;
    public GameObject o_center;
    public GameObject o_cosine;
    public GameObject o_sine;
    Vector3 center;
    Vector3 cosine;
    Vector3 sine;
    public int orbitTime_ms;
    public int orbitStartTime_ms;
    float orbitTime;
    float orbitStartTime;

    // Start is called before the first frame update
    void Start()
    {
        clock = clocktower.GetComponent<I_TimeManager>();
        center = o_center.transform.position;
        sine = o_sine.transform.position;
        cosine = o_cosine.transform.position;
        orbitTime = (float)orbitTime_ms / 1000;
        orbitStartTime = (float)orbitStartTime_ms / 1000;
        this.SetValues(this.GetValues(), new Vector3());
    }

    // Update is called once per frame
    void Update()
    {
        center = o_center.transform.localPosition;
        sine = o_sine.transform.localPosition;
        cosine = o_cosine.transform.localPosition;
        print(center.ToString() + ',' + sine.ToString() + ',' + cosine.ToString());
        float time = clock.GetTime();
        float factor = (time - orbitStartTime) / orbitTime * Mathf.PI;
        Vector3 location = center + Mathf.Cos(factor) * cosine + Mathf.Sin(factor) * sine;
        transform.localPosition = location;
    }

    public void SetValues(JObject X, Vector3 y)
    {
        center = Util2.ToV3(X["center"].ToString());
        sine = Util2.ToV3(X["sine"].ToString());
        cosine = Util2.ToV3(X["cosine"].ToString());
    }

    public JObject GetValues()
    {
        JObject X = new JObject();
        X.Add("center",center.ToString());
        X["sine"] = sine.ToString();
        X["cosine"] = cosine.ToString();
        return X;
    }
}
