using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour {

    ShapeMovementController _shapeController=null;

    public void Touch(GameObject go)
    {
        _shapeController = _shapeController ?? GetComponentInParent<ShapeMovementController>();

        switch (go.tag)
        {
            case "TurnRight":
                _shapeController.RotateClockWise(true);
                break;
            case "TurnLeft":
                _shapeController.RotateClockWise(false);
                break;
            case "Fall":
                _shapeController.InstantFall();
                break;
            case "Move":
                if (_shapeController.transform.position.x > go.transform.position.x)
                {
                    _shapeController.MoveHorizontal(ShapeMovementController.Direction.Right);
                }
                else
                {
                    _shapeController.MoveHorizontal(ShapeMovementController.Direction.Left);
                }
                break;
            default:
                Debug.Log("collision detected but " + go.name);
                break;
        }
    }
}
