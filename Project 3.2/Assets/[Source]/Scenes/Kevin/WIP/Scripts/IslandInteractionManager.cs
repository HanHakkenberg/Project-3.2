using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IslandInteractionManager : MonoBehaviour
{
    enum TradeTypes
    {
        Instant,
        Daily
    }
    public enum InteractionPannels
    {
        Info,
        Trade,
        Pillage
    }

    public static IslandInteractionManager instance;
    public Island activeIsland;
    public InteractableObjects activeInteractableObjects{ get; private set; }
    public InteractableObjects lastInteractableObjects;
    public LootSite activeLootSite;
    GameObject activePannel;
    TradeTypes tradeTypes;
    [Header("InteractionButtons")]
    [SerializeField]
    GameObject tradeButton;
    [SerializeField]
    GameObject pillageButton;
    [SerializeField]
    GameObject exploreButton;
    [SerializeField]
    GameObject colonizeButton;
    [SerializeField]
    GameObject backButton;
    [SerializeField]
    GameObject confirmButton;
    
    #region TradeRelated
    [Header("Excess & Demand")]
    public List<Sprite> resourceImages = new List<Sprite>();
    public Image demandPic;
    public Image excessPic;

    [Header("General Variables")]
    // [SerializeField]
    // GameObject infoPannel;
    public GameObject attitudeParent;
    public TMP_Text attitudeText;
    public TMP_Text islandStatusText;

    [Header("TradePannels Variables")]
    [SerializeField]
    GameObject tradePannel;
    public TMP_Text tradeLeftText;
    public TMP_InputField inputRequest;
    public TMP_InputField inputDemand;
    public TMP_Text demandText;
    public TMP_Text requestText;
    int demandTypeValue;
    int requestTypeValue;

    int demandedValue;
    int requestedValue;
    CivManager.Type paymentType;
    CivManager.Type requestedType;
    #endregion
    //Change
    #region PillageRelated
    [Header("PillagePannels Variables")]
    public TMP_Text foodText;
    public TMP_Text materialsText;
    public TMP_Text moneyText;
    public TMP_Text peopleText;

    [SerializeField]
    GameObject pillagePannel;
    #endregion
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Update() {
        if(activeInteractableObjects != null && activeInteractableObjects.canInteract == true)
        {
            if(activeInteractableObjects != lastInteractableObjects)
            {
                lastInteractableObjects = activeInteractableObjects;
                ToggleInteractionPannels(activeInteractableObjects);
            }
        }
        else if (activeInteractableObjects != null)
        {
            ToggleInteractionPannels(activeInteractableObjects);            
        }
    }

    void Start() 
    {
        inputRequest.onValueChanged.AddListener(delegate{InputCheckOffer();});
    }

    public void InteractableObjectInsert(InteractableObjects interactable)
    {
        activeInteractableObjects = interactable;
        if(activeIsland == null && EventManager.instance.activeEvent == null)
        {
            if(interactable.GetType() == typeof(Island))
            {
                activeIsland = interactable as Island;
                SetIslandVariables();
                ToggleInteractionPannels(activeIsland);
            }
            if(interactable.GetType() == typeof(LootSite))
            {
                activeLootSite = interactable as LootSite;
                ToggleInteractionPannels(activeLootSite);
            }
            UpdateTradeResourceUI();
            UIManager.instance.SwitchPanel(UIManager.Panels.IslandInteraction);
        }
    }

    /// <summary>
    /// This function toggles buttons in interaction pannels acording to the state of the current active object
    /// </summary>
    /// <param name="interactableObjects"> the interactable object being refrenced</param>
    public void ToggleInteractionPannels(InteractableObjects interactableObjects)
    {
        InteractableObjects.InteractionState interactionState = interactableObjects.interactionState;
        DisableAllButtons();
        if(interactableObjects.interactionState != InteractableObjects.InteractionState.LootSite)
        {
            if (interactableObjects.explored)
            {
                switch (interactionState)
                {
                    case InteractableObjects.InteractionState.Unsettled:
                    if(activeIsland.looted != true)
                    {
                        pillageButton.SetActive(true);
                        islandStatusText.text = "This island seems uninhabited you might have some use for its recoures though";
                        if(activeIsland.canInteract)
                        {
                            tradeButton.GetComponent<Button>().interactable = true;
                            pillageButton.GetComponent<Button>().interactable = true;
                            confirmButton.GetComponent<Button>().interactable = true;
                        }
                        else
                        {
                            tradeButton.GetComponent<Button>().interactable = false;
                            pillageButton.GetComponent<Button>().interactable = false;
                            confirmButton.GetComponent<Button>().interactable = false;
                        }
                    }
                    else
                    {
                        islandStatusText.text = "This uninhabited Island has recently bin looted";
                    }
                    break;

                    case InteractableObjects.InteractionState.Settled:
                    attitudeParent.SetActive(true);
                    if(activeIsland.looted != true)
                    {
                        if(activeIsland.attitude >= -50)
                        {
                            tradeButton.SetActive(true);
                            pillageButton.SetActive(true);
                            pillageButton.GetComponent<Button>().interactable = true;
                            if(activeIsland.canInteract)
                            {
                                tradeButton.GetComponent<Button>().interactable = true;
                                pillageButton.GetComponent<Button>().interactable = true;
                            }
                            else
                            {
                                tradeButton.GetComponent<Button>().interactable = false;
                                pillageButton.GetComponent<Button>().interactable = false;
                            }
                            islandStatusText.text = "This island is inhabited and its poeple will talk to you and trade goods depending on your standing whit them";
                        }
                        else
                        {
                            tradeButton.GetComponent<Button>().interactable = false;
                            if(activeIsland.canInteract)
                            {
                                pillageButton.GetComponent<Button>().interactable = true;
                            }
                            else
                            {
                                pillageButton.GetComponent<Button>().interactable = false;                  
                            }
                            islandStatusText.text = "Becouse of youre actions the people of this island wont talk to you";
                        }
                    }
                    else
                    {
                        islandStatusText.text = "This inhabited Island has recently been looted";
                    }
                    break;
                }
            }
            else
            {
                exploreButton.SetActive(true);
                if(activeIsland.canInteract)
                {  
                    exploreButton.GetComponent<Button>().interactable = true;
                }
                else
                {
                    exploreButton.GetComponent<Button>().interactable = false;
                }
                islandStatusText.text = "This island has yet to be explored";
            }
        }
        else
        {
            pillageButton.SetActive(true);
            if(activeLootSite.canInteract)
            {
                pillageButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                pillageButton.GetComponent<Button>().interactable = false;
            }
            islandStatusText.text = "It looks like there might be valuables here";
        }
    }

    void DisableAllButtons()
    {
        pillageButton.SetActive(false);
        tradeButton.SetActive(false);
        pillageButton.SetActive(false);
        exploreButton.SetActive(false);
        attitudeParent.SetActive(false);
        backButton.SetActive(false);
    }

    void SetIslandVariables()
    {       
        switch (activeIsland.rDemand)
        {
            case CivManager.Type.Food:
            demandPic.sprite = resourceImages[0];
            break;

            case CivManager.Type.Mats:
            demandPic.sprite = resourceImages[1];
            break;

            case CivManager.Type.Money:
            demandPic.sprite = resourceImages[2];
            break;
        }

        switch (activeIsland.rExcess)
        {
            case CivManager.Type.Food:
            excessPic.sprite = resourceImages[0];
            break;

            case CivManager.Type.Mats:
            excessPic.sprite = resourceImages[1];
            break;

            case CivManager.Type.Money:
            excessPic.sprite = resourceImages[2];
            break;
        }
    }

    void SwitchInteractionPanels(InteractionPannels panels)
    {
        if(activePannel != null)
        {
            activePannel.SetActive(false);
        }
        switch (panels)
        {
            case InteractionPannels.Pillage:
            if(activePannel != pillagePannel)
            {
                pillagePannel.SetActive(true);
                activePannel = pillagePannel;
                backButton.SetActive(true);
            }
            else
            {
                pillagePannel.SetActive(false);
                activePannel = null;
            }
            break;
            case InteractionPannels.Trade:
            if(activePannel != tradePannel)
            {
                tradePannel.SetActive(true);
                islandStatusText.text = "";
                activePannel = tradePannel;
            }
            else
            {
                tradePannel.SetActive(false);
                activePannel = null;
                WipeTrade();
            }
            break;
        }
    }

    #region UIButtons

    public void Leave()
    {
        
        if(activePannel == tradePannel)
        {
            SwitchInteractionPanels(InteractionPannels.Trade);
        }
        else if(activePannel == pillagePannel)
        {
            SwitchInteractionPanels(InteractionPannels.Pillage);
        }
        
        UIManager.instance.SwitchPanel(UIManager.Panels.Main);
        activeIsland = null;
        activeLootSite = null;
        activeInteractableObjects = null;
    }

    public void Back()
    {
        UpdateTradeResourceUI();
        SwitchInteractionPanels(InteractionPannels.Info);
        backButton.SetActive(false);
        ToggleInteractionPannels(activeIsland);
    }
    
    public void Trade()
    {
        UpdateTradeResourceUI();
        SwitchInteractionPanels(InteractionPannels.Trade);
    }

    public void Pillage()
    {
        int foodVar = 0;
        int matsVar = 0;
        int moneyVar = 0;
        int peopleVar = 0;
        if(activeIsland != null)
        {
            foodVar = Random.Range(10,21);
            matsVar = Random.Range(10,21);
            moneyVar = Random.Range(10,21);
            if (activeIsland.interactionState == Island.InteractionState.Settled)
            {
                activeIsland.UpdateAttitude(-200);
                switch (activeIsland.rDemand)
                {
                    case CivManager.Type.Food:
                    foodVar -= 10;
                    foodVar = Mathf.Clamp(foodVar,0,int.MaxValue);
                    break;

                    case CivManager.Type.Mats:
                    matsVar -= 10;
                    matsVar = Mathf.Clamp(matsVar,0,int.MaxValue);
                    break;

                    case CivManager.Type.Money:
                    moneyVar -= 10;
                    moneyVar = Mathf.Clamp(moneyVar,0,int.MaxValue);
                    break;
                }

                switch (activeIsland.rExcess)
                {
                    case CivManager.Type.Food:
                    foodVar += 10;
                    break;

                    case CivManager.Type.Mats:
                    matsVar += 10;
                    break;

                    case CivManager.Type.Money:
                    moneyVar += 10;
                    break;
                }
                CivManager.instance.AddIncome(foodVar,CivManager.Type.Food);
                CivManager.instance.AddIncome(matsVar,CivManager.Type.Mats);
                CivManager.instance.AddIncome(moneyVar,CivManager.Type.Money);
            }
            else
            {
                CivManager.instance.AddIncome(foodVar,CivManager.Type.Food);
                CivManager.instance.AddIncome(matsVar,CivManager.Type.Mats);
                CivManager.instance.AddIncome(moneyVar,CivManager.Type.Money);
            }
            activeIsland.looted = true;
            activeIsland.recoveryTimer = 3;
        }
        else if (activeLootSite != null)
        {
            switch (activeLootSite.lootType)
            {
                case CivManager.Type.Food:
                    foodVar = Random.Range(10,21);
                    CivManager.instance.AddIncome(foodVar,CivManager.Type.Food);
                break;

                case CivManager.Type.Mats:
                    matsVar = Random.Range(10,21);
                    CivManager.instance.AddIncome(matsVar,CivManager.Type.Mats);
                break;

                case CivManager.Type.Money:
                    moneyVar = Random.Range(10,21);
                    CivManager.instance.AddIncome(moneyVar,CivManager.Type.Money);
                break;

                case CivManager.Type.People:
                    peopleVar = Random.Range(10,21);
                    CivManager.instance.AddIncome(peopleVar,CivManager.Type.People);
                break;
            }
            activeLootSite.looted = true;
            activeLootSite.recoveryTimer = 3;
        }
        DisableAllButtons();
        SwitchInteractionPanels(InteractionPannels.Pillage);
        foodText.text = foodVar.ToString();
        materialsText.text = matsVar.ToString();
        moneyText.text = moneyVar.ToString();
        peopleText.text = peopleVar.ToString();
        islandStatusText.text = "You pillaged the island";
        if(activeLootSite != null)
        {
            activeLootSite.Sink();
        }
    }

    public void Explore()
    {
        activeIsland.explored = true;
        UpdateTradeResourceUI();
        ToggleInteractionPannels(activeIsland);
    }
    #region Trade

    void WipeTrade()
    {
        inputDemand.text = "0";
        inputRequest.text = "0";
        islandStatusText.text = "";
    }

    /// <summary>
    /// Call to update trademenu Resource UI
    /// </summary>
    public void UpdateTradeResourceUI()
    {
        int tradeLeftVar = 0;
        if(activeIsland != null)
        {
            attitudeText.text = activeIsland.attitude.ToString();
            tradeLeftVar = activeIsland.maxTrading - activeIsland.amountTraded;
        }
        tradeLeftText.text = tradeLeftVar.ToString();
    }

        public void Confirm()
        {
            //Confirm deal
            bool canTrade = true;
            if(activeIsland.amountTraded != activeIsland.maxTrading)
            {
                if(paymentType != requestedType)
                {
                    switch (paymentType)
                    {
                        case CivManager.Type.Food:
                        if(CivManager.instance.food < demandedValue)
                        {
                            canTrade = false;
                            islandStatusText.text = "You don't have the required resources";
                        }
                        break;

                        case CivManager.Type.Mats:
                        if(CivManager.instance.mats < demandedValue)
                        {
                            canTrade = false;
                            islandStatusText.text = "You don't have the required resources";
                        }
                        break;

                        case CivManager.Type.Money:
                        if(CivManager.instance.money < demandedValue)
                        {
                            canTrade = false;
                            islandStatusText.text = "You don't have the required resources";
                        }
                        break;
                    }
                }
                else
                {
                    canTrade = false;
                    islandStatusText.text = "Cant trade the same resource";
                }
            }
            else
            {
                canTrade = false;
                islandStatusText.text = "This island wont trade anymore";
            }

            if(canTrade == true)
            {
                activeIsland.amountTraded += requestedValue;
                CivManager.instance.AddIncome(requestedValue,requestedType);
                CivManager.instance.RemoveIncome(demandedValue,paymentType);
                activeIsland.UpdateAttitude(requestedValue / 2);
                islandStatusText.text = "Transaction successful";
            }
            UpdateTradeResourceUI();
            InputCheckOffer();
        }

        void InputCheckOffer()
        {
            float tradeLeft = activeIsland.maxTrading - activeIsland.amountTraded;
            float input;
            //Ifstatement to make sure Parse doesent give a error on a empty input
            if(inputRequest.text != "")
            {
                input = float.Parse(inputRequest.text);
            }
            else
            {
                input = 0;
            }
            //Imput min max check
            if(input > tradeLeft)
            {
                input = tradeLeft;
            }
            else if(input < 0)
            {
                input = 0;
                inputDemand.text = input.ToString();
            }
            requestedValue = Mathf.RoundToInt(input);
            inputRequest.text = requestedValue.ToString();

            //modifier that goes over the price you pay
            float priceModifier = 1;
            if(input != 0)
            {
                if(requestedType == activeIsland.rExcess)
                {
                    priceModifier -= 0.25f;
                }
                else if (requestedType == activeIsland.rDemand)
                {
                    priceModifier += 0.5f;
                }
                if(paymentType == activeIsland.rDemand)
                {
                    priceModifier -= 0.25f;
                }
                else if (paymentType == activeIsland.rExcess)
                {
                    priceModifier += 0.5f;
                }
            }

            input *= priceModifier;
            demandedValue = Mathf.RoundToInt(input);
            inputDemand.text = demandedValue.ToString();
        }
        
        public void CheckRequest(int value)
        {
            requestTypeValue += value;
            if(requestTypeValue < 0)
            {
                requestTypeValue = 2;
            }
            else if(requestTypeValue > 2)
            {
                requestTypeValue = 0;
            }

            switch (requestTypeValue)
            {
                case 0:
                //mats
                requestedType = CivManager.Type.Mats;
                requestText.text = "Materials";
                break;

                case 1:
                //food
                requestedType = CivManager.Type.Food;
                requestText.text = "Food";
                break;

                case 2:
                //money
                requestedType = CivManager.Type.Money;
                requestText.text = "Money";
                break;
            }
            InputCheckOffer();
        }
        public void CheckDemand(int value)
        {
            demandTypeValue += value;
            if(demandTypeValue < 0)
            {
                demandTypeValue = 2;
            }
            else if(demandTypeValue > 2)
            {
                demandTypeValue = 0;
            }

            switch (demandTypeValue)
            {
                case 0:
                //mats
                paymentType = CivManager.Type.Mats;    
                demandText.text = "Materials";
                break;

                case 1:
                //food
                paymentType = CivManager.Type.Food;
                demandText.text = "Food";
                break;

                case 2:
                //money
                paymentType = CivManager.Type.Money;        
                demandText.text = "Money";
                break;
            }
            InputCheckOffer();
        }
        #endregion
    #endregion
}