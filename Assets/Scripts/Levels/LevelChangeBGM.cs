using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeBGM : MonoBehaviour
{
    public GamePlayAudio.LevelType idBGMtoChange;
    public float speedy;
    GamePlayAudio playAudio;
    GamePlayMaster playMaster;
    private void OnEnable()
    {
        playMaster = GamePlayMaster.Instance;
        playAudio = GamePlayAudio.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform.root.CompareTag(GameSettings.Instance.playerTag)))
        {
            Debug.Log("Colidiu");
            playAudio.ChangeBGM(idBGMtoChange, speedy);
            playMaster.actualBGM = idBGMtoChange;
            GetComponent<Collider>().enabled = false;
        }
    }
}
