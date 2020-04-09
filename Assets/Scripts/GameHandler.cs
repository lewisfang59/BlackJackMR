using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject btnHit, btnPass, btnBet, sliderBet, betLabel, playerCardSpawner, dealerCardSpawner, playerStackLabel, btnContinue, winStatusLabel, gameChips, chipSpawner;

    [SerializeField]
    GameObject[] cardPrefab;

    private Deck deck;
    private List<Card> playerHand, dealerHand;
    private TextMeshPro playerStackLabelTMP;
    private Vector3 playerSpawnerPosition, dealerSpawnerPosition;
    private int startingMoney, playerPoint, dealerPoint, betAmount;

    private const string STACK_LABEL_INTRO = "Money $";
    private const string VALUE_OVER_TWENTY_ONE = "Value over 21!";
    private const string DEALER_WIN = "Dealer win...";
    private const string PLAYER_WIN = "Player win!";
    private const string ITS_A_TIE = "It's a tie!";
    private const string USER_LOSE = "Game over";
    private const int DEALER_MIN_POINT = 17;
    private const int MAX_POINT_AVAILABLE = 21;
    private const int START_MONEY = 500;
    private const int MIN_CHIP_IN_STACK = 5;

    private void Awake()
    {
        startingMoney = START_MONEY;
        deck = new Deck(cardPrefab);
        playerStackLabelTMP = playerStackLabel.GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }


    // Update is called once per frame
    void Update()
    {
        if(playerStackLabelTMP != null)
        {
            playerStackLabelTMP.text = STACK_LABEL_INTRO + startingMoney.ToString();
        }
    }

    /// <summary>
    /// determines how many chips would show up. varies depending on how much you are betting per round
    /// </summary>
    public void BetChips()
    {
        int numChips = betAmount / 5;
        Transform chipSpawnerTransform = chipSpawner.transform;

        for (int i = 0; i < numChips; i++)
        {
            GameObject bChip = Instantiate(gameChips, chipSpawnerTransform.position, chipSpawnerTransform.rotation, chipSpawnerTransform);
            bChip.AddComponent<BoxCollider>();
            bChip.AddComponent<Rigidbody>();
            bChip.AddComponent<NearInteractionGrabbable>();
            bChip.AddComponent<ManipulationHandler>();
        }

    }

    //public void RoundResult(bool roundResult)
    //{
    //    int numChips = betAmount / 5;
    //    if (roundResult)
    //    {
    //        for (int i = 0; i < numChips; i++)
    //        {
    //            gameChips = Instantiate(gameChips, transform.position, transform.rotation);
    //            gameChips.AddComponent<BoxCollider>();
    //            gameChips.AddComponent<Rigidbody>();
    //            gameChips.AddComponent<NearInteractionGrabbable>();

    //        }
    //    }
    //}

    /// <summary>
    /// Reset game objects to starting state
    /// </summary>
    public void ResetGame()
    {
        if(playerHand != null)
        {
            DestroyCardsInHand();
        }

        // Reshuffle deck if cards running out in one deck
        if(deck.Cards.Count < 10)
        {
            deck = new Deck(cardPrefab);
        } 

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

    
    /// <summary>
    /// Is it the initial round of the game
    /// </summary>
    public void FirstRoundOfGame()
    {
        // Gets the amount user wants to bet
        betAmount = System.Int16.Parse(betLabel.GetComponent<TextMeshPro>().text);

        ActivateSliderBetObjects(false);
        ActivateHitPassButtons(true);

        PlayerDrawCard(true);
        DealerDrawCard();
        PlayerDrawCard(true);

        CheckForBlackJack();
    }

    /// <summary>
    /// method for checking a blackjack situation has occured
    /// </summary>
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

    /// <summary>
    /// method for drawing a single card from the generated deck
    /// </summary>
    /// <param name="spawner">card spawner object (a place holder)</param>
    /// <param name="spawnerPosition">postion where the drawn card will be placed on table, type Vector3</param>
    /// <returns>Card</returns>
    public Card DrawOneCard(GameObject spawner, Vector3 spawnerPosition)
    {
        Card card = deck.DealRandomCard();

        GameObject instCard = Instantiate(card.CardPrefab, spawnerPosition, spawner.transform.rotation, spawner.transform);

        instCard.AddComponent<BoxCollider>();
        instCard.AddComponent<Rigidbody>();
        instCard.AddComponent<NearInteractionGrabbable>();
        instCard.AddComponent<ManipulationHandler>();

        return card;
    }

    /// <summary>
    /// player draws one card.
    /// </summary>
    /// <param name="isFirstRound">bool to check if it is the first round of a game</param>
    [SerializeField]
    public void PlayerDrawCard(bool isFirstRound)
    {
        // To spawn card a bit to the top right of previous spawned card.
        playerSpawnerPosition.x += 0.2f;
        playerSpawnerPosition.z += 0.2f;

        playerHand.Add(DrawOneCard(playerCardSpawner, playerSpawnerPosition));
        playerPoint = CalculatePoints(playerHand);

        if(!isFirstRound)
        {
            if (playerPoint > MAX_POINT_AVAILABLE)
            {
                PlayerLose(VALUE_OVER_TWENTY_ONE);
            }
            else if (playerPoint == MAX_POINT_AVAILABLE)
            {
                PlayerEndTurn();
            }
        }
    }

    /// <summary>
    /// AI dealer draws card
    /// </summary>
    public void DealerDrawCard()
    {
        // To spawn card a little bit to the right of previous spawned card
        dealerSpawnerPosition.x += 0.2f;

        dealerHand.Add(DrawOneCard(dealerCardSpawner, dealerSpawnerPosition));
        dealerPoint = CalculatePoints(dealerHand);
    }

    /// <summary>
    /// Player ends their turn under the following condition
    /// - User hit 21 after 1st round
    /// - User hit pass button
    /// </summary>
    public void PlayerEndTurn()
    {
        DealerDrawCard();

        while (dealerPoint < DEALER_MIN_POINT)
        {
            
            DealerDrawCard();
        }

        if(dealerPoint > MAX_POINT_AVAILABLE || dealerPoint < playerPoint)
        {
            PlayerWin();
        } 
        else if (dealerPoint > playerPoint)
        {
            PlayerLose(DEALER_WIN);
        } 
        else
        {
            DisplayGameStatus(ITS_A_TIE);
        }
    }


    /// <summary>
    /// calculates the total amount drawn
    /// </summary>
    /// <param name="userHand"></param>
    /// <returns>returns sum of cards</returns>
    private int CalculatePoints(List<Card> userHand)
    {
        int userPoint = 0;

        foreach(Card card in userHand)
        {
            CardValue cardVal = card.Value;

            userPoint += getCardValueToAdd(cardVal, false);
        }

        if(userPoint > MAX_POINT_AVAILABLE)
        {
            userPoint = 0;

            foreach (Card card in userHand)
            {
                userPoint += getCardValueToAdd(card.Value, true);
            }
        }

        return userPoint;
    }

    /// <summary>
    /// method returning the an int value corresponding to CardValue enum.
    /// </summary>
    /// <param name="cardVal">Value of Card</param>
    /// <param name="exceededMax">check to see if exceeding 21</param>
    /// <returns>returns int value of card</returns>
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

    /// <summary>
    /// Set active or inactive for slider object and bet button
    /// </summary>
    /// <param name="activate">is sliding</param>
    private void ActivateSliderBetObjects(bool activate)
    {
        sliderBet.SetActive(activate);
        btnBet.SetActive(activate);
    }

    /// <summary>
    /// Set active or inactive for hit and pass button
    /// </summary>
    /// <param name="activate"></param>
    private void ActivateHitPassButtons(bool activate)
    {
        btnHit.SetActive(activate);
        btnPass.SetActive(activate);
    }

    /// <summary>
    /// Set active or inactive for continue button and win status label
    /// </summary>
    /// <param name="activate"></param>
    private void ActivateGameStatus(bool activate)
    {
        btnContinue.SetActive(activate);
        winStatusLabel.SetActive(activate);
    }

    /// <summary>
    /// Method to deal with conditions of player losing the game
    /// </summary>
    /// <param name="statusText"></param>
    private void PlayerLose(string statusText)
    {
        startingMoney -= betAmount;

        if (startingMoney < MIN_CHIP_IN_STACK)
        {
            GameOver();
        }
        else
        {
            DisplayGameStatus(statusText);
        }
    }

    /// <summary>
    /// Method to deal with conditions of player winning the game
    /// </summary>
    private void PlayerWin()
    {
        startingMoney += betAmount;
        DisplayGameStatus(PLAYER_WIN);
    }

    /// <summary>
    /// method for when game is done, user have no more chips in stack.
    /// </summary>
    private void GameOver()
    {
        DisplayGameStatus(USER_LOSE);

        btnContinue.GetComponent<Interactable>().OnClick.AddListener(delegate
        {
            SceneManager.LoadScene(0);
        }
        );
    }

    /// <summary>
    /// method for showing status for the game
    /// </summary>
    /// <param name="status"></param>
    private void DisplayGameStatus(string status)
    {
        string handsInfo = playerPoint + " vs " + dealerPoint;

        ActivateHitPassButtons(false);
        ActivateGameStatus(true);

        winStatusLabel.GetComponent<TextMeshPro>().text = handsInfo + "\n" + status;
    }

    /// <summary>
    /// method for destroying chips and cards in hand
    /// </summary>
    private void DestroyCardsInHand()
    {

        // Destroy player cards.
        var playerCards = playerCardSpawner.GetComponentsInChildren<NearInteractionGrabbable>(true);

        foreach(var obj in playerCards)
        {
            GameObject c = (obj as NearInteractionGrabbable).gameObject;

            Destroy(c);
        }

        // Destroy dealer cards.
        var dealerCards = dealerCardSpawner.GetComponentsInChildren<NearInteractionGrabbable>(true);

        foreach (var obj in dealerCards)
        {
            GameObject c = (obj as NearInteractionGrabbable).gameObject;

            Destroy(c);
        }

        // Destroy chips bet.
        var chips = chipSpawner.GetComponentsInChildren<NearInteractionGrabbable>(true);

        foreach (var obj in chips)
        {
            GameObject c = (obj as NearInteractionGrabbable).gameObject;

            Destroy(c);
        }
    }
}
