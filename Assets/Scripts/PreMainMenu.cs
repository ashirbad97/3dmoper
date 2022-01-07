using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class PreMainMenu : MonoBehaviour {

    public GameObject playerInputUID;
    public static string playerUID;
    const string apiEndpoint = "https://my-json-server.typicode.com/typicode/demo/posts";
    public void mainMenu()
    {
        playerUID = playerInputUID.GetComponent<TMPro.TextMeshProUGUI>().text;
        Debug.Log(playerUID);
        StartCoroutine(ProcessRequest(apiEndpoint));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Application has been quit");
        Application.Quit();
    }
    private IEnumerator ProcessRequest(string uri)
    {
        Debug.Log("Start send request to API");
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log("Error is : "+ request.error);
            else
                Debug.Log(request.downloadHandler.text);
        }
    }
}
