using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GMNameSpace;
using cardNameSpace;
using CardDrawing;
using FactoryDrawing;

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
            this.transform.position = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData){
        if (eventData.button == PointerEventData.InputButton.Left) {
            DisplayCard card = this.GetComponent<DisplayCard>();
            if (card != null) {
                if (eventData.position.y > 380) {
                    GameManager.tryToPlayCard.Invoke(card.cardID);
                }
            }
            this.transform.SetParent(returnParent);
        }
    }
}
 // https://www.youtube.com/watch?v=zMKUfI8VE2I&list=PL4j7SP4-hFDJvQhZJn9nJb_tVzKj7dR7M&index=16&ab_channel=HumbleToymaker