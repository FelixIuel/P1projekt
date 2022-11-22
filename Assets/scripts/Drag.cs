using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GMNameSpace;
using cardNameSpace;
using CardDisplay;
using FactoryDisplay;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    
    Transform returnParent = null;

    public void Start() {}

    public void OnBeginDrag(PointerEventData eventData){
        if (eventData.button == PointerEventData.InputButton.Left) {
            returnParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);
        }
    }

    public void OnDrag(PointerEventData eventData){
        if (eventData.button == PointerEventData.InputButton.Left) {
            this.transform.position = eventData.position*GameManager.screenScale;
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        this.transform.SetParent(returnParent);
        if (eventData.button == PointerEventData.InputButton.Left) {
            DisplayCard card = this.GetComponent<DisplayCard>();
            if (card != null) {
                if (eventData.position.y > 370*(1+GameManager.screenScale.y)) {
                    GameManager.tryToPlayCard.Invoke(this.gameObject);
                }
            }
        }
    }
}
 // https://www.youtube.com/watch?v=zMKUfI8VE2I&list=PL4j7SP4-hFDJvQhZJn9nJb_tVzKj7dR7M&index=16&ab_channel=HumbleToymaker