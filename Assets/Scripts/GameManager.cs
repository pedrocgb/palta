using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NoHope.NPCs;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private GameObject _clientBaloon = null;
    [SerializeField]
    private GameObject _personaBaloon = null;
    [SerializeField]
    private TextMeshProUGUI _clientDialogueTextMesh = null;
    [SerializeField]
    private TextMeshProUGUI _personaDialogueTextMesh = null;
    [SerializeField]
    private Animator _questionAnimator = null;
    [SerializeField]
    private GameObject _fadePanel = null;
    [SerializeField]
    private Button _closeButton = null;

    [Space(20)]
    [Header("Game Start")]
    [SerializeField]
    private float _startGameDelay = 2.5f;
    private WaitForSeconds _startGameTimer = new WaitForSeconds(2.5f);

    [Space(20)]
    [Header("Dialogue")]
    [SerializeField]
    [TextArea(5, 5)]
    private string[] _clientTexts = null;
    [SerializeField]
    [TextArea(5, 5)]
    private string[] _personaTexts = null;

    [Space(20)]
    [Header("Questions and Answers")]
    [SerializeField]
    private TextMeshProUGUI _finalTextMesh = null;
    [SerializeField]
    [TextArea(5, 5)]
    private string _wrongAnswerText = string.Empty;
    [SerializeField]
    [TextArea(15, 15)]
    private string _correctAnswerText = string.Empty;
    [SerializeField]
    private Button[] _answerButtons = null;

    private void Start()
    {
        _startGameTimer = new WaitForSeconds(_startGameDelay);

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return _startGameTimer;
        _personaBaloon.SetActive(true);
        Dialogue(_personaTexts[0], _personaDialogueTextMesh);

        yield return new WaitForSeconds(1f);
        _clientBaloon.SetActive(true);
        Dialogue(_clientTexts[0], _clientDialogueTextMesh);

        yield return new WaitForSeconds(2f);
        _questionAnimator.Play("Show Question", 0, 0f);

    }

    private void Dialogue(string DialogueText, TextMeshProUGUI BaloonTextMesh)
    {
        StartCoroutine(TypeWriter.StartTyping(DialogueText, BaloonTextMesh, 0.005f));
    }

    public void WrongButton()
    {
        foreach (Button b in _answerButtons)
        {
            b.interactable = false;
        }

        StartCoroutine(StartWrongAnswer());
    }
    private IEnumerator StartWrongAnswer()
    {
        _fadePanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        StartCoroutine(TypeWriter.StartTyping(_wrongAnswerText, _finalTextMesh, 0.008f));

        yield return new WaitForSeconds(2f);
        _closeButton.interactable = true;
    }

    public void CorrectButton()
    {
        foreach (Button b in _answerButtons)
        {
            b.interactable = false;
        }

        StartCoroutine(StartCorrectAnswer());
    }

    public IEnumerator StartCorrectAnswer()
    {
        _personaDialogueTextMesh.text = string.Empty;
        _clientDialogueTextMesh.text = string.Empty;
        Dialogue(_personaTexts[1], _personaDialogueTextMesh);

        yield return new WaitForSeconds(2f);

        Dialogue(_clientTexts[1], _clientDialogueTextMesh);

        yield return new WaitForSeconds(5f);

        _fadePanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        StartCoroutine(TypeWriter.StartTyping(_correctAnswerText, _finalTextMesh, 0.005f));

        yield return new WaitForSeconds(7f);
        _closeButton.interactable = true;
    }

    public void CloseButton()
    {
        SceneManager.LoadScene(0);
    }
}
