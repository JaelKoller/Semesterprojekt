using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Semesterprojekt
{
    public partial class KontaktErstellen : Form
    {
        // Initalisierung String "typeOfContact" für "Speichern und neuer Kontakt erstellen"
        private string typeOfContactNew;

        // Dateipfad für Kontaktdaten-Liste
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        private readonly string contactDataPath = Path.Combine(projectRoot, "data", "contacts.json");

        // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
        private System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers;
        private Control[] groupFieldEmployeesAndCustomers;
        private System.Windows.Forms.Label[] groupLabelEmployees;
        private Control[] groupFieldEmployees;
        private Control[] checkFieldIgnore;

        // Initialisierung mehrfach verwendeter Index-Counter
        private int tabIndexCounter = 1;

        // Initialisierung verwendeter BackColor (analog separater Klasse)
        private Color backColorOK = SystemColors.Window;

        // Initialisierung verwendetes Tag (analog separater Klasse)
        private string tagOK = "true";

        public KontaktErstellen(string typeOfContact)
        {
            InitializeComponent();
            this.Size = new Size(775, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
            groupLabelEmployeesAndCustomers = GroupLabelEmployeesAndCustomers();
            groupFieldEmployeesAndCustomers = GroupFieldEmployeesAndCustomers();
            groupLabelEmployees = GroupLabelEmployees();
            groupFieldEmployees = GroupFieldEmployees();

            Design();
            InitializationLabelToolTip();

            typeOfContactNew = typeOfContact;
            InitializationTypeOfContact();
            UpdateTypeOfContact();
        }

        // Design (Platzierung) der Eingabe-Felder usw.
        private void Design()
        {
            // Platzierung Gruppe Radio-Button (Mitarbeiter vs. Kunde)
            GrpBxCreatKntktMaKunde.Size = new Size(165, 40);
            GrpBxCreatKntktMaKunde.Location = new Point(10, 10);

            // Platzierung Gruppe Mitarbeiter UND Kunde (alle)
            GrpBxCreatKntktDatenAlle.Size = new Size(365, 390);
            GrpBxCreatKntktDatenAlle.Location = new Point(10, 55);

            // Platzierung Gruppe NUR Mitarbeiter (ohne Kunde)
            GrpBxDatenMA.Size = new Size(365, 390);
            GrpBxDatenMA.Location = new Point(385, 55);

            // Platzierung Radio-Buttons (Mitarbeiter vs. Kunde)
            RdbCreatKntktMa.Location = new Point(10, 15);
            RdbCreatKntktKunde.Location = new Point(100, 15);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit 1 
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployeesAndCustomers, groupFieldEmployeesAndCustomers, tabIndexCounter);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeiter (ohne Kunde)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) fortführend
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployees, groupFieldEmployees, tabIndexCounter);

            // Platzierung Buttons "Speichern und ..."
            CmdCreateKntktKontaktErstellen.Size = new Size(150, 60);
            CmdCreateKntktKontaktErstellen.Location = new Point(445, 470);
            CmdCreateKntktDashboard.Size = new Size(150, 60);
            CmdCreateKntktDashboard.Location = new Point(600, 470);
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private int PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, int indexCounter)
        {
            int startLocation = 20;
            int labelXAchse = 10;
            int controlXAchse = 150;
            int tabIndexCounter = indexCounter;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupLabel[i].Location = new Point(labelXAchse, startLocation);
                groupField[i].Size = new Size(200, 20);
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher fix mit 0
                groupLabel[i].TabIndex = 0;
                // Eingabefeld relevant für Tab und daher durchnummeriert (Start bei 1)
                groupField[i].TabIndex = tabIndexCounter++;

                // Default-Tag relevant für Validierung Eingabefelder (Start mit TRUE)
                groupField[i].Tag = tagOK;
            }

            return tabIndexCounter;
        }

        // Erstellung Array für Labels der Gruppe Mitarbeiter UND Kunde (alle)
        private System.Windows.Forms.Label[] GroupLabelEmployeesAndCustomers()
        {
            groupLabelEmployeesAndCustomers = new System.Windows.Forms.Label[]
            {
                LblCreatKntktTitel,
                LblCreatKntktAnrede,
                LblCreatKntktVorname,
                LblCreatKntktName,
                LblCreatKntktBirthday,
                LblCreatKntktGeschlecht,
                LblCreatKntktAdresse,
                LblCreatKntktPLZ,
                LblCreatKntktOrt,
                LblCreatKntktTelGeschaeft,
                LblCreatKntktTelMobile,
                LblCreatKntktEmail
            };

            return groupLabelEmployeesAndCustomers;
        }

        // Erstellung Array für Labels der Gruppe Mitarbeiter (ohne Kunde)
        private System.Windows.Forms.Label[] GroupLabelEmployees()
        {
            groupLabelEmployees = new System.Windows.Forms.Label[]
            {
                LblCreatKntktMaMaNr,
                LblCreatKntktMaAHVNr,
                LblCreatKntktMaNationalitaet,
                LblCreatKntktMaKader,
                LblCreatKntktMaBeschGrad,
                LblCreaKntktMaAbteilung,
                LblCreatKntktMaRolle,
                LblCreatKntktMaLehrj,
                LblCreatKntktMaAktLehrj,
                LblCreatKntktMaOfficeNumber,
                LblCreatKntktEintrDatum,
                LblCreatKntktAustrDatum
            };

            return groupLabelEmployees;
        }

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle)
        private Control[] GroupFieldEmployeesAndCustomers()
        {
            groupFieldEmployeesAndCustomers = new Control[]
            {
                TxtCreatKntktTitel,
                CmBxCreatKntktAnrede,
                TxtCreatKntktVorname,
                TxtCreatKntktName,
                DateCreatKntktBirthday,
                CmBxCreatKntktGeschlecht,
                TxtCreatKntktAdr,
                TxtCreatKntktPLZ,
                TxtCreatKntktOrt,
                TxtCreatKntktTelGeschaeft,
                TxtCreatKntktTelMobile,
                TxtCreatKntktEmail
            };
            
            return groupFieldEmployeesAndCustomers;
        }

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeiter (ohne Kunde)
        private Control[] GroupFieldEmployees()
        {
            groupFieldEmployees = new Control[]
            {
                TxtCreatKntktMaManr,
                TxtCreatKntktMaAHVNr,
                TxtCreatKntktMaNationalitaet,
                TxtCreatKntktMaKader,
                NumCreatKntktMaBeschGrad,
                TxtCreatKntktMaAbteilung,
                TxtCreatKntktMaRolle,
                NumCreatKntktMaLehrj,
                NumCreatKntktMaAktLehrj,
                NumCreatKntktMaOfficeNumber,
                DateCreatKntktEintrDatum,
                DateCreatKntktAustrDatum
            };

            return groupFieldEmployees;
        }

        // Erstellung Array für KEINE-Pflichtfelder-Prüfung
        private Control[] CheckFieldIgnore()
        {
            checkFieldIgnore = new Control[]
            {
                TxtCreatKntktTitel,
                // bei Mitarbeitern bleibt das Feld "Pflicht"
                (!RdbCreatKntktMa.Checked ? TxtCreatKntktTelGeschaeft : null),
                 // bei Mitarbeitern mit CH-Nationalität bleibt das Feld "Pflicht"
                (RdbCreatKntktKunde.Checked || (!string.IsNullOrWhiteSpace(TxtCreatKntktMaNationalitaet.Text) && TxtCreatKntktMaNationalitaet.Text.ToUpper() != "CH") ? TxtCreatKntktMaAHVNr : null),            
                TxtCreatKntktMaKader,
                NumCreatKntktMaLehrj,
                NumCreatKntktMaAktLehrj,
                DateCreatKntktAustrDatum,
            };

            return checkFieldIgnore;
        }

        // Erstellung ToolTip für spezifische Labels (zur besseren Verständlichkeit)
        private void InitializationLabelToolTip()
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip
            {
                AutoPopDelay = 10000, // Standardwert liegt bei 5000ms (Wie lange bleibt Tooltip sichtbar)
                InitialDelay = 100, // Standardwert liegt bei 500ms (Verzögerung bis Tooltip erscheint)
                ReshowDelay = 100, // Standardwert liegt bei 100ms (Verzögerung zwischen mehreren Tooltips hintereinander)
                ShowAlways = true // Standardwert ist FALSE (Tooltip wird auch angezeigt, wenn Formular nicht aktiv)
            };

            SetLabelToolTip(toolTip, LblCreatKntktTitel, "Namenstitel (gekürzt)\r\nz.B. Dr., Ing., Prof.");
            SetLabelToolTip(toolTip, LblCreatKntktPLZ, "4-/5-stellige Postleitzahl\r\n(Schweiz und Nachbarländer)");
            SetLabelToolTip(toolTip,LblCreatKntktMaAHVNr, "Eingabe mit Punkten (CH-Norm)\r\nz.B. 756.1234.5678.90");
            SetLabelToolTip(toolTip,LblCreatKntktMaNationalitaet, "2-stelliger Länderkürzel\r\nz.B. CH, DE, FR, IT");
            SetLabelToolTip(toolTip, LblCreatKntktMaLehrj, "nur relevant für Lernende");
            SetLabelToolTip(toolTip, LblCreatKntktMaAktLehrj, "nur relevant für Lernende");
        }

        // Erzeugung Hover-Effekt bei ToolTip (userfreundlicher)
        private void SetLabelToolTip(System.Windows.Forms.ToolTip tooltip, System.Windows.Forms.Label label, string text)
        {
            tooltip.SetToolTip(label, text);
            
            // Speicherung Original-Schrift (für keine unerwünschten Nebeneffekte)
            Font originalFont = label.Font;

            label.MouseEnter += (s, e) =>
            {
                label.Font = new Font(originalFont, FontStyle.Bold); // Hover-Effekt mit "fetter" Schrift
            };

            label.MouseLeave += (s, e) =>
            {
                label.Font = originalFont; // Original-Schrift
            };
        }

        // Initalisierung Radio-Button auf Basis "Kontaktart"
        private void InitializationTypeOfContact()
        {
            if (typeOfContactNew == "mitarbeiter")
            {
                RdbCreatKntktMa.Checked = true;

                // Automatische Generierung Mitarbeiter Nr. (gemäss JSON)
                string employeeNumberNew = EmployeeNumber.GetEmployeeNumberNext();
                TxtCreatKntktMaManr.Text = employeeNumberNew;
            }
            
            else if (typeOfContactNew == "kunde")
            {
                RdbCreatKntktKunde.Checked = true;
            }
        }

        // Aktualisierung gesperrte Felder auf Basis "Kontaktart"
        private void UpdateTypeOfContact()
        {
            if (RdbCreatKntktMa.Checked)
            {
                typeOfContactNew = "mitarbeiter";
                RdbCreatKntktMa.Checked = true;
                GrpBxDatenMA.Enabled = true;
            }
            
            else if (RdbCreatKntktKunde.Checked)
            {
                typeOfContactNew = "kunde";
                RdbCreatKntktKunde.Checked = true;
                GrpBxDatenMA.Enabled = false;
                CleanGroupFieldEmployees();
            }
        }

        // Bereinigung der Eingabefelder der Gruppe Mitarbeiter (bei Wechsel zu Kunde)
        private void CleanGroupFieldEmployees()
        {
            foreach (Control field in groupFieldEmployees)
            {
                switch (field)
                {
                    case System.Windows.Forms.TextBox txtbxField:
                        txtbxField.Clear();
                        break;
                    case System.Windows.Forms.ComboBox cmbxField:
                        cmbxField.SelectedIndex = -1;
                        break;
                    case DateTimePicker dateField:
                        dateField.Value = new DateTime(1900, 1, 1);
                        break;
                    case NumericUpDown numField:
                        numField.Value = numField.Minimum;
                        break;
                }

                field.BackColor = backColorOK;
                field.Tag = tagOK;
            }
        }

        private void RdbCreatKntktMa_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTypeOfContact();
        }

        private void RdbCreatKntktKunde_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTypeOfContact();
        }

        private void CmdCreateKntktKontaktErstellen_Click(object sender, EventArgs e)
        {
            var checkAndValidation = new CheckAndValidationFields(this);
            var checkAndValidationContent = CheckAndValidationFieldsContent();
            bool checkFieldTag = checkAndValidation.ValidationFields(checkAndValidationContent);

            if (checkFieldTag)
            {
                // Speicherung der Daten in JSON-Datei, falls Duplikatencheck erfolgreich
                if (SaveContactData())
                {
                    this.FormClosed += (s, arg) =>
                    {
                        // Erstellung neues Form "KontaktErstellen"
                        var kontaktErstellenForm = new KontaktErstellen(typeOfContactNew);
                        kontaktErstellenForm.Show();
                    };

                    this.Close();
                }
            }
        }

        private void CmdCreateKntktDashboard_Click(object sender, EventArgs e)
        {
            var checkAndValidation = new CheckAndValidationFields(this);
            var checkAndValidationContent = CheckAndValidationFieldsContent();
            bool checkFieldTag = checkAndValidation.ValidationFields(checkAndValidationContent);

            if (checkFieldTag)
            {
                // Speicherung der Daten in JSON-Datei
                if (SaveContactData())
                {
                    this.Close();
                }
            }
        }

        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationFields"
        private InitializationFields CheckAndValidationFieldsContent()
        {
            return new InitializationFields
            {
                GroupFieldEmployeesAndCustomers = groupFieldEmployeesAndCustomers,
                GroupFieldEmployees = groupFieldEmployees,
                CheckFieldIgnore = CheckFieldIgnore(),
                IsEmployee = RdbCreatKntktMa.Checked,
                IsClient = RdbCreatKntktKunde.Checked,
                Salutation = CmBxCreatKntktAnrede,
                Birthday = DateCreatKntktBirthday,
                Gender = CmBxCreatKntktGeschlecht,
                PLZ = TxtCreatKntktPLZ,
                Email = TxtCreatKntktEmail,
                AHVNumber = TxtCreatKntktMaAHVNr,
                Nationality = TxtCreatKntktMaNationalitaet,
                DateOfEntry = DateCreatKntktEintrDatum
            };
        }
       
        // Speicherung der Kontaktdaten in JSON-Datei
        private bool SaveContactData()
        {
            try
            {
                var contact = new ContactData
                {
                    TypeOfContact = typeOfContactNew
                };

                foreach (Control field in groupFieldEmployeesAndCustomers)
                {
                    contact.Fields[field.Name] = GetControlValue(field);
                }

                if (typeOfContactNew == "mitarbeiter")
                {
                    foreach (Control field in groupFieldEmployees)
                    {
                        contact.Fields[field.Name] = GetControlValue(field);
                    }
                }
                
                // Laden der JSON-Datei (falls vorhanden)
                List<ContactData> contactList = new List<ContactData>();

                if (File.Exists(contactDataPath))
                {
                    string contatcsJSON = File.ReadAllText(contactDataPath);

                    if (!string.IsNullOrWhiteSpace(contatcsJSON))
                    {
                        contactList = JsonSerializer.Deserialize<List<ContactData>>(contatcsJSON) ?? new List<ContactData>();
                    }
                }

                // Duplikatencheck mit Bestätigung durch User (bei Nein "Abbruch")
                if (!CheckDuplicateContact(contactList, contact))
                {
                    return false;
                }
                
                // Hinzufügen neuer Kontakt zur neuen Liste
                contactList.Add(contact);

                // Konvertierung neue Liste in JSON
                string updatedJson = JsonSerializer.Serialize(contactList, new JsonSerializerOptions { WriteIndented = true });
                
                // (Über-)Schreibung der JSON-Datei mit neuer Liste
                File.WriteAllText(contactDataPath, updatedJson);

                // Ausgabe erfolgreiche Speicherung (userfreundlich)
                MessageBox.Show("Kontakt erfolgreich gespeichert!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            catch (Exception exception)
            {
                // Ausgabe Fehler beim Speichern (Ausnahmebehandlung)
                MessageBox.Show($"Fehler beim Speichern:{exception}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Auslesen der Werte für Speicherung der Kontaktdaten in JSON-Datei
        private string GetControlValue(Control field)
        {
            if (field is System.Windows.Forms.TextBox txtbxField)
                return txtbxField.Text;

            if (field is System.Windows.Forms.ComboBox cmbxField)
                return cmbxField.Text;

            if (field is DateTimePicker dateField)
                return dateField.Value.ToString("yyyy-MM-dd");

            if (field is NumericUpDown numField)
                return numField.Value.ToString();

            return string.Empty;
        }

        // Abgleich neuer Kontakt mit bestehenden Kontaktdaten        
        private bool CheckDuplicateContact(List<ContactData> contactList, ContactData newContact)
        {
            // Regex für Split Vorname und Nachname bei Bindestrich und/oder Leerzeichen
            string regex = @"[\s\-]";

            newContact.Fields.TryGetValue("TxtCreatKntktVorname", out var newFirstNameRaw);
            newContact.Fields.TryGetValue("TxtCreatKntktName", out var newLastNameRaw);
            newContact.Fields.TryGetValue("DateCreatKntktBirthday", out var newDateOfBirthRaw);

            string newFirstName = Regex.Split(newFirstNameRaw?.Trim().ToLower() ?? "", regex)[0];
            string newLastName = Regex.Split(newLastNameRaw?.Trim().ToLower() ?? "", regex)[0];
            string newDateOfBirth = newDateOfBirthRaw ?? "";

            // Liste mit allen möglichen Duplikaten (für Anzeige)
            List<string> duplicates = new List<string>();

            foreach (ContactData oldContact in contactList)
            {
                oldContact.Fields.TryGetValue("TxtCreatKntktVorname", out var oldFirstNameRaw);
                oldContact.Fields.TryGetValue("TxtCreatKntktName", out var oldLastNameRaw);
                oldContact.Fields.TryGetValue("DateCreatKntktBirthday", out var oldDateOfBirthRaw);

                string oldFirstName = Regex.Split(oldFirstNameRaw?.Trim().ToLower() ?? "", regex)[0];
                string oldLastName = Regex.Split(oldLastNameRaw?.Trim().ToLower() ?? "", regex)[0];
                string oldDateOfBirth = oldDateOfBirthRaw ?? "";

                // Abgleich nur auf Basis des ersten Namens, falls z.B. noch ein zweiter Name erfasst ist
                if (newFirstName == oldFirstName && newLastName == oldLastName && newDateOfBirth == oldDateOfBirth)
                {
                    duplicates.Add($"- {oldFirstNameRaw} {oldLastNameRaw}, {oldDateOfBirthRaw}");
                }
            }
            
            // Sammelausgabe der ähnlichen Kontakte auf Basis Vorname, Nachname und Geburtsdatum
            if (duplicates.Any())
            {
                string message = "Folgende ähnliche Kontakte existieren bereits:\r\n\r\n" + string.Join("\n", duplicates) + "\r\n\r\nTrotzdem speichern?";
                DialogResult result = MessageBox.Show(message, "Duplikatencheck", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                return result == DialogResult.Yes;
            }
            
            return true;
        }
    }
}