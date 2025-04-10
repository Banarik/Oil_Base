using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Button_script : MonoBehaviour
{
    public class Variables
    {
         public static float[] rez = new float[4] { 10000, 5000, 3000, 1000 };
         public static string[] names = new string[4] { "РВС-10000", "РВС-5000", "РВС-3000", "РВС-1000" };
         public static int[] radiuses = new int[4] { 50, 40, 30, 20 };
        public static int[] numbers = new int[4] { 0, 0, 0, 0 };

    }

    //public var a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputField inputField;
    public Button buttonPrefab;
    public Transform parentPanel;
    public int x;
    public int y;
    public Transform[] original;
    public Transform[] mask;

    private void object_placer()
    {

    }

    public void button_entry_pressed()
    {
        Destroy(GameObject.Find("enter_button"));
        Destroy(GameObject.Find("input"));
        string inputText = inputField.text;
        int old_x = x - 30;
        if(float.TryParse(inputText, out float volume))
        {
             int number;
            //int place = 0;
            for (int i = 0; i < Variables.rez.Length; i++)
            {
                number = Convert.ToInt32(Math.Floor(volume / Variables.rez[i]));
                volume = volume - number * Variables.rez[i];
                if (i == Variables.rez.Length - 1 && volume > 0)
                {
                    number++;
                }

                if (number != 0)
                {
                    //Label1.Text = Label1.Text + "Количество резервуаров " + Variables.names[i] + " равно " + number
                    //+ "\n";
                    Button[] resbutts = new Button[4];
                    resbutts[i] = Instantiate(buttonPrefab, parentPanel);
                    Debug.Log("i = " + i);
                    resbutts[i].GetComponent<RectTransform>().localPosition = new Vector2(old_x + 100,y);
                    old_x += 100;
                    Variables.numbers[i] = number;
                    resbutts[i].GetComponentInChildren<TextMeshProUGUI>().text = Variables.names[i] + "(" + Variables.numbers[i] + ")"; ;
                    //resbutts[i].GetComponent<RectTransform>().anchorMax = new Vector2(0f,1f);
                    Debug.Log("Number of Reservoirs" + Variables.names[i] + " is " + number + "\n");
                    //res.init(Elgrid, Variables.radiuses[i], number, place, Variables.names[i] );
                    //place++;
                }
            }
        }
        else
        {
            Debug.LogError("Некорректный ввод! Введите число.");
        }
            Debug.Log(inputField.text);
    }
}
