using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    // secondary storage : こういうのはこのグループが change after 15 seconds, light resolve.
    [SerializeField] private bool AllowSaveSub;
    [SerializeField] private float waitTimeSaveSub;
    [SerializeField] private List<GameObject> SubGroupCase;
    [SerializeField] private GameObject MonsterObjParent;
    [SerializeField] private GameObject BIGMonster;
    // All Light Control
    private bool AllLightControl;

    void Start()
    {
        instance = this;
        PlayerHP = 100;
        AllowSaveSub = true;
        AllLightControl = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //light control させる時
            if (SwitchAllow == true && LightControlAllow == true)
            {
                LightDown();
            }
            if (AllLightControl == true)
                TurnOnAllLight();
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
                LightControlAllow = true;
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
            }else if(other.gameObject.tag == "AllLightControl")
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
                LightControlAllow = false;
            }
        }
    }
    //--------------------------------------------
    private void DoDoorsAction()
    {
        foreach (var item in GroupCase)
        {
            if(item.name == "Door")
            {
                if (item.GetComponent<BoxCollider>().isTrigger == false)
                {
                    item.GetComponent<Animator>().Play("DoorAnimate");
                    Invoke(nameof(OpenDoor), waitTimeAnimateDoors);
                }else
                {
                    item.GetComponent<Animator>().Play("DoorAnimateClose");
                    Invoke(nameof(CloseDoor), waitTimeAnimateDoors);
                }
            }
        }
        if (AllowSaveSub == true)
        {
            // add storage and lock
            SubGroupCase = GroupCase;
            AllowSaveSub = false;
            // Save storage affter 15s
            Invoke(nameof(SaveSubGroupCase), waitTimeSaveSub);
        }
    }
    private void DoSubDoorsAction()
    {
        foreach (var item in SubGroupCase)
        {
            if (item.name == "Door")
            {
                if (item.GetComponent<BoxCollider>().isTrigger == false)
                {
                    item.GetComponent<Animator>().Play("DoorAnimate");
                    Invoke(nameof(OpenDoor), waitTimeAnimateDoors);
                }
                else
                {
                    item.GetComponent<Animator>().Play("DoorAnimateClose");
                    Invoke(nameof(CloseDoor), waitTimeAnimateDoors);
                }
            }
        }
    }
    //--------------------------------------------
    private void LightDown()
    {
        foreach (var item in GroupCase)
        {
            if (item.name == "Light" && item.activeSelf == false)
            {
                item.SetActive(true);
                item.GetComponent<Animator>().Play("LightDownAnimate");
                DoInvoke();
            }
        }
    }
    private void DoInvoke()
    {
        Invoke(nameof(DoDoorsAction), waitTimeAnimateLightDown);
        Invoke(nameof(LightUp), WaitTimeAnimateDoLightUp);
    }
    private void LightUp()
    {
        foreach (var item in SubGroupCase)
        {
            if(item.name == "Light")
            {
                item.GetComponent<Animator>().Play("LightUpAnimate");
                Invoke(nameof(SetActiveLight), WaitTimeSetActiveLight);
                DoSubDoorsAction();
            }
        }
    }
   
    private void SetActiveLight()
    {
        foreach (var item in SubGroupCase)
        {
            if (item.name == "Light")
            {
                item.SetActive(false);
            }
        }
    }
    //--------------------------------------------
    private void OpenDoor()
    {
        foreach (var item in GroupCase)
        {
            if(item.name == "Door")
            {
                item.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }
    private void CloseDoor()
    {
        foreach (var item in GroupCase)
        {
            if (item.name == "Door")
            {
                item.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
    //--------------------------------------------
    private void SaveSubGroupCase()
    {
        AllowSaveSub = true;
        SubGroupCase = GroupCase;
        Debug.Log("Save New storage");
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
