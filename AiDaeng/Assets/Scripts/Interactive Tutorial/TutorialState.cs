using UnityEngine;

public abstract class TutorialState
{
    public abstract void EnterState(InteractiveTutorial tutorial);
    public abstract void UpdateState(InteractiveTutorial tutorial);
    public abstract void ExitState(InteractiveTutorial tutorial);
}
