using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils.Variables;

[RequireComponent(typeof(PlayerMaster))]
public class PlayerShoot : MonoBehaviour, ICommand, IHasPool
{
    [Tooltip("Identifica se o jogador em modo rapidfire")]
    
    [SerializeField]
    private FloatVariable shootCadency;
    [SerializeField]
    private IntReference idButtonMap;
    [Tooltip("Objeto disparado pelo player")]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private int startpool;

    private ControllerMap controllerMap;
    private float nextShoot;
    private GameObject myShoot;
    private PlayerMaster playerMaster;

    /// <summary>
    /// Executa quando ativa o objeto
    /// </summary>
    /// 
    private void OnEnable()
    {
        SetInitialReferences();
        StartMyPool();
        Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
    }
    /// <summary>
    /// Configura as referencias iniciais
    /// </summary>
    /// 
    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        controllerMap = playerMaster.playerSettings.controllerMap;
    }
    private void Update()
    {
        if (controllerMap.ButtonUp(idButtonMap.Value))
        {
            this.Execute();
        }
    }

    public void Execute()
    {
        if (shootCadency.Value < 0 || !playerMaster.ShouldPlayerBeReady) return;
        playerMaster.CallEventPlayerShoot();
        if (shootCadency.Value > 0 && nextShoot < Time.time)
        {
            nextShoot = Time.time + shootCadency.Value;
            Fire();
        }
        else if (!myShoot || myShoot.activeSelf == false)
            Fire();
    }

    private void Fire()
    {
        myShoot = PoolObjectManager.Instance.GetObject(this);
        double sp = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("player_speed_shoot").DoubleValue;
        myShoot.GetComponent<PlayerBullet>().SetSpeedShoot(sp);
        myShoot.transform.parent = null;
        myShoot.GetComponent<PlayerBullet>().OwnerShoot = playerMaster;
        myShoot.transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        myShoot.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }

    public void StartMyPool(bool isPersistent = false)
    {
        PoolObjectManager.Instance.CreatePool(this, bullet, startpool, transform, isPersistent);
    }
}
