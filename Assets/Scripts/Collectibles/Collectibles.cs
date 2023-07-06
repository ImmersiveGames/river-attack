using MyUtils.Variables;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleItem", menuName = "Agents/Collectible", order = 2)]

public class Collectibles : Enemy
{
    [Header("Collectibles Settings"), Tooltip("Limite de itens coletaveis")]
    public IntReference maxCollectible;
    [Tooltip("Valor em dinheiro do coletavel")]
    public int collectValuable;
    [Tooltip("Quantos itens dele mesmo ele representa")]
    public int ammontColletables = 1;
    [SerializeField]
    private PowerUp powerUp;

    public PowerUp PowerUp { get { return powerUp; } }

}
