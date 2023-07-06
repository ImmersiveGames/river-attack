using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils.Variables;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Agents/Player", order = 101)]
public class Player : ScriptableObject
{
    [SerializeField]
    new public string name;
    public IntVariable score;
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;
    [Header("Skin Settings")]
    [SerializeField]
    public ShopProductSkin playerSkin;
    [Header("Shopping")]
    public IntVariable wealth;
    [SerializeField]
    public List<ShopProduct> listProducts;
    [Header("Controller Settings")]
    public ControllerMap controllerMap;

    public FloatVariable mySpeedy;
    public FloatVariable myAgility;

    [Range(1f, 30f)]
    public float speedHorizontal;
    [Range(1f, 10f)]
    public float speedVertical;
    [Range(1.1f, 5f)]
    public float multiplyVelocityUp;
    [Range(.01f, 1f)]
    public float multuplyVelocityDown;
    public float shootVelocity = 10f;
    [Header("PowerUP Effects")]
    public FloatVariable speedyShoot;
    [Header("Player Lives e HP")]
    public IntReference maxHP;
    public IntVariable actualHP;
    public IntReference startLives;
    public IntReference maxLives;
    public IntVariable lives;
    public IntVariable bombs;
    public IntReference maxBombs;    

    public void ChangeLife(int life)
    {
        if (maxLives != 0 && lives.Value + life >= maxLives)
            lives.SetValue(maxLives);
        else if (lives.Value + life <= 0)
            lives.SetValue(0);
        else
            lives.ApplyChange(life);
    }

    public void InitPlayer()
    {
        lives.SetValue(startLives);
        bombs.SetValue(3);
        actualHP.SetValue(maxHP);
        //LoadListShop();
    }
}