using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] List<QuestionSO> question = new List<QuestionSO>();
    QuestionSO currenQuestion; // Добовление объекта с типа ScriptableObject 
    [SerializeField] TextMeshProUGUI questionText; //  Поле на экране, для текст.

    [Header("Answer")]    
    [SerializeField] GameObject[] answerButton;// Создание массива игровых объектов, для добавления кнопок в инспекторе.
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;
   
    [Header("Button colors")]
    [SerializeField] Sprite defaulrAnswerSprite; // Поле для установки спрайта, вид голубой.
    [SerializeField] Sprite correctAnswerSprited; // Поле для установки спрайта, вид оранжевый.
    [SerializeField] Image timeImage; // получения свойства у объекта
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
        timeImage.fillAmount = timer.fillFraction;// получение значение переменной из другого скрипта(из за того что она public).
                                                  // Работа с заполнением картинки
        if (timer.loadNextQuestion)
        {

            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();//вызов следующего вопроса
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
        DisplayAnswer(index); //передача значение в метод
        SetButtonState(false);//Отключение кнопок
        timer.CancelTimer(); //Вызов метода для присвоения timerValie хначение ноль
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";      
    }

    void DisplayAnswer(int index) 
    {
        Image buttonImage;
        if (currenQuestion != null) { 
        if (index == currenQuestion.GetCorrectAnswerIndex()) //Передача параметра из свойства кнокпки(OnClik) и сравнение значения из ScriptableObject 
        {
            questionText.text = currenQuestion.GetAnswer(currenQuestion.GetCorrectAnswerIndex()); // измениние текста на экране
            buttonImage = answerButton[index].GetComponent<Image>(); // получение компонента для замены одного спрайта на другой.
            buttonImage.sprite = correctAnswerSprited;// замена спрайта, с голубого на оранжывый.
            scoreKeeper.IncrementCorrectAnswers();
            }
        else
        {
            questionText.text = "Ответ не правильный!\n" + currenQuestion.GetAnswer(currenQuestion.GetCorrectAnswerIndex()); // измениние текста на экране
            buttonImage = answerButton[currenQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();// question.GetCorrectAnswerIndex() обращение ScriptableObject,
                                                                                               // вызов функции, для получения числа, правильного ответа.
                                                                                               // Пердача в массив кнопок (answerButton[]), вызов кнопок по индексу. Получение индеска по Image. 
            buttonImage.sprite = correctAnswerSprited;                                         // изменении спрайта. Подсветка правильного ответа
        }
       }
    }

    void GetNextQuestion() {
        if (question.Count>0) 
        {  
            SetButtonState(true); // включение кнопок
            SetDefaultButtonSpites(); // установка спрайтов
            GetRandomQuestion();
            DisplayQuestion();// измение текста на экране и у кнопок
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
          questionText.text = currenQuestion.GetQuestion(); // обращение к объекту ScriptableObject, с вызовом метода(из скрипта QuestionsSo),
                                                    // который возращает строку (вопрос). Потом изменят текст на экране. 
        for (int i = 0; i < answerButton.Length; i++) {// перебор всех кнопок
        TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();// Получение компонента у детей(поле текста), для каждой кнопки
        buttonText.text = currenQuestion.GetAnswer(i); // установка значений из метода (из скрипта QuestionsSo, который возращает массив из объета ScriptableObject 
        }
    }
    void SetButtonState(bool status) {
        for (int i = 0; i < answerButton.Length; i++) {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = status; // Отключения всех кнопок, перебором через цикл
        }
    }
    void SetDefaultButtonSpites()
    {
        for (int i = 0; i < answerButton.Length; i++) {
            Image buttonImage = answerButton[i].GetComponent<Image>(); // получение компонента для замены одного спрайта на другой.
            buttonImage.sprite = defaulrAnswerSprite;// замена спрайта, с оранжывый на голубого .
        }
         

    } 
}
