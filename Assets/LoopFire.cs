using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopFire : MonoBehaviour {

    [SerializeField] LoopFire other;

	public void ReFire(Vector3 From, Vector3 direction, System.Action<RaycastHit2D,bool> callback)
    {
        RaycastHit2D result;
        Debug.DrawRay(new Vector3(other.transform.position.x, From.y), direction*1000, Color.green, 2f);
        int layer = 1 << 9;
        if (result = Physics2D.Raycast(new Vector3(other.transform.position.x, From.y), direction, 1000, layer))
        {
            callback(result,true);
        }

    }
}
