using UnityEngine;

public class LoopFire : MonoBehaviour {

    [SerializeField] LoopFire other;
	Vector3 defualtOffset = new Vector3(27f, 0f, 0f);

	public void ReFire(Vector3 From, Vector3 direction, Laser laser, Color color1, Color color2, System.Action<RaycastHit2D, bool> callback)
    {
        RaycastHit2D result;
		Vector3 fromPos = new Vector3(other.transform.position.x, From.y);
		Vector3 offset = defualtOffset;

		Debug.DrawRay(fromPos, direction*1000, Color.green, 2f);

        int layer = 1 << 9; // Block
		if (result = Physics2D.Raycast(fromPos, direction, 1000, layer))
        {
			offset = new Vector3(result.distance, 0f, 0f);
            callback(result, true);
        }

		//Debug.Log("Refire: " + transform.right);

		if (direction == Vector3.left) {
			laser.Shoot(color1, color2, fromPos, fromPos - offset +  Vector3.left);
		} else {
			laser.Shoot(color1, color2, fromPos, fromPos + offset +  Vector3.right);
		}
    }
}
