using UnityEngine;

public static class MIDINoteConverter
{
    private static readonly string[] NoteNames =
    {
        "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
    };

    public struct NoteInfo
    {
        public string Name;  // e.g., "C#"
        public int Octave; // e.g., 4
    }

    public static string Convert(int noteNumber)
    {
        if (noteNumber < 0 || noteNumber > 127)
        {
            Debug.LogWarning("Invalid Midi note was sent to the converter!");
            return "Invalid";
        }

        NoteInfo noteInfo = new NoteInfo();
        int noteIndex = noteNumber % 12;
        noteInfo.Name = NoteNames[noteIndex];
        noteInfo.Octave = (noteNumber / 12) - 1;

        return noteInfo.Name + noteInfo.Octave;
    }
}
