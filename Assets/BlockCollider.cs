using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour {

    ShapeMovementController _shapeController=null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
                    break;
                default:
                    Debug.Log("collision detected but " + collision.gameObject.name);
                    break;
            }




        }



    }


}
