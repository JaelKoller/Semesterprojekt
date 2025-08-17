using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{


    internal class Notes
    {
        // Dateipfad für JSON "notes" (Notizen-Liste)
        private static readonly string fileName = "notes";
        private static readonly string notePath = InitializationDataPathJson.DataPath(fileName);



        // Auslesen JSON für Ermittlung, Speicherung und Löschung Kontaktdaten
        private static bool LoadNoteData(out List<Notes> noteList)
        {
            try
            {
                if (File.Exists(notePath))
                {

                    string notesJSON = File.ReadAllText(notePath);
                    noteList = JsonSerializer.Deserialize<List<Notes>>(notesJSON) ?? new List<Notes>();
                }

                else
                {
                    noteList = new List<Notes>();
                }

                return true;
            }

            catch (Exception exception)
            {
                // Ausgabe Fehler beim Laden (Ausnahmebehandlung)
                MessageBox.Show($"Fehler beim Laden der JSON-Datei '{fileName}': {exception}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                noteList = null;
                return false;
            }
        }

        /*
        public static void SaveNotes(string title, string date, string text)
        {
            // Hinzufügen (inkl. Speicherung) neuer Notizen
            //noteList.Add(note);
            //SaveNote(noteList);
        }
        */

        // Speicherung neue, zu ändernde oder zu löschende Kundendaten (Schreibprozess)
        public static void SaveNotes(List<Notes> noteList)
        {
            try
            {
                // Erzeugung data-Ordner, falls noch nicht vorhanden (Vermeidung von Exception)
                var directory = Path.GetDirectoryName(notePath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string contactsJSON = JsonSerializer.Serialize(noteList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(notePath, contactsJSON);

                // Ausgabe erfolgreiche Speicherung (userfreundlich)
                MessageBox.Show("Kontakt erfolgreich gespeichert!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception exception)
            {
                // Ausgabe Fehler beim Laden (Ausnahmebehandlung)
                //ShowMessageBox($"Fehler beim Speichern der JSON-Datei '{fileName}': {exception}");
            }
        }



        public string Title { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }


        public override string ToString()
        {
            return $"{Title} - {Date}";
        }
    }




}