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

    public static IslandInteractionManager instance;
    Island activeIsland;
    TradeTypes tradeTypes;

    //destroy this all refrence to this and the button click event
    bool prototypeBool;

    
    #region TradeRelated
    bool trading;
    public int minInput;
    public int maxInput;
    [SerializeField]
    GameObject tradePannel;
    public TMP_Text tradeMessageText;
    public InputField inputRequest;
    public InputField inputDemand;
    public Dropdown tradeTypeDropdown;
    public Dropdown RequestTypeDropdown;
    public Dropdown DemandTypeDropdown;

    int demandedValue;
    int requestedValue;
    CivManager.Type demandedType;
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

    public void PrototypeFunction()
    {
        if(prototypeBool == false)
        {
            prototypeBool = true;
        }
        else
        {
            prototypeBool = false;
        }
        if (trading)
        {
            Trade();
        }
        pillagePannel.SetActive(false);
        UIManager.instance.SwitchPanel(UIManager.Panels.Main);
    }

    public void IslandInsert(Island island)
    {
        if(prototypeBool == true)
        {
            activeIsland = island;
            UIManager.instance.SwitchPanel(UIManager.Panels.IslandInteraction);

            //removethishehexd
            UIManager.instance.mainPannel.SetActive(true);
        }
    }

    #region UIButtons

    public void Leave()
    {
        Trade();
        UIManager.instance.SwitchPanel(UIManager.Panels.Main);
    }

    public void Trade()
    {
        if (!trading)
        {
            trading = true;
            tradePannel.SetActive(true);

            //Change
            pillagePannel.SetActive(false);            
        }
        else
        {
            trading = false;
            tradePannel.SetActive(false);
            
            //change
            inputDemand.text = "0";
            inputRequest.text = "0";
            tradeMessageText.text = "";
        }
    }

    public void Pillage()
    {
        //Change
        if(activeIsland.pillaged != true)
        {
            activeIsland.pillaged = true;
            CivManager.instance.AddIncome(activeIsland.foodLoot,CivManager.Type.Food);
            CivManager.instance.AddIncome(activeIsland.matLoot,CivManager.Type.Mats);
            CivManager.instance.AddIncome(activeIsland.goldLoot,CivManager.Type.Money);

            foodText.text = activeIsland.foodLoot.ToString();
            materialsText.text = activeIsland.matLoot.ToString();
            moneyText.text = activeIsland.goldLoot.ToString();

            pillagePannel.SetActive(true);
            tradePannel.SetActive(false);
        }
        else
        {
            messageText.text = "You already Looted this island for";
        }
    }

        #region Trade

        public void Confirm()
        {
            //Confirm deal
            bool canTrade = true;
            switch (demandedType)
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
                CivManager.instance.RemoveIncome(demandedValue,demandedType);
                tradeMessageText.text = "Transaction successful";
            }
        }
        void InputCheckOffer()
        {
            int input = int.Parse(inputRequest.text);
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
            requestedValue = input;

            //Update dit later
            input *= 2;
            inputDemand.text = input.ToString();
            demandedValue = input;
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
                    demandedType = CivManager.Type.Mats;                    
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
                    demandedType = CivManager.Type.Food;
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
                    demandedType = CivManager.Type.Money;                    
                }
                break;
            }
        }
        #endregion
    #endregion
}
