using Utils.Variables;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleItem", menuName = "Agents/Collectible", order = 2)]

public class Collectibles : Enemy
{
    [Header("Collectibles Settings")]
    public IntReference maxCollectible;
    public int collectValuable;
    [SerializeField]
    private PowerUp powerUp;

    public PowerUp PowerUp { get { return powerUp; } }
}
