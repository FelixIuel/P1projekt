using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GMNameSpace;
using cardNameSpace;
using CardDrawing;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    
    Transform returnParent = null;
    public DisplayCard card;
    
    public void Start() {
    }

    public void OnBeginDrag(PointerEventData eventData){
        returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
    }
    public void OnDrag(PointerEventData eventData){
        this.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData){
        if (eventData.position.y < 360) {
            this.transform.SetParent(returnParent);
        } else {
            GameManager.cardplayed.Invoke(card.cardID);
        }
    }
}
 // https://www.youtube.com/watch?v=zMKUfI8VE2I&list=PL4j7SP4-hFDJvQhZJn9nJb_tVzKj7dR7M&index=16&ab_channel=HumbleToymaker