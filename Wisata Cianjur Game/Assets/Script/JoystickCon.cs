using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickCon : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler{

	public Image bgImage;
	public Image joyStickImage;
	public Vector3 inputVector;

	private void Start()
	{
		bgImage = GetComponent<Image> ();
		joyStickImage = transform.GetChild (0).GetComponent<Image> ();	
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImage.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			//Debug.Log ("LOL");

			pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x*2 , 0, pos.y*2);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			joyStickImage.rectTransform.anchoredPosition = new Vector3 (inputVector.x * (bgImage.rectTransform.sizeDelta.x / 3), inputVector.z * (bgImage.rectTransform.sizeDelta.y / 3));

		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector3.zero;
		joyStickImage.rectTransform.anchoredPosition = Vector3.zero;
	}

}
