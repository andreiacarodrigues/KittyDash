using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	public Text coins;
	public Text shields;
	public Button reviveBtn;
	public Button shieldBtn;

	private DataStorage ds;

	void Start () {
		ds = FindObjectOfType<DataStorage> ();
		coins.text = ds.coins + "";
		shields.text = ds.shields + "";
	}
		
	void Update()
	{
		coins.text = ds.coins + "";
		shields.text = ds.shields + "";

		if ((ds.shields >= 5) || (ds.coins < 800))
			shieldBtn.interactable = false;
		else
			shieldBtn.interactable = true;

		if ((ds.revives > 0) || (ds.coins < 1200))
			reviveBtn.interactable = false;
		else
			reviveBtn.interactable = true;
	}

	public void BuyShield()
	{
		ds.shields++;
		ds.coins -= 800; 
		ds.Save ();
	}

	public void BuyRevive()
	{
		if (ds.revives > 0)
			return;
		
		ds.revives++;
		ds.coins -= 1200; 
		ds.Save ();
	}

	public void GetCoins()
	{
		UnityAds.ShowRewardedAd ("coins");
		ds.Save ();
	}

}
