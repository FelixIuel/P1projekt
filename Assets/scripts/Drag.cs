using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    
    Transform returnParent = null;
    
    public void OnBeginDrag(PointerEventData eventData){
        returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        // print(this.name);
        print("parent name" + this.transform.parent.name);
    }
    public void OnDrag(PointerEventData eventData){
        this.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData){
        print(eventData.position.y);
        if (eventData.position.y < 360) {
            this.transform.SetParent(returnParent);
        }
    }
}
 // https://www.youtube.com/watch?v=zMKUfI8VE2I&list=PL4j7SP4-hFDJvQhZJn9nJb_tVzKj7dR7M&index=16&ab_channel=HumbleToymaker