using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour {

	public Rigidbody2D myBody;
	public float speed;
	public JoystickCon joy;
	public Animator myAnim;

	public float xAxis, yAxis;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		joy = FindObjectOfType<JoystickCon> ();
	}
	
	// Update is called once per frame
	void Update () {

		//		if (Input.GetKey (KeyCode.A) || joy.inputVector.x >=0.5) {
		//			myBody.velocity = new Vector2 (-speed, 0);
		//		}
		//
		//		if (Input.GetKey (KeyCode.D)) {
		//			myBody.velocity = new Vector2 (speed, 0);
		//		}
		//
		//		if (Input.GetKey (KeyCode.W)) {
		//			myBody.velocity = new Vector2 (0, speed);
		//		}
		//
		//		if (Input.GetKey (KeyCode.S)) {
		//			myBody.velocity = new Vector2 (0, -speed);
		//		}

		if (joy.inputVector.x >= 0.7f)
		{
			xAxis = 1;
		}
		else if (joy.inputVector.x <= -0.7f)
		{
			xAxis = -1;
		}
		else
		{
			xAxis = 0;
		}

		if (joy.inputVector.z >= 0.7f)
		{
			yAxis = 1;
		}
		else if (joy.inputVector.z <= -0.7f)
		{
			yAxis = -1;
		}
		else
		{
			yAxis = 0;
		}

		//myBody.velocity = new Vector2 (joy.inputVector.x * speed, joy.inputVector.z * speed);
		myBody.velocity = new Vector2(xAxis * speed, yAxis * speed);
		myAnim.SetFloat("X", xAxis);
		myAnim.SetFloat("Y", yAxis);
	}
}
