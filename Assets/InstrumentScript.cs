using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentScript : MonoBehaviour
{
    bool IsBroken;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RiskBreaking", 1.0f, 1.0f); // Risk breaking every 1 second
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RiskBreaking()
    {
        if (IsBroken)
        {
            return;
        }
        var x = Random.Range(0, 10);
        if (x <= 1)
        {
            IsBroken = true;
            Debug.Log("Instrument broke!!");
        }
        else 
        {
            Debug.Log("Instrument did not break (drew " + x + ")");
        }
    }
}
