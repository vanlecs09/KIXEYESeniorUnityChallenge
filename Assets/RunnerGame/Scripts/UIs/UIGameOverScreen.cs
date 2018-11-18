
using UnuGames;
using UnuGames.MVVM;

// This code is generated automatically by UIMan - UI Generator, please do not modify!

public partial class UIGameOverScreen : UIManScreen {


	int _score = 0;
	[UIManProperty]
	public int Score {
		get { return _score; }
		set { _score = value; OnPropertyChanged(); }
	}

}
