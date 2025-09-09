using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private GameObject signinPanel;
    [SerializeField] private GameObject signupPanel;

    //Main Scene에서 선택한 게임 타입
    private Constants.GameType _gameType;

    //Panel을 띄우기 위한 Canvas 정보
    private Canvas _canvas;


    //Game Logic
    private GameLogic _gameLogic;

    //game씬의 UI를 담당하는 객체
    private GameUIController _gameUIController;

    private void Start()
    {
        var sid = PlayerPrefs.GetString("sid");
        if (string.IsNullOrEmpty(sid))
        {
            OpenSigninPanel();
        }
    }
    // Main에서 Game Scene으로 전환
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }
    //Game 에서 Main Scene으로 전환
    public void ChangeToMainScene()
    {
        _gameLogic?.Dispose();
        _gameLogic = null;
        SceneManager.LoadScene("Main");
    }

    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonCliked onConfirmButtonCliked)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonCliked);
        }
    }

    public void OpenSigninPanel()
    {
        if (_canvas != null)
        {
            var signinPanelObject = Instantiate(signinPanel, _canvas.transform);
            signinPanelObject.GetComponent<SigninPanelController>().Show();
        }
    }
    public void OpenSignupPanel()
    {
        if (_canvas != null)
        {
            var signupPanelObject = Instantiate(signupPanel, _canvas.transform);
            signupPanelObject.GetComponent<SignupPanelController>().Show();
        }
    }
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType gameTurnPanelType)
    {
        _gameUIController.SetGameTurnPanel(gameTurnPanelType);
    }
    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            //Block 초기화
            var blockController = FindFirstObjectByType<BlockController>();
            if (blockController != null)
            {
                blockController.InitBlocks();
            }
           

            //게임 ui 컨트롤러 할당 및 초기화
            _gameUIController = FindFirstObjectByType<GameUIController>();
            if (_gameUIController != null)
            {
                _gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }

            // GameLogic 생성       
            if (_gameLogic != null) _gameLogic.Dispose();
            _gameLogic = new GameLogic(blockController, _gameType);

        }
    }

    private void OnApplicationQuit()
    {
        _gameLogic?.Dispose();
        _gameLogic = null;
    }
}
