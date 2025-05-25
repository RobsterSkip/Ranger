using System;
using TMPro;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tipText;
    [SerializeField]
    private RectTransform _tipWindow;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    void Start()
    {
        HideTip();
    }

    private void ShowTip(string tip, Vector2 mousePos)
    {
        _tipText.text = tip;
      //  _tipWindow.sizeDelta = new Vector2(_tipText.preferredWidth > 200 ? 200 : _tipText.preferredWidth, _tipText.preferredHeight);

        _tipWindow.gameObject.SetActive(true);
        _tipWindow.transform.position = new Vector2(mousePos.x + 75, mousePos.y);
    }

    private void HideTip()
    {
        _tipText.text = null;
        _tipWindow.gameObject.SetActive(false);
    }
}
