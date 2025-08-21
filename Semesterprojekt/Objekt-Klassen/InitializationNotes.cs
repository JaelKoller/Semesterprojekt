using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semesterprojekt
{
    internal class InitializationNotes
    {
        public string DefaultNoteTitle { get; set; }
        public string DefaultNoteText { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }
        public string NoteDate { get; set; }

        public override string ToString()
        {
            return $"{NoteDate} - {NoteTitle}";
        }
    }
}