using Assets.Scripts.GameObjects;
using System.Collections;
using UnityEngine;

public class PieceObject : MonoBehaviour
{
    [SerializeField] public int Team;
    public BoardPlace Place 
    { 
        get { return _place; } 
        set
        {
            _place = value;
            StartCoroutine("Move", _place);
        } 
    }

    private BoardPlace _place;
    private bool _isQueen = false;

    private IEnumerator Move(BoardPlace boardPlace)
    {
        var target = new Vector2(boardPlace.X, boardPlace.Y);
        while ((Vector2)transform.position != target)
        {
            Vector2.MoveTowards(transform.position, target, DefaultSettings.PieceMoveSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}