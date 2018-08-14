using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchManager {

	private static readonly IList<ITouchListener> TouchListeners = new List<ITouchListener>();

	public static void SendDirection(Vector3 direction)
	{
		foreach (var listener in TouchListeners)
		{
			listener.PassDirection(direction);
		}
	}
	
	public static void SendTouchEnded(bool isEnded)
	{
		foreach (var listener in TouchListeners)
		{
			listener.PassTouchEnded(isEnded);
		}
	}
	
	public static void AddDirectionListener(ITouchListener listener)
	{
		TouchListeners.Add(listener);
	}
}
