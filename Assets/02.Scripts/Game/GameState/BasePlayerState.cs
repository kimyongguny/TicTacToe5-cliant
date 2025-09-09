using UnityEngine;

public abstract class BasePlayerState
{
    public abstract void OnEnter(GameLogic gameLogic);
    public abstract void OnExit(GameLogic gameLogic);
    public abstract void HandleMove(GameLogic gameLogic, int row, int col);
    protected abstract void HandleNextTurn(GameLogic gameLogic);

    protected void ProcessMove(GameLogic gameLogic, Constants.PlayerType playerType, int row, int col)
    {
        if (gameLogic.SetNewBoardValue(playerType, row, col))
        {
            // 새롭개 놓여진  marker를 기반으로 게임의 결과를 판단
            var gameResult = gameLogic.CheckGameResult();
            if(gameResult == GameLogic.GameResult.None)
            {
                HandleNextTurn(gameLogic);
            }
            else
            {
                //GameLogic 에게 Game Over 전달
                gameLogic.EndGame(gameResult);
            }
        }
    }

}
