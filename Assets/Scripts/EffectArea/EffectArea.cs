using MyUtils.Variables;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EffectArea", menuName = "Agents/EffectArea", order = 5)]

public class EffectArea : Enemy
{
    [Header("Effect Area Settings")]
    public IntReference maxCollectible;
    [SerializeField]
    public UnityEvent effectAreaActions;

    public void EffectAreaStart(Player player)
    {
        GamePlayPowerUps.target = player;
        if (effectAreaActions != null)
            effectAreaActions.Invoke();
    }
}
