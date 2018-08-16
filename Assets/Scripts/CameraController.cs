using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	[SerializeField] private GameObject _player;

	[SerializeField] private float _xMargin = 0.5f;
	[SerializeField] private float _yMargin = 5.5f;
	[SerializeField] private float _zMargin = -10;

	private void Update ()
	{
		transform.position = new Vector3(_xMargin, _player.transform.position.y + _yMargin, _zMargin);
	}
}
