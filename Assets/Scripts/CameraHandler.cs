using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    float AD_SPD = 0.1f;
    float WS_SPD = 0.1f;
    float QE_SPD = 0.1f;
    public float acl = 1.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform controlledUnit = null;
        if(Input.GetMouseButtonUp(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                controlledUnit = hit.transform;
            }
        }
        if (controlledUnit != null)
        {
            this.transform.SetParent(controlledUnit);
        }
        float time = Time.deltaTime;
        float ws = Input.GetKey(KeyCode.S) ? 0 : 1;
        ws -= Input.GetKey(KeyCode.W) ? 0 : 1;
        float ad = Input.GetKey(KeyCode.A) ? 0 : 1;
        ad -= Input.GetKey(KeyCode.D) ? 0 : 1;
        float qe = Input.GetKey(KeyCode.Q) ? 0 : 1;
        qe -= Input.GetKey(KeyCode.E) ? 0 : 1;
        Vector3 res = new Vector3();
        float facl = Mathf.Pow(this.acl,time);
        if (ws != 0)
        {
            res += transform.forward * ws * WS_SPD;
            WS_SPD = WS_SPD == 0 ? 0.1f : WS_SPD + facl;
        }
        else WS_SPD = 0;
        if (ad != 0)
        {
            res += transform.right * ad * AD_SPD;
            AD_SPD = AD_SPD == 0 ? 0.1f : AD_SPD + facl;
        }
        else AD_SPD = 0;
        if (qe != 0)
        {
            res += transform.up * qe * QE_SPD;
            QE_SPD = QE_SPD == 0 ? 0.1f : QE_SPD + facl;
        }
        else QE_SPD = 0;
        transform.Translate(res*time);
        Debug.Log(ws);
    }
}
