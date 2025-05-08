using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour, IPointerClickHandler
{
    public event Action<Items> OnRightClickEvent;

    [SerializeField]
    private Image _image;

    private Items _item;
    public Items Item
    {
        get { return _item; }
        set { _item = value;

            if (_item == null)
            {
                _image.enabled = false;
            }
            else
            {
                _image.sprite = _item._itemIcon;
                _image.enabled = true;
            }
        }
    }

    private void OnValidate()
    {
        if(_image == null)
        {
            _image = GetComponent<Image>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(Item);
            //    Instantiate(Item, )
            }
        }
    }
}
