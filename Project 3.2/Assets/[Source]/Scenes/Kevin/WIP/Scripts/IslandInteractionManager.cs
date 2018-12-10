using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandInteractionManager : MonoBehaviour
{
    enum TradeTypes
    {
        Instant,
        Daily
    }

    public static IslandInteractionManager instance;
    public Island activeIsland;
    TradeTypes tradeTypes;

    
    #region TradeRelated
    bool trading;
    public int minInput;
    public int maxInput;
    public GameObject tradePannel;
    public InputField inputOffer;
    public InputField inputDemand;
    public Dropdown tradeTypeDropdown;
    public Dropdown OfferTypeDropdown;
    public Dropdown DemandTypeDropdown;


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

    public void IslandInsert(Island island)
    {
        UIManager.instance.SwitchPanel(UIManager.Panels.IslandInteraction);
    }

    #region UIButtons

    public void Leave()
    {
        //Trigger UI  pannel toggle
    }

    public void Trade()
    {
        if (!trading)
        {
            trading = true;
            tradePannel.SetActive(true);

        }
        else
        {
            trading = false;
            tradePannel.SetActive(false);
        }
    }

    public void Pillage()
    {
        //Something that we want whit pillaging
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
