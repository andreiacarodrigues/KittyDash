using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dictionary : MonoBehaviour {


	public GameObject[] pages;
	public Button leftButton;
	public Button rightButton;

	private int currentPage;

	void Start () {
		// Começa com pagina 0 enabled e as outras disabled
		currentPage = 0;
		leftButton.interactable = false;
	}
		
	void Update () {
		if (currentPage > 0) {
			if (currentPage < pages.Length - 1) 
			{
				leftButton.interactable = true;
				rightButton.interactable = true;
			} 
			else 
			{
				leftButton.interactable = true;
				rightButton.interactable = false;
			}
		} 
		else 
		{
			leftButton.interactable = false;
			rightButton.interactable = true;
		}
	}

	public void nextPage()
	{
		pages [currentPage].SetActive (false);
		currentPage++;
		pages [currentPage].SetActive (true);
	}

	public void previousPage()
	{
		pages [currentPage].SetActive (false);
		currentPage--;
		pages [currentPage].SetActive (true);
	}

}
