using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ShapeMovementController : MonoBehaviour {

    #region InternalType
    public enum Direction
    {
        Left,
        Right
    }
    #endregion

    public Transform rotationPivot;

    // PlaySFX or Sound
    [SerializeField] public UnityEvent _onStopFalling; 

    public bool isFastTransition = false;
    public float fastTransitionInterval ;
    private float lastFall = 0;

    public float transitionInterval
        => isFastTransition ? HavanaManager.Instance.FastTransitionInterval : HavanaManager.Instance.GlobalTransitionInterval;
    
    /// <summary>
    /// Define is current falling shape
    /// </summary>
    internal bool isFalling { get; private set; }

    public void ManageStopFalling()
    {
        //Debug.Log($"{name} stops falling");
        foreach(var el in GetComponentsInChildren<Collider2D>())
        {
            el.enabled = false;
        }

        isFalling = false;

        _onStopFalling.Invoke();
    }

    public void ShapeUpdate()
    {
        FreeFall();
    }

    public void RotateClockWise(bool isCw)
    {
        float rotationDegree = (isCw) ? 90.0f : -90.0f;

        transform.RotateAround(rotationPivot.position, Vector3.forward, rotationDegree);

        // Check if it's valid          
        if (Managers.Grid.IsValidGridPosition(this.transform)) // It's valid. Update grid.
        {
            Managers.Grid.UpdateGrid(this.transform);
        }
        else // It's not valid. revert rotation operation.
        {
            transform.RotateAround(rotationPivot.position, Vector3.forward, -rotationDegree);
        }
    }

    public void MoveHorizontal(Direction direction) => MoveHorizontal(direction == Direction.Left ? Vector3.left : Vector3.right);
    public void MoveHorizontal(Vector2 direction)
    {
        float deltaMovement = (direction.Equals(Vector2.right)) ? 1.0f : -1.0f;

        // Modify position
        transform.position += new Vector3(deltaMovement, 0, 0);

        // Check if it's valid
        if (Managers.Grid.IsValidGridPosition(this.transform))// It's valid. Update grid.
        {
            Managers.Grid.UpdateGrid(this.transform);
        }
        else // It's not valid. revert movement operation.
        {
            transform.position += new Vector3(-deltaMovement, 0, 0);
        }
    }

    public void FreeFall()
    {
        if (Time.time - lastFall >= transitionInterval)
        {

            if(isFastTransition)
            {
                ScoreBanner.Instance.InstantFallReward();
            }
            // Modify position
            transform.position += Vector3.down;

            Managers.Audio.PlayDropSound();

            // See if valid
            if (Managers.Grid.IsValidGridPosition(this.transform))
            {
                // It's valid. Update grid.
                Managers.Grid.UpdateGrid(this.transform);
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                GetComponent<ShapeMovementController>().enabled = false;
                GetComponent<TetrisShape>().enabled = false;
                Managers.Game.currentShape = null;

                // Clear filled horizontal lines
                Managers.Grid.PlaceShape(this);
            }

            lastFall = Time.time;
        }
    }
    
    public void InstantFall()
    {
        isFastTransition = true;
    }


}
