﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

	[SerializeField] private GameObject _baseSquare;
	[SerializeField] private GameObject _wallSquare;

	private const float XMargin = -3.5f;
	private const float YMargin = 4.5f;

	private const float XLeftWall = -4.5f;
	private const float XRightWall = 5.5f;
	private const float YWall = -8.5f;

	private readonly float _firstFencePositionY = GameplayManager.FirstCheckPoint - 0.5f;
	private readonly float _distance = GameplayManager.DistanceBetweeCheckPoints;

	private const int FencesNumber = 20;

	private readonly List<int[]> _fenceList = PrebuildedFences.FenceList;

	private void Start () {
		
		for (var x = 1; x < 10; x++)
		{
			var baseSquare = Instantiate(_baseSquare, new Vector3(x * 1 + XLeftWall, YWall, 0), Quaternion.identity);
			baseSquare.transform.parent = transform;
		}

		for (var y = 0; y < Mathf.Abs(_firstFencePositionY - YWall); y++)
		{
			var wallInstantiate1 = Instantiate(_wallSquare, new Vector3(XLeftWall, y * 1 + YWall, 0), Quaternion.identity);
			wallInstantiate1.transform.parent = transform;
			var wallInstantiate2 = Instantiate(_wallSquare, new Vector3(XRightWall, y * 1 + YWall, 0), Quaternion.identity);
			wallInstantiate2.transform.parent = transform;
		}
		
		for (var i = 0; i < FencesNumber; i++)
		{
			var pointer = Random.Range(0, _fenceList.Count);

			var fence = _fenceList[pointer];

			for(var j = 0; j < fence.Length; j++)
			{
				var block = CalculateGameObjectFromValue(fence[j]);

				if (block == null) continue;
				var blockInstantiate = Instantiate(block, new Vector3(j + XMargin, i * _distance + YMargin, 0), Quaternion.identity);
				blockInstantiate.transform.parent = transform;
			}

			for (var t = 0; t < _distance; t++)
			{
				var wallInstantiate1 = Instantiate(_wallSquare, new Vector3(XLeftWall, t * 1 + i * _distance + _firstFencePositionY, 0), Quaternion.identity);
				wallInstantiate1.transform.parent = transform;
				var wallInstantiate2 = Instantiate(_wallSquare, new Vector3(XRightWall, t * 1 + i * _distance + _firstFencePositionY, 0), Quaternion.identity);
				wallInstantiate2.transform.parent = transform;
			}
		}
	}

	private GameObject CalculateGameObjectFromValue(int value)
	{
		return value == 1 ? _baseSquare : null;
	}
}
