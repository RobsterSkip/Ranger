using System;
using TMPro;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    [SerializeField]
    private InventoryInput _inventoryInput;

    [SerializeField]
    private TextMeshProUGUI _tipText;
    [SerializeField]
    private RectTransform _tipWindow;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    private bool _isTipActive;

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

    private void Update()
    {
        if (_isTipActive)
        {
            Vector2 mousePos = Input.mousePosition;
            _tipWindow.position = mousePos + new Vector2(75, 0);
        }

       if(_inventoryInput._inventoryOpen == false)
       {
            _isTipActive = false;
            _tipWindow.gameObject.SetActive(false);
       }
    }

    private void ShowTip(string tip, Vector2 mousePos)
    {
        _tipText.text = tip;
        _tipWindow.gameObject.SetActive(true);
        _isTipActive = true;
    }

    private void HideTip()
    {
        _tipText.text = null;
        _tipWindow.gameObject.SetActive(false);
        _isTipActive = false;
    }
}