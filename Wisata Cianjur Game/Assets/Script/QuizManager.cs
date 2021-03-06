﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public string[] questionPool, answerPool;

    public Text questionText;
    public Button[] answerButton;

    [HideInInspector]
    public string[] questions;
    [HideInInspector]
    public string[] answer;

    private int currentQuestion;

    public RectTransform[] buttonPos;
    public int[] filledPos;
    private int r;
    private bool safe;

    public GameObject winScreen, loseScreen;

    public int whatLevel;
    public SFX theSfx;
    public AudioSource BGM;

    private void Awake()
    {
        whatLevel = PlayerPrefs.GetInt("level");
        for (int i = 0; i < 5; i++)
        {
            questions[i] = questionPool[(whatLevel * 5) + i];
        }

        for (int i = 0; i < 20; i++)
        {
            answer[i] = answerPool[(whatLevel * 20) + i];
        }
    }

    void Start()
    {
        theSfx = FindObjectOfType<SFX>();
        RandomizeBtnPos();
        currentQuestion = 0;
        questionText.text = questions[currentQuestion];
        for (int i = 0; i < 4; i++)
        {
            Text answerText = answerButton[i].GetComponentInChildren<Text>();
            answerText.text = answer[(currentQuestion * 4) + i];
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Peta");
        }
    }

    public void Answering(bool x)
    {
        if (x)
        {
            currentQuestion++;
            if (currentQuestion < questions.Length)
            {
                theSfx.sprinkleSFX.Play();
                RandomizeBtnPos();
                questionText.text = questions[currentQuestion];
                for (int i = 0; i < 4; i++)
                {
                    Text answerText = answerButton[i].GetComponentInChildren<Text>();
                    answerText.text = answer[(currentQuestion * 4) + i];
                }
            }
            else
            {
                theSfx.winSFX.Play();
                print("DONE");
                BGM.Stop();
                winScreen.SetActive(true);
                //UNLOCK NEXT STAGE
                //KASIH BINTANG
                switch (whatLevel)
                {
                    case 0:
                        {
                            PlayerPrefs.SetInt("W1", 1);
                            break;
                        }
                    case 1:
                        {
                            PlayerPrefs.SetInt("M1", 1);
                            break;
                        }
                    case 2:
                        {
                            PlayerPrefs.SetInt("G1", 1);
                            break;
                        }
                    case 3:
                        {
                            PlayerPrefs.SetInt("H1", 1);
                            break;
                        }
                    case 4:
                        {
                            PlayerPrefs.SetInt("C1", 1);
                            break;
                        }
                }
            }
        }
        else
        {
            theSfx.loseSFX.Play();
            BGM.Stop();
            loseScreen.SetActive(true);
        }
    }

    public void RandomizeBtnPos()
    {
        for (int x = 0; x < 4; x++)
        {
            filledPos[x] = -1;    
        }

        for (int i = 0; i < 4; i++)
        {
            do
            {
                r = Random.Range(0, 4);
                IsNotYetFilled();
            } while (safe == false);
            filledPos[i] = r;
            answerButton[i].transform.position = buttonPos[r].position;
        }
    }

    public void IsNotYetFilled()
    {
        safe = true;
        for (int i = 0; i < 4; i++)
        {
            if (r == filledPos[i])
            {
                safe = false;
            }
        }
    }

    public void LoadScene(string x)
    {
        Application.LoadLevel(x);
    }
}
