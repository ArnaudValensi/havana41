using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour {

	[SerializeField] float duration = 1f;

	LineRenderer laserLine;

	void Start () {
		laserLine = GetComponent<LineRenderer>();
	}
	
	public void Shoot(Color color1, Color color2, Vector3 from, Vector3 to) {
		laserLine.enabled = true;
		laserLine.SetPosition(0, from);
		laserLine.SetPosition(1, to);

		Color2 startColor = new Color2(color1, color2);
		laserLine.DOColor(startColor, new Color2(), 1);
	}

}
