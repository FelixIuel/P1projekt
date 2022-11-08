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
        returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
    }
    public void OnDrag(PointerEventData eventData){
        this.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData){

        DisplayCard card = this.GetComponent<DisplayCard>();
        DisplayFactory factory = this.GetComponent<DisplayFactory>();
        if (factory != null) {
            this.transform.SetParent(returnParent);
        }

        if (card != null) {
            if (eventData.position.y < 380) {
                this.transform.SetParent(returnParent);
            } else {
                GameManager.cardplayed.Invoke(card.cardID);
            }
        }
    }
}
 // https://www.youtube.com/watch?v=zMKUfI8VE2I&list=PL4j7SP4-hFDJvQhZJn9nJb_tVzKj7dR7M&index=16&ab_channel=HumbleToymaker