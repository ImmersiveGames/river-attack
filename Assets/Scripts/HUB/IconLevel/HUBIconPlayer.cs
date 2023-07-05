using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

public class HUBIconPlayer : MonoBehaviour {

    [SerializeField]
    private float iconSpeed;
    [SerializeField]
    private Transform iconStart;
    [SerializeField]
    private Vector3Variable startIconPosition;

    private HUBMaster hubMaster;
    private HUBSettings hubSettings;
    private GameManager gameManager;
    private ShopMaster shopMaster;

    private void OnEnable()
    {
        SetInitialReferences();
        
        hubMaster.EventOnFocusElement += GoIcone;
        shopMaster.EventButtonSelect += SkinIcon;
    }

    private void SetInitialReferences()
    {
        hubSettings = HUBSettings.Instance;
        hubMaster = HUBMaster.Instance;
        gameManager = GameManager.Instance;
        shopMaster = ShopMaster.Instance;

    }
    private void Start()
    {
        SkinIcon(gameManager.Players[0], null);
        if (hubSettings.RealFinishLevels.Count < 1)
        {
            startIconPosition.SetValue(iconStart.position);
        }
        Vector3 vec = new Vector3(startIconPosition.Value.x, startIconPosition.Value.y, startIconPosition.Value.z);
        StartCoroutine(HUBCameraMaster.Instance.MoveCam(vec, 0, true));
        transform.position = startIconPosition.Value;
    }
    private void SkinIcon(Player player, ShopProduct item)
    {
        GetComponent<SpriteRenderer>().sprite = player.playerSkin.spriteItem;
    }

    private void GoIcone(Levels nextlevel, Vector3 pos)
    {
        if (!nextlevel.CheckIfLocked(gameManager.levelsFinish.Value))
        {
            Debug.Log("go: " + pos);
            startIconPosition.SetValue(pos);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, (pos - transform.position).normalized);
            StartCoroutine(MoveIcon(pos, iconSpeed));
        }
    }

    private IEnumerator MoveIcon(Vector3 pointTo, float time)
    {
        while (transform.position != pointTo)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointTo, time * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDisable()
    {
        hubMaster.EventOnFocusElement -= GoIcone;
        shopMaster.EventButtonSelect -= SkinIcon;
    }
}
