using System;
using UnityEngine.UI;

public interface IButtonsContainer
{
    public event Action<int, int> OnButtonClicked;
    public void AddButton(Button button);
    public Button GetButton(int i, int j);
}
