using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{

    public void LoadScene(string x)
    {
        Application.LoadLevel(x);
    }
}
