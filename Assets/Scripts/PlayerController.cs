﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour, ITouchListener
{
	[SerializeField] private Sprite _circle;
	[SerializeField] private Sprite _triangle;

	private SpriteRenderer _sprite;
	
	private const float Speed = 20;
	private const float HalfWidth = 0.25f;
	private const float HalfLength = 0.25f;

	private Vector3 _rightPosition;
	private Vector3 _leftPosition;
	
	private Vector3 _direction;
	private bool _isTouchEnded;
	private bool _isShooted = false;
	
	private bool _isColliding;
	
	private void Start()
	{
		TouchManager.AddDirectionListener(this);

		_sprite = GetComponent<SpriteRenderer>();
	}


	private void FixedUpdate ()
	{
		
		if (!(_direction.magnitude > 0)) return;

		transform.up = _direction;
			
		_sprite.sprite = _triangle;
		
		if(!_isTouchEnded) return;
		
		var frameDistance = Speed * Time.fixedDeltaTime;
		
		transform.Translate(Vector3.up * frameDistance);
		
		CheckCollision(frameDistance);

		CheckCheckPoints();

		if (!_isShooted) return;
		GameplayManager.RestBullet();
		_isShooted = false;
	}

	private void CheckCollision(float distance)
	{
		_isColliding = false;

		//distance = distance + HalfLength;
		
		_rightPosition = transform.TransformPoint(Vector3.right * HalfWidth);
		_leftPosition = transform.TransformPoint(Vector3.left * HalfWidth);
		
		var righthit = Physics2D.Raycast(_rightPosition, _direction, distance);
		var lefthit = Physics2D.Raycast(_leftPosition, _direction, distance);
		
		if (righthit.collider != null && !_isColliding)
		{
			Bounce(righthit.point, _direction, righthit.normal);
			_isColliding = true;
			_sprite.sprite = _circle;
		}

		if (lefthit.collider != null && !_isColliding)
		{
			Bounce(lefthit.point, _direction, lefthit.normal);
			_isColliding = true;
			_sprite.sprite = _circle;
		}
	}
	
	private void CheckCheckPoints()
	{
		var checkPoints = GameplayManager.GetPassedCheckPoints();
		var firstCheckPoint = GameplayManager.FirstCheckPoint;

		if (checkPoints.Count == 0)
		{
			if (!(transform.position.y >= firstCheckPoint)) return;
			GameplayManager.AddPassedCheckPoints(firstCheckPoint);
			Debug.Log("1");
			GameplayManager.SumBullet();
		}
		else
		{
			var nextCheckPoint = firstCheckPoint + GameplayManager.DistanceBetweeCheckPoints * checkPoints.Count;

			if (transform.position.y <= checkPoints.Last() - 1)
			{
				Destroy(gameObject);
			}

			if (!(transform.position.y >= nextCheckPoint)) return;
			GameplayManager.AddPassedCheckPoints(nextCheckPoint);
			Debug.Log(GameplayManager.GetPassedCheckPoints().Count);
			GameplayManager.SumBullet();
		}
	}
	
	private void Bounce(Vector3 startPosition, Vector3 direction, Vector3 normal)
	{
		var newDirection = Vector3.Reflect(direction, normal.normalized);
		
		_direction = newDirection;

		transform.up = _direction;
	}

	public void PassDirection(Vector3 direction)
	{
		_direction = -direction;
	}

	public void PassTouchEnded(bool isEnded)
	{
		_isTouchEnded = isEnded;

		if (_isTouchEnded == true)
		{
			_isShooted = true;
		}
	}
}
