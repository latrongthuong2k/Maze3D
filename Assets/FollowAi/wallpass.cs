using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallpass : MonoBehaviour
{
    public KeyCode PassOut;
    [SerializeField] private Renderer myObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(PassOut))
        {
            GetComponent<BoxCollider>().isTrigger = true;
            myObj.material.color = Color.white;
        }
        else if(Input.GetKeyUp(PassOut))
        {
            GetComponent<BoxCollider>().isTrigger = false;
            myObj.material.color = Color.black;
        }
    }
}
