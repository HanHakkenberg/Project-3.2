using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List", menuName = "Variable/List")]
public abstract class ListVariable<T> : ScriptableObject {
	[Multiline][SerializeField] string variableInfo;

	public List<T> value = new List<T>();
	public List<T> baseValue = new List<T>();
	public bool reset = true;
}