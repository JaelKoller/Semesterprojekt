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
        private static readonly string notesDataPath = InitializationDataPathJson.DataPath(fileName);

        // Auslesen JSON für Ermittlung, Speicherung und Löschung Notizen
        private static bool LoadData(out List<Notes> notesDataList)
        {
            try
            {
                if (File.Exists(notesDataPath))
                {

                    string notesJSON = File.ReadAllText(notesDataPath);
                    notesDataList = JsonSerializer.Deserialize<List<Notes>>(notesJSON) ?? new List<Notes>();
                }

                else
                {
                    notesDataList = new List<Notes>();
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

        /*
        public static void SaveNotes(string title, string date, string text)
        {
            // Hinzufügen (inkl. Speicherung) neuer Notizen
            //noteList.Add(note);
            //SaveNote(noteList);
        }
        */

        // Speichervorgang der Notizen
        //public static bool SaveNotesData (string saveMode, string contactStatus, string typeOfContact, string contactNumber, Control[] groupFieldEmployeesAndCustomers, Control[] groupFieldEmployees)
        //{
        //    // Abbruch bei Fehler beim Laden der JSON-Datei
        //    if (!LoadData(out var notesDataList))
        //        return false;

        //    InitializationContactData notestData = null;

        //    switch (saveMode.ToLower())
        //    {
        //        case "save":
        //            notestData = new InitializationContactData
        //            {
        //                // Erfassung mit Default-Kontaktstatus "Aktiv"
        //                ContactStatus = contactStatus,
        //                // Erfassung Kontakttyp mit Gross- und Kleinbuchstaben
        //                TypeOfContact = $"{char.ToUpper(typeOfContact[0])}{typeOfContact.Substring(1)}",
        //                // Erfassung Kontaktnummer für spätere Zuweisung der Notizen
        //                ContactNumber = contactNumber
        //            };
        //            break;

        //        case "update":
        //            // Ermittlung bestehender Kontakt auf Basis Kontakt Nr.
        //            notestData = notesDataList.FirstOrDefault(contact => contact.ContactNumber == contactNumber);
        //            notestData.ContactStatus = contactStatus;
        //            break;
        //    }

        //    foreach (Control field in groupFieldEmployeesAndCustomers)
        //    {
        //        notestData.Fields[field.AccessibleName] = GetControlValue(field);
        //    }

        //    if (typeOfContact == "mitarbeiter")
        //    {
        //        foreach (Control field in groupFieldEmployees)
        //        {
        //            notestData.Fields[field.AccessibleName] = GetControlValue(field);
        //        }
        //    }

        //    // Hinzufügen (inkl. Speicherung) neuer Kontakt
        //    notesDataList.Add(notesData);
        //    SaveData(notesDataList, "save");

        //    return true;
        //}

        // Auslesen der Werte für Speicherung der Kontaktdaten
        private static string GetControlValue(Control field)
        {
            if (field is System.Windows.Forms.TextBox txtbxField)
                return txtbxField.Text.Trim();

            if (field is System.Windows.Forms.ComboBox cmbxField)
                return cmbxField.Text;

            if (field is NumericUpDown numField)
                return numField.Value.ToString();

            return string.Empty;
        }



        //// Löschung aller Notizen pro Kontakt
        //public static bool DeleteNotesData(string number)
        //{
        //    // Abbruch bei Fehler beim Laden der JSON-Datei
        //    if (!LoadData(out var notesDataList))
        //        return false;

        //    // Entfernung Kontaktdaten (Block) auf Basis Kontakt Nr.
        //    notesDataList.RemoveAll(contact => contact.ContactNumber.Equals(number));

        //    // Speicherung JSON 
        //    SaveData(notesDataList, "delete");

        //    return true;
        //}

        // Speicherung neuer oder zu löschende Notizen pro Kontakt (Schreibprozess)
        private static void SaveData(List<Notes> notesDataList, string saveMode)
        {
            // Vorbereitung Text für MessageBox (abhängig von Auftragsart)
            string message = string.Empty;

            if (saveMode == "save")
                message = "gespeichert";
            else if (saveMode == "delete")
                message = "gelöscht";

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

                // Ausgabe erfolgreiche Speicherung (userfreundlich)
                MessageBox.Show($"Notiz erfolgreich {message}!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
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