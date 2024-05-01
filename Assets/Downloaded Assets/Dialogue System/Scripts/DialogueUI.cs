using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

namespace HeneGames.DialogueSystem
{
    public class DialogueUI : MonoBehaviour
    {
        public static DialogueUI instance { get; private set; }

        private DialogueManager currentDialogueManager;

        [SerializeField] private Image portrait;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private GameObject dialogueWindow;
        

        [SerializeField] private bool animateText = true;
        [Range(0.1f, 1f)]
        [SerializeField] private float textAnimationSpeed = 0.5f;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            dialogueWindow.SetActive(false);
            

            // Start dialogue automatically when the UI is ready
            StartAutomaticDialogue();
        }

        private void StartAutomaticDialogue()
        {
            if (currentDialogueManager != null)
            {
                currentDialogueManager.StartDialogue();
            }
        }

        public void StartDialogue(DialogueManager _dialogueManager)
        {
            currentDialogueManager = _dialogueManager;
        }

        public void ShowSentence(DialogueCharacter _dialogueCharacter, string _message)
        {
            try
            {
                StopAllCoroutines();

            dialogueWindow.SetActive(true);

            portrait.sprite = _dialogueCharacter.characterPhoto;
            nameText.text = _dialogueCharacter.characterName;

            if (animateText)
            {
               
                    StartCoroutine(WriteTextToTextmesh(_message, messageText));
               
            }
            else
            {
                messageText.text = _message;
            }
            }
            catch (Exception) { }
        }

        public void ClearText()
        {
            dialogueWindow.SetActive(false);
        }

       

        private IEnumerator WriteTextToTextmesh(string _text, TextMeshProUGUI _textMeshObject)
        {

            _textMeshObject.text = "";
            char[] _letters = _text.ToCharArray();

            float _speed = 1f - textAnimationSpeed;


            //foreach (char _letter in _letters)
            //{
            //    _textMeshObject.text += _letter;
            //    yield return new WaitForSeconds(0.1f * _speed);
            //}
            StringBuilder stringBuilder = new StringBuilder();
            bool inTag = false;
            int tagStartIndex = -1;

            for (int i = 0; i < _letters.Length; i++)
            {
                if (_letters[i] == '<')
                {
                    inTag = true;
                    tagStartIndex = i;
                }
                else if (_letters[i] == '>')
                {
                    inTag = false;
                    if (tagStartIndex >= 0)
                    {
                        stringBuilder.Append(_text.Substring(tagStartIndex, i - tagStartIndex + 1));
                        tagStartIndex = -1;
                    }
                    continue;
                }

                if (!inTag)
                {
                    stringBuilder.Append(_letters[i]);
                    _textMeshObject.text = stringBuilder.ToString();
                    yield return new WaitForSeconds(0.1f * _speed);
                }
            }
        }
    }
}