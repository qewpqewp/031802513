using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseManager<InputManager>
{
    private bool isStart = false;
      public InputManager()
    {
        MonoManager.GetInstance().AddUpdateListener(MyUpdate);
        MonoManager.GetInstance().AddFixedUpdateListener(MyFixedUpdate);
    }
    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }
    private void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
            EventCenter.GetInstance().EventTrigger("某键按下", key);
        if (Input.GetKey(key))
            EventCenter.GetInstance().EventTrigger("某键持续", key);

    }
    private void CheckAxis()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Dictionary<string, float> dic = new Dictionary<string, float>();
            dic.Add("Horizontal", Input.GetAxisRaw("Horizontal"));
            dic.Add("Vertical", Input.GetAxisRaw("Vertical"));
            EventCenter.GetInstance().EventTrigger("方向键",dic);
        }
    }
    private void MyUpdate()
    {
        if (!isStart)
            return;
        CheckKeyCode(KeyCode.Q);
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.E);
        CheckKeyCode(KeyCode.R);
        CheckKeyCode(KeyCode.D);
        CheckKeyCode(KeyCode.F);
        CheckKeyCode(KeyCode.X);
        CheckKeyCode(KeyCode.Space);
        CheckKeyCode(KeyCode.Escape);

    }
    private void MyFixedUpdate()
    {
        if (!isStart)
            return;
        CheckAxis();

    }
}
