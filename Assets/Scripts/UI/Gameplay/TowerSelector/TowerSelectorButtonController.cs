using Assets.Scripts.Gameplay.States;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSelectorButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isTowerSelected;
    private bool _isEnabled;
    private GameplayManager _gameplayManagerInstance;
    private GameObject _instantiatedGhost;

    public GameObject SelectableTower;
    public GameObject SelectableTowerGhost;
    public Button SelectTowerButton;

    public void Start()
    {
        if (GameplayManager.InstanceExists)
        {
            _gameplayManagerInstance = GameplayManager.Instance;
            _gameplayManagerInstance.GameplayStateChanged += OnStateChanged;
            SetComponentsEnabled(_gameplayManagerInstance.CurrentState == GameplayState.Building);
        }
    }

    public void Update()
    {
        if (_isTowerSelected && _instantiatedGhost != null)
        {
            var inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            var ghostPosition = inputRay.GetPoint(7);
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
        if (_isEnabled)
        {
            DeselectTower();
        }
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
}

