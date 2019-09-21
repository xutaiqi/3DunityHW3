using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Scene Controller
/// Usage: host on a gameobject in the scene   
/// responsiablities:
///   acted as a scene manager for scheduling actors.log something ...
///   interact with the director and players
/// </summary>
/// 
///

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{

    // the first scripts
    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }
    GameObject Boat, Devils1, Devils2, Devils3, Priests1, Priests2, Priests3, Left, Right;

    int[] checkleft = {1,1,1,1,2,2,2};//Priests:1 Devils:2
    int[] checkright = {1,0,0,0,0,0,0};




    // loading resources for the first scence 

    public void LoadResources()
    {
        Boat = Instantiate<GameObject>(
                                Resources.Load<GameObject>("prefabs/Boat"),
            new Vector3((float)0, (float)-1, (float)0), Quaternion.identity);
        Boat.name = "Boat";
        Devils1 = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/Devils1"),
            new Vector3((float)-0.3, (float)-0.5, (float)3.5), Quaternion.identity);
        Devils1.name = "Devils1";
        Devils2 = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/Devils2"),
            new Vector3((float)-0.3, (float)-0.5, (float)4), Quaternion.identity);
        Devils2.name = "Devils2";
        Devils3 = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/Devils3"),
            new Vector3((float)-0.3, (float)-0.5, (float)4.5), Quaternion.identity);
        Devils3.name = "Devils3";
        Priests1 = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/Priests1"),
            new Vector3((float)-0.3, (float)-0.5, (float)2), Quaternion.identity);
        Priests1.name = "Priests1";
        Priests2 = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/Priests2"),
            new Vector3((float)-0.3, (float)-0.5, (float)2.5), Quaternion.identity);
        Priests2.name = "Priests2";
        Priests3 = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/Priests3"),
            new Vector3((float)-0.3, (float)-0.5, (float)3), Quaternion.identity);
        Priests3.name = "Priests3";
        Left = Instantiate<GameObject>(
            Resources.Load<GameObject>("prefabs/Left"),
            new Vector3((float)-0.3, (float)-0.7, (float)3.5), Quaternion.identity);
        Left.name = "Left";
        Right = Instantiate<GameObject>(
                                Resources.Load<GameObject>("prefabs/Right"),
            new Vector3((float)-0.3, (float)-0.7, (float)-6), Quaternion.identity);
        Right.name = "Right";
    }

    int state = 0;//0:continue 1:fail 2:succeed
    void checkGame()
    {
        int count1 = 0;
        int count2 = 0;
        for (int i = 1; i <= 6; i++)
        {
            if (checkleft[i] == 1)
                count1++;
            if (checkleft[i] == 2)
                count2++;
        }
        if (count2 > count1&&count1!=0)
            state = 1;
        count1 = 0;
        count2 = 0;
        for (int i = 1; i <= 6; i++)
        {
            if (checkright[i] == 1)
                count1++;
            if (checkright[i] == 2)
                count2++;
        }
        if (count2 > count1&&count1!=0)
            state = 1;

        if (checkright[1] == 1 && checkright[2] == 1 && checkright[3] == 1)
            state = 2;
    }

    void Restart()
    {
        Priests1.transform.position = new Vector3(-0.3F, -0.5F, 2F);
        Priests2.transform.position = new Vector3(-0.3F, -0.5F, 2.5F);
        Priests3.transform.position = new Vector3(-0.3F, -0.5F, 3F);
        Devils1.transform.position = new Vector3(-0.3F, -0.5F, 3.5F);
        Devils2.transform.position = new Vector3(-0.3F, -0.5F, 4F);
        Devils3.transform.position = new Vector3(-0.3F, -0.5F, 4.5F);
        Boat.transform.position = new Vector3(0F, -1F, 0F);
        Priests1flag = 1;
        Priests2flag = 1;
        Priests3flag = 1;
        Devils1flag = 1;
        Devils2flag = 1;
        Devils3flag = 1;
        Boatchair = 0;
        BoatFlag = 1;
        checkleft[1] = checkleft[2] = checkleft[3] = 1;
        checkleft[4] = checkleft[5] = checkleft[6] = 2;
        for (int i = 1; i <= 6;i++){
            checkright[i] = 0;
        }

    }
    public void Pause()
    {
        throw new System.NotImplementedException();
    }

    public void Resume()
    {
        throw new System.NotImplementedException();
    }

    #region IUserAction implementation
    public void GameOver()
    {
        SSDirector.getInstance().NextScene();
    }
    #endregion
    int Boatchair = 0;
    //0代表船上没人，1代表船上右边有人 ，2代表船上左边有人，3代表船上满人

    int BoatFlag = 1;

    //上述变量： 
    // 1代表船上没人，且在左岸
    // 2代表船上没人，且在右岸
    // 3代表船上有人，且在左岸，左
    // 4代表船上有人，且在右岸
    double Priests1flag = 1;

    double Priests2flag = 1;

    double Priests3flag = 1;

    double Devils1flag = 1;
    double Devils2flag = 1;
    double Devils3flag = 1;
    void OnGUI()
    {
        if (state == 0)
        {
            //give advice firs
            bool DriveFlag = GUI.Button(new Rect(350, 350, 100, 50), "Drive");

            if (DriveFlag == true && BoatFlag == 1 && Boatchair != 0)
            {

                Boat.transform.position = new Vector3(0, -1, -3);
                BoatFlag = 2;
                if (Math.Abs(Priests1flag - 2.1) < 1E-6 || Math.Abs(Priests1flag - 2.2) < 1E-6)
                {
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Priests1flag = 3.1;
                }
                if (Math.Abs(Priests1flag - 2.3) < 1E-6 || Math.Abs(Priests1flag - 2.4) < 1E-6)
                {
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Priests1flag = 3.3;
                }

                if (Math.Abs(Priests2flag - 2.1) < 1E-6 || Math.Abs(Priests2flag - 2.2) < 1E-6)
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Priests2flag = 3.1;
                }
                if (Math.Abs(Priests2flag - 2.3) < 1E-6 || Math.Abs(Priests2flag - 2.4) < 1E-6)
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Priests2flag = 3.3;
                }

                if (Math.Abs(Priests3flag - 2.1) < 1E-6 || Math.Abs(Priests3flag - 2.2) < 1E-6)
                {
                    Priests3.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Priests3flag = 3.1;
                }
                if (Math.Abs(Priests3flag - 2.3) < 1E-6 || Math.Abs(Priests3flag - 2.4) < 1E-6)
                {
                    Priests3.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Priests3flag = 3.3;
                }

                if (Math.Abs(Devils1flag - 2.1) < 1E-6 || Math.Abs(Devils1flag - 2.2) < 1E-6)
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Devils1flag = 3.1;
                }
                if (Math.Abs(Devils1flag - 2.3) < 1E-6 || Math.Abs(Devils1flag - 2.4) < 1E-6)
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Devils1flag = 3.3;
                }

                if (Math.Abs(Devils2flag - 2.1) < 1E-6 || Math.Abs(Devils2flag - 2.2) < 1E-6)
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Devils2flag = 3.1;
                }
                if (Math.Abs(Devils2flag - 2.3) < 1E-6 || Math.Abs(Devils2flag - 2.4) < 1E-6)
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Devils2flag = 3.3;
                }

                if (Math.Abs(Devils3flag - 2.1) < 1E-6 || Math.Abs(Devils3flag - 2.2) < 1E-6)
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Devils3flag = 3.1;
                }
                if (Math.Abs(Devils3flag - 2.3) < 1E-6 || Math.Abs(Devils3flag - 2.4) < 1E-6)
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Devils3flag = 3.3;
                }
                checkGame();
            }
            else if (DriveFlag == true && BoatFlag == 2)
            {
                Boat.transform.position = new Vector3(0, -1, 0);
                BoatFlag = 1;
                if (Math.Abs(Priests1flag - 3.2) < 1E-6 || Math.Abs(Priests1flag - 3.1) < 1E-6)
                {
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Priests1flag = 2.2;
                }
                if (Math.Abs(Priests1flag - 3.3) < 1E-6 || Math.Abs(Priests1flag - 3.4) < 1E-6)
                {
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Priests1flag = 2.4;
                }

                if (Math.Abs(Priests2flag - 3.2) < 1E-6 || Math.Abs(Priests2flag - 3.1) < 1E-6)
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Priests2flag = 2.2;
                }
                if (Math.Abs(Priests2flag - 3.3) < 1E-6 || Math.Abs(Priests2flag - 3.4) < 1E-6)
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Priests2flag = 2.4;
                }

                if (Math.Abs(Priests3flag - 3.2) < 1E-6 || Math.Abs(Priests3flag - 3.1) < 1E-6)
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Priests2flag = 2.2;
                }
                if (Math.Abs(Priests3flag - 3.3) < 1E-6 || Math.Abs(Priests3flag - 3.4) < 1E-6)
                {
                    Priests3.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Priests3flag = 2.4;
                }


                if (Math.Abs(Devils1flag - 3.2) < 1E-6 || Math.Abs(Devils1flag - 3.1) < 1E-6)
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Devils1flag = 2.2;
                }
                if (Math.Abs(Devils1flag - 3.3) < 1E-6 || Math.Abs(Devils1flag - 3.4) < 1E-6)
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Devils1flag = 2.4;
                }

                if (Math.Abs(Devils2flag - 3.2) < 1E-6 || Math.Abs(Devils2flag - 3.1) < 1E-6)
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Devils2flag = 2.2;
                }
                if (Math.Abs(Devils2flag - 3.3) < 1E-6 || Math.Abs(Devils2flag - 3.4) < 1E-6)
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Devils2flag = 2.4;
                }

                if (Math.Abs(Devils3flag - 3.2) < 1E-6 || Math.Abs(Devils3flag - 3.1) < 1E-6)
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Devils3flag = 2.2;
                }
                if (Math.Abs(Devils3flag - 3.3) < 1E-6 || Math.Abs(Devils3flag - 3.4) < 1E-6)
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Devils3flag = 2.4;
                }
                checkGame();
            }





            bool P1flag = GUI.Button(new Rect(260, 250, 30, 30), "P1");
            bool P2flag = GUI.Button(new Rect(225, 250, 30, 30), "P2");
            bool P3flag = GUI.Button(new Rect(190, 250, 30, 30), "P3");
            bool D1flag = GUI.Button(new Rect(155, 250, 30, 30), "D1");
            bool D2flag = GUI.Button(new Rect(120, 250, 30, 30), "D2");
            bool D3flag = GUI.Button(new Rect(85, 250, 30, 30), "D3");
            if (P1flag == true)
            {
                if (Math.Abs(Priests1flag - 1) < 1E-6 && BoatFlag == 1 && Boatchair == 0)
                {
                    Debug.Log(Boatchair);
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Priests1flag = 2.1;
                    Boatchair = 1;
                    checkleft[1] = 0;
                }
                if (Math.Abs(Priests1flag - 1) < 1E-6 && BoatFlag == 1 && Boatchair == 2)
                {
                    Debug.Log(Boatchair);
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Priests1flag = 2.1;
                    Boatchair = 3;
                    checkleft[1] = 0;
                }
                if (Math.Abs(Priests1flag - 1) < 1E-6 && BoatFlag == 1 && Boatchair == 1)
                {
                    Debug.Log(Boatchair);
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Priests1flag = 2.3;
                    Boatchair = 3;
                    checkleft[1] = 0;
                }


                if (Math.Abs(Priests1flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 0)
                {
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Priests1flag = 3.2;
                    Boatchair = 1;
                    checkright[1] = 0;
                }
                if (Math.Abs(Priests1flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 1)
                {
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Priests1flag = 3.4;
                    Boatchair = 3;
                    checkright[1] = 0;
                }
                if (Math.Abs(Priests1flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 2)
                {
                    Priests1.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Priests1flag = 3.2;
                    Boatchair = 3;
                    checkright[1] = 0;
                }

                if (Math.Abs(Priests1flag - 2.2) < 1E-6)
                {
                    Priests1.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)2);
                    Priests1flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkleft[1] = 1;
                }
                if (Math.Abs(Priests1flag - 2.4) < 1E-6)
                {
                    Priests1.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)2);
                    Priests1flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkleft[1] = 1;
                }
                if (Math.Abs(Priests1flag - 3.1) < 1E-6)
                {
                    Priests1.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-4.5);
                    Priests1flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkright[1] = 1;
                }
                if (Math.Abs(Priests1flag - 3.3) < 1E-6)
                {
                    Priests1.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-4.5);
                    Priests1flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkright[1] = 1;
                }
            }
            if (P2flag == true)
            {
                if (Math.Abs(Priests2flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 0))
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Priests2flag = 2.1;
                    if (Boatchair == 0)
                        Boatchair = 1;
                    checkleft[2] = 0;
                }
                if (Math.Abs(Priests2flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 2))
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Priests2flag = 2.1;
                    if (Boatchair == 2)
                        Boatchair = 3;
                    checkleft[2] = 0;
                }
                if (Math.Abs(Priests2flag - 1) < 1E-6 && BoatFlag == 1 && Boatchair == 1)
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Priests2flag = 2.3;
                    Boatchair = 3;
                    checkleft[2] = 0;
                }


                if (Math.Abs(Priests2flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 0)
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Priests2flag = 3.2;
                    Boatchair = 1;
                    checkright[2] = 0;
                }
                if (Math.Abs(Priests2flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 1)
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Priests2flag = 3.4;
                    Boatchair = 3;
                    checkright[2] = 0;
                }
                if (Math.Abs(Priests2flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 2)
                {
                    Priests2.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Priests2flag = 3.2;
                    Boatchair = 3;
                    checkright[2] = 0;
                }
                if (Math.Abs(Priests2flag - 2.2) < 1E-6)
                {
                    Priests2.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)2.5);//
                    Priests2flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkleft[2] = 1;
                }
                if (Math.Abs(Priests2flag - 2.4) < 1E-6)
                {
                    Priests2.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)2.5);//
                    Priests2flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkleft[2] = 1;
                }
                if (Math.Abs(Priests2flag - 3.1) < 1E-6)
                {
                    Priests2.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-5.0);//
                    Priests2flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkright[2] = 1;
                }
                if (Math.Abs(Priests2flag - 3.3) < 1E-6)
                {
                    Priests2.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-5.0);//
                    Priests2flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkright[2] = 1;
                }
            }
            if (P3flag == true)
            {
                if (Math.Abs(Priests3flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 0))
                {
                    Priests3.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Priests3flag = 2.1;
                    if (Boatchair == 0)
                        Boatchair = 1;
                    checkleft[3] = 0;
                }
                if (Math.Abs(Priests3flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 2))
                {
                    Priests3.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Priests3flag = 2.1;
                    if (Boatchair == 2)
                        Boatchair = 3;
                    checkleft[3] = 0;
                }
                if (Math.Abs(Priests3flag - 1) < 1E-6 && BoatFlag == 1 && Boatchair == 1)
                {
                    Priests3.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Priests3flag = 2.3;
                    Boatchair = 3;
                    checkleft[3] = 0;
                }


                if (Math.Abs(Priests3flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 0)
                {
                    Priests3.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Priests3flag = 3.2;
                    Boatchair = 1;
                    checkright[3] = 0;
                }
                if (Math.Abs(Priests3flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 1)
                {
                    Priests3.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Priests3flag = 3.4;
                    Boatchair = 3;
                    checkright[3] = 0;
                }
                if (Math.Abs(Priests3flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 2)
                {
                    Priests3.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Priests3flag = 3.2;
                    Boatchair = 3;
                    checkright[3] = 0;
                }
                if (Math.Abs(Priests3flag - 2.2) < 1E-6)
                {
                    Priests3.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)3);//+0.5
                    Priests3flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkleft[3] = 1;
                }
                if (Math.Abs(Priests3flag - 2.4) < 1E-6)
                {
                    Priests3.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)3);//+0.5
                    Priests3flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkleft[3] = 1;
                }
                if (Math.Abs(Priests3flag - 3.1) < 1E-6)
                {
                    Priests3.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-5.5);//-0.5
                    Priests3flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkright[3] = 1;
                }
                if (Math.Abs(Priests3flag - 3.3) < 1E-6)
                {
                    Priests3.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-5.5);//-0.5
                    Priests3flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkright[3] = 1;
                }
            }
            if (D1flag == true)
            {
                if (Math.Abs(Devils1flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 0))
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Devils1flag = 2.1;
                    if (Boatchair == 0)
                        Boatchair = 1;
                    checkleft[4] = 0;
                }
                if (Math.Abs(Devils1flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 2))
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Devils1flag = 2.1;
                    if (Boatchair == 2)
                        Boatchair = 3;
                    checkleft[4] = 0;
                }
                if (Math.Abs(Devils1flag - 1) < 1E-6 && BoatFlag == 1 && Boatchair == 1)
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Devils1flag = 2.3;
                    Boatchair = 3;
                    checkleft[4] = 0;
                }


                if (Math.Abs(Devils1flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 0)
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Devils1flag = 3.2;
                    Boatchair = 1;
                    checkright[4] = 0;
                }
                if (Math.Abs(Devils1flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 1)
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Devils1flag = 3.4;
                    Boatchair = 3;
                    checkright[4] = 0;
                }
                if (Math.Abs(Devils1flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 2)
                {
                    Devils1.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Devils1flag = 3.2;
                    Boatchair = 3;
                    checkright[4] = 0;
                }
                if (Math.Abs(Devils1flag - 2.2) < 1E-6)
                {
                    Devils1.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)3.5);//+0.5
                    Devils1flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkleft[4] = 2;
                }
                if (Math.Abs(Devils1flag - 2.4) < 1E-6)
                {
                    Devils1.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)3.5);//+0.5
                    Devils1flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkleft[4] = 2;
                }
                if (Math.Abs(Devils1flag - 3.1) < 1E-6)
                {
                    Devils1.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-6.0);//-0.5
                    Devils1flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkright[4] = 2;
                }
                if (Math.Abs(Devils1flag - 3.3) < 1E-6)
                {
                    Devils1.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-6.0);//-0.5
                    Devils1flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkright[4] = 2;
                }
            }

            if (D2flag == true)
            {
                if (Math.Abs(Devils2flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 0))
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Devils2flag = 2.1;
                    if (Boatchair == 0)
                        Boatchair = 1;
                    checkleft[5] = 0;
                }
                if (Math.Abs(Devils2flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 2))
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Devils2flag = 2.1;
                    if (Boatchair == 2)
                        Boatchair = 3;
                    checkleft[5] = 0;
                }
                if (Math.Abs(Devils2flag - 1) < 1E-6 && BoatFlag == 1 && Boatchair == 1)
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Devils2flag = 2.3;
                    Boatchair = 3;
                    checkleft[5] = 0;
                }


                if (Math.Abs(Devils2flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 0)
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Devils2flag = 3.2;
                    Boatchair = 1;
                    checkright[5] = 0;
                }
                if (Math.Abs(Devils2flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 1)
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Devils2flag = 3.4;
                    Boatchair = 3;
                    checkright[5] = 0;
                }
                if (Math.Abs(Devils2flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 2)
                {
                    Devils2.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Devils2flag = 3.2;
                    Boatchair = 3;
                    checkright[5] = 0;
                }
                if (Math.Abs(Devils2flag - 2.2) < 1E-6)
                {
                    Devils2.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)4);//+0.5
                    Devils2flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkleft[5] = 2;
                }
                if (Math.Abs(Devils2flag - 2.4) < 1E-6)
                {
                    Devils2.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)4);//+0.5
                    Devils2flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkleft[5] = 2;
                }
                if (Math.Abs(Devils2flag - 3.1) < 1E-6)
                {
                    Devils2.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-6.5);//-0.5
                    Devils2flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkright[5] = 2;
                }
                if (Math.Abs(Devils2flag - 3.3) < 1E-6)
                {
                    Devils2.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-6.5);//-0.5
                    Devils2flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkleft[5] = 2;
                }
            }
            if (D3flag == true)
            {
                if (Math.Abs(Devils3flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 0))
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Devils3flag = 2.1;
                    if (Boatchair == 0)
                        Boatchair = 1;
                    checkleft[6] = 0;
                }
                if (Math.Abs(Devils3flag - 1) < 1E-6 && BoatFlag == 1 && (Boatchair == 2))
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)0);
                    Devils3flag = 2.1;
                    if (Boatchair == 2)
                        Boatchair = 3;
                    checkleft[6] = 0;
                }
                if (Math.Abs(Devils3flag - 1) < 1E-6 && BoatFlag == 1 && Boatchair == 1)
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)1);
                    Devils3flag = 2.3;
                    Boatchair = 3;
                    checkleft[6] = 0;
                }


                if (Math.Abs(Devils3flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 0)
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Devils3flag = 3.2;
                    Boatchair = 1;
                    checkright[6] = 0;
                }
                if (Math.Abs(Devils3flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 1)
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)-2);
                    Devils3flag = 3.4;
                    Boatchair = 3;
                    checkright[6] = 0;
                }
                if (Math.Abs(Devils3flag - 4) < 1E-6 && BoatFlag == 2 && Boatchair == 2)
                {
                    Devils3.transform.position = new Vector3((float)0, (float)-0.8, (float)-3);
                    Devils3flag = 3.2;
                    Boatchair = 3;
                    checkright[6] = 0;
                }
                if (Math.Abs(Devils3flag - 2.2) < 1E-6)
                {
                    Devils3.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)4.5);//+0.5
                    Devils3flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkleft[6] = 2;
                }
                if (Math.Abs(Devils3flag - 2.4) < 1E-6)
                {
                    Devils3.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)4.5);//+0.5
                    Devils3flag = 1;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkright[6] = 0;
                }
                if (Math.Abs(Devils3flag - 3.1) < 1E-6)
                {
                    Devils3.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-7.0);//-0.5
                    Devils3flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 2;
                    if (Boatchair == 1)
                        Boatchair = 0;
                    checkright[6] = 2;
                }
                if (Math.Abs(Devils3flag - 3.3) < 1E-6)
                {
                    Devils3.transform.position = new Vector3((float)-0.3, (float)-0.5, (float)-7.0);//-0.5
                    Devils3flag = 4;
                    if (Boatchair == 3)
                        Boatchair = 1;
                    if (Boatchair == 2)
                        Boatchair = 0;
                    checkright[6] = 2;
                }
            }
        }
        else{
            if(state==1){
                GUI.Label(new Rect(350, 50, 100, 50), "YOU FAILED!");
            }
            else if(state==2){
                GUI.Label(new Rect(350, 50, 100, 50), "YOU SUCCEED!");
            }
            if(GUI.Button(new Rect(350,150,100,50),"ReStart")){
                Restart();
                state = 0;
            }


        }
    }

     void Start()
	{
		
	}

	// Update is called once per frame
	void Update () {
        
		//give advice first
	}

}
