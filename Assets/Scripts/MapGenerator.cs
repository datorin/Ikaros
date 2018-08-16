using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

	[SerializeField] private GameObject _baseSquare;

	private const float XMargin = -3.5f;
	private const float YMargin = 4.5f;

	private const int FencesNumber = 20;

	private readonly List<int[]> _fenceList = PrebuildedFences.FenceList;

	private void Start () {
		for (var i = 0; i < FencesNumber; i++)
		{
			var pointer = Random.Range(0, _fenceList.Count);
			
			Debug.Log(pointer);

			var fence = _fenceList[pointer];

			for(var j = 0; j < fence.Length; j++)
			{
				var block = CalculateGameObjectFromValue(fence[j]);

				if (block != null)
				{
					Instantiate(block, new Vector3(j + XMargin, i * GameplayManager.DistanceBetweeCheckPoints + YMargin, 0),
						Quaternion.identity);
				}
			}
		}
	}

	private GameObject CalculateGameObjectFromValue(int value)
	{
		return value == 1 ? _baseSquare : null;
	}
}
