using UnityEngine;

public class ShowInternetConnection : MonoBehaviour
{
    public float secChecking;
    private void OnEnable()
    {
        InvokeRepeating("CheckInternetConnection",0, secChecking);
    }

    private void CheckInternetConnection()
    {
        if (!InternetCheck.InternetConnection  && !this.gameObject.activeSelf)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}