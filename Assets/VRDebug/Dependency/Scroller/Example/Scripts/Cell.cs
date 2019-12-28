using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
	[SerializeField] private Text elementName = null;


	public string ElementName{ set { elementName.text = value; } }
}
