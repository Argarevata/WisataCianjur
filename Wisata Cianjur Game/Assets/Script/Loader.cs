using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Loader : MonoBehaviour
{
    public Image myImage;
    public Text myText;
    public Sprite[] mySprites;
    public string[] myStrings;

    public Text myTitle;
    public string[] myTitleString;

    public int whatLevel;
    public bool isInMainMenu;

    private void Start()
    {
        if (!isInMainMenu)
        {
            whatLevel = PlayerPrefs.GetInt("level");

            myImage.sprite = mySprites[whatLevel];
            myText.text = myStrings[whatLevel];
            myTitle.text = myTitleString[whatLevel];
        }
    }

    public void LoadScene(string x)
    {
        if (!isInMainMenu)
        {
            switch (whatLevel)
            {
                case 0:
                    {
                        PlayerPrefs.SetInt("W0", 1);
                        break;
                    }
                case 1:
                    {
                        PlayerPrefs.SetInt("M0", 1);
                        break;
                    }
                case 2:
                    {
                        PlayerPrefs.SetInt("G0", 1);
                        break;
                    }
                case 3:
                    {
                        PlayerPrefs.SetInt("H0", 1);
                        break;
                    }
                case 4:
                    {
                        PlayerPrefs.SetInt("C0", 1);
                        break;
                    }
            }
        }
        

        Application.LoadLevel(x);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isInMainMenu)
            {
                Application.LoadLevel("Peta");
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
