using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils.Variables;

public class HUBIconPlayer : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private float speedIcone = 10;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private AudioEventSample iconMoveSound;

    private void OnEnable()
    {
        SetupIconPlayer();
    }

    private void Update()
    {
        if (GetComponent<SpriteRenderer>().sprite != player.playerSkin.spriteItem)
        {
            GetComponent<SpriteRenderer>().sprite = player.playerSkin.spriteItem;

        }
    }
    public void SetupIconPlayer()
    {
        GetComponent<SpriteRenderer>().sprite = player.playerSkin.spriteItem;
    }

    public void SetPosition(Transform pos)
    {
        this.transform.position = pos.position - offset;
    }

    public void GoIcone(Vector3 pos)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (pos - transform.position).normalized);
        // Vector3 npos = (transform.rotation.y < 0)? pos + offset: pos - offset;
        StartCoroutine(MoveIcon(pos, speedIcone));
    }

    private IEnumerator MoveIcon(Vector3 pointTo, float time)
    {
        iconMoveSound.Play(GetComponent<AudioSource>());
        while (transform.position != pointTo)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointTo, time * Time.deltaTime);
            yield return null;
        }
        iconMoveSound.Stop(GetComponent<AudioSource>());
        HUBMaster.Instance.hubPause = false;
    }
}
