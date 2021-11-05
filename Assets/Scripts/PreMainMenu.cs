using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PreMainMenu : MonoBehaviour {

    public GameObject playerInputUID;
    public static string playerUID;

    public void mainMenu()
    {
        playerUID = playerInputUID.GetComponent<TMPro.TextMeshProUGUI>().text;
        Debug.Log(playerUID);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Application has been quit");
        Application.Quit();
    }
}
