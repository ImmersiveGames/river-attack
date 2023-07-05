using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ui_Joystick : Singleton<Ui_Joystick>, IDragHandler, IPointerUpHandler, IPointerDownHandler 
{
	private Image bgImg;
	private Image joystickImg;
    private Vector3 inputDirection;
    
    public static Vector3 InputDirection { get { return Instance.inputDirection; } set { Instance.inputDirection = value; } }

    // Use this for initialization
    void Start () 
	{
		bgImg = GetComponent<Image> ();
		joystickImg = transform.GetChild(0).GetComponent<Image> ();
		inputDirection = Vector3.zero;
	}

	public virtual void OnDrag (PointerEventData ped)
	{
		Vector2 pos = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(bgImg.rectTransform,
			   ped.position,
			   ped.pressEventCamera,
			   out pos)) 
		{
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
			float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;
			inputDirection = new Vector3 (x,y,0);
			inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

			joystickImg.rectTransform.anchoredPosition = 
				new Vector3 (inputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3),
				inputDirection.y * (bgImg.rectTransform.sizeDelta.y / 3));
        }
	}

	public virtual void OnPointerUp (PointerEventData ped)
	{
		inputDirection = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}

	public virtual void OnPointerDown (PointerEventData ped)
	{
		OnDrag (ped);
	}
}
