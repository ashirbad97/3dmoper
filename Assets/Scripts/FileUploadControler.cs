using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;

public class FileUploadControler : MonoBehaviour {
    public string uploadEndpoint;
    public string subjectFolderPath;
    public string[] fileList = new string[6];
    public string[] fileNameList = new string[6];
    public TMPro.TMP_Text userInfoLabel;
    public TMPro.TMP_Text uploadCompletionLabel;
    FileInfo[] allFiles;
    // Use this for initialization
    void Start () {
        //Endpoint only for debugging purpose
        //###############################################################################################
        //subjectFolderPath = "D:/Ashirbad/Unity_Projects/3dmoperTrial/Assets/trialOutput/ashirbad97/19";
        //uploadEndpoint = "https://3dmoper.xyz/api/fileIploadHandler";
        //###############################################################################################
        //Endpoint for actual production
        subjectFolderPath = subjectData.subjectFilePath;
        uploadEndpoint = subjectData.fileUploadEndpoint;
        //Changing the label to show user information
        userInfoLabel.text = "Subject UID is: "+subjectData.subjectuid+"\n Session ID is: "+subjectData.sessionId;
        Debug.Log("In Exit Scene");
        Debug.Log("Subject Folder Path " + subjectFolderPath);
        Debug.Log("Subject Upload Endpoint " + uploadEndpoint);
        Debug.Break();
        UploadFiles();
	}
    public void UploadFiles() 
    {
        Debug.Log("Will Upload " + subjectFolderPath + " to " + uploadEndpoint);
        DirectoryInfo currentDir = new DirectoryInfo(subjectFolderPath);
        Debug.Log("Directory Info " + currentDir);
        allFiles = currentDir.GetFiles("*.csv");
        Debug.Log(allFiles.Length);
        for (int i = 0; i < allFiles.Length; i++)
        {
            fileNameList[i] = Path.GetFileName(allFiles[i].ToString());
            fileList[i] = allFiles[i].ToString();
            Debug.Log("Will upload the file " + allFiles[i]);
        }
        StartCoroutine(StartUpload());
    }
    public void endGame()
    {
        Application.Quit();
    }
    IEnumerator StartUpload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        for (int i = 0; i < allFiles.Length; i++)
        {
            Debug.Log("Current Index is : "+ i);
            //Reading the contents of the file to upload
            byte[] rawData = System.IO.File.ReadAllBytes(allFiles[i].ToString());
            formData.Add(new MultipartFormFileSection(fileNameList[i], rawData));
            Debug.Log(allFiles[i]);
        }

        UnityWebRequest www = UnityWebRequest.Post(uploadEndpoint, formData);
        
        yield return www.SendWebRequest();
        Debug.Log(www);
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            uploadCompletionLabel.text = "Error in data upload: "+www.error;
        }
        //Block checks and execute if the POST request is successful, appears not to work
        else
        {
            Debug.Log("Uploaded all Files");
            //Displaying Label to inform upload is completed
            uploadCompletionLabel.text = "Data upload complete. Please proceed to the following link to view the results: < link = 'https3dmoper.xyz' > 3dmoper.xyz </ link >";
            //Application.Quit();
        }
    }
}
