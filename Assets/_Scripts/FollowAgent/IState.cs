using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnterState(FollowAgent agent);
    void OnExitState(FollowAgent agent);
    void Update(FollowAgent agent);
}