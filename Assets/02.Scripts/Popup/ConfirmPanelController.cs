using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanalController
{
    [SerializeField] private TMP_Text messageText;

    public delegate void OnConfirmButtonCliked();
    public OnConfirmButtonCliked _onConfirmButtonCliked;

    //Confirm Panal을 표시하는 메서드
    public void Show(string message , OnConfirmButtonCliked onConfirmButtonCliked)
    {
        messageText.text = message;
        _onConfirmButtonCliked = onConfirmButtonCliked;
        base.Show();
    }
    //확인 버튼
    public void OnClickConfirmButton()
    {
        Hide(() =>
        {
            _onConfirmButtonCliked?.Invoke();
        });
    }
    //x 버튼
    public void OnClickCloseButton()
    {
        Hide();
    }
}
