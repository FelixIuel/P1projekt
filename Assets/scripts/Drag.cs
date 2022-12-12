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
    private PointerEventData _lastPointerData ;


    public void OnBeginDrag(PointerEventData eventData){
        if (eventData.button == PointerEventData.InputButton.Left) {
            returnParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);
            _lastPointerData = eventData;
        }
    }

    public void OnDrag(PointerEventData eventData){
        if (eventData.button == PointerEventData.InputButton.Left) {
            this.transform.position = eventData.position*GameManager.screenScale;
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        _lastPointerData = null;
        this.transform.SetParent(returnParent);
        DisplayCard card = this.GetComponent<DisplayCard>();
        DisplayFactory factory = this.GetComponent<DisplayFactory>();
        if (card != null) {
            this.transform.SetParent(GameObject.FindGameObjectWithTag("Hand").transform);
            if (eventData.button == PointerEventData.InputButton.Left) {
                    if (eventData.position.y > 350/(GameManager.screenScale.y)) {
                        GameManager.tryToPlayCard.Invoke(this.gameObject);
                    }
            }
        }

        if (factory != null) {
            this.transform.SetParent(GameObject.FindGameObjectWithTag("Board").transform);
            if (eventData.position.y <= 350/GameManager.screenScale.y) {
                if (GameManager.instance.TryToSell()) {
                    Destroy(this.gameObject);
                } else {
                    this.transform.position = (GameObject.FindGameObjectWithTag("Board").transform.position);
                }
            }
        }
    }
    public void CancelDrag () {
        if (_lastPointerData != null) {
            this.transform.position = (GameObject.FindGameObjectWithTag("Board").transform.position);
            this.transform.SetParent(GameObject.FindGameObjectWithTag("Board").transform);
            _lastPointerData.pointerDrag = null;
        }
    }
}
 // https://www.youtube.com/watch?v=zMKUfI8VE2I&list=PL4j7SP4-hFDJvQhZJn9nJb_tVzKj7dR7M&index=16&ab_channel=HumbleToymaker