using UnityEngine;
using MyUtils.Variables;

public class UIFuel : MonoBehaviour {

    [SerializeField]
    private IntVariable fuel;
    [SerializeField]
    [MinMaxRange(200,600)]
    private FloatRanged minMaxAngleRotation;
    [SerializeField]
    private float smoothRotation;

    private float point;
    private float pointFull;
    private Quaternion startRotation;

    private void OnEnable()
    {
        pointFull = (float)fuel.Value;
        point = (float)fuel.Value;
        startRotation = transform.rotation;
    }
    /// <summary>
    /// Envia o valor dp combustivel para a UI do ponteiro de combustivel
    /// </summary>
    /// 
    private void RotatePointerUI()
    {
        if (fuel.Value != point || fuel.Value == pointFull)
        {
            point = fuel.Value;
            float cento = MyUtils.Tools.GetCento(fuel.Value, pointFull);
            float centoDistancia = MyUtils.Tools.GetValorCento(cento, minMaxAngleRotation.maxValue - startRotation.eulerAngles.z);
            float dis = ((minMaxAngleRotation.maxValue - centoDistancia) < minMaxAngleRotation.minValue)? minMaxAngleRotation.minValue : minMaxAngleRotation.maxValue - centoDistancia;
            Quaternion novo = Quaternion.Euler(0, 0, dis);
            transform.rotation = Quaternion.Lerp(transform.rotation, novo, smoothRotation * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update () {
        RotatePointerUI();
    }
}
