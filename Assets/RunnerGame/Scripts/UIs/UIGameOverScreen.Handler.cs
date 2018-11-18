using UnityEngine;
using UnuGames;
using UnuGames.MVVM;

public partial class UIGameOverScreen : UIManScreen {

#region Fields

	// Your fields here
#endregion

#region Built-in Events
	public override void OnShow (params object[] args)
	{
		base.OnShow (args);
	}

	public override void OnShowComplete ()
	{
		base.OnShowComplete ();
		LeaderBoard.Instance.SendScoreToLeaderBoard();
	}

	public override void OnHide ()
	{
		base.OnHide ();
	}

	public override void OnHideComplete ()
	{
		base.OnHideComplete ();
	}
#endregion

#region Custom implementation
	void Start()
	{
		Score = GameData.Instance.UserData.Score;
	}
	public void OnBtnRetry()
	{
		UIMan.Instance.DestroyUI<UIGameOverScreen>();
		UIMan.Instance.ShowScreen<UIGamePlayScreen>();
	}

	public void OnBtnHome()
	{
		UIMan.Instance.DestroyUI<UIGameOverScreen>();
		UIMan.Instance.ShowScreen<UIStartScreen>();
	}
	// Your custom code here
#endregion

#region Override animations
	/* Uncommend this for override show/hide animation of Screen/Dialog use tweening code
	public override IEnumerator AnimationShow ()
	{
		return base.AnimationShow ();
	}

	public override IEnumerator AnimationHide ()
	{
		return base.AnimationHide ();
	}

	public override IEnumerator AnimationIdle ()
	{
		return base.AnimationHide ();
	}
	*/
#endregion
}
