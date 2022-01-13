﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class PreMainMenu : MonoBehaviour {
    public static string jioFiEndpoint = "http://192.168.225.193:3000/api/checkUser";
    public static string iitdWifiEndPoint = "http://10.194.127.140:3000/api/checkUser";
    public apiServerPayload fetchedPayload;
    string responseOkMsg = "Subject Found";
    public TMP_InputField subjectUID;
    public string apiEndpoint = iitdWifiEndPoint;
    public void mainMenu()
    {
        subjectData.subjectuid = subjectUID.text;
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
                Debug.Log("Error is : " + request.error);
                Debug.Log("User Not Found, add User and try again");
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
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    Debug.Log("User Not Found, add User and try again");
                }
            }           
        }
    }
}
