using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultLines : MonoBehaviour {

    [SerializeField]
    private Image imageLine;
    [SerializeField]
    private Text qnt;
    [SerializeField]
    private Text pnt;

    public void SetDisplay(Sprite sprite, int quantity, int points)
    {
        imageLine.sprite = sprite;
        qnt.text = string.Format("x{0}", quantity);
        pnt.text = string.Format("{0}pts", points);
    }
}
