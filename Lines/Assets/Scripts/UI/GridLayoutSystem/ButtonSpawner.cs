using UnityEngine;

public class ButtonSpawner : MonoBehaviour, IButtonSpawner
{
    public const int BUTTON_COUNT = 81;

    private IButtonsContainer _buttonContainer;
    private IButtonFactory _buttonFactory;

    private void Awake()
    {
        _buttonContainer = GetComponentInChildren<IButtonsContainer>();
        _buttonFactory = GetComponentInChildren<IButtonFactory>();
    }

    private void Start()
    {
        SpawnButtons();
    }

    public void SpawnButtons()
    {
        for (int i = 0; i < BUTTON_COUNT; i++)
        {
            var newButton = _buttonFactory.CreateButton();
            _buttonContainer.AddButton(newButton);
        }
    }
}
