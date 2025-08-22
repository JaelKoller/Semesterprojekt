using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Semesterprojekt
{
    internal class InitializationNotes
    {
        // Nutzung "[JsonIgnore]" für Unterbindung Mitgabe Einträge ans JSON
        [JsonIgnore] public string ContactNumber { get; set; }
        [JsonIgnore] public string DefaultNoteTitle { get; set; }
        [JsonIgnore] public string DefaultNoteText { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }
        public string NoteDate { get; set; }

        public override string ToString() => $"{NoteDate} - {NoteTitle}";
    }

    internal class ContactNotes
    {
        public string ContactNumber { get; set; }
        public List<InitializationNotes> Notes { get; set; } = new List<InitializationNotes>();
    }
}