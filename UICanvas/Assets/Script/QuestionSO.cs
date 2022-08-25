using UnityEngine;

[CreateAssetMenu(menuName ="Quiz Question",fileName = "New Question")] // Создание типа ScriptableObject, задание имени файла и названия в пункте меню. 

public class QuestionSO : ScriptableObject
{  [TextArea(2,6)]//параметры, для задание длинны текста
   [SerializeField] string question = "Первый полет в космос человека?";
   [SerializeField] string[] answer = new string[4];
   [SerializeField] int index; // параметр для правильного ответа

    public string GetQuestion() {

        return question; // метод возращает строку(вопрос)
    }

    public int GetCorrectAnswerIndex() { 

        return index;//// метод возращает число, заданное в ScriptableObject
    }

    public string GetAnswer(int i) { // передача параметра, метод который возращает строку по индексу в массиве.(Ответ) 
        return answer[i];
    }
}
