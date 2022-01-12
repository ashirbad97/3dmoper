using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class PreMainMenu : MonoBehaviour {

    public static string playerUID;
    public TMP_InputField subjectUID; 
    const string apiEndpoint = "http://192.168.225.193:3000/api/checkUser";
    public void mainMenu()
    {
        playerUID = subjectUID.text;
        StartCoroutine(ProcessRequest(apiEndpoint, playerUID));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Application has been quit");
        Application.Quit();
    }
    private IEnumerator ProcessRequest(string uri, string SubjectUid)
    {

        WWWForm form = new WWWForm();
        form.AddField("uid", SubjectUid);

        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log("Error is : "+ request.error);
            else
                Debug.Log(request.downloadHandler.text);
        }
    }
}
