using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour
{
	public static string finalResult;

	private static string reward;

	public static void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
		else
		{
			Debug.Log ("Ad not ready");
		}
	}

	public static void ShowRewardedAd(string r)
	{
		reward = r;

		if(Advertisement.IsReady())
			Advertisement.Show("rewardedVideo", new ShowOptions(){resultCallback = HandleAdResult});
	}

	public static bool AdAvailable()
	{
		return Advertisement.IsReady ();
	}

	public static void HandleAdResult(ShowResult result)
	{
		switch(result)
		{
		case ShowResult.Finished:
			DataStorage ds = (DataStorage)FindObjectOfType<DataStorage> ();
			if(reward == "revive")
				ds.revives++;
			if(reward == "coins")
				ds.coins+= 1000;
			ds.Save ();
			break;

		case ShowResult.Skipped:
			finalResult = "Skipped";
			break;

		case ShowResult.Failed:
			finalResult = "Failed";
			break;
		}
	}

}