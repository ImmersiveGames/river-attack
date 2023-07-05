using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

[RequireComponent(typeof(PlayerMaster))]
public class PlayerShoot : MonoBehaviour, ICommand
{
    [Tooltip("Identifica se o jogador em modo rapidfire")]
    [SerializeField]
    private ControllerMap controllerMap;
    [SerializeField]
    private FloatVariable shootCadency;
    [SerializeField]
    private IntReference idButtonMap;
    [Tooltip("Objeto disparado pelo player")]
    [SerializeField]
    private GameObject bullet;
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
    }
    /// <summary>
    /// Configura as referencias iniciais
    /// </summary>
    /// 
    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
    }
    private void Update()
    {
        if (controllerMap.ButtonDown(idButtonMap.Value))
        {
            Execute();
        }
    }

    public void Execute()
    {

        if (shootCadency.Value < 0 || !playerMaster.ShouldPlayerBeReady) return;
        playerMaster.CallEventPlayerShoot();
        if (shootCadency.Value > 0 && nextShoot < Time.time)
        {
            nextShoot = Time.time + shootCadency.Value;
            myShoot = Instantiate(bullet, transform.position, bullet.transform.rotation);
            myShoot.GetComponent<PlayerBullet>().OwnerShoot = playerMaster;
        }
        else if (myShoot != true)
        {
            myShoot = Instantiate(bullet, transform.position, bullet.transform.rotation);
            myShoot.GetComponent<PlayerBullet>().OwnerShoot = playerMaster;
        }
    }
    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}
