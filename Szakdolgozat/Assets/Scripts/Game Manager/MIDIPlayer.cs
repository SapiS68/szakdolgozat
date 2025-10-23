using TMPro;
using UnityEngine;

public class MIDIPlayer : MonoBehaviour
{
    public MIDIParser parser;
    private float timer = 0;
    private int index = 0;
    public TextMeshProUGUI noteTMP;
    private MidiNote activeNote;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (parser == null || parser.notes.Count == 0)
        {
            Debug.LogWarning("Parser is empty!");
            return;
        }

        timer += Time.deltaTime;

        while (index < parser.notes.Count && parser.notes[index].NoteStartTime <= timer)
        {
            activeNote = parser.notes[index];
            ShowNote(activeNote);
            Debug.Log(activeNote);

            index++;
        }

        if (activeNote != null && activeNote.NoteStartTime + activeNote.NoteDuation <= timer)
        {
            activeNote = null;
            HideNote();
        }
    }

    void ShowNote(MidiNote note)
    {
        noteTMP.text = MIDINoteConverter.Convert(note.NoteNumber);
    }

    void HideNote()
    {
        noteTMP.text = string.Empty;
    }
}
