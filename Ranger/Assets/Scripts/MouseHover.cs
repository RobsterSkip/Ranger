using System;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    [SerializeField]
    private RectTransform _tipWindow;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
