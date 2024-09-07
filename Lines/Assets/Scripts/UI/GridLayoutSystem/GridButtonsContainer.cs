using System;
using UnityEngine;
using UnityEngine.UI;

public class GridButtonsContainer : MonoBehaviour, IButtonsContainer
{
    [SerializeField] private Transform panel;

    public event Action<int, int> OnButtonClicked;

    private Button[,] _buttonsGrid;

    private void Awake()
    {
        _buttonsGrid = new Button[Lines.BTN_SIZE, Lines.BTN_SIZE];
    }

    public void AddButton(Button button)
    {
        button.transform.SetParent(panel, false);

        int buttonIndex = panel.childCount - 1;
        int row = buttonIndex / Lines.BTN_SIZE;
        int col = buttonIndex % Lines.BTN_SIZE;

        _buttonsGrid[row, col] = button;
        button.onClick.AddListener(() => OnClick(row, col));
    }

    public Button GetButton(int i, int j)
    {
        return _buttonsGrid[i, j];
    }

    private void OnClick(int i, int j)
    {
        OnButtonClicked?.Invoke(i, j);
    }
}
