using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool GameRunning
    {
        get { return _gameRunning; }
        set 
        { 
            if (value != _gameRunning) _gameRunning = value; 
        }
    }

    [SerializeField] private BoardController _boardController;

    private bool _gameRunning = false;
    private int _currentTeam = 1;

    void Update()
    {
        if (_gameRunning)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (raycast && raycast.collider.GetComponent<PieceObject>()?.Team == _currentTeam)
                {
                    _boardController.GetAvailableSpots(raycast.collider.GetComponent<PieceObject>());
                }
            }
        }
    }
}