using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchListener
{
    void PassDirection(Vector3 direction);

    void PassTouchEnded(bool isEnded);
}
