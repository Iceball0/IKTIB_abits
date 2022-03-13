using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour
{
    
    public float speed;
    public Transform tr;
    
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private Boolean nearTeacher;
    private float nearRadius;
    private GameObject[] teachers;
    private Transform teacherPosition;
    private Transform teacherTransform;
    private List<string> dialogCodes;
    private Animator anim;
    
    public static GameObject nearbyTeacher;
    public Joystick joystick;
    public Button interactButton;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        nearTeacher = false;
        nearRadius = 2.5f;
        teachers = GameObject.FindGameObjectsWithTag("QuestGiver");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        moveVelocity = moveInput.normalized * speed;

        if (joystick.Horizontal > 0 && 1 - joystick.Horizontal < 1 - joystick.Vertical &&
            1 - joystick.Horizontal < Math.Abs(-1 - joystick.Vertical))
        {
            anim.SetInteger("Dir", 1);
        } 
        else if (joystick.Horizontal < 0 && Math.Abs(-1 - joystick.Horizontal) < 1 - joystick.Vertical &&
                 Math.Abs(-1 - joystick.Horizontal) < Math.Abs(-1 - joystick.Vertical))
        {
            anim.SetInteger("Dir", 2);
        }
        else if (joystick.Vertical > 0 && 1 - joystick.Vertical < 1 - joystick.Horizontal &&
                 1 - joystick.Vertical < Math.Abs(-1 - joystick.Horizontal))
        {
            anim.SetInteger("Dir", 3);
        }
        else if (joystick.Vertical < 0 && Math.Abs(-1 - joystick.Vertical) < 1 - joystick.Horizontal &&
                 Math.Abs(-1 - joystick.Vertical) < Math.Abs(-1 - joystick.Horizontal))
        {
            anim.SetInteger("Dir", 4);
        }
        else
        {
            anim.SetInteger("Dir", 0);
        }
        
        
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        nearbyTeacher = FindClosetsQuests();
        teacherTransform = nearbyTeacher.GetComponent<Transform>();
        
        dialogCodes = nearbyTeacher.GetComponent<TeacherQuestData>().dialogCodes;

        if (Vector2.Distance(teacherTransform.position, tr.position) < nearRadius && !nearTeacher && 
            dialogCodes.Count > 0)
        {
            nearTeacher = true;
            interactButton.interactable = true;
        }
        
        if (Vector2.Distance(teacherTransform.position, tr.position) >= nearRadius && nearTeacher)
        {
            nearTeacher = false;
            interactButton.interactable = false;
        }
    }

    GameObject FindClosetsQuests()
    {
        float distance = Mathf.Infinity;
        GameObject closestTeacher = null;
            
        foreach (GameObject closestQuestGiver in teachers)
        {

            float teacherDistance = Vector2.Distance(closestQuestGiver.GetComponent<Transform>().position,
                tr.position);
            if (teacherDistance < distance)
            {
                distance = teacherDistance;
                closestTeacher = closestQuestGiver;
            }

        }

        return closestTeacher;
    }
}
