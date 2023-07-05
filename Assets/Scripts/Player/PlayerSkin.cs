using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMaster))]

public class PlayerSkin : MonoBehaviour
{
    [SerializeField]
    private ShopProductSkin defaultSkin;

    private PlayerMaster playerMaster;

    private void OnEnable()
    {
        SteInitialReferences();
        ShopProductSkin skin = playerMaster.playerSettings.playerSkin ?? defaultSkin;
        SetPLayerSkin(skin);
        playerMaster.EventChangeSkin += SetPLayerSkin;
    }

    private void SteInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
    }

    private void SetPLayerSkin(ShopProductSkin skin)
    {
        DestroyImmediate(transform.GetChild(0).gameObject);
        GameObject go = Instantiate(skin.GetSkin, transform);
        go.transform.SetAsFirstSibling();
    }

    private void OnDisable()
    {
        playerMaster.EventChangeSkin -= SetPLayerSkin;
    }
}
