using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class UdpSrvrSample : MonoBehaviour {

    byte[] data = new byte[1024];
    UdpClient newsock;
    IPEndPoint sender;
    DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

    private void Start()
    {



        Application.runInBackground = true;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        newsock = new UdpClient(ipep);

        Debug.Log("Waiting for a client...");

        sender = new IPEndPoint(IPAddress.Any, 9051);

    }
    /*
    private GameObject FindObject(string name)
    {
        foreach(GameObject g in myObjects)
        {
            Debug.Log(g.name);
            if (g.name == name)
            {
                return g;
            }
        }
        Debug.Log("Object not found.");
        return null;
    }
    */

    private void SetVisible(string[] msgArgs)
    {
        // Format: SetVisible {object name} {true|false}
        // Assert the correct # of arguments
        GameObject thingToSetVisible = GameObject.Find(msgArgs[1]);
        if (msgArgs[2] == "true")
        {
            thingToSetVisible.GetComponent<Renderer>().enabled = true;
        }
        else if (msgArgs[2] == "false")
        {
            thingToSetVisible.GetComponent<Renderer>().enabled = false;
        }
    }

    private float cvtFloat(string s)
    {
        return float.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
    }

    private void CreatePrimitive(string[] msgArgs)
    {
        PrimitiveType myPrimitive;
        switch (msgArgs[1])
        {
            case "Sphere":
                myPrimitive = PrimitiveType.Sphere;
                break;
            case "Cube":
                myPrimitive = PrimitiveType.Cube;
                break;
            default:
                myPrimitive = PrimitiveType.Sphere;
                break;
        }
        GameObject myobj = GameObject.CreatePrimitive(myPrimitive);
        myobj.name = msgArgs[2];
        myobj.transform.position = new Vector3(cvtFloat(msgArgs[4]), cvtFloat(msgArgs[5]), cvtFloat(msgArgs[6]));
        myobj.transform.localScale = new Vector3(cvtFloat(msgArgs[8]), cvtFloat(msgArgs[9]), cvtFloat(msgArgs[10]));
        myobj.transform.eulerAngles = new Vector3(cvtFloat(msgArgs[12]), cvtFloat(msgArgs[13]), cvtFloat(msgArgs[14]));
        Rigidbody gameObjectsRigidBody = myobj.AddComponent<Rigidbody>();
        gameObjectsRigidBody.mass = 1;
        gameObjectsRigidBody.useGravity = false;
    }
    private void SetProperty(string[] msgArgs)
    {
        GameObject myobj = GameObject.Find(msgArgs[1]);
        Vector3 mydata = new Vector3(cvtFloat(msgArgs[3]), cvtFloat(msgArgs[4]), cvtFloat(msgArgs[5]));
        switch (msgArgs[2])
        {
            case "localPosition":
                myobj.transform.localPosition = mydata;
                break;
            case "position":
                myobj.transform.position = mydata;
                break;
            case "velocity":
                myobj.GetComponent<Rigidbody>().velocity = mydata;
                break;
            case "scale":
                myobj.transform.localScale = mydata;
                break;
            case "rotation":
                myobj.transform.eulerAngles = mydata;
                break;
        }
    }
    private void SetTexture(string[] msgArgs)
    {
        GameObject myobj = GameObject.Find(msgArgs[1]);
        Texture myTexture = (Texture)Resources.Load(msgArgs[2]);
        Renderer myRenderer = myobj.GetComponent<Renderer>();
        myRenderer.material.SetTexture("_MainTex", myTexture);

    }
    private void Update()
    {
        try
        {
            if (newsock.Available > 0) // Only read if we have some data 
            {                           // queued in the network buffer. 
                byte[] data = newsock.Receive(ref sender);

                // TODO: Convert underscores to whitespace
                // TODO: Allow some flexibility with capitalization, or no?
                string myMessage = Encoding.ASCII.GetString(data, 0, data.Length);
                myMessage = myMessage.Trim();
                string[] msgArgs = myMessage.Split(' ');
                switch (msgArgs[0])
                {
                    case "SetVisible":
                        SetVisible(msgArgs);
                        break;
                    case "CreatePrimitive":
                        CreatePrimitive(msgArgs);
                        break;
                    case "SetProperty":
                        SetProperty(msgArgs);
                        break;
                    case "SetTexture":
                        SetTexture(msgArgs);
                        break;
                    default:
                        break;
                }

                //long getTime = (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds - 28800000;
                //long sendTime = Convert.ToInt64(Encoding.ASCII.GetString(data, 0, data.Length));
                //Debug.Log(getTime - sendTime);

                /*
                string welcome = "Welcome to my test server";
                data = Encoding.ASCII.GetBytes(welcome);
                newsock.Send(data, data.Length, sender);

                data = newsock.Receive(ref sender);

                Debug.Log(Encoding.ASCII.GetString(data, 0, data.Length));
                newsock.Send(data, data.Length, sender);
                */
            }
        }
        catch (SocketException e)
        {
            Debug.Log("Error!");
            Debug.Log(e.ErrorCode);
        }


    }
}