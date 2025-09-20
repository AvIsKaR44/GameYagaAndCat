using UnityEngine;


public class GameKeyboardStarter : MonoBehaviour, IDependency<GameStateTracker>
{
    private GameStateTracker gameStateTracker;
    public void Construct(GameStateTracker obj) => gameStateTracker = obj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            gameStateTracker.LaunchPreparationStart();
        }
    }
}
