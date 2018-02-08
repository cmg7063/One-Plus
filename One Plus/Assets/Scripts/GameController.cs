using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public Button instructions;
    public Button play;
    public Button quit;
    public Button close;

    private GameObject instructionPane;

    private bool instructionsClicked;

    // Use this for initialization
    void Start()
    {
        instructionPane = GameObject.FindGameObjectWithTag("ShowOnClick");
        hideText();
    }

   void hideText()
    {
        instructionPane.SetActive(false);
    }

    void showText()
    {
        instructionPane.SetActive(true);
    }

    public void Instructions()
    {
        showText();
    }

    public void Close()
    {
        hideText();
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}