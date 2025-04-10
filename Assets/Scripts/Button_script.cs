using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

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
    public GameObject[] mask;
    public Transform original_tmp;
    public Transform mask_tmp;
    private Vector3 curPos;
    public int id;
    private bool is_on;
    public GameObject original_model;

    
    
    public void SetMask()
    {
        foreach (Transform obj in original)
        {
            string name = obj.name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[0];
            if (id.ToString() == name)
            {
                mask_tmp = Instantiate(obj);
                mask_tmp.gameObject.SetActive(false);
                original_tmp.gameObject.SetActive(false);
            }
        }
    }
    
    public void resbutton_click()
    {
        string namebutton = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("knopka" + namebutton);
        switch(namebutton)
        {
            case "Resbutt_0":
                id = 0;
                break;
            case "Resbutt_1":
                id = 1;
                break;
            case "Resbutt_2":
                id = 2;
                break;
            case "Resbutt_3":
                id = 3;
                break;
        }
        is_on = true;
        SetMask();
        Debug.Log("Pressed");
        mask_tmp = Instantiate(original[id]);
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
                    Button[] resbutts = new Button[4];
                    resbutts[i] = Instantiate(buttonPrefab, parentPanel);
                    Debug.Log("i = " + i);
                    resbutts[i].GetComponent<RectTransform>().localPosition = new Vector2(old_x + 100,y);
                    old_x += 100;
                    Variables.numbers[i] = number;
                    resbutts[i].GetComponentInChildren<TextMeshProUGUI>().text = Variables.names[i] + "(" + Variables.numbers[i] + ")";
                    Debug.Log("Number of Reservoirs" + Variables.names[i] + " is " + number + "\n");
                    resbutts[i].transform.name = "Resbutt_" + i;
                    resbutts[i].onClick.AddListener(resbutton_click) ;
                   
                }
            }
        }
        else
        {
            Debug.LogError("Некорректный ввод! Введите число.");
        }
            Debug.Log(inputText);
    }
    
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            curPos = hit.point;
        }

        if (mask_tmp)
        {
            mask_tmp.position = curPos;

            if (Input.GetMouseButtonDown(0) && is_on)
            {
                original_tmp = Instantiate(original[id]);
                original_tmp.gameObject.SetActive(true);
                original_tmp.position = mask_tmp.position;
                original_tmp.localEulerAngles = mask_tmp.localEulerAngles;
                original_tmp = null;
                Destroy(mask_tmp.gameObject);

            }
            else if(Input.GetMouseButtonDown(1))
            {
                Destroy(original_tmp.gameObject );
                Destroy(mask_tmp.gameObject) ;
            }
        }
    }
}
