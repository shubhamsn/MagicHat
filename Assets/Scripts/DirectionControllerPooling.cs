using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionControllerPooling : MonoBehaviour {

	[SerializeField]
	private GameObject directionController;

	private Camera cam;
	private float maxWidthX;
	private float maxWidthY;

	[SerializeField]
	private bool selfDestroy = false;

	[SerializeField]
	private int IntialWaitTime = 3;

	[SerializeField]
	private int StartWaitTime = 5;

	[SerializeField]
	private int EndWaitTime = 10;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float directionControllerWidth = directionController.GetComponent<Renderer>().bounds.extents.x;
		maxWidthX = targetWidth.x - directionControllerWidth;
		maxWidthY = targetWidth.y - directionControllerWidth;
		StartCoroutine (Spawn ());
	}

	public IEnumerator Spawn () 
	{
		yield return new WaitForSeconds (IntialWaitTime);
		GameObject instaceDirectionController;
		//counting = true;
		while (!GameController.instance.gameOver) {
			Vector3 spawnPosition = new Vector3 (
				transform.position.x + Random.Range (-maxWidthX, maxWidthX), 
				Random.Range (-maxWidthY, maxWidthY),
				0.0f
			);
			Quaternion spawnRotation = Quaternion.identity;
			instaceDirectionController = Instantiate (directionController, spawnPosition, spawnRotation) as GameObject;
			if(selfDestroy)
				instaceDirectionController.GetComponent<DirectionController> ().selfDestroy = selfDestroy;
			yield return new WaitForSeconds (Random.Range (StartWaitTime, EndWaitTime));
		}
		yield return new WaitForSeconds (2.0f);
		//gameOverText.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		//restartButton.SetActive (true);
	}
	
}
