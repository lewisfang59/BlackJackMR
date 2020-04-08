using BlackJackGameLogic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject btnHit, btnPass, btnBet, sliderBet, betLabel, playerCardSpawner, dealerCardSpawner, playerStackLabel, btnContinue, winStatusLabel;

    [SerializeField]
    GameObject[] cardPrefab;

    private Deck deck;
    private List<Card> playerHand, dealerHand;
    private TextMeshPro playerStackTMP;
    private Vector3 playerSpawnerPosition, dealerSpawnerPosition;
    private int startingMoney, playerPoint, dealerPoint, betAmount;
    private string playerStackLabelText;

    private const string stackLabelIntro = "Money $";

    private void Awake()
    {
        startingMoney = 500;
        deck = new Deck(cardPrefab);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStackTMP = playerStackLabel.GetComponent<TextMeshPro>();
        ResetGame();

    }


    // Update is called once per frame
    void Update()
    {
        // playerStackTMP.text = stackLabelIntro + startingMoney.ToString();
        
    }

    
    /// <summary>
    /// Reset game objects to starting state
    /// </summary>
    public void ResetGame()
    {
        // Card spawning position
        playerSpawnerPosition = playerCardSpawner.transform.position;
        dealerSpawnerPosition = dealerCardSpawner.transform.position;

        playerHand = new List<Card>();
        dealerHand = new List<Card>();

        playerPoint = 0;
        dealerPoint = 0;

        ActivateSliderBetObjects(true);
        ActivateHitPassButtons(false);
        ActivateGameStatus(false);
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
        playerPoint = CalculatePoints(playerHand);

        if(!isFirstRound)
        {
            if (playerPoint > 21)
            {
                PlayerBusted();
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
        dealerPoint = CalculatePoints(dealerHand);
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

    // TODO add situation if user get 2 aces if have time.
    private int CalculatePoints(List<Card> userHand)
    {
        int userPoint = 0;

        foreach(Card card in userHand)
        {
            CardValue cardVal = card.Value;

            userPoint += getCardValueToAdd(cardVal, false);
        }

        if(userPoint > 21)
        {
            userPoint = 0;

            foreach (Card card in userHand)
            {
                userPoint += getCardValueToAdd(card.Value, true);
            }
        }

        Debug.Log(userPoint + " Point");

        return userPoint;
    }

    private int getCardValueToAdd(CardValue cardVal, bool exceededMax)
    {
        if (cardVal == CardValue.ten
                || cardVal == CardValue.jack
                || cardVal == CardValue.queen
                || cardVal == CardValue.king)
        {
            return 10;
        }
        else if (cardVal == CardValue.ace)
        {
            return exceededMax ? 1 : 11;
        }

        return (int)cardVal;
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

    private void ActivateGameStatus(bool activate)
    {
        btnContinue.SetActive(activate);
        winStatusLabel.SetActive(activate);
    }

    ////////////////////////////////////////////////// Below is undone 

    private void PlayerBusted()
    {
        string status = "Your busted!";
        startingMoney -= betAmount;

        DestroyCardsInHand();
        DisplayGameStatus(status);
    }

    private void PlayerWin()
    {
        string status = "Player win!";
        startingMoney += betAmount;

        DestroyCardsInHand();
        DisplayGameStatus(status);
    }

    private void DealerWin()
    {
        string status = "Dealer win..";
        startingMoney -= betAmount;

        DestroyCardsInHand();
        DisplayGameStatus(status);
    }

    private void NoOneWin()
    {
        string status = "It's a tie!";

        DestroyCardsInHand();
        DisplayGameStatus(status);
    }

    private void DisplayGameStatus(string status)
    {
        string handsInfo = playerPoint + " vs " + dealerPoint;

        ActivateHitPassButtons(false);
        ActivateGameStatus(true);

        winStatusLabel.GetComponent<TextMeshPro>().text = handsInfo + "\n" + status;
    }

    private void DestroyCardsInHand()
    {
        var playerCards = playerCardSpawner.GetComponentsInChildren<NearInteractionGrabbable>(true);

        foreach(var obj in playerCards)
        {
            GameObject c = (obj as NearInteractionGrabbable).gameObject;

            Destroy(c);
        }

        var dealerCards = dealerCardSpawner.GetComponentsInChildren<NearInteractionGrabbable>(true);

        foreach (var obj in dealerCards)
        {
            GameObject c = (obj as NearInteractionGrabbable).gameObject;

            Destroy(c);
        }
    }
}
