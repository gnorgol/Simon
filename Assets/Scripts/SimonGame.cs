using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class SimonGame : MonoBehaviour
{
    public Button redButton;
    public Button greenButton;
    public Button blueButton;
    public Button yellowButton;

    private List<int> sequence = new List<int>();
    private List<int> playerInput = new List<int>();
    private int score = 0;
    private int highScore = 0;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    // Variables pour gérer l'état du jeu
    private bool isPlayerTurn = false;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
        scoreText.text = "Score: " + score;
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        isPlayerTurn = false;
        playerInput.Clear();
        // Ajouter une nouvelle couleur à la séquence
        sequence.Add(Random.Range(0, 4));
        yield return new WaitForSeconds(1);

        foreach (int colorIndex in sequence)
        {
            ActivateButton(colorIndex);
            yield return new WaitForSeconds(1); // Temps entre les activations
            DeactivateButtons();
            yield return new WaitForSeconds(0.5f);
        }

        isPlayerTurn = true;
    }

    void ActivateButton(int index)
    {
        switch (index)
        {
            case 0:
                // Activer le bouton rouge
                redButton.GetComponent<Image>().color = Color.white;
                break;
            case 1:
                greenButton.GetComponent<Image>().color = Color.white;
                break;
            case 2:
                blueButton.GetComponent<Image>().color = Color.white;
                break;
            case 3:
                yellowButton.GetComponent<Image>().color = Color.white;
                break;
        }
    }

    void DeactivateButtons()
    {
        redButton.GetComponent<Image>().color = Color.red;
        greenButton.GetComponent<Image>().color = Color.green;
        blueButton.GetComponent<Image>().color = Color.blue;
        yellowButton.GetComponent<Image>().color = Color.yellow;
    }

    public void OnButtonPress(int colorIndex)
    {
        if (!isPlayerTurn)
            return;

        playerInput.Add(colorIndex);
        ActivateButton(colorIndex);
        Invoke("DeactivateButtons", 0.5f);

        // Vérifier si l'entrée est correcte
        if (playerInput[playerInput.Count - 1] != sequence[playerInput.Count - 1])
        {
            // Mauvaise entrée, terminer le jeu
            GameOver();
        }
        else if (playerInput.Count == sequence.Count)
        {
            // Séquence complète, augmenter le score et continuer
            score++;
            scoreText.text = "Score: " + score;
            if (score > highScore)
            {
                highScore = score;
                highScoreText.text = "High Score: " + highScore;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
            StartCoroutine(PlaySequence());
        }
    }
    void GameOver()
    {
        isPlayerTurn = false;
        sequence.Clear();
        score = 0;
        scoreText.text = "Score: " + score;
        // Afficher un message ou un effet de fin de jeu si désiré
        StartCoroutine(PlaySequence());
    }

}