using UnityEngine;
using UnityEngine.UI;

public class WorkBar : MonoBehaviour
{
    public static WorkBar Instance;

    private ClientData _currentClient;

    [SerializeField] private ViewResulBar viewResulBar;

    [Header("Timer bar data")]
    private TimerBar _timerBar;
    [SerializeField] private Text _timerBarText;

    [Header("View pay client")]
    [SerializeField] private ShowPayClient showPayClient;

    private int currentEarnedCoin = 0;

    //Debug Mask
    public GameObject[] _objectsMask;

    public void ActiveMaskObjects(bool isAcitve)
    {
        for (int i = 0; i < _objectsMask.Length; i++)
        {
            _objectsMask[i].SetActive(isAcitve);
        }
    }
    //Debug

    private void Awake()
    {
        if (Instance == null) Instance = this;

        _timerBar = new TimerBar(_timerBarText);
    }

    private void Start()
    {
        EventsGame.OnClientCome += ClientCome;
        EventsGame.OnStartGame += StartTimerBar;
        EventsGame.OnTimerBarEnd += EndTimerBar;
        EventsGame.OnStopGame += GameStop;
    }

    private void OnDisable()
    {
        EventsGame.OnClientCome -= ClientCome;
        EventsGame.OnStartGame -= StartTimerBar;
        EventsGame.OnTimerBarEnd -= EndTimerBar;
        EventsGame.OnStopGame -= GameStop;
    }

    private void StartTimerBar()
    {
        currentEarnedCoin = 0;
        _timerBar.SetTimer(91);

        EventsGame.OnTrakingTouchActive?.Invoke(true);
        StartCoroutine(_timerBar.WaitStartTimer());
    }

    private void EndTimerBar()
    {
        StopCoroutine(_timerBar.WaitStartTimer());
        EventsGame.OnStopGame?.Invoke();

        viewResulBar.ShowResult(GetEarnedCoin());

        currentEarnedCoin = 0;
    }

    private void GameStop()
    {
        StopAllCoroutines();
        StopCoroutine(_timerBar.WaitStartTimer());
    }

    public void ClientCome(ClientData client, ClientDataToChanged clientDataToChanged, Sprite sprite)
    {
        _currentClient = client;

        if (clientDataToChanged.stateClient == StateClient.Close)
        {
            clientDataToChanged.stateClient = StateClient.Open;
            MainDataClientsHandler.Instance.SaveDataChangedClients();
        }
    }

    public int CalculatingCost(DrinkFinal drinkFinal)
    {
        int sumSourceDrink = 0;

        for (int i = 0; i < drinkFinal.strengths.Count; i++)
        {
            int price = MainDataClientsHandler.Instance.GetCurrentPriceDrink(drinkFinal.strengths[i]);
            sumSourceDrink += price;
        }

        for (int i = 0; i < drinkFinal.syrupTypes.Count; i++)
        {
            int price = MainDataClientsHandler.Instance.GetCurrentPriceSyrup(drinkFinal.syrupTypes[i]);
            sumSourceDrink += price;
        }

        return sumSourceDrink;
    }

    public void ReceivePayment(int valuePay, int tips)
    {
        currentEarnedCoin += valuePay + tips;
        showPayClient.ShowPay(valuePay, tips);
        BalancePlayer.Instance.AddToBalance(valuePay + tips);
    }

    public int GetEarnedCoin()
    {
        return currentEarnedCoin;
    }
}
