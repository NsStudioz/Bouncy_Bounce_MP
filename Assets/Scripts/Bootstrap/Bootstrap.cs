using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Bootstrap : MonoBehaviour
{

    void Start()
    {
        Application.targetFrameRate = 60; // regular game's frame rate, not network.
        SceneManager.LoadScene(1);
    }
}
