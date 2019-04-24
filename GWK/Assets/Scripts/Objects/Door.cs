using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string Scene;

    public void Open()
    {
        SceneManager.LoadScene(Scene);
    }
}
