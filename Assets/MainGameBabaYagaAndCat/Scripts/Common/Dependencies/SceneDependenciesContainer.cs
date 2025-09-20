using UnityEngine;


public class SceneDependenciesContainer : Dependency
{        
    [SerializeField] private GameStateTracker gameStateTracker;
    [SerializeField] private GameTimeTracker gameTimeTracker;
    [SerializeField] private GameResultTime gameResultTime;
    [SerializeField] private UIGameResultsPanel uIFinishResults;
    [SerializeField] private Movement playerMovement;

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<GameStateTracker>(gameStateTracker, monoBehaviourInScene);
        Bind<GameTimeTracker>(gameTimeTracker, monoBehaviourInScene);
        Bind<GameResultTime>(gameResultTime, monoBehaviourInScene);
        Bind<UIGameResultsPanel>(uIFinishResults, monoBehaviourInScene);
        Bind<Movement>(playerMovement, monoBehaviourInScene);
    }

    private void Awake()
    {
        FindAllObjectToBind();           
    }
}
