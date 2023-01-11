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
    public GameObject MainMenu;
    public GameObject MiniMap;

    private void Start()
    {
        instance = this;
        MainMenu.SetActive(true);
        MiniMap.SetActive(false);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("ThuongScene");
        MiniMap.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenuUI()
    {
        SceneManager.LoadScene("ThuongScene");
        MainMenu.SetActive(true);
        MiniMap.SetActive(false);

    }
    private void Update()
    {
        if(PlayerBehavior.Instance.PlayerHP <= 0)
        {
            GameOverUI.SetActive(true);
            MiniMap.SetActive(false);
        }
    }
}
