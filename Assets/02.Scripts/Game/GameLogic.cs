using System;
using UnityEngine;
using static Constants;

public class GameLogic : IDisposable
{
    public BlockController blockController;    //block�� ó���� ��ü

    private Constants.PlayerType[,] _board;     //������ ���� 

    public BasePlayerState firstPlayerState;    // Player A
    public BasePlayerState secondPlayerState;   //Player B
    public BasePlayerState _currentPlayerState; //���� ���� Player

    public enum GameResult { None, Win, Lose, Draw }

    private MultiplayController _multiplayController;
    private string _roomId;

    public GameLogic(BlockController blockController, GameType gameType)
    {
        this.blockController = blockController;
        //���� �ʱ�ȭ
        _board = new PlayerType[BlockColumnCount, BlockColumnCount];

        //game Type �ʱ�ȭ
        switch (gameType)
        {
            case GameType.SinglePlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new AIState();

                SetState(firstPlayerState);
                break;
            case GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);

                SetState(firstPlayerState);
                break;
            case GameType.MultiPlay:
                _multiplayController = new MultiplayController((state, roomId) =>
                {
                    _roomId = roomId;
                    switch (state)
                    {
                        case MultiplayControllerState.CreateRoom:
                            Debug.Log("## Create Room ##");
                            //ToDo: ���ȭ�� UI ǥ��
                            break;
                        case MultiplayControllerState.JoinRoom:
                            Debug.Log("## Join Room ##");
                            firstPlayerState = new MultiplayerState(true, _multiplayController);
                            secondPlayerState = new PlayerState(false,_multiplayController,_roomId);
                            SetState(firstPlayerState);
                            break;
                        case MultiplayControllerState.StartGame:
                            Debug.Log("## Start Game ##");
                            firstPlayerState = new PlayerState(true, _multiplayController, _roomId);
                            secondPlayerState = new MultiplayerState(false, _multiplayController);
                            SetState(firstPlayerState);
                            break;
                        case MultiplayControllerState.ExitRoom:
                            Debug.Log("## Exit Game ##");
                            break;
                        case MultiplayControllerState.EndGame:
                            Debug.Log("## End Game ##");
                            break;

                    }
                });

                break;


        }
    }
    public Constants.PlayerType[,] GetBoard()
    {
        return _board;
    }

    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(this);
    }

    //_board �迭�� ���ο� Marker ���� �Ҵ�
    public bool SetNewBoardValue(PlayerType playerType, int row, int col)
    {
        if (_board[row, col] != PlayerType.None) return false;

        if (playerType == PlayerType.PlayerA)
        {
            _board[row, col] = playerType;
            blockController.PlaceMaker(Block.MarkerType.O, row, col);
            return true;
        }
        else if (playerType == PlayerType.PlayerB)
        {
            _board[row, col] = playerType;
            blockController.PlaceMaker(Block.MarkerType.X, row, col);
            return true;
        }
        return false;
    }
    //game Over ó��
    public void EndGame(GameResult gameResult)
    {
        //game Logic ����
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        //Game Over �г� ǥ��
        GameManager.Instance.OpenConfirmPanel("���ӿ���", () =>
        {
            GameManager.Instance.ChangeToMainScene();
        });
    }

    //������ ����� Ȯ���ϴ� �Լ�
    public GameResult CheckGameResult()
    {
        if (TicTacToeAI.CheckGameWin(PlayerType.PlayerA, _board)) { return GameResult.Win; }
        if (TicTacToeAI.CheckGameWin(PlayerType.PlayerB, _board)) { return GameResult.Lose; }
        if (TicTacToeAI.CheckGameDraw(_board)) { return GameResult.Draw; }
        return GameResult.None;
    }

    public void Dispose()
    {
        _multiplayController?.LeaveRoom(_roomId);
        _multiplayController?.Dispose();
    }
}
