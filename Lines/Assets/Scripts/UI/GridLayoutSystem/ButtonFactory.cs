using UnityEngine;
using UnityEngine.UI;

public class ButtonFactory : MonoBehaviour, IButtonFactory
{
    [SerializeField] private GameObject _buttonPrafab;

    public Button CreateButton()
    {
        GameObject buttonObj = Instantiate(_buttonPrafab);

        return buttonObj.GetComponent<Button>();
    }
}
