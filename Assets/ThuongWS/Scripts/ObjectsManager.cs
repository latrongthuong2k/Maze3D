using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    private static ObjectsManager instance;
    public static ObjectsManager Instance => instance;

    [SerializeField] public List<GameObject> Group1;
    [SerializeField] public List<GameObject> Group2;
    [SerializeField] public List<GameObject> Group3;
    [SerializeField] public List<GameObject> Group4;
    [SerializeField] public List<GameObject> Group5;
    [SerializeField] public List<GameObject> Group6;
    //--------------------------------------------------
    [SerializeField] public GameObject Directional_Light;
    [SerializeField] public List<GameObject> GroupSwitch;
    [SerializeField] public List<GameObject> lightList = new List<GameObject>();
    [SerializeField] public List<GameObject> AllDoor;
    [SerializeField] private Transform SpecialCapsule;
    [SerializeField] private Vector3 rotation;
    private void Start()
    {
        instance = this;
    }

    private void Reset()
    {
        Group1 = new List<GameObject>(GameObject.FindGameObjectsWithTag("MonsterVsLightGroup"));
        Group2 = new List<GameObject>(GameObject.FindGameObjectsWithTag("MonsterVsLightGroup1"));
        Group3 = new List<GameObject>(GameObject.FindGameObjectsWithTag("MonsterVSLightGroup2"));
        Group4 = new List<GameObject>(GameObject.FindGameObjectsWithTag("MonsterVsLightGroup3"));
        Group5 = new List<GameObject>(GameObject.FindGameObjectsWithTag("MonsterVsLightGroup4"));
        Group6 = new List<GameObject>(GameObject.FindGameObjectsWithTag("MonsterVsLightGroup5"));
    }
    private void Update()
    {
        SpecialCapsule.Rotate(rotation * Time.deltaTime * 0.5f);
    }
}
