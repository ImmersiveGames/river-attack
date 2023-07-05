using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Agents/Player", order = 101)]

public class Player : ScriptableObject
{
    
    [SerializeField]
    new public string name;
    [SerializeField]
    public IntVariable score;
    [SerializeField]
    public IntVariable wealth;
    [Header("Player Controle")]
    [SerializeField]
    [Range(1f, 10f)]
    public float speedHorizontal;
    [SerializeField]
    public FloatVariable speedVertical;
    [SerializeField]
    [Range(1.1f, 5f)]
    public float multiplyVelocityUp;
    [SerializeField]
    [Range(.01f, 1f)]
    public float multuplyVelocityDown;
    [SerializeField]
    public FloatVariable mySpeedy;
    [SerializeField]
    public FloatVariable myAgility;
    [SerializeField]
    public FloatVariable speedyShoot;

    [Header("Player Lives e HP")]
    [SerializeField]
    public IntReference maxHP;
    [SerializeField]
    public IntVariable actualHP;
    [SerializeField]
    public IntReference startLives;
    [SerializeField]
    public IntVariable lives;
    
    [Header("Player Skin")]
    [SerializeField]
    public ShopProductSkin playerDefaultSkin;
    [SerializeField]
    public ShopProductSkin playerSkin;
    [Header("Player HitList")]
    [SerializeField]
    private ListResults ListEnemysHit;
    [SerializeField]
    private ListResults ListCollectibles;
    [Header("Player Shop")]
    [SerializeField]
    private ListShopProduct listProducts;
    public ListShopProduct GetShopList { get { return listProducts; } }

    private void OnEnable()
    {
        mySpeedy.SetValue(speedVertical.Value);
        myAgility.SetValue(speedHorizontal);
        actualHP.SetValue(maxHP);
        speedyShoot.SetValue(0);
        lives.SetValue(startLives);
        score.SetValue(0);
        playerSkin = playerSkin ?? playerDefaultSkin;
    }

    public void UpdateEnemys(List<ItemResults> enemys)
    {
        if(enemys.GetType() == typeof(Collectibles))
        {
            ListCollectibles.AddRange(enemys);
        }
        else
        {
            ListEnemysHit.AddRange(enemys);
        }
    }

    public void AddFundsToWallet(int coins)
    {
        wealth.ApplyChange(coins);
        if (wealth.Value < 0)
            wealth.SetValue(0);
    }
}
