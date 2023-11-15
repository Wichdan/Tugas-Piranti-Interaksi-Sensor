using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsStartGame, IsGameOver;
    [SerializeField] GameObject _GameOverPanel;

    [Header("Start CountDown")]
    [SerializeField] float _second = 3f;
    [SerializeField] TextMeshProUGUI _CDTimerText;
    [SerializeField] GameObject _StartGameCDPanel;

    [Header("Lenght Count")]
    [SerializeField] float _lenghtCount = 0f;
    [SerializeField] TextMeshProUGUI _lenghtCountText;

    [Header("Result")]
    [SerializeField] TextMeshProUGUI _bestLenghtCountText;
    [SerializeField] TextMeshProUGUI _bestCoinsText;

    private AudioSource _audioSource;

    public static GameManager Instance;

    public float LenghtCount { get => _lenghtCount; set => _lenghtCount = value; }

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CountDownTimer();
        UpdateLenghtCount();

        if (IsGameOver && !_GameOverPanel.activeSelf)
        {
            _audioSource.Stop();
            _GameOverPanel.SetActive(true);
            IsStartGame = false;

            //save lenght
            int lenghtCountConvert = Mathf.RoundToInt(_lenghtCount);
            if (lenghtCountConvert > SimpleSave.Instance.Getint("BestLenght"))
            {
                SimpleSave.Instance.SetInt("BestLenght", lenghtCountConvert);
                _bestLenghtCountText.text = "Best Lenght: " + SimpleSave.Instance.Getint("BestLenght").ToString("00");
            }
            else
                _bestLenghtCountText.text = "Best Lenght: " + SimpleSave.Instance.Getint("BestLenght").ToString("00");

            //save coins
            if (CoinsManager.Instance.BestCoins > SimpleSave.Instance.Getint("BestCoins"))
            {
                SimpleSave.Instance.SetInt("BestCoins", CoinsManager.Instance.BestCoins);
                _bestCoinsText.text = "Best Coins: " + SimpleSave.Instance.Getint("BestCoins").ToString("00");
            }
            else
                _bestCoinsText.text = "Best Coins: " + SimpleSave.Instance.Getint("BestCoins").ToString("00");
                
        }
    }

    void CountDownTimer()
    {
        if (_second < 0)
        {
            IsStartGame = true;
            _StartGameCDPanel.SetActive(false);
            return;
        }
        _second -= Time.deltaTime;

        int round = Mathf.RoundToInt(_second);
        _CDTimerText.text = round.ToString();

        if (round == 0)
        {
            _CDTimerText.text = "GO!";
        }
    }

    void UpdateLenghtCount()
    {
        if (!IsStartGame) return;
        if (IsGameOver) return;
        _lenghtCount += Time.deltaTime;
        int lenghtCountConvert = Mathf.RoundToInt(_lenghtCount);

        _lenghtCountText.text = "Lenght: " + lenghtCountConvert.ToString("00");
    }

    public void RestartLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }
}
