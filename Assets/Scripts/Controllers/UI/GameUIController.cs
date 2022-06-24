using Assets.Scripts.Infrastructure;
using CheckerScoreAPI.Model;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class GameUIController : MonoBehaviour
    {
        public APICommunication APICommunicator
        {
            get 
            { 
                if (_apiCommunicator == null)
                {
                    _apiCommunicator = new APICommunication();
                }
                return _apiCommunicator;
            }
        }

        private APICommunication _apiCommunicator;
        [SerializeField] private InputField _playerNameField;
        [SerializeField] private Text _playerNameLabel;

        public async void OnPlayerNameEnter()
        {
            var name = _playerNameField.text;

            var result = await APICommunicator.CreatePlayer(name);

            if (!result.Success)
            {
                _playerNameLabel.text = result.Message;
                _playerNameLabel.color = new Color(0, 0, 0, 255);
            }

            _playerNameLabel.color = Color.red;
        }

        public void OnPlayerNameValueChanged()
        {
            _playerNameLabel.color = new Color(0, 0, 0, 0);
        }

        public async void OnMatchResultSend()
        {
            var matchResult = new MatchResult()
            {
                Player1Id = 1,
                Player2Id = 2,
                WinnerID = 1,
                MatchTime = DateTime.Now
            };

            var result = await APICommunicator.PostMatchResult(matchResult);

            _playerNameLabel.text = result.Message;
            _playerNameLabel.color = result.Success ? Color.green : Color.red;
        }
    }
}
