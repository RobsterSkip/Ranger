using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public enum IconType
    {
        Fish,
        Plant,
        Bug
    }
    [SerializeField] private Sprite _fishIcon;
    [SerializeField] private Sprite _bugIcon;
    [SerializeField] private Sprite _plantIcon;

    private SpriteRenderer _backgroundSpriteRenderer;
    private SpriteRenderer _iconSpriteRenderer;
    private TextMeshPro _textMeshPro;

    private Vector2 padding = new Vector2(15f, 7f);
    private void Awake()
    {
        _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        _iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        _textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        SetUp(IconType.Bug, "feesh aaaaaaaaaaaaaaaaaaaaa");
    }

    private void SetUp(IconType icon, string text)
    {
        _textMeshPro.SetText(text);
        _textMeshPro.ForceMeshUpdate();
        Vector2 textSize = _textMeshPro.GetRenderedValues(false);

        _backgroundSpriteRenderer.size = textSize + padding;

        _iconSpriteRenderer.sprite = GetIconSprite(icon);
    }

    private Sprite GetIconSprite(IconType icon)
    {
        switch (icon)
        {
            default: 
                case IconType.Fish: return _fishIcon;
                case IconType.Plant: return _plantIcon;
                case IconType.Bug:return _bugIcon;
        }
    }
}
