using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string _tipToShow;

    private float _timeToWait = 0.1f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        MouseHover.OnMouseLoseFocus();
    }

    private void ShowMessage()
    {
        MouseHover.OnMouseHover(_tipToShow, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(_timeToWait);

        ShowMessage();
    }
}
