using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //Main Scene에서 선택한 게임 타입
    private Consrants.GameType _gameType;
    // Main에서 Game Scene으로 전환
    public void ChangeToGameScene(Consrants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }
    //Game 에서 Main Scene으로 전환
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        // TODO: 씬 전환시 처리할 함수
    }
}
