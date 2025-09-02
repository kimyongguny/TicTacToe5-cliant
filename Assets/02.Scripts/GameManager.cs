using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //Main Scene���� ������ ���� Ÿ��
    private Consrants.GameType _gameType;
    // Main���� Game Scene���� ��ȯ
    public void ChangeToGameScene(Consrants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }
    //Game ���� Main Scene���� ��ȯ
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        // TODO: �� ��ȯ�� ó���� �Լ�
    }
}
