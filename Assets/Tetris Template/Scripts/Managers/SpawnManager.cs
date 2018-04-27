using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using System.Linq;

public class SpawnManager : MonoBehaviour {

	public GameObject[] shapeTypes;
    public List<Transform> _startPositionPossibility;

    [SerializeField] Transform _previewPlace;
    int next = 0;

    private void Awake()
    {
        Assert.IsNotNull(_startPositionPossibility);
        Assert.IsTrue(_startPositionPossibility.Count > 0);

        next = Random.Range(0, shapeTypes.Length);
    }

    public void Spawn()
	{
        foreach(Transform children in _previewPlace)
        {
            Destroy(children.gameObject);
        }

		// Random Shape
        int j = Random.Range(0, _startPositionPossibility.Count);

		// Spawn Group at current Position
		GameObject temp = Instantiate(shapeTypes[next], _startPositionPossibility[j].position, Quaternion.identity, Managers.Game.blockHolder) ;
        Managers.Game.currentShape = temp.GetComponent<TetrisShape>();
        Managers.Input.isActive = true;

        next = Random.Range(0, shapeTypes.Length);
        GameObject preview = Instantiate(shapeTypes[next], Vector3.zero, Quaternion.identity, _previewPlace);
        Destroy(preview.GetComponent<TetrisShape>());
        Destroy(preview.GetComponent<ShapeMovementController>());
        preview.GetComponentsInChildren<BlockCollider>().ForEach(i => Destroy(i));
        preview.GetComponentsInChildren<BoxCollider2D>().ForEach(i => Destroy(i));
        preview.GetComponentsInChildren<BonusMalusSlot>().ForEach(i => Destroy(i));
        preview.GetComponentsInChildren<Rigidbody2D>().ForEach(i => Destroy(i));
        preview.transform.localPosition = Vector3.zero;

        NotificationManager.Instance.FireNotification(EventNotification.ShapeCreation, Managers.Game.currentShape);
    }

    //private void LateUpdate()
    //{
    //    foreach(Transform p in _previewPlace)
    //    {
    //        p.position = Vector3.zero;
    //        p.rotation = Quaternion.identity;
    //    }
    //}

    public void Spawn(GameObject toInstantiate, int x, int y)
    {
        // Random Shape
        int i = Random.Range(0, shapeTypes.Length);
        int j = Random.Range(0, _startPositionPossibility.Count);

        // Spawn Group at current Position
        GameObject temp = Instantiate(toInstantiate, new Vector3(x,y,0), Quaternion.identity, Managers.Game.blockHolder);
        Managers.Grid.gameGridcol[x].row[y] = temp.transform;
        temp.GetComponent<TetrisShape>().Fill();

    }
}

