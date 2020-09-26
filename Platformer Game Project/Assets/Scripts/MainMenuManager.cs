using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _levelsPanel;

    [SerializeField]
    private Button[] _levelButtons = new Button[2];

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _clickSound;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Level 2") == 1)
        {
            _levelButtons[1].interactable = true;
        }
        else
        {
            _levelButtons[1].interactable = false;
        }

        _audioSource = GameObject.Find("FxManager").GetComponent<AudioSource>();
    }

    public void StartGameButton()
    {
        _audioSource.PlayOneShot(_clickSound, 0.2f);
        _levelsPanel.SetActive(true);  
    }

    public void QuitGameButton()
    {
        _audioSource.PlayOneShot(_clickSound, 0.2f);
        Application.Quit();
    }

    public void Level1Button()
    {
        _audioSource.PlayOneShot(_clickSound, 0.2f);
        SceneManager.LoadScene("Level 1");
    }

    public void Level2Button()
    {
        _audioSource.PlayOneShot(_clickSound, 0.2f);
        SceneManager.LoadScene("Level 2");
    }

    public void CloseLevelsPanel()
    {
        _audioSource.PlayOneShot(_clickSound, 0.2f);
        _levelsPanel.SetActive(false);
    }
}
