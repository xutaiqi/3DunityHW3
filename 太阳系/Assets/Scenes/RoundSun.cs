using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSun : MonoBehaviour
{
    public void LoadResources()
    {
        GameObject sunset = Instantiate<GameObject>(
                                Resources.Load<GameObject>("Scenes/sun"),
                                Vector3.zero, Quaternion.identity);
        sunset.name = "sunset";
        Debug.Log("load sunset ...\n");
    }
    private void Awake()
    {
        LoadResources();
    }
    public Transform sun;
    public Transform moon;
    public Transform Mercury;
    public Transform Venus;
    public Transform earth;
    public Transform Mars;
    public Transform Jupite;
    public Transform Saturn;
    public Transform Neptune;
    public Transform Uranus;
    // Use this for initialization
    void Start()
    {
        sun.position = Vector3.zero;
        earth.position = new Vector3(6, 0, 0);
        moon.position = new Vector3(8, 0, 0);
        Mercury.position = new Vector3(6, 0, 0);
        Venus.position = new Vector3(6, 0, 0);
        Mars.position = new Vector3(6, 0, 0);
        Jupite.position = new Vector3(6, 0, 0);
        Saturn.position = new Vector3(6, 0, 0);
        Neptune.position = new Vector3(6, 0, 0);
        Uranus.position = new Vector3(6, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        earth.RotateAround(sun.position, Vector3.up, 10 * Time.deltaTime);
        earth.Rotate(Vector3.up * 30 * Time.deltaTime);
        moon.transform.RotateAround(earth.position, Vector3.up, 359 * Time.deltaTime);

        Mercury.RotateAround(sun.position, Vector3.up, 20 * Time.deltaTime);
        Venus.RotateAround(sun.position, Vector3.up, 15 * Time.deltaTime);
        Mars.RotateAround(sun.position, Vector3.up, 25 * Time.deltaTime);
        Jupite.RotateAround(sun.position, Vector3.up, 18 * Time.deltaTime);
        Saturn.RotateAround(sun.position, Vector3.up, 12 * Time.deltaTime);
        Neptune.RotateAround(sun.position, Vector3.up, 40 * Time.deltaTime);
        Uranus.RotateAround(sun.position, Vector3.up, 30 * Time.deltaTime);

    }
}
