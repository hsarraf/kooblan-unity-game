using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HUDEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        CanvasHUD.Instance._UIActivated = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //CanvasSelector.Instance._UIActivated = false;
        StartCoroutine(OnPointerUpCo());
    }

    IEnumerator OnPointerUpCo()
    {
        yield return null;
        CanvasHUD.Instance._UIActivated = false;
    }

}
