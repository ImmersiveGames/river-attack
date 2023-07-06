using System.Collections;
using MyUtils.Variables;
using UnityEngine;

[RequireComponent(typeof(PlayerMaster))]
public class PlayerDistance : MonoBehaviour
{
    [SerializeField]
    private FloatReference cadencyDistance;
    private GamePlayMaster gamePlay;
    private PlayerMaster playerMaster;
    public int distanceOffset { get; set; }
    public int pathDistance { get; private set; }

    private float checkTime = 2;
    private float lifeTime;

    private void Awake()
    {
        pathDistance = 0;
    }
    private void OnEnable()
    {
        SetInitialReferences();
        playerMaster.EventPlayerReload += ClearDistance;
        gamePlay.EventCheckPoint += Log;
    }

    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        gamePlay = GamePlayMaster.Instance;
        distanceOffset = (int)(gamePlay.GetActualLevel().levelMilstones[0].z / cadencyDistance);
        lifeTime = Time.time + checkTime;
    }

    private void Log(Vector3 position)
    {
        GamePlaySettings.Instance.pathDistance = pathDistance;
        if (GameManager.Instance.firebase.MyFirebaseApp != null && GameManager.Instance.firebase.dependencyStatus == Firebase.DependencyStatus.Available)
        {
            Firebase.Analytics.Parameter[] Distance = {
                new Firebase.Analytics.Parameter("TotalDistance", pathDistance),
            new Firebase.Analytics.Parameter("Milstone", gamePlay.GetActualPath())
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Distance", Distance);
        }
    }

    private void LateUpdate()
    {
        UpdateDistance();
        if (Time.time > lifeTime)
        {
            //Debug.Log("Lifetime: "+ lifeTime +" Time: "+ Time.time);
            lifeTime = Time.time + checkTime;
            gamePlay.CallEventCheckPlayerPosition(transform.position);
        }
    }

    private void UpdateDistance()
    {
        if (playerMaster.ShouldPlayerBeReady)
            pathDistance = (int)(transform.position.z / cadencyDistance) - distanceOffset;
    }

    private void ClearDistance()
    {
        pathDistance = (int)(transform.position.z / cadencyDistance) - distanceOffset;
    }

    private void OnDisable()
    {
        playerMaster.EventPlayerReload -= ClearDistance;
        gamePlay.EventCheckPoint -= Log;
    }
}
