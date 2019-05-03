using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadZone : MonoBehaviour
{

    public string Scene;
    public LayerMask Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(Scene);
    }

}
