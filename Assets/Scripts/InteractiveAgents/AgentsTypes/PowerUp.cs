using Utils.Variables;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Namespace:      None
/// Class:          PowerUp
/// Description:    Scriptable of Powerups
/// Author:         Renato Innocenti                    Date: 30/03/2018
/// Notes:          copyrights 2017-2018 (c) immersivegames.com.br - contato@immersivegames.com.br       
/// Revision History:
/// Name: v0.1           Date: 30/03/2018       Description: Esboço inicial
/// </summary>
///

[CreateAssetMenu(fileName = "PowerUp", menuName = "Agents/PowerUp", order = 3)]
public class PowerUp : ScriptableObject//Collectibles
{
    [Header("PowerUp Set")]
    [SerializeField]
    public new string name;
    [SerializeField]
    public FloatReference duration;

    [SerializeField]
    public bool canAccumulateEffects; // este powerup pode acumular com outros efeitos;

    [SerializeField]
    public bool canAccumulateDuration; // este power up acumula o tempo com outros iguais a ele;

    // used to apply the Powerup of the Powerup
    [SerializeField]
    public UnityEvent startAction;

    // used to remove the Powerup of the Powerup
    [SerializeField]
    public UnityEvent endAction;

    public void PowerUpStart(Player player)
    {
        GamePlayPowerUps.target = player;
        if (startAction != null)
            startAction.Invoke();
    }
public void PowerUpEnd(Player player)
    {
        GamePlayPowerUps.target = player;
        if (endAction != null)
            endAction.Invoke();
    }
}
