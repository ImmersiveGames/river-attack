using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utils.Variables;
using UnityEngine;

public class UIDistance : MonoBehaviour {
    [SerializeField]
    private IntVariable pathDistance;
    [SerializeField]
    private Text textDistance;

    private int m_distance;

    private void OnEnable()
    {
        m_distance = 0;
        textDistance.text = m_distance.ToString();
    }

    private void LateUpdate()
    {
        if (m_distance != (int)pathDistance.Value)
        {
            m_distance = (int)pathDistance.Value;
            textDistance.text = m_distance.ToString();
        }
    }
}
