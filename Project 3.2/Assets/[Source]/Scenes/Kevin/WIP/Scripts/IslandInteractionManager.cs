﻿using System.Collections;
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
    
    #region TradeRelated
    [SerializeField]
    GameObject tradePannel;
    [SerializeField]
    Image excessImg,demandImg;
    public List<Sprite> resourceImages = new List<Sprite>();
    public int minInput;
    public int maxInput;
    public TMP_Text tradeMessageText;
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
        inputDemand.onValueChanged.AddListener(delegate{InputCheckDemand();});
        tradeTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckTradeType();});
        RequestTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckRequest();});
        DemandTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckDemand();});

        DropdownCheckRequest();
    }

    public void IslandInsert(Island island)
    {
        activeIsland = island;
        UIManager.instance.SwitchPanel(UIManager.Panels.IslandInteraction);

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

    void WipeTrade()
    {
        inputDemand.text = "0";
        inputRequest.text = "0";
        tradeMessageText.text = "";
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

        public void Confirm()
        {
            //Confirm deal
            bool canTrade = true;
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

            if(canTrade == true)
            {
                CivManager.instance.AddIncome(requestedValue,requestedType);
                CivManager.instance.RemoveIncome(demandedValue,paymentType);
                tradeMessageText.text = "Transaction successful";
            }
        }

        void InputCheckOffer()
        {
            //Input min max
            float input = int.Parse(inputRequest.text);
            if(input > maxInput)
            {
                input = maxInput;
            }
            else if(input < minInput)
            {
                input = minInput;
                inputDemand.text = input.ToString();
            }
            inputRequest.text = input.ToString();
            requestedValue = Mathf.RoundToInt(input);

            //modifier that goes over the price you pay
            float priceModifier = 1;
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

            input *= priceModifier;
            inputDemand.text = input.ToString();
            demandedValue = Mathf.RoundToInt(input);
        }
        void InputCheckDemand()
        {
            // int input = int.Parse(inputDemand.text);
            // if(input > maxInput)
            // {
            //     input = maxInput;
            // }
            // else if(input < minInput)
            // {
            //     input = minInput;
            // }
            // inputDemand.text = input.ToString();
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
                if(DemandTypeDropdown.value == 0)
                {
                    RequestTypeDropdown.value += 1;
                }
                else
                {
                    requestedType = CivManager.Type.Mats;                    
                }
                break;

                case 1:
                //food
                if(DemandTypeDropdown.value == 1)
                {
                    RequestTypeDropdown.value += 1;
                }
                else
                {
                    requestedType = CivManager.Type.Food;   
                }
                break;

                case 2:
                //money
                if(DemandTypeDropdown.value == 2)
                {
                    RequestTypeDropdown.value = 0;
                }
                else
                {
                    requestedType = CivManager.Type.Money;  
                }
                break;
            }
        }
        void DropdownCheckDemand()
        {
            switch (DemandTypeDropdown.value)
            {
                case 0:
                //mats
                if(RequestTypeDropdown.value == 0)
                {
                    DemandTypeDropdown.value += 1;
                }
                else
                {
                    paymentType = CivManager.Type.Mats;                    
                }
                break;

                case 1:
                //food
                if(RequestTypeDropdown.value == 1)
                {
                    DemandTypeDropdown.value += 1;
                }
                else
                {
                    paymentType = CivManager.Type.Food;
                }
                break;

                case 2:
                //money
                if(RequestTypeDropdown.value == 2)
                {
                    DemandTypeDropdown.value = 0;
                }
                else
                {
                    paymentType = CivManager.Type.Money;                    
                }
                break;
            }
        }
        #endregion
    #endregion
}
