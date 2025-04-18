using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;
public class Register : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public TMP_InputField confirmPasswordField;
    public TextMeshProUGUI uiText;
   
    public void callRegister()
    {
        StartCoroutine(Registers());
    }

    private IEnumerator Registers()
    {
        
        if (nameField.text.Length>=4 && Regex.IsMatch(nameField.text, @"^[a-zA-Z]*$"))
        {
            if (emailField.text.Length>=13 && Regex.IsMatch(emailField.text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                if (passwordField.text.Length >= 5)
                {
                    if (passwordField.text==confirmPasswordField.text)
                    {
                        WWWForm form = new WWWForm();
                        form.AddField("name", nameField.text);
                        form.AddField("email", emailField.text);
                        form.AddField("pass", passwordField.text);

                        WWW www = new WWW("http://localhost/connect/register.php", form);
                        yield return www;


                        if (www.text == "0")
                        {
                            SceneManager.LoadScene(1);
                        }

                        else
                        {
                            Debug.Log("user creation failed. Error #" + www.text);
                        }

                    }
                    else
                    {
                        uiText.text = "Passwords Dont Match";
                    }
                }
                else
                {
                    uiText.text = "Password Length Too Small";
                }
            }
            else
            {
                uiText.text = "Enter Valid Email";
            }

        }
        else
        {
            uiText.text = "Enter Valid Name";
        }


        
    }



}
