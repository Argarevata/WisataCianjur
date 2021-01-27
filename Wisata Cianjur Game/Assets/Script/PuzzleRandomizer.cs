using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] puzzlePieces, puzzlePos;
    public int[] filledPos;
    private int r;
    private bool safe;

    public DragController[] thePuzzlePieces;
    public SpriteRenderer[] puzzlePieceRenderer;
    public Sprite[] puzzleImage;

    public int whatLevel;
    public bool win;
    public GameObject winScreen;
    public SFX theSFX;
    public AudioSource bgm;

    private void Awake()
    {
        RandomizePosition();
        whatLevel = PlayerPrefs.GetInt("level");
        for (int i = 0; i < 16; i++)
        {
            puzzlePieceRenderer[i].sprite = puzzleImage[(whatLevel*16)+i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!win)
        {
            int x = 0;
            for (int i = 0; i < 16; i++)
            {
                if (thePuzzlePieces[i].locked)
                {
                    x++;
                }
            }
            if (x >= 16)
            {
                win = true;
                print("WIN YEAY");
                bgm.Stop();
                winScreen.SetActive(true);
                theSFX.winSFX.Play();
                //UNLOCK NEXT STAGE
                //KASIH BINTANG
                switch (whatLevel)
                {
                    case 0:
                        {
                            PlayerPrefs.SetInt("W2", 1);
                            break;
                        }
                    case 1:
                        {
                            PlayerPrefs.SetInt("M2", 1);
                            break;
                        }
                    case 2:
                        {
                            PlayerPrefs.SetInt("G2", 1);
                            break;
                        }
                    case 3:
                        {
                            PlayerPrefs.SetInt("H2", 1);
                            break;
                        }
                    case 4:
                        {
                            PlayerPrefs.SetInt("C2", 1);
                            break;
                        }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Peta");
        }

    }

    public void RandomizePosition()
    {
        for (int x = 0; x < 16; x++)
        {
            filledPos[x] = -1;
        }

        for (int i = 0; i < 16; i++)
        {
            do
            {
                r = Random.Range(0, 16);
                IsNotYetFilled();
            } while (safe == false);
            filledPos[i] = r;
            puzzlePieces[i].transform.position = puzzlePos[r].position;
        }
    }

    public void IsNotYetFilled()
    {
        safe = true;
        for (int i = 0; i < 16; i++)
        {
            if (r == filledPos[i])
            {
                safe = false;
            }
        }
    }

    public void LoadLevel(string x)
    {
        Application.LoadLevel(x);
    }
}
