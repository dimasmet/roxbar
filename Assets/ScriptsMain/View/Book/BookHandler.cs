using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHandler : MonoBehaviour
{
    public static BookHandler Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    [SerializeField] private Transform _parentSpawn;
    [SerializeField] private ClientBookButton _prefabButton;

    private List<ClientBookButton> _listClientButtons;

    private ClientBookButton _currentChoiceButton;

    [Header("View client")]
    [SerializeField] private ViewBook _viewBook;

    public void InitBookClients(List<ClientData> clientDatas, List<ClientDataToChanged> clientDataToChangeds, List<Sprite> imagesSmallClient, List<Sprite> imagesBigClient)
    {
        _listClientButtons = new List<ClientBookButton>();

        for (int i = 0; i < clientDatas.Count; i++)
        {
            ClientBookButton clientBook = Instantiate(_prefabButton, _parentSpawn);
            clientBook.InitClient(clientDatas[i], clientDataToChangeds[i], imagesSmallClient[i], imagesBigClient[i]);

            _listClientButtons.Add(clientBook);
        }

        ChoiceClient(_listClientButtons[0]);
    }

    public void ChoiceClient(ClientBookButton client)
    {
        SoundsGame.Instance.PlayShotSound(SoundName.Click);
        if (client != _currentChoiceButton)
        {
            if (_currentChoiceButton != null)
                _currentChoiceButton.StateChoiceClient(false);

            _currentChoiceButton = client;
            _currentChoiceButton.StateChoiceClient(true);

            _viewBook.ShowCharacter(_currentChoiceButton.GetImageBig());
        }
    }

    public void UpdateView()
    {
        for (int i = 0; i < _listClientButtons.Count; i++)
        {
            _listClientButtons[i].UpdateViewActualData();
        }
    }
}
