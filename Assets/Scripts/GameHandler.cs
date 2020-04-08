using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackJackGameLogic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject btnHit, btnPass, btnBet, sliderBet, betLabel, playerCardSpawner, dealerCardSpawner;

    [SerializeField]
    GameObject[] cardPrefab;

    private Deck deck;
    private List<Card> playerHand, dealerHand;
    private Vector3 playerSpawnerPosition, dealerSpawnerPosition;
    private int startingMoney, playerPoint, dealerPoint, betAmount;

    private void Awake()
    {
        startingMoney = 500;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void ResetGame()
    {
        deck = new Deck(cardPrefab);

        // Card spawning position
        playerSpawnerPosition = playerCardSpawner.transform.position;
        dealerSpawnerPosition = dealerCardSpawner.transform.position;

        playerHand = new List<Card>();
        dealerHand = new List<Card>();

        playerPoint = 0;
        dealerPoint = 0;

        ActivateSliderBetObjects(true);
        ActivateHitPassButtons(false);
    }

    

    public void FirstRoundOfGame()
    {
        betAmount = System.Int16.Parse(betLabel.GetComponent<TextMeshPro>().text);

        ActivateSliderBetObjects(false);
        ActivateHitPassButtons(true);

        PlayerDrawCard(true);
        DealerDrawCard();
        PlayerDrawCard(true);

        CheckForBlackJack();
    }

    private void CheckForBlackJack()
    {
        CardValue cardInHand;

        if (playerHand[0].Value == CardValue.ace)
        {
            cardInHand = playerHand[1].Value;

        } else if (playerHand[1].Value == CardValue.ace)
        {
            cardInHand = playerHand[0].Value;
        } else
        {
            return;
        }

        if (cardInHand == CardValue.ten
                || cardInHand == CardValue.jack
                || cardInHand == CardValue.queen
                || cardInHand == CardValue.king)
        {
            PlayerWin();
        }
    }

    public Card DrawOneCard(GameObject spawner, Vector3 spawnerPosition)
    {
        Debug.Log("DEAL CARD");

        Card card = deck.DealRandomCard();

        GameObject instCard = Instantiate(card.CardPrefab, spawnerPosition, spawner.transform.rotation, spawner.transform);

        instCard.AddComponent<BoxCollider>();
        instCard.AddComponent<Rigidbody>();
        instCard.AddComponent<NearInteractionGrabbable>();
        instCard.AddComponent<ManipulationHandler>();

        return card;
    }

    [SerializeField]
    public void PlayerDrawCard(bool isFirstRound)
    {
        playerSpawnerPosition.x += 0.2f;
        playerSpawnerPosition.z += 0.2f;

        playerHand.Add(DrawOneCard(playerCardSpawner, playerSpawnerPosition));
        playerPoint = CalculatePoint(playerHand);

        if(!isFirstRound)
        {
            if (playerPoint > 21)
            {
                DealerWin();
            }
            else if (playerPoint == 21)
            {
                PlayerEndTurn();
            }
        }
    }

    public void DealerDrawCard()
    {
        dealerSpawnerPosition.x += 0.2f;

        dealerHand.Add(DrawOneCard(dealerCardSpawner, dealerSpawnerPosition));
        dealerPoint = CalculatePoint(dealerHand);
    }

    public void PlayerEndTurn()
    {
        // Dealer draw card
        DealerDrawCard();

        while (dealerPoint < 17)
        {
            
            DealerDrawCard();
        }

        if(dealerPoint > 21 || dealerPoint < playerPoint)
        {
            PlayerWin();
        } 
        else if (dealerPoint > playerPoint)
        {
            DealerWin();
        } 
        else
        {
            NoOneWin();
        }
    }

    private int CalculatePoint(List<Card> userHand)
    {
        int userPoint = 0;

        foreach(Card card in userHand)
        {
            int cardVal = (int)card.Value;

            if (cardVal >= 10)
            {
                userPoint += 10;
            } 
            else if (cardVal == 1)
            {
                userPoint += 11;
            }
            else
            {
                userPoint += cardVal;
            }
        }

        if(userPoint > 21)
        {
            userPoint = 0;

            foreach (Card card in userHand)
            {
                if(card.Value == CardValue.ace)
                {
                    userPoint += 1;
                } else
                {
                    userPoint += (int)card.Value;
                }
            }
        }

        Debug.Log(userPoint + " Point");

        return userPoint;
    }

    private void ActivateSliderBetObjects(bool activate)
    {
        sliderBet.SetActive(activate);
        btnBet.SetActive(activate);
    }

    private void ActivateHitPassButtons(bool activate)
    {
        btnHit.SetActive(activate);
        btnPass.SetActive(activate);
    }

    private void PlayerWin()
    {
        startingMoney += betAmount;
        Debug.Log("Player win!");

        //ResetGame();
    }

    private void DealerWin()
    {
        Debug.Log("Dealer win!");
    }

    private void NoOneWin()
    {
        Debug.Log("Nobody win!");
    }

    private void EndGame()
    {
        
    }

    private void NewRound()
    {
        DestroyCardsInHand();

        sliderBet.SetActive(true);
        btnBet.SetActive(true);
        btnHit.SetActive(false);
        btnPass.SetActive(false);
    }

    private void DestroyCardsInHand()
    {
        var playerCards = playerCardSpawner.GetComponentsInChildren<NearInteractionGrabbable>(true);

        foreach(var obj in playerCards)
        {
            GameObject c = (obj as NearInteractionGrabbable).gameObject;

            Destroy(c);
        }

        // TODO Destroy dealer hand
    }
}
