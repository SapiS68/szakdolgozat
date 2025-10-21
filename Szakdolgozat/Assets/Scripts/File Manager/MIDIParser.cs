using UnityEngine;
using Melanchall.DryWetMidi.Core;
using NUnit.Framework;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.IO;

public class MIDIParser : MonoBehaviour
{
    public TextAsset midiFile;
    public List<MidiNote> notes = new List<MidiNote>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ParseMidi();
        foreach (var note in notes)
        {
            Debug.Log(note);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ParseMidi()
    {
        if (midiFile == null)
        {
            Debug.LogError("Midi file is missing");
            return;
        }

        notes.Clear();

        MidiFile mf;
        using (var stream = new MemoryStream(midiFile.bytes))
        {
            mf = MidiFile.Read(stream);
        }

        TempoMap tempoMap = mf.GetTempoMap();

        foreach (var note in mf.GetNotes())
        {
            MidiNote midiNote = new MidiNote();
            midiNote.NoteNumber = note.NoteNumber;
            midiNote.NoteStartTime = (float)note.TimeAs<MetricTimeSpan>(tempoMap).TotalSeconds;
            midiNote.NoteDuation = (float)note.LengthAs<MetricTimeSpan>(tempoMap).TotalSeconds;
            notes.Add(midiNote);
        }
        notes.Sort((a, b) => a.NoteStartTime.CompareTo(b.NoteStartTime));
    }
}

public class MidiNote
{
    public int NoteNumber { get; set; }
    public float NoteStartTime { get; set; }
    public float NoteDuation { get; set; }

    public override string ToString()
    {
        return NoteNumber + " at: " + NoteStartTime + " for: " + NoteDuation;
    }
}
