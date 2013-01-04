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

        private void PopulateAccountList()
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

        private void button3_Click(object sender, EventArgs e)
        {            
            if (comboBox1.Text == "Privatkonto")
            {
                PrivateAccount NewAccount = new PrivateAccount();
                FileStream fs = new FileStream("Privat_" + (listBox1.SelectedItem.ToString() + "_" + (NewAccount.GetAccountNumber()) + ".txt"), FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Privatkonto");
                sw.WriteLine(NewAccount.GetAccountNumber());
                sw.WriteLine(NewAccount.GetCurrentMoney());
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


        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            PopulateAccountList();
            
            string valdKund = "";
            string[] kundArray = System.IO.File.ReadAllLines("kontolista.txt");
            for (int i = 0; i < kundArray.Length; i++) //will crash if no item selected though
            {

                if (kundArray[i] == listBox1.SelectedItem.ToString()) //if kundArray[] == selected item on list
                {
                    valdKund = kundArray[i]; //set kundInfo to kundArray[i] (ex: 8501017852)
                }
            }
            //tar reda på vilken kund som är vald i listbox           
 

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string valtKonto = "";
                string[] kontoArray = System.IO.File.ReadAllLines("kontolista.txt");

                valtKonto = listBox2.SelectedItem.ToString();

                for (int i = 0; i < kontoArray.Length; i++) //tar bort filen 903233435.txt
                {
                    if (kontoArray[i] == listBox2.SelectedItem.ToString())
                    {
                        MessageBox.Show(valtKonto + " är nu borttaget");
                        File.Delete(valtKonto + ".txt");
                    }
                }
                var oldInfo = System.IO.File.ReadAllLines("kontolista.txt");
                var newInfo = kontoArray.Where(line => !line.Contains(valtKonto)); //tar bort valt nummer från kundlista
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

        private void button1_Click(object sender, EventArgs e)
        {
            string[] KontoInfoArray = System.IO.File.ReadAllLines(listBox2.SelectedItem.ToString() + ".txt");
   
            int transaction = int.Parse(textBox1.Text);   
            int moneyAvailible = int.Parse(KontoInfoArray[2]);
                        
            KontoInfoArray[2] = Convert.ToString(moneyAvailible - transaction);
            System.IO.File.WriteAllLines(listBox2.SelectedItem.ToString() + ".txt", KontoInfoArray);
            ShowSaldo(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] KontoInfoArray = System.IO.File.ReadAllLines(listBox2.SelectedItem.ToString() + ".txt");

            int transaction = int.Parse(textBox1.Text);
            int moneyAvailible = int.Parse(KontoInfoArray[2]);

            KontoInfoArray[2] = Convert.ToString(moneyAvailible + transaction);
            System.IO.File.WriteAllLines(listBox2.SelectedItem.ToString() + ".txt", KontoInfoArray);
            ShowSaldo(); 
        }
    }
}
