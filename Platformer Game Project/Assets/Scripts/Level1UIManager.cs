using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private GameObject _escMenuPanel;

    [SerializeField]
    private GameObject _winGamePanel;

    private AudioSource _fxAudioSource;
    private AudioSource _musicAudioSource;

    [SerializeField]
    private AudioClip _clickSound;

    private bool _isMusicEnabled;
    [SerializeField]
    private TMP_Text _isMusicOnOrOff;
    private bool _isFxEnabled;
    [SerializeField]
    private TMP_Text _isFxOnOrOff;

    [SerializeField]
    private string _nextLevel;

    [SerializeField]
    private string _currentLevel;

    private void Start()
    {
        _fxAudioSource = GameObject.Find("FxManager").GetComponent<AudioSource>();
        _musicAudioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();

        _isFxEnabled = true;
        _isMusicEnabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_escMenuPanel.activeInHierarchy)
            {
                Time.timeScale = 1;  
                _escMenuPanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                _escMenuPanel.SetActive(true);
            }
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        PlayClick();
        SceneManager.LoadScene(_currentLevel);
    }

    public void ReturnHome()
    {
        Time.timeScale = 1;
        PlayClick();
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseGameOverPanel()
    {
        Time.timeScale = 1;
        _gameOverPanel.SetActive(false);
    }

    public void CloseEscMenuPanel()
    {
        Time.timeScale = 1;
        PlayClick();
        _escMenuPanel.SetActive(false);
    }

    public void OpenWinGamePanel()
    {
        _winGamePanel.SetActive(true);
        PlayerPrefs.SetInt(_nextLevel, 1);
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        PlayClick();
        SceneManager.LoadScene(_nextLevel);
    }

    public void OnClickMusicButton()
    {
        if (_isMusicEnabled)
        {
            _isMusicEnabled = false;
            _isMusicOnOrOff.text = "OFF";
            _musicAudioSource.enabled = false;
        }
        else
        {
            if (_isFxEnabled)
            {
                PlayClick();
            }
            _isMusicEnabled = true;
            _isMusicOnOrOff.text = "ON";
            _musicAudioSource.enabled = true;
        }
    }

    public void OnClickFxButton()
    {
        if (_isFxEnabled)
        {
            _isFxEnabled = false;
            _isFxOnOrOff.text = "OFF";
            _fxAudioSource.enabled = false;
        }
        else
        {
            PlayClick();
            _isFxEnabled = true;
            _isFxOnOrOff.text = "ON";
            _fxAudioSource.enabled = true;
        }
    }

    private void PlayClick()
    {
        _fxAudioSource.PlayOneShot(_clickSound, 0.2f);
    }

}
