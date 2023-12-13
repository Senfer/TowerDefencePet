using Assets.Scripts.Gameplay.States;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSelectorButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isTowerSelected;
    private bool _isEnabled;
    private bool _isInsufficientFunds;
    private GameplayManager _gameplayManagerInstance;
    private PlayerWalletManager _playerWalletManager;
    private GameObject _instantiatedGhost;
    private int _towerCost;

    public GameObject SelectableTower;
    public GameObject SelectableTowerGhost;
    public Button SelectTowerButton;
    public Color InsufficientFundsColor;
    public string TowerTitleFormatWithCost;

    public void Start()
    {
        if (GameplayManager.InstanceExists)
        {
            _gameplayManagerInstance = GameplayManager.Instance;
            _gameplayManagerInstance.GameplayStateChanged += OnStateChanged;
            SetComponentsEnabled(_gameplayManagerInstance.CurrentState == GameplayState.Building);
            _playerWalletManager = _gameplayManagerInstance.WalletManager;
            _playerWalletManager.CurrencyAmountChanged += OnCurrencyAmountChanged;
        }

        _towerCost = SelectableTower.GetComponent<Launcher>().towerData.cost;
        SelectTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = string.Format(TowerTitleFormatWithCost, _towerCost);
    }

    private void OnDestroy()
    {
        _gameplayManagerInstance.GameplayStateChanged -= OnStateChanged;
        _playerWalletManager.CurrencyAmountChanged -= OnCurrencyAmountChanged;
    }

    public void Update()
    {
        if (_isTowerSelected && _instantiatedGhost != null)
        {
            var camera = Camera.main;
            var inputRay = camera.ScreenPointToRay(Input.mousePosition);
            var ghostPosition = inputRay.GetPoint(-camera.transform.position.z);
            _instantiatedGhost.transform.position = ghostPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isTowerSelected && _isEnabled)
        { 
            SelectTower(); 
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        DeselectTower();
    }

    private void SelectTower()
    {
        _isTowerSelected = true;
        GameplayManager.Instance.SelectTowerToBuild(SelectableTower);
        _instantiatedGhost = Instantiate(SelectableTowerGhost);
    }

    private void DeselectTower()
    {
        _isTowerSelected = false;
        GameplayManager.Instance.DeselectTower();
        Destroy(_instantiatedGhost);
        _instantiatedGhost = null;
    }

    private void OnStateChanged(GameplayState previousState, GameplayState currentState)
    {
        if (currentState == GameplayState.Building)
        {
            SetComponentsEnabled(true);
        }

        if (previousState == GameplayState.Building)
        {
            SetComponentsEnabled(false);
        }
    }

    private void SetComponentsEnabled(bool isEnabled)
    {
        _isEnabled = isEnabled;
        SelectTowerButton.interactable = isEnabled;
    }

    private void OnCurrencyAmountChanged(int previousAmount, int currentAmount)
    {
        var buttonImage = SelectTowerButton.GetComponent<Image>();
        if (_towerCost > currentAmount)
        {
            _isInsufficientFunds = true;
            SetComponentsEnabled(false);
            buttonImage.color = InsufficientFundsColor;
        }

        if (_towerCost < currentAmount)
        {
            _isInsufficientFunds = false;
            buttonImage.color = SelectTowerButton.colors.normalColor;
            if (GameplayManager.Instance.CurrentState == GameplayState.Building)
            {
                SetComponentsEnabled(true);
            }
        }
    }
}

