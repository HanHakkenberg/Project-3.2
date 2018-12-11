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
    public InputField inputOffer;
    public InputField inputDemand;
    public Dropdown tradeTypeDropdown;
    public Dropdown OfferTypeDropdown;
    public Dropdown DemandTypeDropdown;
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
        inputOffer.onValueChanged.AddListener(delegate{InputCheckOffer();});
        inputDemand.onValueChanged.AddListener(delegate{InputCheckDemand();});
        tradeTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckTradeType();});
        OfferTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckOffer();});
        DemandTypeDropdown.onValueChanged.AddListener(delegate{DropdownCheckDemand();});
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
        UIManager.instance.SwitchPanel(UIManager.Panels.Main);
    }

    public void IslandInsert(Island island)
    {
        if(prototypeBool == true)
        {
            activeIsland = island;
            UIManager.instance.SwitchPanel(UIManager.Panels.IslandInteraction);
        }
    }

    #region UIButtons

    public void Leave()
    {
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
        }
        void InputCheckOffer()
        {
            int input = int.Parse(inputOffer.text);
            if(input > maxInput)
            {
                input = maxInput;
            }
            else if(input < minInput)
            {
                input = minInput;
            }
            inputOffer.text = input.ToString();
        }
        void InputCheckDemand()
        {
            int input = int.Parse(inputDemand.text);
            if(input > maxInput)
            {
                input = maxInput;
            }
            else if(input < minInput)
            {
                input = minInput;
            }
            inputDemand.text = input.ToString();
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
        void DropdownCheckOffer()
        {
            switch (OfferTypeDropdown.value)
            {
                case 0:
                //mats
                break;

                case 1:
                //food
                break;

                case 2:
                //money
                break;
            }
        }
        void DropdownCheckDemand()
        {
            switch (DemandTypeDropdown.value)
            {
                case 0:
                //mats
                break;

                case 1:
                //food
                break;

                case 2:
                //money
                break;
            }
        }
        #endregion
    #endregion
}
