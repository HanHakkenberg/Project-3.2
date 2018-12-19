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
        Trade,
        Pillage
    }

    public static IslandInteractionManager instance;
    Island activeIsland;
    GameObject activePannel;
    TradeTypes tradeTypes;
    Button tradeButton;
    Button pillageButton;

    
    #region TradeRelated
    [Header("Excess & Demand")]
    public List<Sprite> resourceImages = new List<Sprite>();
    public TMP_Text excessType;
    public TMP_Text demandType;
    public Image demandPic;
    public Image excessPic;

    [Header("TradePannels Variables")]    
    [SerializeField]
    GameObject tradePannel;
    public TMP_Text tradeLeftText,tradeMessageText,tradeFoodText,tradeMoneyText,tradeMaterialText;
    public InputField inputRequest;
    public InputField inputDemand;
    public Dropdown tradeTypeDropdown;
    public Dropdown RequestTypeDropdown;
    public Dropdown DemandTypeDropdown;
    int demandedValue;
    int requestedValue;
    CivManager.Type paymentType;
    CivManager.Type requestedType;
    #endregion
    //Change
    #region PillageRelated
    [SerializeField]
    TMP_Text foodText,materialsText,moneyText,messageText;
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

    void Start() 
    {
        inputRequest.onValueChanged.AddListener(delegate{InputCheckOffer();});
        tradeTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckTradeType();});
        RequestTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckRequest();});
        DemandTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckDemand();});
    }

    public void IslandInsert(Island island)
    {
        activeIsland = island;
        IslandUICheck();
        UIManager.instance.SwitchPanel(UIManager.Panels.IslandInteraction);
        SetIslandVariables();
    }

    void IslandUICheck()
    {
        if(activeIsland.canInteract)
        {
            tradeButton.interactable = true;
            pillageButton.interactable = true;
        }
        else
        {
            tradeButton.interactable = false;
            pillageButton.interactable = false;
        }
    }

    void SetIslandVariables()
    {       
        switch (activeIsland.rDemand)
        {
            case CivManager.Type.Food:
            demandPic.sprite = resourceImages[0];
            demandType.text = "Food";
            break;

            case CivManager.Type.Mats:
            demandPic.sprite = resourceImages[1];
            demandType.text = "Materials";
            break;

            case CivManager.Type.Money:
            demandPic.sprite = resourceImages[2];
            demandType.text = "Money";
            break;
        }

        switch (activeIsland.rExcess)
        {
            case CivManager.Type.Food:
            excessPic.sprite = resourceImages[0];
            excessType.text = "Food";
            break;

            case CivManager.Type.Mats:
            excessPic.sprite = resourceImages[1];
            excessType.text = "Materials";
            break;

            case CivManager.Type.Money:
            excessPic.sprite = resourceImages[2];
            excessType.text = "Money";
            break;
        }
    }

    public void SwitchInteractionPanels(InteractionPannels panels)
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
    }

    public void Trade()
    {
        UpdateTradeResourceUI();
        SwitchInteractionPanels(InteractionPannels.Trade);
    }

    public void Pillage()
    {
        //Change
        if(activeIsland.looted != true)
        {
            activeIsland.looted = true;
            SwitchInteractionPanels(InteractionPannels.Pillage);
            if(activeIsland.settled == true)
            //Change
            if(activeIsland.looted != true)
            {
                activeIsland.UpdateAttitude(-4);
            }
        }
        else
        {
            messageText.text = "You already Looted this island";
            if(activePannel != pillagePannel)
            {
                SwitchInteractionPanels(InteractionPannels.Pillage);
            }
        }
    }
    #region Trade

    void WipeTrade()
    {
        inputDemand.text = "0";
        inputRequest.text = "0";
        tradeMessageText.text = "";
    }

    /// <summary>
    /// Call to update trademenu Resource UI
    /// </summary>
    public void UpdateTradeResourceUI()
    {
        tradeFoodText.text = CivManager.instance.food.ToString();
        tradeMaterialText.text = CivManager.instance.mats.ToString();
        tradeMoneyText.text = CivManager.instance.money.ToString();
        int tradeLeftVar = 0;
        if(activeIsland != null)
        {
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
                            tradeMessageText.text = "You don't have the required resources";
                        }
                        break;

                        case CivManager.Type.Mats:
                        if(CivManager.instance.mats < demandedValue)
                        {
                            canTrade = false;
                            tradeMessageText.text = "You don't have the required resources";
                        }
                        break;

                        case CivManager.Type.Money:
                        if(CivManager.instance.money < demandedValue)
                        {
                            canTrade = false;
                            tradeMessageText.text = "You don't have the required resources";
                        }
                        break;
                    }
                }
                else
                {
                    canTrade = false;
                    tradeMessageText.text = "Cant trade the same resource";
                }
            }
            else
            {
                canTrade = false;
                tradeMessageText.text = "This island wont trade anymore";
            }

            if(canTrade == true)
            {
                activeIsland.amountTraded += requestedValue;
                CivManager.instance.AddIncome(requestedValue,requestedType);
                CivManager.instance.RemoveIncome(demandedValue,paymentType);
                tradeMessageText.text = "Transaction successful";
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
        void DropdownCheckTradeType()
        {
            switch (tradeTypeDropdown.value)
            {
                case 0:
                tradeTypes = TradeTypes.Instant;
                break;

                case 1:
                tradeTypes = TradeTypes.Daily;        
                break;
            }
        }
        void DropdownCheckRequest()
        {
            switch (RequestTypeDropdown.value)
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
            switch (DemandTypeDropdown.value)
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
