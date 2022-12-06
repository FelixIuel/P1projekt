using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardNameSpace;
using CardDisplay;
using GMNameSpace;

namespace handNameSpace{
    public class Hand : MonoBehaviour {
        public GameObject factoryPrefab;
        public GameObject dealPrefab;
        public GameObject projectPrefab;
        
        public Hand(){}

        public List<Card> DiscardHand() {
            List<Card> temp = new List<Card>();
            for (int i = 0; i < this.transform.childCount; i++ ) {
                GameObject cardGO = this.transform.GetChild(i).gameObject;
                Card cardToReturn = cardGO.GetComponent<DisplayCard>().GetCard();  
                Destroy(cardGO);
                temp.Add(cardToReturn);
            }
            return temp;
        }


        public int Size(){
            return this.transform.childCount;
        }

        public Card Discard(int index){
            GameObject cardGO = this.transform.GetChild(index).gameObject;
            Card cardToReturn = cardGO.GetComponent<DisplayCard>().GetCard();
            DestroyImmediate(cardGO);
            return cardToReturn;
        }

        public void CreateCard(Card card) {
            GameObject CardGO = null;
            switch(card.type){
                case CardType.FactoryType:
                    CardGO = Instantiate(factoryPrefab);
                    break;
                case CardType.DealType:
                    CardGO = Instantiate(dealPrefab);
                    break;
                case CardType.ProjectType:
                    CardGO = Instantiate(projectPrefab);
                    break;
            }
            CardGO.transform.SetParent(this.transform);
            CardGO.GetComponent<DisplayCard>().SetCard(card);
        }
    }
}