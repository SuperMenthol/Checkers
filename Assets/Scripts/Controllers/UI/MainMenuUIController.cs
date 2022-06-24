using Assets.Scripts.Infrastructure;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
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

    void Start()
    {

    }

    public async void OnPlayerNameInputChange(InputField field)
    {

    }

    public async void OnNewPlayerProceed(InputField field)
    {
        var name = field.text;

        var result = await APICommunicator.CreatePlayer(name);

        var playerLabel = field.GetComponentsInChildren<Text>().Where(x => x.gameObject.name == "Lbl").First();
        playerLabel.text = result.Message;

        playerLabel.color = result.Success ? Color.green : Color.red;
    }

    public async void OnLoginProceed(InputField field)
    {
        var name = field.text;

        var result = await APICommunicator.Login(name);

        var playerLabel = field.GetComponentsInChildren<Text>().Where(x => x.gameObject.name == "Lbl").First();
        if (result.Success)
        {
            playerLabel.text = string.Format("{0} logged in successfully (Player ID: {1})", result.Result.PlayerName, result.Result.PlayerId);
            playerLabel.color = Color.green;
            return;
        }

        playerLabel.text = "Player not found";
        playerLabel.color = Color.red;
    }

    public void OnExitClick()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
