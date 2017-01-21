using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Button start;
    public Button credits;

	// Use this for initialization
	void Start () {
        start = start.GetComponent<Button>();
        credits = credits.GetComponent<Button>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Credits()
    {
        SceneManager.LoadScene(3);
    }
}
