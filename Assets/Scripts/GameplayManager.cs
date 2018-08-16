using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayManager
{

	public static float FirstCheckPoint = 5;
	public static float DistanceBetweeCheckPoints = 10;
	
	private static List<float> _passedCheckPoints = new List<float>();

	private static int _bullets = 3;

	public static void AddPassedCheckPoints(float checkPoint)
	{
		_passedCheckPoints.Add(checkPoint);
	}

	public static List<float> GetPassedCheckPoints()
	{
		return _passedCheckPoints;
	}

	public static int GetBullets()
	{
		return _bullets;
	}

	public static void SumBullet()
	{
		if (_bullets > 2) return;
		_bullets++;

	}

	public static void RestBullet()
	{
		if (_bullets < 1) return;
		_bullets--;
	}
}
