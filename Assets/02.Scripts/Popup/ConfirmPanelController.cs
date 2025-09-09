using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanalController
{
    [SerializeField] private TMP_Text messageText;

    public delegate void OnConfirmButtonCliked();
    public OnConfirmButtonCliked _onConfirmButtonCliked;

    //Confirm Panal�� ǥ���ϴ� �޼���
    public void Show(string message , OnConfirmButtonCliked onConfirmButtonCliked)
    {
        messageText.text = message;
        _onConfirmButtonCliked = onConfirmButtonCliked;
        base.Show();
    }
    //Ȯ�� ��ư
    public void OnClickConfirmButton()
    {
        Hide(() =>
        {
            _onConfirmButtonCliked?.Invoke();
        });
    }
    //x ��ư
    public void OnClickCloseButton()
    {
        Hide();
    }
}
