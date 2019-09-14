using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public delegate void PostRequestCallback(bool error, string response);
public class HttpRequestApi : MonoBehaviour{

    string Header = "Bearer 4e09a815-6f53-4506-996a-39322f0ac417";
    string url = "https://sbxcloud.com/api/data/v1/row";
    public IEnumerator PostRequest(string bodyJsonString, PostRequestCallback callback)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer 4e09a815-6f53-4506-996a-39322f0ac417");

        yield return request.SendWebRequest();

        Debug.Log(request);
        Debug.Log("Response: " + request.downloadHandler.text);
        Debug.Log("Response: " + request.isNetworkError);
        Debug.Log("Response: " + request.responseCode);

        callback(!request.isNetworkError, request.downloadHandler.text);
    }
}