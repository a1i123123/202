using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{

    public Text Trigger;
    // Start is called before the first frame update
    void Start()
    {
        Trigger.text = "You were brought to the last mission for your military application to be accepted." +
            "  KILL 100 ZOMBIES";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
