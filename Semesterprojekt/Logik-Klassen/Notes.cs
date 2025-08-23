using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace Semesterprojekt
{


    internal class Notes
    {
        // Dateipfad für JSON "notes" (Notizen-Liste)
        private static readonly string fileName = "notes";
        private static readonly string notesDataPath = InitializationDataPathJson.DataPath(fileName);

        // Auslesen JSON für Ermittlung, Speicherung und Löschung Notizen
        private static bool LoadData(out List<ContactNotes> notesDataList)
        {
            try
            {
                if (File.Exists(notesDataPath))
                {

                    string notesJSON = File.ReadAllText(notesDataPath);
                    notesDataList = JsonSerializer.Deserialize<List<ContactNotes>>(notesJSON) ?? new List<ContactNotes>();
                }

                else
                {
                    notesDataList = new List<ContactNotes>();
                }

                return true;
            }

            catch (Exception exception)
            {
                // Ausgabe Fehler beim Laden (Ausnahmebehandlung)
                ShowMessageBox($"Fehler beim Laden der JSON-Datei '{fileName}': {exception}");
                notesDataList = null;
                return false;
            }
        }

        // Suche der Notizen
        public static ContactNotes SearchNotesData(string contactNumber)
        {
            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var notesDataList))
                return null;

            // Ausgabe Such-Resultat als Liste
            return notesDataList.FirstOrDefault(contact => string.Equals(contact.ContactNumber, contactNumber));
        }
        
        // Speichervorgang der Notizen
        public static bool SaveNotesData(InitializationNotes noteData)
        {
            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var notesDataList))
                return false;

            // Suche nach bestehender Kontakt Nr. (für Hinzufügen)
            var contactNotes = notesDataList.FirstOrDefault(contact => contact.ContactNumber.Equals(noteData.ContactNumber));

            if (contactNotes == null)
            {
                // Erfassung neuer Notizblock inkl. Kontakt Nr.
                contactNotes = new ContactNotes
                {
                    ContactNumber = noteData.ContactNumber,
                    Notes = new List<InitializationNotes> { noteData }
                };

                // Hinzufügen neuer Notizblock inkl. Kontakt Nr.
                notesDataList.Add(contactNotes);
            }

            else
            {
                // Hinzufügen neuer Notiz
                contactNotes.Notes.Add(noteData);
            }

            // Speicherung neuer Notiz
            SaveData(notesDataList);
            return true;
        }

        // Löschung aller Notizen pro Kontakt
        public static bool DeleteNotesData(string contactNumber)
        {
            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var notesDataList))
                return false;

            // Entfernung Kontaktdaten (Block) auf Basis Kontakt Nr.
            notesDataList.RemoveAll(contact => contact.ContactNumber.Equals(contactNumber));

            // Speicherung JSON 
            SaveData(notesDataList);

            return true;
        }

        // Speicherung neuer oder zu löschende Notizen pro Kontakt (Schreibprozess)
        private static void SaveData(List<ContactNotes> notesDataList)
        {
            try
            {
                // Erzeugung data-Ordner, falls noch nicht vorhanden (Vermeidung von Exception)
                var directory = Path.GetDirectoryName(notesDataPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string notesJSON = JsonSerializer.Serialize(notesDataList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(notesDataPath, notesJSON);
            }

            catch (Exception exception)
            {
                // Ausgabe Fehler beim Laden (Ausnahmebehandlung)
                ShowMessageBox($"Fehler beim Speichern der JSON-Datei '{fileName}': {exception}");
            }
        }

        // Erzeugung MessageBox (Popup) bei JSON-Fehler
        private static void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}