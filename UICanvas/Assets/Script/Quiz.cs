using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] List<QuestionSO> question = new List<QuestionSO>();
    QuestionSO currenQuestion; // ���������� ������� � ���� ScriptableObject 
    [SerializeField] TextMeshProUGUI questionText; //  ���� �� ������, ��� �����.

    [Header("Answer")]    
    [SerializeField] GameObject[] answerButton;// �������� ������� ������� ��������, ��� ���������� ������ � ����������.
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;
   
    [Header("Button colors")]
    [SerializeField] Sprite defaulrAnswerSprite; // ���� ��� ��������� �������, ��� �������.
    [SerializeField] Sprite correctAnswerSprited; // ���� ��� ��������� �������, ��� ���������.
    [SerializeField] Image timeImage; // ��������� �������� � �������
    [Header("Timer")]
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = question.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timeImage.fillAmount = timer.fillFraction;// ��������� �������� ���������� �� ������� �������(�� �� ���� ��� ��� public).
                                                  // ������ � ����������� ��������
        if (timer.loadNextQuestion)
        {

            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();//����� ���������� �������
            timer.loadNextQuestion = false; 
        }
        else 
        { 
            if (!hasAnsweredEarly && !timer.insAnsweringQuestion) 
            {
                DisplayAnswer(-1);
                SetButtonState(false);
            }
        }
    }

    public void OnAnswerSelected(int index) {
        hasAnsweredEarly = true; 
        DisplayAnswer(index); //�������� �������� � �����
        SetButtonState(false);//���������� ������
        timer.CancelTimer(); //����� ������ ��� ���������� timerValie �������� ����
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";      
    }

    void DisplayAnswer(int index) 
    {
        Image buttonImage;
        if (currenQuestion != null) { 
        if (index == currenQuestion.GetCorrectAnswerIndex()) //�������� ��������� �� �������� �������(OnClik) � ��������� �������� �� ScriptableObject 
        {
            questionText.text = currenQuestion.GetAnswer(currenQuestion.GetCorrectAnswerIndex()); // ��������� ������ �� ������
            buttonImage = answerButton[index].GetComponent<Image>(); // ��������� ���������� ��� ������ ������ ������� �� ������.
            buttonImage.sprite = correctAnswerSprited;// ������ �������, � �������� �� ���������.
            scoreKeeper.IncrementCorrectAnswers();
            }
        else
        {
            questionText.text = "����� �� ����������!\n" + currenQuestion.GetAnswer(currenQuestion.GetCorrectAnswerIndex()); // ��������� ������ �� ������
            buttonImage = answerButton[currenQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();// question.GetCorrectAnswerIndex() ��������� ScriptableObject,
                                                                                               // ����� �������, ��� ��������� �����, ����������� ������.
                                                                                               // ������� � ������ ������ (answerButton[]), ����� ������ �� �������. ��������� ������� �� Image. 
            buttonImage.sprite = correctAnswerSprited;                                         // ��������� �������. ��������� ����������� ������
        }
       }
    }

    void GetNextQuestion() {
        if (question.Count>0) 
        {  
            SetButtonState(true); // ��������� ������
            SetDefaultButtonSpites(); // ��������� ��������
            GetRandomQuestion();
            DisplayQuestion();// ������� ������ �� ������ � � ������
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
      
    }

    void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, question.Count);
        currenQuestion = question[index];
        if (question.Contains(currenQuestion)) 
        {
            question.Remove(currenQuestion);
        }
        
    }

    void DisplayQuestion() { 
          questionText.text = currenQuestion.GetQuestion(); // ��������� � ������� ScriptableObject, � ������� ������(�� ������� QuestionsSo),
                                                    // ������� ��������� ������ (������). ����� ������� ����� �� ������. 
        for (int i = 0; i < answerButton.Length; i++) {// ������� ���� ������
        TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();// ��������� ���������� � �����(���� ������), ��� ������ ������
        buttonText.text = currenQuestion.GetAnswer(i); // ��������� �������� �� ������ (�� ������� QuestionsSo, ������� ��������� ������ �� ������ ScriptableObject 
        }
    }
    void SetButtonState(bool status) {
        for (int i = 0; i < answerButton.Length; i++) {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = status; // ���������� ���� ������, ��������� ����� ����
        }
    }
    void SetDefaultButtonSpites()
    {
        for (int i = 0; i < answerButton.Length; i++) {
            Image buttonImage = answerButton[i].GetComponent<Image>(); // ��������� ���������� ��� ������ ������ ������� �� ������.
            buttonImage.sprite = defaulrAnswerSprite;// ������ �������, � ��������� �� �������� .
        }
         

    } 
}
