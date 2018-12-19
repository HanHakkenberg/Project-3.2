using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivManager : MonoBehaviour
{
   /// <summary>
   /// Resourse types
   /// </summary>
   public enum Type
   {
      Mats,
      Money,
      Food,
      People,
      Stability
   }

   public static CivManager instance;
   #region Resources
   public int mats{ get; private set; }
   public int money{ get; private set; }
   public int food{ get; private set; }
   public int people{ get; private set; }
   #endregion

   #region ResoursCaps
   public int matsCap{ get; private set; }
   public int moneyCap{ get; private set; }
   public int foodCap{ get; private set; }
   public int peopleCap{ get; private set; }

   #endregion

   #region Ledger
   public int matsIncome{ get; private set; }
   public int moneyIncome{ get; private set; }
   public int foodIncome{ get; private set; }
   public int peopleIncome{ get; private set; }
   #endregion

   #region Stability
   /// <summary>
   /// stability variable. Used to show in UI and as a condition to decide the stability modifier
   /// </summary>
   public int stability{get; private set;}
   /// <summary>
   /// Resource modifier. use this to modify the income from buildings on the main island.
   /// </summary>
   private float _stabilityModifier = 1f;
   public float stabilityModifier{get {return _stabilityModifier;} private set{_stabilityModifier = value;}}
   #endregion

   #region OverTime
   int foodToEat;
   [Tooltip("Time in seconds till ResourseUseOverTime function ticks")]
   [SerializeField]
   private float repeatTime;
   private int foodStep;
   #endregion

   void Awake() 
   {
      if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        GameManager.shortGameplayTick += ResourceUseOverTime;
        GameManager.longGameplayTick += ResourceUseOverTimeVariableUpdate;
   }
   void Start() 
   {
      foodCap = 1000;
      matsCap = 1000;
      moneyCap = 1000;
      peopleCap = 1000;
      people = 10;
      food = 100;
      mats = 100;
      money = 100;
      UIManager.instance.UpdateResourceUI(); //misschien removen idk?
      UpdateStability();
      ResourceUseOverTimeVariableUpdate();
   }

   /// <summary>
   /// Call this function when you want to add a value to a type of resource (poeple, money, etc)
   /// </summary>
   /// <param name="toAdd">The value that needs to be added to the type</param>
   /// <param name="type">The type the value gets added to</param>
   public void AddIncome(int toAdd, Type type)
   {
      //makes sure all input is positive
      toAdd = Mathf.Abs(toAdd);
      
      switch (type)
      {
         case Type.Mats:
            mats += toAdd;
            matsIncome += toAdd;
            if (mats > matsCap)
            {
               mats = matsCap; 
            }
            UIManager.instance.RecourceUIPopup(toAdd,Type.Mats);
            break;
         case Type.Money:
            money += toAdd;
            moneyIncome += toAdd;
            if(money > moneyCap)
            {
               money = moneyCap;
            }
            UIManager.instance.RecourceUIPopup(toAdd,Type.Money);
            break;
         case Type.Food:
            food += toAdd;
            foodIncome += toAdd;
            if(food > foodCap)
            {
               food = foodCap;
            }
            UIManager.instance.RecourceUIPopup(toAdd,Type.Food);            
            break;
         case Type.People:
            people += toAdd;
            peopleIncome += toAdd;
            if(people > peopleCap)
            {
               people = peopleCap;
            }
            UIManager.instance.RecourceUIPopup(toAdd,Type.People);
            break;       
         case Type.Stability:
            stability += toAdd;
            stability = Mathf.Clamp(stability, -2 , 2);
            UpdateStability();
            UIManager.instance.RecourceUIPopup(toAdd,Type.Stability);
            break;     
      }
      IslandInteractionManager.instance.UpdateTradeResourceUI();
   }

   /// <summary>
   /// Call this function when you want to remove a value from a type of resource (poeple, money, etc)
   /// </summary>
   /// <param name="toRemove">The value that needs to be removed from the type</param>
   /// <param name="type">The type the value gets removed from</param>
   public void RemoveIncome(int toRemove, Type type)
   {
      //makes sure all input is negative
      toRemove = -Mathf.Abs(toRemove);
      
      switch (type)
      {
         case Type.Mats:
            mats += toRemove;
            matsIncome += toRemove;
            if(mats < 0)
            {
               mats = 0;
            }
            UIManager.instance.RecourceUIPopup(toRemove,Type.Mats);
            break;
         case Type.Money:
            money += toRemove;
            moneyIncome += toRemove;
            if(money < 0)
            {
               money = 0;
            }
            UIManager.instance.RecourceUIPopup(toRemove,Type.Money);
            break;
         case Type.Food:
            food += toRemove;
            foodIncome += toRemove;
            if(food < 0)
            {
               food = 0;
            }
            UIManager.instance.RecourceUIPopup(toRemove,Type.Food);
            break;
         case Type.People:
            people += toRemove;
            peopleIncome += toRemove;
            if(people < 0)
            {
               people = 0;
            }
            UIManager.instance.RecourceUIPopup(toRemove,Type.People);
            break;
         case Type.Stability:
            stability += toRemove;
            stability = Mathf.Clamp(stability, -2 , 2);
            UpdateStability();
            UIManager.instance.RecourceUIPopup(toRemove,Type.Stability);
            break;
      }
      IslandInteractionManager.instance.UpdateTradeResourceUI();
   }

   void UpdateStability()
   {
      switch (stability)
      {
         case 2:
            stabilityModifier = 1.2f;
         break;
         case 1:
            stabilityModifier = 1.1f;
         break;
         case -1:
            stabilityModifier = 0.8f;
         break;
         case -2:
            stabilityModifier = 0.5f;
         break;
         default:
            stabilityModifier = 1;
         break;
      }
   }

   private void ResourceUseOverTimeVariableUpdate()
   {
      foodToEat = people * 3;
      foodStep = foodToEat / GameManager.instance.lengthOfDay;
   }

   public void ResourceUseOverTime()
   {
      if (food > 0)
      {
         foodToEat -= foodStep;
         RemoveIncome(foodStep,Type.Food);
      }
      else
      {
         //There Will Be consequences
      }
      if (food < 0 && food != 0)
      {
         food = 0;
         //Also consequences
      }
   }
}
