using UnityEngine;

public class MainPanalController : MonoBehaviour
{
    public void OnClickSinglePlayButton() 
    {
        GameManager.Instance.ChangeToGameScene(Consrants.GameType.SinglePlay);
    }
    public void OnClickDualPlayButton()
    {
        GameManager.Instance.ChangeToGameScene(Consrants.GameType.DualPlay);
    }
    public void OnClickMultiPlayButton()
    {
        GameManager.Instance.ChangeToGameScene(Consrants.GameType.MultiPlay);
    }
    public void OnClickSettingsButton()
    {

    }
}
