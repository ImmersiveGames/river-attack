using System;
using UnityEngine;
using UnityEngine.UI;
using MyUtils.Variables;
[RequireComponent(typeof(Image))]
public class UIFuelBar : MonoBehaviour
{
    [SerializeField]
    private int playerId;
    [SerializeField]
    private float smoothTransition;
    [SerializeField]
    private Text textFuel;
    [SerializeField]
    private Color colorFull;
    [SerializeField, Range(0, 1)]
    private float medFuel;
    [SerializeField]
    private Color colorMed;
    [SerializeField, Range(0, 1)]
    private float alertFuel;
    [SerializeField]
    private Color colorAlert;
    private float point;
    private float pointFull;

    private Image progressbar;
    private Quaternion myRot;
    private Player player;
    private GamePlayMaster gamePlay;

    private void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventResetPlayers += Init;
    }

    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        player = gamePlay.GetPlayerSettings(playerId);
        progressbar = GetComponent<Image>();
    }

    private void Start()
    {
        Init();  
    }

    private void Init()
    {
        pointFull = MyUtils.Tools.GetCento(player.actualHP.Value, player.maxHP.Value);
        point = pointFull / 100;
        progressbar.fillAmount = point;
        progressbar.color = colorFull;
    }
    /// <summary>
    /// Envia o valor dp combustivel para a UI do ponteiro de combustivel
    /// </summary>
    /// 
    private void FillPointerUI()
    {
        if (player.actualHP.Value != point || player.actualHP.Value == player.maxHP.Value)
        {
            float cento = MyUtils.Tools.GetCento(player.actualHP.Value, pointFull);
            point = cento / 100;
            textFuel.text = (cento > player.maxHP.Value) ? player.maxHP.Value.ToString() + "%" : cento.ToString("0") + "%";
            Color actualColor = (point > medFuel) ? colorFull : (point > alertFuel) ? colorMed : colorAlert;
            progressbar.fillAmount = Mathf.Lerp(progressbar.fillAmount, point, Time.deltaTime);
            progressbar.color = Color.Lerp(progressbar.color, actualColor, Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FillPointerUI();
    }

    private void OnDisable()
    {
        gamePlay.EventResetPlayers -= Init;
    }
}
