using UnityEngine;
using Firebase;
using System.Collections.Generic;

public class GameManagerFirebase
{
    public DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    public FirebaseApp MyFirebaseApp { get; private set; }
    public FirebaseManagerRemoteConfig firebaseRemote;

    private FadeScenesManager fadeScenes;

    public GameManagerFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                MyFirebaseApp = FirebaseApp.DefaultInstance;
                firebaseRemote = new FirebaseManagerRemoteConfig();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
}
