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
      BuildingMaterials,
      Money,
      Food,
      Poeple
   }

   public static CivManager instance;
   #region Resources
   public int buildingMaterials{ get; private set; }
   public int money{ get; private set; }
   public int food{ get; private set; }
   public int poeple{ get; private set; }
   #endregion

   #region Ledger
   public int buildingMaterialsIncome{ get; private set; }
   public int moneyIncome{ get; private set; }
   public int foodIncome{ get; private set; }
   public int poepleIncome{ get; private set; }
   
   #endregion

   #region Stability

   /// <summary>
   /// stability variable. Used to show in UI and as a condition to decide the stability modifier
   /// </summary>
   public int stability{
      get; private set;
   }
   /// <summary>
   /// Resource modifier. use this to modify the income from buildings on the main island.
   /// </summary>
   /// 
   /// 
   /// 
   private float _stabilityModifier = 1f;
   public float stabilityModifier{
      get {
         return _stabilityModifier;
      } 
      private set{
         _stabilityModifier = value;
      }
   }
   #endregion

   #region OverTime
   int foodToEat;
   [Tooltip("Time in seconds till ResourseUseOverTime function ticks")]
   [SerializeField]
   private float repeatTime;
   private int foodStep;
   #endregion


   void Start() 
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
        UpdateStability(0);
        ResourseUseOverTimeVariableUpdate();
        StartCoroutine(ResourseUseOverTime());
   }

   /// <summary>
   /// Call this function when you want to add a value to a type of resource (poeple, money, etc)
   /// </summary>
   /// <param name="toAdd">The value that needs to be added to the type</param>
   /// <param name="type">The type the value gets added to</param>
   public void AddIncome(int toAdd, Type type)
   {
      toAdd = Mathf.Abs(toAdd);
      
      switch (type)
      {
         case Type.BuildingMaterials:
            buildingMaterials += toAdd;
            buildingMaterialsIncome += toAdd;
            break;
         case Type.Money:
            money += toAdd;
            moneyIncome += toAdd;
            break;
         case Type.Food:
            food += toAdd;
            foodIncome += toAdd;
            break;
         case Type.Poeple:
            poeple += toAdd;
            poepleIncome += toAdd;
            break;            
      }
   }

   /// <summary>
   /// Call this function when you want to remove a value from a type of resource (poeple, money, etc)
   /// </summary>
   /// <param name="toRemove">The value that needs to be removed from the type</param>
   /// <param name="type">The type the value gets removed from</param>
   public void RemoveIncome(int toRemove, Type type)
   {
      toRemove = -Mathf.Abs(toRemove);
      
      switch (type)
      {
         case Type.BuildingMaterials:
            buildingMaterials -= toRemove;
            break;
         case Type.Money:
            money -= toRemove;
            break;
         case Type.Food:
            food -= toRemove;
            break;
         case Type.Poeple:
            poeple -= toRemove;
            break;
      }
   }

   /// <summary>
   /// call this function if you need to add or remove stability
   /// </summary>
   /// <param name="toUpdate">The value that is used to update the stability</param>
   public void UpdateStability(int toUpdate)
   {
      stability += toUpdate;
      stability = Mathf.Clamp(stability, -2 , 2);

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

   private void ResourseUseOverTimeVariableUpdate()
   {
      foodToEat = poeple * 3;
      foodStep = foodToEat / GameManager.instance.lengthOfDay;
      print(foodStep);
   }

   public IEnumerator ResourseUseOverTime()
   {
      yield return new WaitForSeconds(repeatTime);
      if (food > 0)
      {
         foodToEat -= foodStep;
         food -= foodStep;
      }
      if (food < 0 && food != 0)
      {
         food = 0;
      }
      StartCoroutine(ResourseUseOverTime());
   }
}
