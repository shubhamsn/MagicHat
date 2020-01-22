using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatController : MonoBehaviour {

	public Camera cam;

	private float maxWidth;
	private bool canControl;

	[SerializeField]
	private bool singleHat = true;

	[SerializeField]
	private bool doubleHatUp = false;
	[SerializeField]
	private bool doubleHatDown = false;

	public Text testText;

	void Awake()
	{
		if (cam == null)
			cam = Camera.main;
	}

	// Use this for initialization
	void Start () {
		Vector2 upperCorner = new Vector2 (Screen.width, Screen.height);
		Vector2 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = 0f;
		if(GetComponent<Renderer>() != null)
			hatWidth = GetComponent<Renderer>().bounds.extents.x;
		else
			hatWidth = 3 * GetComponentInChildren <Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - hatWidth;
		canControl = true;
		Debug.Log ("Position " + transform.position);

	}
	
	/*void FixedUpdate () 
	{
		if (canControl)
		{
			if (Input.touchCount > 0) 
			{
				Touch touch = Input.GetTouch (0);
				if (touch.phase == TouchPhase.Moved) 
				{
					Vector2 rawPosition = cam.ScreenToWorldPoint (touch.position);
					Vector2 targetPosition = new Vector2 (rawPosition.x, transform.position.y);
					float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
					targetPosition = new Vector2 (targetWidth, targetPosition.y);
					//transform.position = Vector2.Lerp (transform.position, targetPosition, 1);
					GetComponent<Rigidbody2D> ().MovePosition (targetPosition);
					//GetComponent<Rigidbody2D> ().velocity  = new Vector2 (targetPosition.x, 0f);
					//transform.position = new Vector2 (rawPosition.x, transform.position.y);
					//transform.position = new Vector2 (targetWidth, targetPosition.y);
				}
			}
		}
	}*/

	
	Vector2 leftFingerMovedBy, leftFingerPos, leftFingerLastPos;
	float slideMagnitudeX, slideMagnitudeY;

	void FixedUpdate () 
	{
		if (canControl)
		{
			if (Input.touchCount > 0) 
			{
				Touch touch = Input.GetTouch (0);
				if (doubleHatUp && cam.ScreenToWorldPoint (touch.position).y > -2f) 
				{
					MoveHat (touch);		
				} 
				else if (doubleHatDown && cam.ScreenToWorldPoint (touch.position).y < -2f ) 
				{
					MoveHat (touch);
				} 
				else if (singleHat) 
				{
					MoveHat(touch);
				}
			}
		}
	}

	private void MoveHat(Touch touch)
	{
		if (Input.touchCount > 0) 
		{
			touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began)
			{
				leftFingerPos = Vector2.zero;
				leftFingerLastPos = Vector2.zero;
				leftFingerMovedBy = Vector2.zero;

				slideMagnitudeX = 0;
				slideMagnitudeY = 0;

				// record start position
				leftFingerPos = touch.position;

			}
			else if (touch.phase == TouchPhase.Moved) 
			{

				leftFingerMovedBy = cam.ScreenToWorldPoint (touch.position) - cam.ScreenToWorldPoint (leftFingerPos); // or Touch.deltaPosition : Vector2
				//Debug.Log("world "+ leftFingerMovedBy + "normal " + (touch.position - leftFingerPos));
				// The position delta since last change.
				leftFingerLastPos = leftFingerPos;
				leftFingerPos = touch.position;

				// slide horz
				slideMagnitudeX = leftFingerMovedBy.x;	 //leftFingerMovedBy.x / Screen.width;

				// slide vert
				slideMagnitudeY = leftFingerMovedBy.y;			//leftFingerMovedBy.y / Screen.height;

				transform.position = new Vector2 
					(
						Mathf.Clamp (transform.position.x + slideMagnitudeX, -maxWidth, maxWidth),
						transform.position.y
					);
				//transform.position = new Vector2 (Mathf.Clamp (transform.position.x, -maxWidth, maxWidth), transform.position.y);
			}
			testText.text = "touch" + cam.ScreenToWorldPoint (touch.position).y;
		}

	}

	public void ToggleControl (bool toggle) {
		canControl = toggle;
	}
}
