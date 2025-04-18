using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
public class Login : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;

    public TMP_Text errorMsg;


    public void CallLogin()
    {
        StartCoroutine(LoginMethod());
    }

    private IEnumerator LoginMethod()
    {
        WWWForm form = new WWWForm();
        form.AddField("email",email.text);
        form.AddField("pass",password.text);

        if(email.text.Length>=13 && Regex.IsMatch(email.text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            if(password.text.Length >= 5)
            {
            WWW www = new WWW("http://localhost/connect/login.php",form);
            yield return www;

            if (www.text == "2")
            {
                SceneManager.LoadScene(1);
            }
            else if (www.text=="1")
            {
                errorMsg.text = "Sql Connection Error";

            }

            else 
            {
                errorMsg.text = $"Wrong Credentials. Error : +{www.text}";
            }

            }
            else
            {
                errorMsg.text = "Password Length Too Small";
            }
        }
        else
        {
            errorMsg.text = "Invalid Email";
        }

        
    }

}
