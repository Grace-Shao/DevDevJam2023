using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    public enum ButtonType {
        Play,
        Credits,
        Exit,
        Back
    } [SerializeField] ButtonType buttonType;
    private Button button;

    void Start() {
        button = GetComponent<Button>();
        switch (buttonType) {
            case ButtonType.Play:
                button.onClick.AddListener(Play);
                break;
            case ButtonType.Credits:
                button.onClick.AddListener(Credits);
                break;
            case ButtonType.Exit:
                button.onClick.AddListener(Exit);
                break;
            case ButtonType.Back:
                button.onClick.AddListener(Back);
                break;
        }
    }

    private void Play() {
        TransitionManager.Instance.LoadScene("Intro");
    }

    public void Credits() {
        TransitionManager.Instance.LoadScene("Credits");
    }

    public void Exit() {
        TransitionManager.Instance.Quit();
    }

    public void Back() {
        TransitionManager.Instance.LoadScene("Title");
    }
}
