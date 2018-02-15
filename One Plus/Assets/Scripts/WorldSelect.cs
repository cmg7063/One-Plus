using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldSelect : MonoBehaviour
{
    public Button city;
    public Button forest;
    public Button swamp;
    public Button beach;
    public Button mountain;
    public Button menu;

	// Use this for initialization
	void Start ()
    {
		
	}

    public void City()
    {
        SceneManager.LoadScene("City_1");
    }

    public void Forest()
    {
        SceneManager.LoadScene("Forest_1");
    }

    public void Swamp()
    {
        SceneManager.LoadScene("Swamp_1");
    }

    public void Beach()
    {
        SceneManager.LoadScene("Beach_1");
    }

    public void Mountain()
    {
        SceneManager.LoadScene("Mountain_1");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
