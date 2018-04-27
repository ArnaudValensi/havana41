using UnityEngine;
using System.Collections;

public class GamePlayState : _StatesBase {

    [SerializeField] bool _logUpdate=false;
    private float gamePlayDuration;

	#region implemented abstract members of _StatesBase
	public override void OnActivate ()
	{
        // Managers.UI.panel.SetActive(false);
        // Managers.UI.ActivateUI(Menus.INGAME);

        gamePlayDuration = Time.time;
		Managers.Spawner.Spawn();
		Managers.Game.isGameActive = true;
        //Debug.Log ("<color=green>Gameplay State</color> OnActive");	
	}
	public override void OnDeactivate ()
	{
        Managers.Game.stats.timeSpent += Time.time - gamePlayDuration;
		//Debug.Log ("<color=red>Gameplay State</color> OnDeactivate");
	}

	public override void OnUpdate ()
	{
        if(Managers.Game.currentShape!=null)
            Managers.Game.currentShape.movementController.ShapeUpdate();

        if(_logUpdate)
		    Debug.Log ("<color=yellow>Gameplay State</color> OnUpdate");
	}
	#endregion

}
