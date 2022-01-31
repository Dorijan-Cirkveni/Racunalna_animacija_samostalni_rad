using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_DynamicScript
{
    void SetValues(JObject X, Vector3 y);
    JObject GetValues();
}
