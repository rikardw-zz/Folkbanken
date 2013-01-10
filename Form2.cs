using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Folkbanken
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PopulateAccountList();
            FileInfo fi = new FileInfo("kundlista.txt"); //lägger till kundlista
            
            StreamReader sr = fi.OpenText();
            while (!sr.EndOfStream){
            
                listBox1.Items.Add(sr.ReadLine());                                    
            }            
        }

        private void PopulateAccountList() //används för att hämta info/uppdatera lista
        {
            FileStream fs = new FileStream("kontolista.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            while (!sr.EndOfStream) //as long as ! end of stream
            {
                listBox2.Items.Add(sr.ReadLine());//add one item. Item = text per line 
            }
            sr.Close();
            fs.Close();
        }

        private void button3_Click(object sender, EventArgs e) //lägger till konto
        {            
            if (comboBox1.Text == "Privatkonto")
            {
                PrivateAccount NewAccount = new PrivateAccount(); //nytt KONTO, namn på fil beror på val av KUND och kontotyp
                FileStream fs = new FileStream("Privat_" + (listBox1.SelectedItem.ToString() + "_" + (NewAccount.GetAccountNumber()) + ".txt"), FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Privatkonto");
                sw.WriteLine(NewAccount.GetAccountNumber()); //skriver in kontonummer
                sw.WriteLine(NewAccount.GetCurrentMoney()); //skriver in pengar (default = 0)
                sw.Close();
                fs.Close();
                FileStream fs2 = new FileStream("kontolista.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.WriteLine("Privat_" + (listBox1.SelectedItem.ToString() + "_" + (NewAccount.GetAccountNumber())));

                MessageBox.Show("Det nya kontonummret för privatkontot är: " + Convert.ToString(NewAccount.GetAccountNumber()));
                sw2.Close();
                fs2.Close();
            }

            else if (comboBox1.Text == "Framtidskonto")
            {
                FutureAccount NewAccount = new FutureAccount();
                FileStream fs = new FileStream("Framtid_" + (listBox1.SelectedItem.ToString() + "_" + (NewAccount.GetAccountNumber()) + ".txt"), FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Framtidskonto");
                sw.WriteLine(NewAccount.GetAccountNumber());
                sw.WriteLine(NewAccount.GetCurrentMoney());
                sw.Close();
                fs.Close();
                FileStream fs2 = new FileStream("kontolista.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.WriteLine("Framtid_" + (listBox1.SelectedItem.ToString() + "_" + (NewAccount.GetAccountNumber())));

                MessageBox.Show("Det nya kontonummret för framtidskontot är: " + Convert.ToString(NewAccount.GetAccountNumber()));
                sw2.Close();
                fs2.Close();
            }
            else if (comboBox1.Text == "Servicekonto")
            {
                ServiceAccount NewAccount = new ServiceAccount();
                FileStream fs = new FileStream("Service_" + (listBox1.SelectedItem.ToString() + "_" + (NewAccount.GetAccountNumber()) + ".txt"), FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Servicekonto");
                sw.WriteLine(NewAccount.GetAccountNumber());
                sw.WriteLine(NewAccount.GetCurrentMoney());
                sw.Close();
                fs.Close();
                FileStream fs2 = new FileStream("kontolista.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.WriteLine("Service_" + (listBox1.SelectedItem.ToString() + "_" + (NewAccount.GetAccountNumber())));

                MessageBox.Show("Det nya kontonummret för servicekontot är: " + Convert.ToString(NewAccount.GetAccountNumber()));
                sw2.Close();
                fs2.Close();
            }
            else
            {
                MessageBox.Show("Vänligen fyll i vilken typ av konto användaren ska ha");
            }
            listBox2.Items.Clear();                
            PopulateAccountList();                      
        }               

        private void button4_Click(object sender, EventArgs e) //TAR BORT KONTO
        {
            if (listBox2.SelectedItem != null)
            {
                string valtKonto =  listBox2.SelectedItem.ToString();
                string[] kontoArray = System.IO.File.ReadAllLines("kontolista.txt");
                
                for (int i = 0; i < kontoArray.Length; i++) //tar bort filen 903233435.txt
                {
                    if (kontoArray[i] == listBox2.SelectedItem.ToString())
                    {
                        MessageBox.Show(valtKonto + " är nu borttaget");
                        File.Delete(valtKonto + ".txt");
                    }
                }
                var oldInfo = System.IO.File.ReadAllLines("kontolista.txt"); //använde var istället för string. Fungerade inte annars
                var newInfo = kontoArray.Where(line => !line.Contains(valtKonto)); //tar bort valt nummer från kundlista
                //newInfo = kontoArray minus valtKonto
                System.IO.File.WriteAllLines("kontolista.txt", newInfo); //skriver om filen 

                listBox2.Items.Clear();
                PopulateAccountList();
            }
            else 
            {
                MessageBox.Show("Vänligen markera vilket konto du vill ta bort först");
            }
        
        }

        private void button6_Click(object sender, EventArgs e)
        {

            ShowSaldo();       
        }

        private void ShowSaldo()
    
        {
        string[] KontoInfoArray = System.IO.File.ReadAllLines(listBox2.SelectedItem.ToString() + ".txt");
        textBox2.Text = Convert.ToString(KontoInfoArray[2]);        
        }

        private void button1_Click(object sender, EventArgs e) //Sätter in pengar
        {
            string[] KontoInfoArray = System.IO.File.ReadAllLines(listBox2.SelectedItem.ToString() + ".txt");
            //Skapar nytt konto med denna info: Kundnummer_ + Privatkonto_ + KontoNummer
   
            int transaction = int.Parse(textBox1.Text);   
            int moneyAvailible = int.Parse(KontoInfoArray[2]);
                        
            KontoInfoArray[2] = Convert.ToString(moneyAvailible - transaction); //Pengar MINUS vald summa pengar
            System.IO.File.WriteAllLines(listBox2.SelectedItem.ToString() + ".txt", KontoInfoArray);
            ShowSaldo(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] KontoInfoArray = System.IO.File.ReadAllLines(listBox2.SelectedItem.ToString() + ".txt");

            int transaction = int.Parse(textBox1.Text);
            int moneyAvailible = int.Parse(KontoInfoArray[2]);

            KontoInfoArray[2] = Convert.ToString(moneyAvailible + transaction); //Pengar PLUS vald summa pengar
            System.IO.File.WriteAllLines(listBox2.SelectedItem.ToString() + ".txt", KontoInfoArray);
            ShowSaldo(); 
        }
    }
}
