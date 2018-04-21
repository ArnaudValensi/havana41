using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour {

    ShapeMovementController _shapeController=null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("test");
        if (_shapeController == null) _shapeController = GetComponentInParent<ShapeMovementController>();

        // if is actif
        if(_shapeController.isFalling)
        {
            switch(collision.gameObject.tag)
            {
                case "TurnRight":
                    _shapeController.RotateClockWise(true);
                    break;
                case "TurnLeft":
                    _shapeController.RotateClockWise(false);
                    break;
                case "Fall":
                    _shapeController.FreeFall();
                    break;
                case "Move":
                    if(_shapeController.transform.position.x < _shapeController.transform.position.x)
                    {
                        _shapeController.MoveHorizontal(ShapeMovementController.Direction.Right);
                    }
                    else
                    {
                        _shapeController.MoveHorizontal(ShapeMovementController.Direction.Left);
                    }
                    break;
                default:
                    Debug.Log("collision detected but " + collision.gameObject.name);
                    break;
            }




        }



    }


}
