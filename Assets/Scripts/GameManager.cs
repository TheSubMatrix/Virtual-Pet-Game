using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const float timeBetweenStatLosses = 5;

    [SerializeField] TMP_InputField _nameInput;
    [SerializeField] CanvasGroup _startScreenCanvasGroup;
    [SerializeField] CanvasGroup _gameplayCanvasGroup;
    [SerializeField] CanvasGroup _gameOverCanvasGroup;
   
    [SerializeField] Image _energyMask;
    [SerializeField] Image _happinessMask;
    [SerializeField] Image _fullnessMask;

    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _neglectScreenText;
    string _neglectStringTextDefaultString;

    Pet _currentPet;
    private void Start()
    {
        if(_neglectScreenText != null)
        {
            _neglectStringTextDefaultString = _neglectScreenText.text;
        }
    }

    public void OnSubmitButtonPressed()
    {
        if (_nameInput != null && _nameInput.text != null)
        {
            _currentPet = new Pet(_nameInput.text);
            if(_startScreenCanvasGroup != null && _gameplayCanvasGroup != null)
            {
                _startScreenCanvasGroup.alpha = 0f;
                _startScreenCanvasGroup.blocksRaycasts = false;
                _startScreenCanvasGroup.interactable = false;
                _gameplayCanvasGroup.alpha = 1f;
                _gameplayCanvasGroup.blocksRaycasts = true;
                _gameplayCanvasGroup.interactable = true;
                _fullnessMask.fillAmount = (float)_currentPet.CurrentFullness / (float)_currentPet.MaxFullness;
                _happinessMask.fillAmount = (float)_currentPet.CurrentHappiness / (float)_currentPet.MaxHappiness;
                _energyMask.fillAmount = (float)_currentPet.CurrentEnergyLevel / (float)_currentPet.MaxEnergyLevel;
            }
            if(_nameText != null && _currentPet != null)
            {
                _nameText.text = _currentPet.Name;
            }
            StartCoroutine(UpdatePetStats(timeBetweenStatLosses));
        }
    }
    public void OnPlayButtonPressed()
    {
        if(_currentPet != null)
        {
            _currentPet.Play();
            _happinessMask.fillAmount = (float)_currentPet.CurrentHappiness / (float)_currentPet.MaxHappiness;
        }
    }
    public void OnFeedButtonPressed()
    {
        if(_currentPet != null)
        {
            _currentPet.Eat();
            _fullnessMask.fillAmount = (float)_currentPet.CurrentFullness / (float)_currentPet.MaxFullness;
        }
    }
    public void OnSleepButtonPressed()
    {
        if( _currentPet != null)
        {
            _currentPet.Sleep();
            _energyMask.fillAmount = (float)_currentPet.CurrentEnergyLevel / (float)_currentPet.MaxEnergyLevel;
        }
    }
    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    IEnumerator UpdatePetStats(float timeToTake)
    {
        yield return new WaitForSeconds(timeToTake);
        int randomNum = Random.Range(0, 3);
        if(_currentPet != null )
        {
            switch (randomNum)
            {
                case 0:
                    if (_currentPet.LoseFullness())
                    {
                        LoseGame();
                    }
                    if(_fullnessMask != null)
                    {
                        _fullnessMask.fillAmount = (float)_currentPet.CurrentFullness / (float)_currentPet.MaxFullness;
                    }
                    break;
                case 1:
                    if (_currentPet.LoseHappiness())
                    {
                        LoseGame();
                    }
                    if (_happinessMask != null)
                    {
                        _happinessMask.fillAmount = (float)_currentPet.CurrentHappiness / (float)_currentPet.MaxHappiness;
                    }
                    break;
                case 2:
                    if (_currentPet.LoseEnergy())
                    {
                        LoseGame();
                    }
                    if( _energyMask != null)
                    {
                        _energyMask.fillAmount = (float)_currentPet.CurrentEnergyLevel / (float)_currentPet.MaxEnergyLevel;
                    }
                    break;
            }
        }

        StartCoroutine(UpdatePetStats(timeToTake));
    }
    void LoseGame()
    {
        if(_currentPet != null)
        {
            _neglectScreenText.text = _currentPet.Name + _neglectStringTextDefaultString;
        }
        _gameplayCanvasGroup.alpha = 0f;
        _gameplayCanvasGroup.blocksRaycasts = false;
        _gameplayCanvasGroup.interactable = false;
        _gameOverCanvasGroup.alpha = 1f;
        _gameOverCanvasGroup.blocksRaycasts = true;
        _gameOverCanvasGroup.interactable = true;
    }
}
