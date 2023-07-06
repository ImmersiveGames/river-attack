using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaster : MonoBehaviour
{

    [SerializeField]
    public int playerIndex;
    [SerializeField]
    public bool cheasePlayer;
    [SerializeField]
    private PlayerMaster target; // identifica o alvo da camera
    [SerializeField]
    private Vector3 offset; // compensação do alvo para a camera

    public Enemy obstacle;
    private bool isShake = false;

    private float shakePower;
    private float shakeDuration;

    private GamePlayMaster gamePlay;

    private void OnEnable()
    {
        SetInitialReferences();
    }
    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        if (target == null && gamePlay.GetPlayer(playerIndex))
            target = gamePlay.GetPlayer(playerIndex).GetComponent<PlayerMaster>();

        gamePlay.EventShakeCam += ShakeCam;
    }

    public void ShakeCam(float power, float intime)
    {

        shakePower = power;
        shakeDuration = intime;
        if (!isShake) StartCoroutine(Shake());
    }

    public IEnumerator Shake()
    {
        isShake = true;
        Vector3 startpos = transform.localPosition;
        while (shakeDuration > 0.01f)
        {
            Vector3 skp = Random.insideUnitSphere * shakePower;
            transform.localPosition = new Vector3(offset.x + skp.x, target.transform.position.y + offset.y + skp.y, target.transform.position.z + offset.z + skp.x + skp.z);
            shakeDuration -= Time.deltaTime * 1f;
            yield return null;
        }
        transform.localPosition = startpos;
        isShake = false;
    }
    void LateUpdate()
    {
        if (target != null && target.transform.gameObject.activeSelf && cheasePlayer && !isShake)
        {
            transform.position = new Vector3(offset.x, target.transform.position.y + offset.y, target.transform.position.z + offset.z);
        }
    }

    private void OnDisable()
    {
        gamePlay.EventShakeCam -= ShakeCam;
    }
}
