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
            string nazwaPlikuWejsciowego = textBox1.Text;

            int k1 = 0;
            int k1p = 0;
            int k2 = 0;
            int k2p = 0;
            int k3 = 0;
            int k3p = 0;
            int k4 = 0;
            int k4p = 0;
            int k5 = 0;
            int k5p = 0;
            int k6 = 0;
            int k6p = 0;
            int k7 = 0;
            int k7p = 0;
            int k8 = 0;
            int k8p = 0;
            int k9 = 0;
            int k9p = 0;
            int k10 = 0;
            int k10p = 0;
            int k13 = 0;
            int k13p = 0;
            int k14 = 0;
            int k14p = 0;
            int k15 = 0;
            int k15p = 0;
            int k16 = 0;
            int k16p = 0;
            int k17 = 0;
            int k17p = 0;
            int k18 = 0;
            int k18p = 0;
            int k19 = 0;
            int k19p = 0;
            int k20 = 0;
            int k20p = 0;
            int k21 = 0;
            int k21p = 0;
            int k22 = 0;
            int k22p = 0;
            int suma = 0;
            
            int counter = 0;
            int rekord; // nr zestawu danych w linii
            string linia; // ciag znakow do analizy
            char znak = ','; // znak rozdzielajacy dane
            char znak2 = '\''; // char 39 - znak ktory pomijamy '

            string nozzle, windex, serial, concl, testname; // dane otrzymane po analizie linii

            // parametry uzytkownika
            string dysza = textBox5.Text;
            string procedura = textBox11.Text;
            string status = textBox6.Text;
            string odnr = textBox3.Text;
            string donr = textBox4.Text;

            // Read the file it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@nazwaPlikuWejsciowego);
            
            while ((linia = file.ReadLine()) != null)
            {
                counter++;
                textBox8.Text = counter.ToString(); // ? nie odswieza?
                rekord = 1;
                nozzle = "";
                windex = "";
                serial = "";
                concl = "";
                testname = "";

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

                if (concl == status && testname == procedura && nozzle == dysza)
                    if (Int32.Parse(serial) >= Int32.Parse(odnr) && Int32.Parse(serial) <= Int32.Parse(donr))
                    {
                            if (windex == "1")
                                k1++;
                            if (windex == "1.5")
                                k1p++;
                            if (windex == "2")
                                k2++;
                            if (windex == "2.5")
                                k2p++;
                            if (windex == "3")
                                k3++;
                            if (windex == "3.5")
                                k3p++;
                            if (windex == "4")
                                k4++;
                            if (windex == "4.5")
                                k4p++;
                            if (windex == "5")
                                k5++;
                            if (windex == "5.5")
                                k5p++;
                            if (windex == "6")
                                k6++;
                            if (windex == "6.5")
                                k6p++;
                            if (windex == "7")
                                k7++;
                            if (windex == "7.5")
                                k7p++;
                            if (windex == "8")
                                k8++;
                            if (windex == "8.5")
                                k8p++;
                            if (windex == "9")
                                k9++;
                            if (windex == "9.5")
                                k9p++;
                            if (windex == "10")
                                k10++;
                            if (windex == "10.5")
                                k10p++;
                            if (windex == "13")
                                k13++;
                            if (windex == "13.5")
                                k13p++;
                            if (windex == "14")
                                k14++;
                            if (windex == "14.5")
                                k14p++;
                            if (windex == "15")
                                k15++;
                            if (windex == "15.5")
                                k15p++;
                            if (windex == "16")
                                k16++;
                            if (windex == "16.5")
                                k16p++;
                            if (windex == "17")
                                k17++;
                            if (windex == "17.5")
                                k17p++;
                            if (windex == "18")
                                k18++;
                            if (windex == "18.5")
                                k18p++;
                            if (windex == "19")
                                k19++;
                            if (windex == "19.5")
                                k19p++;
                            if (windex == "20")
                                k20++;
                            if (windex == "20.5")
                                k20p++;
                            if (windex == "21")
                                k21++;
                            if (windex == "21.5")
                                k21p++;
                            if (windex == "22")
                                k22++;
                            if (windex == "22.5")
                                k22p++;
                    }
            }
            file.Close();
            
            richTextBox1.Text += "GEAR\tPCS" + "\n";
            richTextBox1.Text += "1,0\t" + k1.ToString() + "\n";
            richTextBox1.Text += "1,5\t" + k1p.ToString() + "\n";
            richTextBox1.Text += "2,0\t" + k2.ToString() + "\n";
            richTextBox1.Text += "2,5\t" + k2p.ToString() + "\n";
            richTextBox1.Text += "3,0\t" + k3.ToString() + "\n";
            richTextBox1.Text += "3,5\t" + k3p.ToString() + "\n";
            richTextBox1.Text += "4,0\t" + k4.ToString() + "\n";
            richTextBox1.Text += "4,5\t" + k4p.ToString() + "\n";
            richTextBox1.Text += "5,0\t" + k5.ToString() + "\n";
            richTextBox1.Text += "5,5\t" + k5p.ToString() + "\n";
            richTextBox1.Text += "6,0\t" + k6.ToString() + "\n";
            richTextBox1.Text += "6,5\t" + k6p.ToString() + "\n";
            richTextBox1.Text += "7,0\t" + k7.ToString() + "\n";
            richTextBox1.Text += "7,5\t" + k7p.ToString() + "\n";
            richTextBox1.Text += "8,0\t" + k8.ToString() + "\n";
            richTextBox1.Text += "8,5\t" + k8p.ToString() + "\n";
            richTextBox1.Text += "9,0\t" + k9.ToString() + "\n";
            richTextBox1.Text += "10,0\t" + k10.ToString() + "\n";
            richTextBox1.Text += "10,5\t" + k10p.ToString() + "\n";
            richTextBox1.Text += "13,0\t" + k13.ToString() + "\n";
            richTextBox1.Text += "13,5\t" + k13p.ToString() + "\n";
            richTextBox1.Text += "14,0\t" + k14.ToString() + "\n";
            richTextBox1.Text += "14,5\t" + k14p.ToString() + "\n";
            richTextBox1.Text += "15,0\t" + k15.ToString() + "\n";
            richTextBox1.Text += "15,5\t" + k15p.ToString() + "\n";
            richTextBox1.Text += "16,0\t" + k16.ToString() + "\n";
            richTextBox1.Text += "16,5\t" + k16p.ToString() + "\n";
            richTextBox1.Text += "17,0\t" + k17.ToString() + "\n";
            richTextBox1.Text += "17,5\t" + k17p.ToString() + "\n";
            richTextBox1.Text += "18,0\t" + k18.ToString() + "\n";
            richTextBox1.Text += "18,5\t" + k18p.ToString() + "\n";
            richTextBox1.Text += "19,0\t" + k19.ToString() + "\n";
            richTextBox1.Text += "19,5\t" + k19p.ToString() + "\n";
            richTextBox1.Text += "20,0\t" + k20.ToString() + "\n";
            richTextBox1.Text += "20,5\t" + k20p.ToString() + "\n";
            richTextBox1.Text += "21,0\t" + k21.ToString() + "\n";
            richTextBox1.Text += "21,5\t" + k21p.ToString() + "\n";
            richTextBox1.Text += "22,0\t" + k22.ToString() + "\n";
            richTextBox1.Text += "22,5\t" + k22p.ToString() + "\n";

            suma = k1 + k1p + k2 + k2p + k3 + k3p + k4 + k4p;
            suma = suma + k5 + k5p + k6 + k6p + k7 + k7p;
            suma = suma + k13 + k13p + k14 + k14p + k15 + k15p + k16 + k16p;
            suma = suma + k17 + k17p + k18 + k18p + k19 + k19p + k20 + k20p;
            suma = suma + k21 + k21p + k22 + k22p;
            richTextBox1.Text += "SUMA:\t" + suma.ToString() + "\n";
            
            //System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.  
            //System.Console.ReadLine();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
