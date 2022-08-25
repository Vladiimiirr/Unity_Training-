using UnityEngine;

[CreateAssetMenu(menuName ="Quiz Question",fileName = "New Question")] // �������� ���� ScriptableObject, ������� ����� ����� � �������� � ������ ����. 

public class QuestionSO : ScriptableObject
{  [TextArea(2,6)]//���������, ��� ������� ������ ������
   [SerializeField] string question = "������ ����� � ������ ��������?";
   [SerializeField] string[] answer = new string[4];
   [SerializeField] int index; // �������� ��� ����������� ������

    public string GetQuestion() {

        return question; // ����� ��������� ������(������)
    }

    public int GetCorrectAnswerIndex() { 

        return index;//// ����� ��������� �����, �������� � ScriptableObject
    }

    public string GetAnswer(int i) { // �������� ���������, ����� ������� ��������� ������ �� ������� � �������.(�����) 
        return answer[i];
    }
}
