﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akhn_8
{
    static class Constants
    {
        public const string NAZWA_APL = "Akhn 8.1 build 0203";
    }
    
    public class GearTable
    {
        public string namePos { get; set; }
        public int sumPos { get; set; }

        public GearTable(string snamePos, int nsumPos)
        {
            namePos = snamePos;
            sumPos = nsumPos;
        }
    }
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void CzyscWyjscie()
        {
            richTextBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                CzyscWyjscie();
                // textBox8.Text = "";  // ?
            }

            //richTextBox1.Text += "Proszę czekać, to chwilę potrwa ...\n\n";

            List<GearTable> nGear = new List<GearTable>();
            // nGear.Add(new GearTable("1", 0));
            // nGear.Add(new GearTable("1.5", 0));

            int ok = 0;
            string linia;
            string nazwaPlikuConfig = "geartype.txt";
            
            if (!System.IO.File.Exists(nazwaPlikuConfig))
            {
                richTextBox1.Text += "Brak dostępu do pliku " + nazwaPlikuConfig + " !\n";
                ok++;
            }
            else
            {
                // Read the file it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(@nazwaPlikuConfig);
                while ((linia = file.ReadLine()) != null)
                {
                    if (linia[0] != '/') nGear.Add(new GearTable(linia, 0));
                }
                file.Close();
            }

            richTextBox1.Text += "Pozycji : " + nGear.Count + "\n"; // ile

            // parametry uzytkownika
            string nazwaPlikuWejsciowego = textBox1.Text;
            string dysza = textBox5.Text;
            string procedura = textBox11.Text;
            string status = textBox6.Text;
            string odnr = textBox3.Text;
            string donr = textBox4.Text;

            int serialInt, odnrInt, donrInt;

            bool czyZliczac = true; // na false po zakończeniu tabeli results (unikam zliczania z tresults)
            bool czyZliczacTymRazem = true;

            if (nGear.Count == 0)
            {
                richTextBox1.Text += "Brak danych dot. szukanych kół w pliku " + nazwaPlikuConfig +"\n";
                ok++;
            }

            czyZliczacTymRazem = Int32.TryParse(odnr, out odnrInt);
            if (odnr.Length != 8 || czyZliczacTymRazem==false)
            {
                richTextBox1.Text += "Sprawdź nr seryjny OD!\n";
                ok++;
            }

            czyZliczacTymRazem = Int32.TryParse(donr, out donrInt);
            if (donr.Length != 8 || czyZliczacTymRazem == false)
            {
                richTextBox1.Text += "Sprawdź nr seryjny DO!\n";
                ok++;
            }

            if (odnr[0] != donr[0] || odnr[1] != donr[1])
            {
                richTextBox1.Text += "Sprawdź czy numery seryjne są w jednej grupie!\n";
                ok++;
            }

            if (odnrInt > donrInt)
            {
                richTextBox1.Text += "Sprawdź numery seryjne (numer OD > numeru DO)!\n";
                ok++;
            }

            if (!System.IO.File.Exists(nazwaPlikuWejsciowego))
            {
                richTextBox1.Text += "Brak dostępu do pliku " + nazwaPlikuWejsciowego + "!\n";
                ok++;
            }

            if (ok==0)
            {
                int suma = 0;
                int counter = 0;
                int rekord; // nr zestawu danych w linii
                char znak = ','; // znak rozdzielajacy dane
                char znak2 = '\''; // char 39 - znak ktory pomijamy '
                string nozzle, windex, serial, concl, testname; // dane otrzymane po analizie linii
                
                // Read the file it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(@nazwaPlikuWejsciowego);
                while ((linia = file.ReadLine()) != null)
                {
                    counter++;
                    // textBox8.Text = counter.ToString(); // ? nie odswieza?
                    rekord = 1;
                    nozzle = "";
                    windex = "";
                    serial = "";
                    concl = "";
                    testname = "";

                    if (linia == "/*!40000 ALTER TABLE `results` ENABLE KEYS */;") czyZliczac = false;
                    for (int i = 0; i <= linia.Length - 1; i++)
                    {
                        if (linia[i] == znak) rekord++;
                        // `ProdN`,`POS`,`Tnum`,`TType`,`dP`,`Tx1`,`Tx2`,`T1`,`H1`,`P2`,`dPx0`,`Nozzle`,`MUTimp`,`TTime`,`PulseT`,`PulseR`,`Vref`,`Vmut`,`Error`,`Cerror`,`wIndex`,`Concl`,`Reason`,`Serial`,`Protocol`,`Year`,`MUT`,`Flow`,`Test_name`,`T_id`,`dP_ave`
                        if (rekord == 12) if (linia[i] != znak) nozzle = nozzle + linia[i]; // Nozzle
                        if (rekord == 21) if (linia[i] != znak) windex = windex + linia[i]; // wIndex
                        if (rekord == 22) if (linia[i] != znak && linia[i] != znak2) concl = concl + linia[i]; // Concl
                        if (rekord == 24) if (linia[i] != znak && linia[i] != znak2) serial = serial + linia[i]; // Serial
                        if (rekord == 29) if (linia[i] != znak && linia[i] != znak2) testname = testname + linia[i]; // Test_name
                    }
                    // spr czy serial jest poprawny
                    czyZliczacTymRazem = Int32.TryParse(serial, out serialInt);

                    if (czyZliczac==true && czyZliczacTymRazem == true && concl == status && testname == procedura && nozzle == dysza)
                        if (serialInt >= odnrInt && serialInt <= donrInt)
                        {
                            //if (windex == "1") k1++;
                            foreach (GearTable nowy in nGear)
                            {
                                if (windex == nowy.namePos)
                                {
                                    nowy.sumPos++;
                                    break;
                                }
                            }
                        }
                }
                file.Close();

                richTextBox1.Text += "GEAR\tPCS" + "\n";

                // if ((k1 == 0 && checkBox2.Checked) || k1 > 0) richTextBox1.Text += "1,0\t" + k1.ToString() + "\n";
                foreach (GearTable nowy in nGear)
                {
                    if (nowy.sumPos > 0 || checkBox2.Checked)
                    {
                        richTextBox1.Text += nowy.namePos + "\t" + nowy.sumPos.ToString() + "\n";
                        suma += nowy.sumPos;
                        // suma = k1 + k1p + k2 + k2p + k3 + k3p + k4 + k4p;
                    }
                }
                richTextBox1.Text += "SUMA:\t" + suma.ToString() + "\n";
            }
            else
            {
                richTextBox1.Text += "Błędy :  " + ok.ToString() + "\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CzyscWyjscie();
        }
    }
}
