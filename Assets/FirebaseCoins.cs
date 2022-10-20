using System.Collections;
using UnityEngine;
// using Firebase;
// using Firebase.Database;
// using Firebase.Auth;
using TMPro;
using System.Net;

public class FirebaseCoins : MonoBehaviour
{


    /*


    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;
    public DatabaseReference DBreference;


    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }



    private IEnumerator UpdateCoin(string coin)
    {
        string ip = new WebClient().DownloadString("http://icanhazip.com");
        var DBTask = DBreference.Child("users").Child(ip).Child("Coin").SetValueAsync(coin);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("Updated");
        }
    }


    public void SaveButton() {
        StartCoroutine(UpdateCoin(GameObject.Find("Player").GetComponent<PlayerSwipe>().coinText.text));
    }



    */

}
