using UnityEngine;

public class MainPanalController : MonoBehaviour
{
    public void OnClickSinglePlayButton() 
    {
        GameManager.Instance.ChangeToGameScene(Constants.GameType.SinglePlay);
    }
    public void OnClickDualPlayButton()
    {
        GameManager.Instance.ChangeToGameScene(Constants.GameType.DualPlay);
    }
    public void OnClickMultiPlayButton()
    {
        GameManager.Instance.ChangeToGameScene(Constants.GameType.MultiPlay);
    }
    public void OnClickSettingsButton()
    {

    }
}
