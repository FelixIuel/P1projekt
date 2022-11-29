using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using factoryNameSpace;
using GMNameSpace;
using cardNameSpace;
using CardDisplay;
using FactoryDisplay;

public class HoverManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject objectPrefab;
    private GameObject showOnHover;
    public Vector3 showOffset = new Vector3(300f,0f,0f);
    public Card cardToShow;

    public void SetCard(Card _card) {
        cardToShow = _card;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        this.transform.SetAsLastSibling();
        showOnHover = Instantiate(objectPrefab);
        showOnHover.transform.SetParent(this.transform);
        showOnHover.gameObject.transform.position = this.transform.position+showOffset;
        if (this.GetComponent<DisplayFactory>() != null) {
            if (cardToShow != null) {
                showOnHover.transform.localScale *= 1.25f;
                showOnHover.GetComponent<DisplayCard>().ToggleCanPlay(false);
                showOnHover.GetComponent<DisplayCard>().SetCard(cardToShow);
                showOnHover.tag = "HoverDisplay";
                Destroy(showOnHover.GetComponent<Drag>());
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        GameObject[] hoverDisplays = GameObject.FindGameObjectsWithTag("HoverDisplay");
        foreach(GameObject _display in hoverDisplays)
        Destroy(_display);
        showOnHover = null;
    }
}
