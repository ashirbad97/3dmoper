using UnityEngine;
[SerializeField]
public class apiServerPayload {
    public int sessionId;
    public string timestamp;
    public string subjectId;
    public string _id;
    public int __v;

    public static apiServerPayload createFromJson(string jsonPayload)
    {
        return JsonUtility.FromJson<apiServerPayload>(jsonPayload);
    }
}
