using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Lines _lines;

    [SerializeField] private AudioSource _cutSound;
    [SerializeField] private List<Image> _ballSprites;

    private IButtonsContainer _buttonsContainer;

    private void Awake()
    {
        _buttonsContainer = GetComponentInChildren<IButtonsContainer>();

        _buttonsContainer.OnButtonClicked += OnButtonClicked;
    }

    private void Start()
    {
        _lines = new Lines(ShowBox, PlayCut);
        _lines.Start();
    }

    private void OnButtonClicked(int i, int j)
    {
        _lines.Click(i, j);
    }

    private void ShowBox(int i, int j, int ball)
    {
        _buttonsContainer.GetButton(i, j)
            .GetComponent<Image>().sprite = _ballSprites[ball].sprite;
    }

    private void PlayCut()
    {
        _cutSound.Play();
    }

    private void OnDestroy()
    {
        _buttonsContainer.OnButtonClicked -= OnButtonClicked;
    }
}
