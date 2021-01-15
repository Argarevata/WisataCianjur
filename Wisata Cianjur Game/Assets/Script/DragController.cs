using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour {

	private float deltaX, deltaY;
	private Vector2 mousePosition;
	private Vector2 initialPosition;

	//Ukuran No
	public Vector3 startSize;
	public Vector3 clickedSize;

	//Target dimana objek harus diletakkan
	public GameObject targetObject;

	//Apakah objek sudah di posisi target
	public bool onTarget;

	//Apakah objek sudah terkunci di posisi target
	public bool locked;

	// Use this for initialization
	void Start () {
		locked = false;
		onTarget = false;
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	private void OnMouseDown()
	{
		if (locked == false) {
			deltaX = Camera.main.ScreenToWorldPoint (Input.mousePosition).x - transform.position.x;
			deltaY = Camera.main.ScreenToWorldPoint (Input.mousePosition).y - transform.position.y;
			transform.localScale = clickedSize;
		}
	}

	private void OnMouseDrag()
	{
		if (locked == false) {
			mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector2 (mousePosition.x - deltaX, mousePosition.y - deltaY);
		}
	}

	private void OnMouseUp()
	{
		if (!onTarget) {
			transform.position = new Vector2 (initialPosition.x, initialPosition.y);
			transform.localScale = startSize;
		} else {
			transform.position = targetObject.transform.position;
			transform.localScale = clickedSize;
			locked = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == targetObject.name) {
			onTarget = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == targetObject.name) {
			onTarget = false;
		}
	}
}
