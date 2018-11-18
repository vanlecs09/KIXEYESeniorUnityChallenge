using UnityEngine;
using UnuGames;
using UnuGames.MVVM;

public partial class UIGamePlayScreen : UIManScreen
{

    #region Fields

    // Your fields here
    private ObstacleSystem _obstaclelSystem;
    private Player _player;
    private CameraSystem _cameraSystem;
    #endregion

    #region Built-in Events
    public override void OnShow(params object[] args)
    {
        base.OnShow(args);
    }

    public override void OnShowComplete()
    {
        base.OnShowComplete();
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    public override void OnHideComplete()
    {
        base.OnHideComplete();
    }
    #endregion

    #region Custom implementation
    // Your custom code here
    void Start()
    {
        _player = new Player();
        _player.Init();

        _obstaclelSystem = new ObstacleSystem();
        _obstaclelSystem.Init();
        _obstaclelSystem.PlayerTrans = _player.PlayerTrans;

        _cameraSystem = new CameraSystem();
        _cameraSystem.Init(_player.PlayerTrans);

        UGame.EventManager.StartListening(EventNames.PLAYER_PASS_OBSTACLE, PlayerPassObstacle);
        UGame.EventManager.StartListening(EventNames.PAUSE_GAME, PauseGame);
        UGame.EventManager.StartListening(EventNames.RESUME_GAME, ResumeGame);
		UGame.EventManager.StartListening(EventNames.GMAE_OVER, GameOver);

        GameData.Instance.UserData.Score = 0;
        Score = GameData.Instance.UserData.Score;
    }

    void Update()
    {
        this._player.Update(Time.deltaTime);
        this._obstaclelSystem.Update(Time.deltaTime);

        UpdateInput();
    }

    void FixedUpdate()
    {
        this._player.FixedUpdate(Time.fixedDeltaTime);
        // _obstaclelSystem.FixedUpdate(Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        this._cameraSystem.LateUpdate(Time.deltaTime);
    }

    void UpdateInput()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            _player.Jump();
        }
        #elif UNITY_ANDROID || UNITY_IOS
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            _player.Jump();
        }
        #endif
    }

    void OnDestroy()
    {
        this._player.Shutdown();
        this._cameraSystem.Shutdown();
        this._obstaclelSystem.Shutdown();
        this._player = null;
        this._cameraSystem = null;
        this._obstaclelSystem = null;

        UGame.EventManager.StopListening(EventNames.PAUSE_GAME, PauseGame);
        UGame.EventManager.StopListening(EventNames.RESUME_GAME, ResumeGame);
        UGame.EventManager.StopListening(EventNames.PLAYER_PASS_OBSTACLE, PlayerPassObstacle);
        UGame.EventManager.StopListening(EventNames.GMAE_OVER, GameOver);
    }

    void PlayerPassObstacle(System.Object obj = null)
    {
        Score += 10;
        GameData.Instance.UserData.Score = Score;
    }

    public void OnBtnPause()
    {
        UIMan.Instance.ShowDialog<UIPausePopUp>();
        UGame.EventManager.TriggerEvent(EventNames.PAUSE_GAME);
    }


    void PauseGame(System.Object obj = null)
    {
        Time.timeScale = 0;
    }

    void ResumeGame(System.Object obj = null)
    {
        Time.timeScale = 1;
    }

    void GameOver(System.Object obj = null)
    {
        UIMan.Instance.DestroyUI<UIGamePlayScreen>();
        UIMan.Instance.ShowScreen<UIGameOverScreen>();
    }
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
