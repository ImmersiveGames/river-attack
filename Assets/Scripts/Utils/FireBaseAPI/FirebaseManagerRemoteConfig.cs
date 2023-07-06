using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.RemoteConfig;

public class FirebaseManagerRemoteConfig
{
    public FirebaseManagerRemoteConfig()
    {
        FirebaseRemoteConfig.FetchAsync(TimeSpan.Zero).ContinueWith(ntask =>
        {
            if (ntask.IsCanceled)
            {
                Debug.Log("Fetch canceled.");
            }
            else if (ntask.IsFaulted)
            {
                Debug.Log("Fetch encountered an error.");
            }
            else if (ntask.IsCompleted)
            {
                Debug.Log("Fetch completed successfully!");
            }

            ConfigInfo info = FirebaseRemoteConfig.Info;
            switch (info.LastFetchStatus)
            {
                case LastFetchStatus.Success:
                    FirebaseRemoteConfig.ActivateFetched();
                    Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                                           info.FetchTime));
                    break;
                case LastFetchStatus.Failure:
                    switch (info.LastFetchFailureReason)
                    {
                        case FetchFailureReason.Error:
                            Debug.Log("Fetch failed for unknown reason");
                            break;
                        case FetchFailureReason.Throttled:
                            Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                            break;
                    }
                    break;
                case LastFetchStatus.Pending:
                    Debug.Log("Latest Fetch call still pending.");
                    break;
            }
        });
    }

    public T GetRemoteConfig<T>(string name, T defaultValue)
    {
        ConfigInfo info = FirebaseRemoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success) return defaultValue;
        FirebaseRemoteConfig.ActivateFetched();
        if (typeof(T) == typeof(Boolean))
            return (T)(object)FirebaseRemoteConfig.GetValue(name).BooleanValue;
        if (typeof(T) == typeof(string))
            return (T)(object)FirebaseRemoteConfig.GetValue(name).StringValue;
        if (typeof(T) == typeof(long) || (typeof(T) == typeof(int)))
            return (T)(object)FirebaseRemoteConfig.GetValue(name).LongValue;
        if (typeof(T) == typeof(double) || (typeof(T) == typeof(float)))
            return (T)(object)FirebaseRemoteConfig.GetValue(name).DoubleValue;
        else
            return defaultValue;
    }
}
