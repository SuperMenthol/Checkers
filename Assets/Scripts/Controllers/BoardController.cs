using Assets.Scripts.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] public GameObject PlayerGO;

    [SerializeField] private Sprite _redSprite;
    [SerializeField] private Sprite _greenSprite;
    [SerializeField] private Sprite _shadeSprite;

    private List<KeyValuePair<float, float>> boardCheckDirections = new List<KeyValuePair<float, float>>();

    private List<BoardPlace> _placesList = new List<BoardPlace>();
    private List<GameObject> _shadeSpots = new List<GameObject>();

    void Start()
    {
        InitializeBoard();
        InitializeCheckingCollection();
    }

    public void GetAvailableSpots(PieceObject piece)
    {
        ClearClickableSpots();

        var checkedPlace = piece.Place;

        var result = new List<BoardPlace>();
        var interm = new List<KeyValuePair<BoardPlace, KeyValuePair<float,float>>>();

        foreach (var item in boardCheckDirections)
        {
            var spotToCheck = _placesList.FirstOrDefault(p => 
                p.X.Equals(checkedPlace.X + item.Key) 
                && p.Y.Equals(checkedPlace.Y + item.Value));

            if (spotToCheck != null)
            {
                interm.Add(new KeyValuePair<BoardPlace,KeyValuePair<float,float>>(spotToCheck, item));
            }
        }

        foreach (var spot in interm)
        {
            result.Add(CheckSpot(spot.Key,spot.Value));
        }

        AddClickableSpots(result);
    }

    private BoardPlace CheckSpot(BoardPlace checkedPlace, KeyValuePair<float,float> direction, int index = 0)
    {
        ++index;

        if (checkedPlace.OccupiedBy == null) return checkedPlace;
        if (checkedPlace.OccupiedBy.Team != checkedPlace.OccupiedBy.Team) return index > 1 ? null : CheckSpot(checkedPlace, direction, index);

        return null;
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < DefaultSettings.Rows; i++)
        {
            for (int j = 0; j < DefaultSettings.Rows; j++)
            {
                if ((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))
                {
                    var newPlace = new BoardPlace(i * DefaultSettings.PiecesDistance, j * DefaultSettings.PiecesDistance);
                    _placesList.Add(newPlace);

                    if (j < DefaultSettings.Rows * 0.5f - 1)
                    {
                        PlacePiece(newPlace, 2);
                    }

                    if (j > DefaultSettings.Rows * 0.5f)
                    {
                        PlacePiece(newPlace, 1);
                    }
                }
            }
        }
    }

    private void PlacePiece(BoardPlace place, int team)
    {
        var newPlayer = Instantiate(PlayerGO);
        var pieceScript = newPlayer.GetComponent<PieceObject>();
        newPlayer.transform.position = new Vector2(place.X, place.Y);

        pieceScript.Place = place;
        pieceScript.Team = team;

        place.Place(pieceScript);

        newPlayer.GetComponent<SpriteRenderer>().sprite = team == 1 ? _redSprite : _greenSprite;
    }

    private void InitializeCheckingCollection()
    {
        boardCheckDirections.Add(new KeyValuePair<float, float>(-DefaultSettings.PiecesDistance, DefaultSettings.PiecesDistance));
        boardCheckDirections.Add(new KeyValuePair<float, float>(DefaultSettings.PiecesDistance, DefaultSettings.PiecesDistance));
        boardCheckDirections.Add(new KeyValuePair<float, float>(-DefaultSettings.PiecesDistance, -DefaultSettings.PiecesDistance));
        boardCheckDirections.Add(new KeyValuePair<float, float>(DefaultSettings.PiecesDistance, -DefaultSettings.PiecesDistance));
    }

    private void AddClickableSpots(List<BoardPlace> places)
    {
        foreach (var spot in places)
        {
            var newShadeObject = new GameObject();
            var spriteRenderer = newShadeObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _shadeSprite;
            newShadeObject.transform.position = new Vector2(spot.X, spot.Y);

            _shadeSpots.Add(newShadeObject);
        }
    }

    private void ClearClickableSpots()
    {
        for (int i = 0; i < _shadeSpots.Count; i++)
        {
            GameObject.Destroy(_shadeSpots[i].gameObject);
        }
    }
}