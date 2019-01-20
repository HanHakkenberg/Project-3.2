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
    public Island activeIsland{ get; private set; }
    public Island lastActiveIsland;
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
    // public TMP_Dropdown tradeTypeDropdown;
    public TMP_Dropdown requestTypeDropdown;
    public TMP_Dropdown paymentTypeDropdown;
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
        if(activeIsland != null && activeIsland.canInteract == true)
        {
            if(activeIsland != lastActiveIsland)
            {
                lastActiveIsland = activeIsland;
                ToggleInteractionPannels(activeIsland);
            }
        }
        else if (activeIsland != null)
        {
            ToggleInteractionPannels(activeIsland);            
        }
    }

    void Start() 
    {
        inputRequest.onValueChanged.AddListener(delegate{InputCheckOffer();});
        // tradeTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckTradeType();});
        requestTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckRequest();});
        paymentTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckDemand();});
    }

    public void InteractableObjectInsert(InteractableObjects interactable)
    {
        if(activeIsland == null)
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
        if(interactableObjects.interactionState != InteractableObjects.InteractionState.LootSite)
        {
            InteractableObjects.InteractionState interactionState = interactableObjects.interactionState;
            DisableAllButtons();
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
                        }
                        else
                        {
                            tradeButton.GetComponent<Button>().interactable = false;
                            pillageButton.GetComponent<Button>().interactable = false;
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
        if(activeIsland != null)
        {
            int foodVar = Random.Range(10,21);
            int matsVar = Random.Range(10,21);
            int moneyVar = Random.Range(10,21);
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
            DisableAllButtons();
            SwitchInteractionPanels(InteractionPannels.Pillage);
            foodText.text = foodVar.ToString();
            materialsText.text = matsVar.ToString();
            moneyText.text = moneyVar.ToString();
            islandStatusText.text = "You pillaged the island";
        }
        else if (activeLootSite != null)
        {
            switch (activeLootSite.lootType)
            {
                case CivManager.Type.Food:
                    int foodVar = Random.Range(10,21);
                    CivManager.instance.AddIncome(foodVar,CivManager.Type.Food);
                break;

                case CivManager.Type.Mats:
                    int matsVar = Random.Range(10,21);
                    CivManager.instance.AddIncome(matsVar,CivManager.Type.Mats);
                break;

                case CivManager.Type.Money:
                    int moneyVar = Random.Range(10,21);
                    CivManager.instance.AddIncome(moneyVar,CivManager.Type.Money);
                break;
            }
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
        
        void DropdownCheckRequest()
        {
            switch (requestTypeDropdown.value)
            {
                case 0:
                //mats
                requestedType = CivManager.Type.Mats;
                break;

                case 1:
                //food
                requestedType = CivManager.Type.Food;
                break;

                case 2:
                //money
                requestedType = CivManager.Type.Money;
                break;
            }
            InputCheckOffer();
        }
        void DropdownCheckDemand()
        {
            switch (paymentTypeDropdown.value)
            {
                case 0:
                //mats
                paymentType = CivManager.Type.Mats;                    
                break;

                case 1:
                //food
                paymentType = CivManager.Type.Food;
                break;

                case 2:
                //money
                paymentType = CivManager.Type.Money;                    
                break;
            }
            InputCheckOffer();
        }
        #endregion
    #endregion
}
