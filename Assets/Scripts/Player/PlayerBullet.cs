/// <summary>
/// Namespace:      None
/// Class:          PlayerBullet
/// Description:    Controla o objeto de disparo do jogador
/// Author:         Renato Innocenti                    Date: 26/03/2018
/// Notes:          copyrights 2017-2018 (c) immersivegames.com.br - contato@immersivegames.com.br       
/// Revision History:
/// Name: v1.0           Date: 26/03/2018       Description: Dipara o tiro e saindo da tela desaparece
/// </summary>
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    #region Variable Private Inspector
    [SerializeField] AudioEventSample audioShoot;
    [SerializeField] float shootVelocity = 10f;
    [SerializeField] bool bulletLifeTime = false;
    [SerializeField] float lifeTime = 2f;
    private float startTime;
    public PlayerMaster OwnerShoot { get; set; }
    public Transform myPool { get; set; }
    #endregion

    private void OnEnable()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioShoot.Play(audioSource);
    }
    /// <summary>
    /// Executa quando o objeto esta ativo
    /// </summary>
    /// 
    private void Start()
    {
        startTime = Time.time + lifeTime;
    }

    public void SetSpeedShoot(double speedy)
    {
        shootVelocity = (float)speedy;
    }
    /// <summary>
    /// Executa a cada atualização de frame da fisica
    /// </summary>
    /// 
    void FixedUpdate()
    {
        MoveShoot();
        AutoDestroy();
    }
    /// <summary>
    /// Proper this object forward
    /// </summary>
    /// 
    private void MoveShoot()
    {
        if (GamePlayMaster.Instance.ShouldBePlayingGame)
        {
            float speedy = shootVelocity * Time.deltaTime;
            transform.Translate(Vector3.forward * speedy);
        }
        else
        {
            DestroyMe();
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.transform.root.CompareTag(GameSettings.Instance.playerTag))
        {
            if (collision.transform.root.CompareTag(GameSettings.Instance.collectionTag)){
                Collectibles collectibles = (Collectibles)collision.transform.root.GetComponent<EnemyMaster>().enemy;
                if (collectibles.PowerUp != null) return;
            } 
            DestroyMe();
        }
    }
    /// <summary>
    /// If OnLifeTime set, auto destroyer this object
    /// </summary>
    /// 
    private void AutoDestroy()
    {
        if (bulletLifeTime && Time.time >= startTime)
        {
            DestroyMe();
        }
    }
    /// <summary>
    /// Atalho para destruir este objeto
    /// </summary>
    private void DestroyMe()
    {
        //Destroy(this.gameObject);
        
        gameObject.SetActive(false);
        gameObject.transform.SetParent(myPool);
        gameObject.transform.SetAsLastSibling();
    }
    /// <summary>
    /// Se o objeto sai da tela ele é destruido
    /// </summary>
    private void OnBecameInvisible()
    {
        Invoke("DestroyMe", .1f);
    }
}
