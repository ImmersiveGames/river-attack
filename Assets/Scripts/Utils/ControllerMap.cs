using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[CreateAssetMenu(fileName = "ControllerMap", menuName = "GameManagers/ControllerMap", order = 101)]
public class ControllerMap : ScriptableObject
{
    public AxiesSettings[] axiesHorizontal;
    public AxiesSettings[] axiesVertical;
    public string[] actions;
    public AxiesSettings[] actionsTriggers;

    public Vector3 InputDirection(Vector3 nInput, int axie)
    {
        if (Mathf.Abs(nInput.x) <= axiesHorizontal[axie].deadZone)
            nInput.x = 0;
        if (Mathf.Abs(nInput.y) <= axiesVertical[axie].deadZone)
            nInput.y = 0;
        return nInput;
    }

    public Vector3 InputDirection(int axie)
    {
        float x = CrossPlatformInputManager.GetAxis(axiesHorizontal[axie].name);
#if UNITY_EDITOR
        x = Input.GetAxis(axiesHorizontal[axie].name);
#endif
        if (Mathf.Abs(x) <= axiesHorizontal[axie].deadZone)
            x = 0;
        float y = CrossPlatformInputManager.GetAxis(axiesVertical[axie].name);
#if UNITY_EDITOR
         y = Input.GetAxis(axiesVertical[axie].name);
#endif
        if (Mathf.Abs(y) <= axiesVertical[axie].deadZone)
            y = 0;
        
        Vector3 inputDirection = new Vector3(x, y, 0);
        inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;
        return inputDirection;
    }

    public bool ButtonDown(int action)
    {
        return Input.GetButtonDown(actions[action]);
    }
    public bool ButtonUp(int action)
    {
        return Input.GetButtonUp(actions[action]);
    }

    //TODO: Botões trigger mapear;
}
[System.Serializable]
public class AxiesSettings
{
    public string name;
    [Range(0, 1)]
    public float deadZone = 0.2f;
}