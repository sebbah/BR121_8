using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
                
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                CzyscWyjscie();
                //TODO: textBox8.Text = "";
            }

            //TODO: richTextBox1.Text += "Proszę czekać, to chwilę potrwa ...\n\n";

            List<GearTable> nGear = new List<GearTable>();
            // nGear.Add(new GearTable("1", 0));
            // nGear.Add(new GearTable("1.5", 0));

            int iloscBledow = 0;
            string linia;
            string nazwaPlikuConfig = "geartype.txt";

            if (!System.IO.File.Exists(nazwaPlikuConfig))
            {
                richTextBox1.Text += "Brak dostępu do pliku " + nazwaPlikuConfig + " !\n";
                iloscBledow++;
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

            richTextBox1.Text += "Załadowano " + nGear.Count + " pozycji do wyszukiwania.\n"; // ile typów kol jest szukanych.

            // parametry uzytkownika
            string nazwaPlikuWejsciowego, dysza, procedura, status, odnr, donr;
            nazwaPlikuWejsciowego = textBox1.Text;
            dysza = textBox5.Text;
            procedura = textBox11.Text;
            status = textBox6.Text;
            odnr = textBox3.Text;
            donr = textBox4.Text;

            int serialInt, odnrInt, donrInt;

            bool czyZliczac = true; // zostaje przestawione na false po zakończeniu tabeli results (unikam zliczania z tresults)
            bool czyZliczacTymRazem = true;

            if (nGear.Count == 0)
            {
                richTextBox1.Text += "Brak danych dot. szukanych kół w pliku " + nazwaPlikuConfig + "\n";
                iloscBledow++;
            }

            czyZliczacTymRazem = Int32.TryParse(odnr, out odnrInt);
            if (odnr.Length != 8 || czyZliczacTymRazem == false)
            {
                richTextBox1.Text += "Sprawdź nr seryjny OD!\n";
                iloscBledow++;
            }

            czyZliczacTymRazem = Int32.TryParse(donr, out donrInt);
            if (donr.Length != 8 || czyZliczacTymRazem == false)
            {
                richTextBox1.Text += "Sprawdź nr seryjny DO!\n";
                iloscBledow++;
            }

            if (odnr[0] != donr[0] || odnr[1] != donr[1])
            {
                richTextBox1.Text += "Sprawdź czy numery seryjne są w jednej grupie!\n";
                iloscBledow++;
            }

            if (odnrInt > donrInt)
            {
                richTextBox1.Text += "Sprawdź numery seryjne (numer OD > numeru DO)!\n";
                iloscBledow++;
            }

            if (!System.IO.File.Exists(nazwaPlikuWejsciowego))
            {
                richTextBox1.Text += "Brak dostępu do pliku " + nazwaPlikuWejsciowego + "!\n";
                iloscBledow++;
            }

            if (iloscBledow == 0)
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
                    //TODO: textBox8.Text = counter.ToString();
                    rekord = 1;
                    nozzle = "";
                    windex = "";
                    serial = "";
                    concl = "";
                    testname = "";

                    if (linia == Declarations.KONIEC_DANYCH) czyZliczac = false;
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

                    if (czyZliczac == true && czyZliczacTymRazem == true && concl == status && testname == procedura && nozzle == dysza)
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
                        // suma = k1 + k1p + k2 + ...;
                    }
                }
                richTextBox1.Text += "SUMA:\t" + suma.ToString() + "\n";
                textBox8.Text = counter.ToString();
            }
            else
            {
                richTextBox1.Text += "Błędy :  " + iloscBledow.ToString() + "\n";
            }
        }

        private void CzyscWyjscie() => richTextBox1.Text = "";
        
        private void button2_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            CzyscWyjscie();
        }
    }
}
