using System.Collections;
using UnityEngine;


public class List : MonoBehaviour
{
    [SerializeField] private int numberOfElements = 10;
    [SerializeField] private Scroller scroller = null;
    [SerializeField] private Cell cellPrefab = null;

	private void Start()
	{
		//add elements
		
		for(int n = 0 ; n < numberOfElements ; n++)
		{
            AddElement();
		}        

	}

    private void Update()
    {
        if (Input.GetKeyDown( KeyCode.Space ))
            AddElement();
    }

    private void AddElement()
    {
        Cell cell = Instantiate( cellPrefab );
        cell.ElementName = scroller.GridLayout.transform.childCount.ToString();
        scroller.AddElement( cell.gameObject );
    }

    /*
	private IEnumerator CO_Test()
	{
		//create 10 elements one by one
		for(int n = 0 ; n < 10 ; n++)
		{
			Cell cell = CreateElement();
			cell.ElementName = n.ToString();
			yield return new WaitForSeconds(0.4f);
		}

		yield return new WaitForSeconds(1.0f);
		//clear all the elements
		ClearList();

		//fill it aigan like crazy
		for(int n = 0 ; n < 100 ; n++)
		{
			Cell cell = CreateElement();
			cell.ElementName = n.ToString();
			yield return new WaitForSeconds(0.2f);
		}

	}*/

}
