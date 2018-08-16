using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour {
	
	private Vector3 _startPosition;
	private Vector3 _direction;
	
	private int _bullets;

	[SerializeField] private Text _bulletsText;

	// Update is called once per frame
	private void Update () 
	{
		_bullets = GameplayManager.GetBullets();
		_bulletsText.text = _bullets.ToString();
		
		/*if (Input.touchCount > 0)
		{
			var touch = Input.GetTouch(0);
			
			if (touch.phase == TouchPhase.Began)
			{
				_startPosition = touch.position;
			}

			if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				_direction = touch.position - _startPosition;
			}

			if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				Debug.Log(_startPosition + " / "+ _direction);
			}
		}*/

		if (Input.GetMouseButtonDown(0) && _bullets >= 1)
		{
			_startPosition = Input.mousePosition;
			TouchManager.SendTouchEnded(false);
		}

		if (Input.GetMouseButton(0) && _bullets >= 1)
		{
			_direction = (Input.mousePosition - _startPosition).normalized;
			TouchManager.SendDirection(_direction);
		}
		
		if(Input.GetMouseButtonUp(0) && _bullets >= 1)
		{
			TouchManager.SendDirection(_direction);
			TouchManager.SendTouchEnded(true);
		}
	}
}
