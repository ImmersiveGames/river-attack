using UnityEngine;
public class DontDestroyThis : Singleton<DontDestroyThis> {

    void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    protected override void OnDestroy() { }
}