using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{

    internal class CheckAndValidationNoteFields
    {
        // Initialisierung mehrfach verwendeter BackColor
        private static readonly Color backColorOK = SystemColors.Window;
        private static readonly Color backColorNOK = Color.LightPink;

        // Prüfung auf Defaultwert (für OK-Fall Rückgabe "TRUE")
        public static bool CheckNoteFields(InitializationNotes noteData, TextBox noteTitle, TextBox noteText, out string errorMessage)
        {
            // Initialisierung OUT-Argument
            errorMessage = string.Empty;

            // Initialisierung Prüfung auf Defaultwert
            bool isDefaultNoteTitle = noteTitle.Text.Trim().ToLower().Equals(noteData.DefaultNoteTitle.ToLower());
            bool isDefaultNoteText = noteText.Text.Trim().ToLower().Equals(noteData.DefaultNoteText.ToLower());


            // Prüfung Notiz-Titel und Notiz auf Defaultwert
            if (isDefaultNoteTitle && isDefaultNoteText)
            {
                SetBackColorAndTag(noteText, true);
                SetBackColorAndTag(noteTitle, true);
                errorMessage = $"Es ist ein gültiger Titel und Text zu erfassen (nicht Defaultwert).";
                return false;
            }

            // Prüfung Notiz-Titel auf Defaultwert
            if (isDefaultNoteTitle)
            {
                SetBackColorAndTag(noteText, false);
                SetBackColorAndTag(noteTitle, true);
                errorMessage = $"Es ist ein gültiger Titel zu erfassen (nicht Defaultwert).";
                return false;
            }

            // Prüfung Notiz auf Defaultwert
            if (isDefaultNoteText)
            {
                SetBackColorAndTag(noteText, true);
                SetBackColorAndTag(noteTitle, false);
                errorMessage = $"Es ist ein gültiger Text zu erfassen (nicht Defaultwert).";
                return false;
            }

            SetBackColorAndTag(noteText, false);
            SetBackColorAndTag(noteTitle, false);
            return true;
        }

        // Setzung Backcolor und Tag nach erfolgter Prüfung auf Defaultwert
        private static void SetBackColorAndTag(TextBox note, bool isDefault)
        {
            if (!isDefault)
            {
                note.BackColor = backColorOK;
                return;
            }

            note.BackColor = backColorNOK;
            note.Focus();
        }
    }
}