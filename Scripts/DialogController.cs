using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using TMPro;

public class DialogController : MonoBehaviour
{

    public Button DialogButton;
    public Image DialogImage;
    public GameObject DialogTextField;
    public Joystick Joystick;
    public GameObject NameField;
    public Image Decan_photo;
    public Image Teacher_1_photo;
    public Image Teacher_2_photo;
    
    public List<string> dialogFind;

    private GameObject nearbyTeacher;
    private TextMeshProUGUI DialogText;
    private TextMeshProUGUI NameText;
    private TeacherQuestData dialogCode;
    private Image activeImage;
    private string curQuestCode;
    private List<string> dialogActive;
    private List<string> dialogCodes;

    public void startDialog()
    {
        string readFileFromPath = Application.streamingAssetsPath + "/Dialog_files/" + "Dialogs" + ".txt";

        List<string> dialogLines = File.ReadAllLines(readFileFromPath).ToList();

        DialogText = DialogTextField.GetComponent<TextMeshProUGUI>();
        NameText = NameField.GetComponent<TextMeshProUGUI>();

        nearbyTeacher = PlayerController.nearbyTeacher;
        dialogCode = nearbyTeacher.GetComponent<TeacherQuestData>();
        curQuestCode = dialogCode.dialogCodes[0];
        
        dialogFind = dialogLines.FindAll(e => e.Contains(curQuestCode));

        switch (nearbyTeacher.name)
        {
            case "Декан":
                activeImage = Decan_photo;
                break;
            case "Татьяна Васильевна":
                activeImage = Teacher_1_photo;
                break;
            case "Василий Александрович":
                activeImage = Teacher_2_photo;
                break;
        }

        DialogButton.gameObject.SetActive(true);
        DialogImage.gameObject.SetActive(true);
        DialogTextField.gameObject.SetActive(true);
        activeImage.gameObject.SetActive(true);
        NameField.SetActive(true);
        
        Joystick.gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        DialogText.text = dialogFind[0].Substring(4);
        NameText.text = nearbyTeacher.name;

    }

    public void nextDialog()
    {
        
        dialogActive = DialogButton.GetComponent<DialogController>().dialogFind;
        nearbyTeacher = PlayerController.nearbyTeacher;
        
        switch (nearbyTeacher.name)
        {
            case "Декан":
                activeImage = Decan_photo;
                break;
            case "Татьяна Васильевна":
                activeImage = Teacher_1_photo;
                break;
            case "Василий Александрович":
                activeImage = Teacher_2_photo;
                break;
        }
        
        DialogText = DialogTextField.GetComponent<TextMeshProUGUI>();
        NameText = NameField.GetComponent<TextMeshProUGUI>();
        
        if (dialogActive.Count > 0)
        {
            dialogActive.Remove(dialogActive[0]);

            if (dialogActive.Count >= 1)
            {
                DialogText.text = dialogActive[0].Substring(4);
                NameText.text = nearbyTeacher.name;
                
                DialogButton.GetComponent<DialogController>().dialogFind = dialogActive;
            } else
            {
                dialogCodes = PlayerController.nearbyTeacher.GetComponent<TeacherQuestData>().dialogCodes;
                PlayerController.nearbyTeacher.GetComponent<TeacherQuestData>().dialogCodes.Remove(dialogCodes[0]);
                    
                this.gameObject.SetActive(false);
                DialogImage.gameObject.SetActive(false);
                DialogTextField.gameObject.SetActive(false);
                NameField.SetActive(false);
                activeImage.gameObject.SetActive(false);
        
                Joystick.gameObject.SetActive(true);
                DialogButton.gameObject.SetActive(true);
                DialogButton.interactable = false;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
            DialogImage.gameObject.SetActive(false);
            DialogTextField.gameObject.SetActive(false);
            NameField.SetActive(false);
            activeImage.gameObject.SetActive(false);
        
            Joystick.gameObject.SetActive(true);
            DialogButton.gameObject.SetActive(true);
            DialogButton.interactable = false;
        }

    }
}
