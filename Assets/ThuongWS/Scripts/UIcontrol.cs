using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIcontrol : MonoBehaviour
{
    private static UIcontrol instance;
    public static UIcontrol Instance => instance;
    public Transform PlayerOffSet;
    public Transform Player;
   [SerializeField] public GameObject GameOverUI;
   [SerializeField] public GameObject GameWinUI;
    private void Start()
    {
        instance = this;
        //GameOverUI.SetActive(false);
    }
    public void ReStart()
    {
        //Player = PlayerOffSet;
        //GameOverUI.SetActive(false);
        Invoke(nameof(DoAfter1s), 1f);
    }
    public void DoAfter1s()
    {
        SceneManager.LoadScene("ThuongScene");
    }
    public void MainMenu()
    {
        //GameOverUI.SetActive(true);
    }
    private void Update()
    {
        if(PlayerBehavior.Instance.PlayerHP <= 0)
        {
            GameOverUI.SetActive(true);
        }
    }
}
