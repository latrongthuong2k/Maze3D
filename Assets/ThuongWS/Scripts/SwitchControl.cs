using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    [SerializeField] protected GameObject Map;
    [SerializeField] protected GameObject[] Kabe;
    private Renderer mapRenderer;
    

    // Start is called before the first frame update
    private void Start()
    {
        if(Map == null)
        {
            Debug.LogWarning("Need Map Obj!");
            return;
        }
        mapRenderer = Map.GetComponent<Renderer>();
    }
    private void Reset()
    {
        if (Kabe == null)
        {
            Debug.LogWarning("Need set tag ( Kabe ) for wall Obj!");
            return;
        }
        Kabe = GameObject.FindGameObjectsWithTag("Kabe");
    }
    // Update is called once per frame
    private void Update()
    {
        Switch();
    }
    private void Switch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (mapRenderer.material.color == Color.white)
            {
                mapRenderer.material.color = Color.black;
                if (Kabe == null)
                {
                    return;
                }
                KabeAction();
            }
            else if (mapRenderer.material.color == Color.black)
            {
                mapRenderer.material.color = Color.white;
                if (Kabe == null)
                {
                    return;
                }
                KabeAction();
            }
        }
    }
    private void KabeAction()
    {
        foreach (var item in Kabe)
        {
            if (mapRenderer.material.color == Color.white && item.GetComponent<Renderer>().material.color == Color.white)
            {
                item.GetComponent<BoxCollider>().isTrigger = true;
                foreach (var item2 in Kabe)
                {
                    if(item2.GetComponent<Renderer>().material.color == Color.black)
                    {
                        item2.GetComponent<BoxCollider>().isTrigger = false;
                    }
                }
            }
            else if (mapRenderer.material.color == Color.black && item.GetComponent<Renderer>().material.color == Color.black)
            {
                item.GetComponent<BoxCollider>().isTrigger = true;
                foreach (var item2 in Kabe)
                {
                    if (item2.GetComponent<Renderer>().material.color == Color.white)
                    {
                        item2.GetComponent<BoxCollider>().isTrigger = false;
                    }
                }
            }
        }
    }
}