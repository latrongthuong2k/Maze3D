using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using System.Threading.Tasks;
using static UnityEditor.Progress;

public class PlayerBehavior : MonoBehaviour
{
    private static PlayerBehavior instance;
    public static PlayerBehavior Instance => instance;

    [SerializeField] public int PlayerHP;
    //
    [SerializeField] private bool SwitchAllow;
    [SerializeField] private bool LightControlAllow;
    [SerializeField] private float waitTimeAnimateDoors;
    [SerializeField] private float waitTimeAnimateLightDown;
    [SerializeField] private float WaitTimeAnimateDoLightUp;
    [SerializeField] private float WaitTimeSetActiveLight;
    [SerializeField] private List<string> NameTagAction = new List<string>();
    [SerializeField] private List<GameObject> GroupCase;

    // secondary storage : こういうのはこのグループが change after 15 seconds, light resolve
    [SerializeField] private GameObject MonsterObjParent;
    [SerializeField] private GameObject BIGMonster;
    enum ShortKey
    {
        Door,
        OpenDoors,
        CloseDoors
    }
    
    // All Light Control
    private bool AllLightControl;

    void Start()
    {
        instance = this;
        PlayerHP = 100;
        AllLightControl = false;
        //
       
    }
    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(SwitchAllow == true )
            {
                await LightDown(GroupCase);
            }
            TurnOnAllLight(); // if true
        }
    }
    //--------------------------------------------
    // いつもSwitchのところに入るとSwitchを押せる。
    private void OnTriggerEnter(Collider other)
    { 
        foreach (var item in NameTagAction)
        {
            if (item == other.gameObject.tag && other.name != "Door")
            {
                SwitchAllow = true;
                switch (other.gameObject.tag)
                {
                    case "MonsterVsLightGroup":
                        GroupCase = ObjectsManager.Instance.Group1;
                        break;
                    case "MonsterVsLightGroup1":
                        GroupCase = ObjectsManager.Instance.Group2;
                        break;
                    case "MonsterVSLightGroup2":
                        GroupCase = ObjectsManager.Instance.Group3;
                        break;
                    case "MonsterVsLightGroup3":
                        GroupCase = ObjectsManager.Instance.Group4;
                        break;
                    case "MonsterVsLightGroup4":
                        GroupCase = ObjectsManager.Instance.Group5;
                        break;
                    case "MonsterVsLightGroup5":
                        GroupCase = ObjectsManager.Instance.Group6;
                        break;
                }
            }
            else if (other.gameObject.tag == "AllLightControl")
            {
                AllLightControl = true;
            }
            else if (other.gameObject.tag == "WinOffset")
            {
                UIcontrol.Instance.GameWinUI.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (var item in NameTagAction)
        {
            if (item == other.gameObject.tag && other.name != "Door")
            {
                SwitchAllow = false;
            }
            else if (other.gameObject.tag == "AllLightControl")
                LightControlAllow = false;
        }
    }
    //--------------------------------------------
    private async Task LightDown(List<GameObject> GroupCase)
    {
        foreach(var item in GroupCase)
        {
            if (item.name == "Light" && item.activeSelf == false)
            {
                item.SetActive(true);
                item.GetComponent<Animator>().Play("LightDownAnimate");
                await DoSomthingToDoor(GroupCase, ShortKey.OpenDoors);
                await LightUp(GroupCase);
                await DoSomthingToDoor(GroupCase, ShortKey.CloseDoors);
            }
        }
    }
    private async Task LightUp(List<GameObject> GroupCase)
    {
        await Task.Delay(6000);
        for (int i = 0; i < GroupCase.Count; i++)
        {
            if (GroupCase[i].name == "Light")
            {
                GroupCase[i].GetComponent<Animator>().Play("LightUpAnimate");
                await SetActiveLight(GroupCase);
            }
        }
    }
    private async Task SetActiveLight(List<GameObject> GroupCase)
    {
        await Task.Delay(1300);
        for (int i = 0; i < GroupCase.Count; i++)
        {
            if (GroupCase[i].name == "Light")
            {
                GroupCase[i].SetActive(false);
            }
        }
    }
    //--------------------------------------------
    private async Task DoSomthingToDoor(List<GameObject> GroupCase , ShortKey Action)
    {
        if (Action == ShortKey.OpenDoors)
        {
            await Task.Delay(1000);
            foreach (var item in GroupCase)
            {
                if (item.name == "Door")
                {
                    item.GetComponent<Animator>().Play("DoorAnimate");
                    item.GetComponent<BoxCollider>().isTrigger = true;
                }
            }
        }else if(Action == ShortKey.CloseDoors)
        {
            await Task.Delay(0);
            foreach (var item in GroupCase)
            {
                if (item.name == "Door")
                {
                    item.GetComponent<Animator>().Play("DoorAnimateClose");
                    item.GetComponent<BoxCollider>().isTrigger = false;
                }
            }
        }
        
    }
    //--------------------------------------------
    private void TurnOnAllLight()
    {
        if (AllLightControl == true)
        {
            ObjectsManager.Instance.Directional_Light.SetActive(true);
            foreach (var light in ObjectsManager.Instance.lightList)
            {
                light.SetActive(true);
                light.GetComponent<Animator>().Play("LightDownAnimate");
                foreach (var Door in ObjectsManager.Instance.AllDoor)
                {
                    Door.GetComponent<Animator>().Play("DoorAnimate");
                    Door.GetComponent<BoxCollider>().isTrigger = true;
                }
                foreach (var Switch in ObjectsManager.Instance.GroupSwitch)
                {
                    Switch.SetActive(false);
                }
            }
            // active BigEvent;
            BIGMonsterActive();
        }
    }
    private void BIGMonsterActive()
    {
        MonsterObjParent.SetActive(false);
        BIGMonster.SetActive(true);
    }
}
