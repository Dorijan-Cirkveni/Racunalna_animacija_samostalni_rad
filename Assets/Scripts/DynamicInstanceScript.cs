using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInstanceScript : MonoBehaviour, I_DynamicScript
{
    public string dataRaw;
    public GameObject library;
    LibraryScript libs;
    public string name;
    void Start()
    {
        libs = library.GetComponent<LibraryScript>();
    }
    public void Remove(Transform parent=null)
    {
        return;
        if (parent is null) parent = this.transform.parent;
        foreach (CameraHandler cam in this.GetComponentsInChildren<CameraHandler>()) cam.transform.SetParent(parent);
        foreach (DynamicInstanceScript dis in this.GetComponentsInChildren<DynamicInstanceScript>()) dis.Remove(parent);
        Object.Destroy(gameObject);
    }
    public void Manage()
    {
        JObject data = JObject.Parse(this.dataRaw);
        Dictionary<string, GameObject> X = data.Value<Dictionary<string, GameObject>>("subobjects");
    }
    public void Manage(string instructions)
    {
        dataRaw = instructions;
        Manage();
    }

    public void SetValues(JObject X, Vector3 cam_pos)
    {
        dataRaw = X.ToString();
        name = X.Value<string>("name");
        DynamicInstanceScript Y = GetComponentInParent<DynamicInstanceScript>();
        if (X != null) library = Y.library;
        if (X.ContainsKey("orbit"))
        {
            OrbitScript orbitScript = GetComponent<OrbitScript>();
            orbitScript.SetValues(X.Value<JObject>("orbit"), new Vector3());
        }
        List<string> children = X.Value<List<string>>("children");
        Dictionary<string,JObject> ch_data = X.Value<Dictionary<string, JObject>>("ch_data");
        List<string> present = new List<string>();
        foreach (DynamicInstanceScript child in gameObject.GetComponentsInChildren<DynamicInstanceScript>())
        {
            string name = child.name;
            if (children.Contains(name))
            {
                present.Add(name);
                JObject X2 = ch_data[name];
                Vector3 location = Util2.ToV3(X2.Value<string>("location"));
                float dis = X2.Value<float>("dis");
                bool sub = X2.Value<bool>("sub");
                if ((Vector3.Distance(location, cam_pos) > dis) != sub) child.Remove();
            }
            else
            {
                child.Remove();
            }
        }
        foreach (JObject plan in ch_data.Values)
        {
            string name = plan.Value<string>("name");
            if (present.Contains(name)) continue;
            string type = plan.Value<string>("type");
            GameObject temp = libs.getFor(type);
            GameObject ntemp = Instantiate<GameObject>(temp);
            ntemp.transform.parent = this.transform;
            DynamicInstanceScript dis2 = ntemp.GetComponent<DynamicInstanceScript>();
            dis2.SetValues(plan,cam_pos);
        }
    }

    public JObject GetValues()
    {
        JObject X = new JObject();
        X.Add("name", name);

        return X;
    }
}
