using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour {

	public Rigidbody2D myBody;
	public float speed;
	public JoystickCon joy;
	public Animator myAnim;
	public Camera theCam;
	public SpriteRenderer[] stars;

	public float xAxis, yAxis;
	public float camSizeMax, camSizeMin, camZoomMultiplier;
	public bool camZoomIn { get; set; }
	public bool camZoomOut { get; set; }

	public GameObject interactBtn;
	public int[] activeLevel;
	public Button[] levelButton;
	public int interactedObject; //ada juga playerprefs nyaa
	public float playerPosX, playerPosY; //ada juga playerprefs nyaa

	//PLAYERPREFS BOOL
	// W0, W1, W2 = waduk
	// M0, M1, M2 = masjid
	// G0, G1, G2 = gunung
	// H0, H1, H2 = hutan
	// C0, C1, C2 = curug

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt("G1", 0);
		//PlayerPrefs.SetInt("G2", 0);
		//PlayerPrefs.SetInt("G0", 0);
		//PlayerPrefs.SetInt("C1", 0);
		//PlayerPrefs.SetInt("C2", 0);
		//PlayerPrefs.SetInt("C0", 0);
		//      PlayerPrefs.SetInt("H1", 0);
		//      PlayerPrefs.SetInt("H2", 0);
		//      PlayerPrefs.SetInt("H0", 0);
		//      PlayerPrefs.SetInt("M1", 0);
		//      PlayerPrefs.SetInt("M2", 0);
		//      PlayerPrefs.SetInt("M0", 0);
		//      PlayerPrefs.SetInt("W1", 0);
		//      PlayerPrefs.SetInt("W2", 0);
		//      PlayerPrefs.SetInt("W0", 0);

		playerPosX = PlayerPrefs.GetFloat("posX");
		playerPosY = PlayerPrefs.GetFloat("posY");

		myBody = GetComponent<Rigidbody2D> ();
		joy = FindObjectOfType<JoystickCon> ();

		transform.position = new Vector2(playerPosX, playerPosY);

		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				string x = "";
				if (i == 0)
				{
					x += "W";
				}
				else if (i == 1)
				{
					x += "M";
				}
				else if (i == 2)
				{
					x += "G";
				}
				else if (i == 3)
				{
					x += "H";
				}
				else
				{
					x += "C";
				}
				x += j.ToString();

				if (PlayerPrefs.GetInt(x) == 1)
				{
					stars[(i * 3) + j].color = new Color(1, 1, 1, 1);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		PlayerPrefs.SetFloat("posX", transform.position.x);
		PlayerPrefs.SetFloat("posY", transform.position.y);

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

		if (camZoomIn)
		{
			if (theCam.orthographicSize > camSizeMin)
			{
				theCam.orthographicSize -= Time.deltaTime * camZoomMultiplier;
			}
		}

		if (camZoomOut)
		{
			if (theCam.orthographicSize < camSizeMax)
			{
				theCam.orthographicSize += Time.deltaTime * camZoomMultiplier;
			}
		}
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "wisata")
		{
			other.transform.localScale = new Vector2(0.225f, 0.225f);
			interactBtn.SetActive(true);

			if (other.name == "Waduk")
			{
				activeLevel[0] = PlayerPrefs.GetInt("W0");
				activeLevel[1] = PlayerPrefs.GetInt("W1");
				activeLevel[2] = PlayerPrefs.GetInt("W2");
				interactedObject = 0;
			}
			else if (other.name == "Masjid")
			{
				activeLevel[0] = PlayerPrefs.GetInt("M0");
				activeLevel[1] = PlayerPrefs.GetInt("M1");
				activeLevel[2] = PlayerPrefs.GetInt("M3");
				interactedObject = 1;
			}
			else if (other.name == "Gunung")
			{
				activeLevel[0] = PlayerPrefs.GetInt("G0");
				activeLevel[1] = PlayerPrefs.GetInt("G1");
				activeLevel[2] = PlayerPrefs.GetInt("G2");
				interactedObject = 2;
			}
			else if (other.name == "Hutan")
			{
				activeLevel[0] = PlayerPrefs.GetInt("H0");
				activeLevel[1] = PlayerPrefs.GetInt("H1");
				activeLevel[2] = PlayerPrefs.GetInt("H2");
				interactedObject = 3;
			}
			else if (other.name == "Curug")
			{
				activeLevel[0] = PlayerPrefs.GetInt("C0");
				activeLevel[1] = PlayerPrefs.GetInt("C1");
				activeLevel[2] = PlayerPrefs.GetInt("C2");
				interactedObject = 4;
			}
			PlayerPrefs.SetInt("level", interactedObject);
			RefreshButton();
		}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
		if (other.tag == "wisata")
		{
			other.transform.localScale = new Vector2(0.15f, 0.15f);
			interactBtn.SetActive(false);
		}
	}

	public void RefreshButton()
	{
		for (int i = 1; i < 3; i++)
		{
			if (activeLevel[i-1] == 1)
			{
				levelButton[i].interactable = true;
			}
			else
			{
				levelButton[i].interactable = false;
			}
		}
		levelButton[0].interactable = true;
	}

    public void LoadLevel(string x)
    {
        Application.LoadLevel(x);
    }
}
