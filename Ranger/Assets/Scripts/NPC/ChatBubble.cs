using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public enum IconType
    {
        Fish,
        Plant,
        Bug,
        Question
    }
    [SerializeField] private Sprite _fishIcon;
    [SerializeField] private Sprite _bugIcon;
    [SerializeField] private Sprite _plantIcon;
    [SerializeField] private Sprite _questonIcon;

    private SpriteRenderer _backgroundSpriteRenderer;
    private SpriteRenderer _iconSpriteRenderer;
    private TextMeshPro _textMeshPro;

    private float _bubbleTimerCount = 1f;
    private float _bubbleTime;

    private Vector2 padding = new Vector2(-1f, 7f);
    private void Awake()
    {
        _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        _iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        _textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();

        _backgroundSpriteRenderer.enabled = false;
        _iconSpriteRenderer.enabled = false;
        _textMeshPro.enabled = false;
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
                case IconType.Question:return _questonIcon;
        }
    }

    public void Create()
    {
        Debug.Log("Created");
        _bubbleTime += Time.deltaTime;
        _backgroundSpriteRenderer.enabled = true;
        _iconSpriteRenderer.enabled = true;
        _textMeshPro.enabled = true;
        SetUp(IconType.Question, "Hello Ranger! \nDo you have anything for me?");

        if (_bubbleTime > _bubbleTimerCount)
        {
            //Debug.Log("Destroyed");
            //_backgroundSpriteRenderer.enabled = false;
            //_iconSpriteRenderer.enabled = false;
            //_textMeshPro.enabled = false;
            //_bubbleTime = 0;
        }
    }

    public void Remove()
    {
        Debug.Log("Called");
        _backgroundSpriteRenderer.enabled = false;
        _iconSpriteRenderer.enabled = false;
        _textMeshPro.enabled = false;
    }
}
