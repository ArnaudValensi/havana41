using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour {

    ShapeMovementController _shapeController=null;
    BonusMalus _bonusMalus;

    public void Touch(GameObject go, Gun.BulletType type= Gun.BulletType.Null, bool inverse=false)
    {
        
        string order = type == Gun.BulletType.Null ? go.tag : System.Enum.GetName(typeof(Gun.BulletType), type);
        _shapeController = _shapeController ?? GetComponentInParent<ShapeMovementController>();
        _bonusMalus = _bonusMalus ?? GetComponentInChildren<BonusMalus>();

        _bonusMalus?.Fire();

        switch (order)
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
                    _shapeController.MoveHorizontal(!inverse ? ShapeMovementController.Direction.Right : ShapeMovementController.Direction.Left);
                }
                else
                {
                    _shapeController.MoveHorizontal(!inverse ? ShapeMovementController.Direction.Left : ShapeMovementController.Direction.Right);
                }
                break;
            default:
                Debug.Log("collision detected but " + go.name);
                break;
        }
    }
}
