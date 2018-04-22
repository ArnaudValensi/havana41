using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class SpawnManager : MonoBehaviour {

	public GameObject[] shapeTypes;

    public List<Transform> _startPositionPossibility;

    private void Awake()
    {
        Assert.IsNotNull(_startPositionPossibility);
        Assert.IsTrue(_startPositionPossibility.Count > 0);
    }

    public void Spawn()
	{
		// Random Shape
   		 int i = Random.Range(0, shapeTypes.Length);
        int j = Random.Range(0, _startPositionPossibility.Count);
        

		// Spawn Group at current Position
		GameObject temp =Instantiate(shapeTypes[i], _startPositionPossibility[j].position, Quaternion.identity, Managers.Game.blockHolder) ;
        Managers.Game.currentShape = temp.GetComponent<TetrisShape>();
        Managers.Input.isActive = true;

        NotificationManager.Instance.FireNotification(EventNotification.ShapeCreation, Managers.Game.currentShape);
    }
}
