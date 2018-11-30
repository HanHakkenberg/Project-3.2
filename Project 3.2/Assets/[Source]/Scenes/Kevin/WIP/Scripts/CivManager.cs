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

   #region Resources
   int buildingMaterials;
   int money;
   int food;
   int poeple;
   #endregion

   public static float stability;

   private void Start() {
      AddIncome(-11,Type.Poeple);
      RemoveIncome(11,Type.Poeple);
   }

   /// <summary>
   /// Call this function when you want to add a value to a type of resource (poeple, money, etc)
   /// </summary>
   /// <param name="toAdd">The value that needs to be added to the type</param>
   /// <param name="type">The type the value gets added to</param>
   public void AddIncome(int toAdd, Type type)
   {
      toAdd = Mathf.Abs(toAdd);

      print(toAdd);
      
      switch (type)
      {
         case Type.BuildingMaterials:
            buildingMaterials += toAdd;
            break;
         case Type.Money:
            money += toAdd;
            break;
         case Type.Food:
            food += toAdd;
            break;
         case Type.Poeple:
            poeple += toAdd;
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

      print(toRemove);
      
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


}
