using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _offsetPlayerPosition;

    [Header("Corridor")]
    [SerializeField] private GameObject _corridor;
    [SerializeField] private Parallax[] _speedWalls;

    [Header("Display")]
    [SerializeField] private GameObject _display;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private Text _difficultyStateText;

    [Header("Counters")]
    [SerializeField] private Text _attemptCounterText;
    [SerializeField] private Text _lastAttemptTimerText;

    [Header("Buttons")]
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _restartButton;

    [HideInInspector] public float SpeedLine;

    private bool _gameIsStart = false;
    private float _timer = 0f;
    private int _attemptCounter = 0;
    private LineSpawner _lineSpawner;
    private string[] _difficulty = {"Легкий", "Средний", "Сложный"};
    private int _difficultyCurrent;


    private void Awake()
    {
        _display.SetActive(true);
        _restartButton.SetActive(false);
        _gameOverText.SetActive(false);
        
        _difficultyStateText.text = _difficulty[_difficultyCurrent];
        
        _lineSpawner = _corridor.GetComponentInChildren<LineSpawner>();
        DifficultyCurrentSetting();
        _corridor.SetActive(false);
        
        _attemptCounter = PlayerPrefs.GetInt("AttemptCounter", _attemptCounter);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameIsStart)
        {
            _timer += Time.deltaTime;
            Debug.Log(_timer);
        }
    }

    public void GameStart()
    {
        _display.SetActive(false);
        _corridor.SetActive(true);
        _gameIsStart = true;
        GameObject player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        player.transform.position = transform.position + _offsetPlayerPosition;
    }

    public void GameRestart()
    {
        _display.SetActive(false);
        _gameIsStart = true;
        CorridorEnable();

        GameObject player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        player.transform.position = transform.position + _offsetPlayerPosition;
    }

    public void GameOver()
    {
        _gameIsStart = false;
        
        CorridorDisable();
        
        _display.SetActive(true);
        _startButton.SetActive(false);
        _restartButton.SetActive(true);
        _gameOverText.SetActive(true);
        TimerView();
        AttemptCounterView();
    }

    private void TimerView()
    {
        _lastAttemptTimerText.text = "Продолжительность последней попытки:\n" + _timer.ToString("F1") + " c.";
        _timer = 0f;
    }

    private void AttemptCounterView()
    {
        _attemptCounter++;
        PlayerPrefs.SetInt("AttemptCounter", _attemptCounter);
        _attemptCounterText.text = "Количество попыток\n" + _attemptCounter.ToString();
    }

    private void CorridorDisable()
    {
        _lineSpawner.OnDisable();

        //уничтожаем все заспавненные линии
        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
            Destroy(obstacle);

        _corridor.SetActive(false);
    }

    private void CorridorEnable()
    {
        _corridor.SetActive(true);
    }

    public void DifficultyChange()
    {
        _difficultyCurrent++;
        if (_difficultyCurrent >= _difficulty.Length)
            _difficultyCurrent = 0;
        DifficultyCurrentSetting();
    }

    private void DifficultyCurrentSetting()
    {
        _difficultyStateText.text = _difficulty[_difficultyCurrent];

        if (_difficultyCurrent == 0)
        {
            for (int i = 0; i < _speedWalls.Length; i++)
                _speedWalls[i].animationSpeed = 5f;
            _lineSpawner.SpawnRate = 2f;
            SpeedLine = 5f;
            _difficultyStateText.color = Color.green;
        }
        else if (_difficultyCurrent == 1)
        {
            for (int i = 0; i < _speedWalls.Length; i++)
                _speedWalls[i].animationSpeed = 7.5f;
            _lineSpawner.SpawnRate = 1.5f;
            SpeedLine = 7.5f;
            _difficultyStateText.color = Color.yellow;
        }
        else if (_difficultyCurrent == 2)
        {
            for (int i = 0; i < _speedWalls.Length; i++)
                _speedWalls[i].animationSpeed = 10f;
            _lineSpawner.SpawnRate = 1f;
            SpeedLine = 10f;
            _difficultyStateText.color = Color.red;
        }
    }
}

