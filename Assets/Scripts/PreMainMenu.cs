using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class PreMainMenu : MonoBehaviour {
    //Endpoints only for debugging purposes
    //public static string jioFiEndpoint = "http://192.168.225.193:3000/api/checkUser";
    //public static string iitdWifiEndPoint = "http://10.17.6.83:3000/api/checkUser";
    public string fileUploadEndPoint;
    public string apiEndpoint;
    public apiServerPayload fetchedPayload;
    string responseOkMsg = "Subject Found";
    public TMP_InputField subjectUID;
    public GameObject uiCanvas;
    public void mainMenu()
    {
        subjectData.fileUploadEndpoint = fileUploadEndPoint;
        subjectData.subjectuid = subjectUID.text;
        Debug.Log("User Search :  "+subjectData.subjectuid);
        Debug.Log("Endpoint is "+ apiEndpoint);
        StartCoroutine(ProcessRequest(apiEndpoint, subjectData.subjectuid));
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
            {
                Debug.Log("Network Error is: " + request.error);
            }
            else
            {
                if (request.responseCode == 200)
                {
                        Debug.Log("User Found, Starting Trials");
                        Debug.Log(request.downloadHandler.text);
                        Debug.Log(request.responseCode);
                        fetchedPayload = apiServerPayload.createFromJson(request.downloadHandler.text);
                        subjectData.sessionId = fetchedPayload.sessionId;
                        Debug.Log(subjectData.sessionId);
                        PlayerPrefs.SetInt("counter", 1);
                        //Loads the scene with black ball
                        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        // Disable the UI Canvas
                        uiCanvas.SetActive(false);
                        // Instead of just loading the scene, load it additively
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1,LoadSceneMode.Additive);
                }
                else
                {
                    Debug.Log("User Not Found, add User and try again");
                }
            }           
        }
    }
}
