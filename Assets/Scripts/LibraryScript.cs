using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryScript : MonoBehaviour
{
    Dictionary<string, DynamicInstanceScript> objects = new Dictionary<string, DynamicInstanceScript>();
    public GameObject getFor(string name)
    {
        return objects[name].gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (DynamicInstanceScript dis in GetComponentsInChildren<DynamicInstanceScript>())
        {
            string name = dis.gameObject.name;
            objects.Add(name,dis);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
