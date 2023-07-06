using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils;

public class PlayerBombSet : MonoBehaviour {

    [SerializeField]
    private ParticleSystem pSystem;
    public PlayerMaster OwnerShoot { get; set; }
    [SerializeField]
    private float radiusSize;
    [SerializeField]
    private float radiusSpeed;
    [SerializeField]
    private float shakeForce;
    [SerializeField]
    private float shakeTime;
    [SerializeField]
    private long milsecondsVibrate;
    public float timeLife { get; private set; } 
    private float endlife;
    private double tParam;

    private Collider myCol;

    private void OnEnable()
    {
        timeLife = pSystem.main.duration;
        myCol = GetComponent<Collider>();
    }

    // Use this for initialization
    void Start () {
        endlife = Time.time + timeLife;
    }

    private void AutoDestroy()
    {
        if (Time.time >= endlife)
        {
            DestroyMe();
        }
    }
    private void ExpandCollider()
    {
        tParam += Time.deltaTime * radiusSpeed;
        GamePlayMaster.Instance.CallEventShakeCam(shakeForce, shakeTime);
#if UNITY_ANDROID && !UNITY_EDITOR
        ToolsAndroid.Vibrate(milsecondsVibrate);
        Handheld.Vibrate();
#endif
        if (myCol.GetType() == typeof(SphereCollider)) {
            SphereCollider sphere = (SphereCollider)myCol;
            sphere.radius = Mathf.Lerp(0.5f, radiusSize, (float)tParam);
        } 
    }
    void FixedUpdate()
    {
        ExpandCollider();
        AutoDestroy();
    }

    private void DestroyMe()
    {
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
