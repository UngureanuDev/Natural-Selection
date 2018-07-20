using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuButtons : MonoBehaviour
{

	public GameObject optionsPanel;
    public GameObject confirmPanel;
    public Canvas LobbyCanvas;
    public Canvas MenuCanvas;

    private bool IsCameraInLeft;

    private void Awake()
    {
        confirmPanel.SetActive(false);
        LobbyCanvas.enabled = false;
        MenuCanvas.enabled = true;
        MoveCameraLeft();
    }

    public void PlayClick()
    {
        LobbyCanvas.enabled = true;
        MenuCanvas.enabled = false;
        MoveCameraLeft();
    }

    public void BackClick()
    {
        LobbyCanvas.enabled = false;
        MenuCanvas.enabled = true;
        MoveCameraLeft();
    }
    public void QuitClick()
    {
        confirmPanel.SetActive(true);
        MoveCameraLeft();
    }
    public void YesClick()
    {
        Application.Quit();
    }
    public void NoClick()
    {
        confirmPanel.SetActive(false);
    }

	public void OptionsClick()
	{
		optionsPanel.SetActive(true);
	}

    private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }

    void MoveCameraLeft()
    {
        Camera.main.transform.position = new Vector3(-768, 358, -10);

    }
    void MoveCameraRight()
    {
        Camera.main.transform.position = new Vector3(506, 358, -10);
    }
}
