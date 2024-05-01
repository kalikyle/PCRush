using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeneGames.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public int currentSentence;
        private float coolDownTimer;
        public bool dialogueIsOn;

        [Header("References")]
        [SerializeField] private AudioSource audioSource;

        [Header("Events")]
        public UnityEvent startDialogueEvent;
        public UnityEvent nextSentenceDialogueEvent;
        public UnityEvent endDialogueEvent;

        [Header("Dialogue")]
        [SerializeField] public List<NPC_Centence> sentences = new List<NPC_Centence>();



        private void Start()
        {
            // Start the dialogue automatically at the beginning of the game
            StartDialogue();
        }

         private bool mouseButtonDown;

        private void Update()
        {
            if (dialogueIsOn)
            {
                if (coolDownTimer > 0f)
                {
                    coolDownTimer -= Time.deltaTime;

                    if (Input.GetMouseButtonDown(0))
                    {
                        mouseButtonDown = true;
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        mouseButtonDown = false;
                    }

                    if (mouseButtonDown)
                    {
                        StopAllCoroutines(); // Skip text animation
                        coolDownTimer -= 0.2f; // Reduce cooldown time by 0.2f
                    }

                    if (coolDownTimer <= 0f)
                    {
                        NextSentence(out bool lastSentence);
                        if (lastSentence)
                        {
                            Debug.Log("Dialogue reached the end.");
                        }
                        else
                        {
                            coolDownTimer = 5f; // Reset cooldown for the next sentence
                        }
                    }
                }
                else // If cooldown is over
                {
                    // Reset the animation and cooldown for the next sentence
                    coolDownTimer = 5f;
                    StopAllCoroutines();
                    NextSentence(out bool lastSentence);
                }
            }
        }

        //private void Update()
        //{
        //    // Automatically progress to the next sentence after a certain delay
        //    if (dialogueIsOn && coolDownTimer <= 0f)
        //    {
        //       NextSentence(out bool lastSentence);
        //        if (lastSentence)
        //        {
        //            // Dialogue reached the end, you can perform actions or start a new dialogue here
        //           Debug.Log("Dialogue reached the end.");
        //       }
        //    }
        //    else
        //   {
        //       coolDownTimer -= Time.deltaTime;


        //    }

        //    //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        //    //{
        //    //   // Ensure there is an ongoing dialogue
        //    //   if (dialogueIsOn)
        //    //   {
        //    //       NextSentence(out bool lastSentence);
        //    //      if (lastSentence)
        //    //      {
        //    //           // Dialogue reached the end, you can perform actions or start a new dialogue here
        //    //           Debug.Log("Dialogue reached the end.");
        //    //       }
        //    //   }
        //    //}
        //}

        public void StartDialogue()
        {
            // Reset sentence index
            currentSentence = 0;

            // Show first sentence in dialogue UI
            ShowCurrentSentence();

            // Play dialogue sound
            PlaySound(sentences[currentSentence].sentenceSound);

            // Set dialogue as active
            dialogueIsOn = true;
            coolDownTimer = 5f; // Adjust this to control the delay between sentences
        }

        public void NextSentence(out bool lastSentence)
        {
            // Add one to sentence index
            currentSentence++;

            // Next sentence event
            nextSentenceDialogueEvent.Invoke();

            // If last sentence, stop dialogue and return
            if (currentSentence > sentences.Count - 1)
            {
                StopDialogue();
                lastSentence = true;
                return;
            }

            // If not last sentence, continue...
            lastSentence = false;

            // Play dialogue sound
            PlaySound(sentences[currentSentence].sentenceSound);

            // Show next sentence in dialogue UI
            ShowCurrentSentence();

            // Set cooldown before the next sentence
            coolDownTimer = 5f; // Adjust this to control the delay between sentences
        }

        public void StopDialogue()
        {
            // Stop dialogue event
            endDialogueEvent.Invoke();

            // Hide dialogue UI
            //DialogueUI.instance.ClearText();

            // Stop audio source
            if (audioSource != null)
            {
                audioSource.Stop();
            }

            // Set dialogue as inactive
            dialogueIsOn = false;
        }

        private void PlaySound(AudioClip _audioClip)
        {
            // Play the sound only if it exists
            if (_audioClip == null || audioSource == null)
                return;

            // Stop the audio source to avoid overlapping
            audioSource.Stop();

            // Play sentence sound
            audioSource.PlayOneShot(_audioClip);
        }

        private void ShowCurrentSentence()
        {
            try
            {
                // Show the current sentence on the screen
                DialogueUI.instance.ShowSentence(sentences[currentSentence].dialogueCharacter, sentences[currentSentence].sentence);

                // Invoke sentence event
                sentences[currentSentence].sentenceEvent.Invoke();
            }catch(Exception) { }
        }
    }

    [System.Serializable]
    public class NPC_Centence
    {
        public DialogueCharacter dialogueCharacter;
        [TextArea(3, 10)]
        public string sentence;
        public AudioClip sentenceSound;
        public UnityEvent sentenceEvent;
    }
}