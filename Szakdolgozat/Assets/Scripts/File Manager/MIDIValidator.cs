using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MIDIValidator : MonoBehaviour
{

    public string filePath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReadMidi();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: File Manager script
    void ReadMidi() 
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found!");
            return;
        }

        byte[] buffer = new byte[1024];
        List<string> hex = new List<string>();

        using (FileStream fs = new FileStream(filePath, FileMode.Open)) 
        using (BinaryReader br = new BinaryReader(fs))
        {
            try
            {
                br.Read(buffer, 0, 14);
                for (int i = 0; i < 14; i++)
                {
                    hex.Add(DectoHex(buffer[i]));
                    Debug.Log(hex[i]);
                }
                if (hex[9] != "0")
                {
                    Debug.LogWarning("Multitrack MIDI detected! May cause errors!");
                }
            }
            catch (System.Exception e)
            {

                Debug.LogError(e.Message);
            }
        }

    }

    string DectoHex(int input)
    {
        string s;
        s = Convert.ToString(input, 16);
        return s;
    }
}
