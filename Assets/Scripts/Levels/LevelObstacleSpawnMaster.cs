using UnityEngine;

public class LevelObstacleSpawnMaster : MonoBehaviour
{
    [SerializeField]
    private bool persistPrefab = true;
    [SerializeField]
    private GameObject[] prefab;
    [SerializeField]
    private bool goalLevel;
    
    [Header("Show prefab ID -Only View")]
    public int viewIdPrefab;
    public Color wireColor = new Color(255, 0, 0, 050);

    public GameObject GetPrefab { get { return prefab[viewIdPrefab]; } }

    private void Awake()
    {
        this.gameObject.SetActive(true);
        SpawnObstacles();
        this.gameObject.SetActive(false);
    }

    private void SpawnObstacles()
    {
        if (prefab != null && prefab.Length > 0)
        {
            SetGameObjectsActive(prefab, false);
            int sort = Random.Range(0, prefab.Length - 1);
            GameObject enemy = Instantiate<GameObject>(prefab[sort], this.transform.position, this.transform.rotation, this.transform.root);
            if (!persistPrefab)
            {
                enemy.GetComponent<EnemyMaster>().goalLevel = goalLevel;
                ObstacleMoviment om = enemy.GetComponent<ObstacleMoviment>();
                ObstacleMoveByApproach omba = enemy.GetComponent<ObstacleMoveByApproach>();
                if (GetComponent<LevelObstacleSpawnMoviment>() && om)
                {
                    LevelObstacleSpawnMoviment spawnMoviment = GetComponent<LevelObstacleSpawnMoviment>();
                    om.canMove = spawnMoviment.canMove;
                    om.MovementSpeed = spawnMoviment.MovementSpeed;
                    om.MoveDirection = spawnMoviment.MoveDirection;
                    om.FreeDirection = spawnMoviment.FreeDirection;
                    om.CurveMoviment = spawnMoviment.CurveMoviment;
                }
                if (GetComponent<LevelObstacleSpawnMovApproch>() && omba)
                {
                    LevelObstacleSpawnMovApproch spawnMovimentApp = GetComponent<LevelObstacleSpawnMovApproch>();
                    omba.RadiusPlayerProximity = spawnMovimentApp.RadiusPlayerProximity;
                    omba.TimeToCheck = spawnMovimentApp.TimeToCheck;
                    omba.dificultType = spawnMovimentApp.dificultType;
                    omba.RandomPlayerDistanceNear = spawnMovimentApp.RandomPlayerDistanceNear;
                    omba.enDifList = spawnMovimentApp.enDifList;
                }
            }
            enemy.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void SetGameObjectsActive(GameObject[] objects, bool active)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(active);
        }
    }

    [ContextMenu("LoadPrefab")]
    private void LoadPrefab()
    {
        if (prefab != null && prefab.Length > 0)
        {
            goalLevel = prefab[viewIdPrefab].GetComponent<EnemyMaster>().goalLevel;
        }
    }

    #region DrawGizmos
    void OnDrawGizmos()
    {
        if (prefab != null && prefab.Length > 0)
        {
            Gizmos.color = wireColor;
            Mesh mesh = prefab[viewIdPrefab].GetComponentInChildren<MeshFilter>().sharedMesh;
            Gizmos.DrawWireMesh(mesh, transform.position, transform.rotation, transform.localScale);
        }
    }
    #endregion
}