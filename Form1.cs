// TESTADE EN DEL MED KODEN VIA FORM1 TILL EN BÖRJAN, VÄNLIGEN IGNORERA
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
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {          
            PopulateCustomerList();                 
        }

        private void PopulateCustomerList() //Adds items form kundlista.txt to listbox
        {
            FileStream fs = new FileStream("kundlista.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
                        
            while (!sr.EndOfStream) //as long as ! end of stream
            {
               listBox1.Items.Add(sr.ReadLine());//add one item. Item = text per line 
           
            }
            
            sr.Close();
            fs.Close();
        }



        private void button1_Click(object sender, EventArgs e)//Knapp som lägger till folk
        {            
            Customer NewUser = new Customer(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text); //creates variable Customer from Customer.Class Also creater NewUser from said variable        
            string personInfo = Convert.ToString(NewUser.GetId());

            FileStream fs = new FileStream(personInfo + ".txt", FileMode.Append, FileAccess.Write);
            FileStream fs2 = new FileStream("kundlista.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            StreamWriter sw2 = new StreamWriter(fs2);

            //info till kundfil
            sw.WriteLine(NewUser.GetId());
            sw.WriteLine(NewUser.GetForeName());
            sw.WriteLine(NewUser.GetLastName());
            sw.WriteLine(NewUser.GetBirthInfo());
            sw.WriteLine(NewUser.GetStreetAdress());
            sw.WriteLine(NewUser.GetPostAdress());
            sw.WriteLine(NewUser.GetHomePhone());
            sw.WriteLine(NewUser.GetMobilePhone());

            sw2.WriteLine(NewUser.GetId()); //info till kundlista            
            
            sw.Close();
            fs.Close();
            sw2.Close();
            fs2.Close();
            listBox1.Items.Add(NewUser.GetId());
            MessageBox.Show("Informationen har lagts till");            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button6_Click(object sender, EventArgs e) //Knapp för ändring av info
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Vänligen välj typ av kundinformation du vill ändra först");
            }
            else
            {
                if (listBox1.SelectedItem != null)
                {
                    string valdKund = listBox1.SelectedItem.ToString();
                    string[] PersonInfoArray = System.IO.File.ReadAllLines(valdKund + ".txt");

                    Form3 form3 = new Form3();
                    form3.infoChangeNumber = valdKund;
                    form3.Show();

                    form3.label2.Text = PersonInfoArray[0]; //skickar info till labels i Form3
                    form3.textBox1.Text = PersonInfoArray[1];
                    form3.textBox2.Text = PersonInfoArray[2];
                    form3.textBox3.Text = PersonInfoArray[3];
                    form3.textBox4.Text = PersonInfoArray[4];
                    form3.textBox5.Text = PersonInfoArray[5];
                    form3.textBox6.Text = PersonInfoArray[6];
                    form3.textBox7.Text = PersonInfoArray[7];

                    if (comboBox1.Text == "Förnamn") //ifsats för vilken information som skall ändras
                    {
                        form3.textBox8.Text = PersonInfoArray[1];
                        form3.changeIndex = 1; //changeindex = informationen som ska ändras
                    }
                    else if (comboBox1.Text == "Efternamn")
                    {
                        form3.textBox8.Text = PersonInfoArray[2];
                        form3.changeIndex = 2;

                    }
                    else if (comboBox1.Text == "Personnummer")
                    {
                        form3.textBox8.Text = PersonInfoArray[3];
                        form3.changeIndex = 3;
                    }
                    else if (comboBox1.Text == "Gatuadress")
                    {
                        form3.textBox8.Text = PersonInfoArray[4];
                        form3.changeIndex = 4;
                    }
                    else if (comboBox1.Text == "Postadress")
                    {
                        form3.textBox8.Text = PersonInfoArray[5];
                        form3.changeIndex = 5;
                    }
                    else if (comboBox1.Text == "Hemtelefon")
                    {
                        form3.textBox8.Text = PersonInfoArray[6];
                        form3.changeIndex = 6;
                    }
                    else if (comboBox1.Text == "Mobiltelefon")
                    {
                        form3.textBox8.Text = PersonInfoArray[7];
                        form3.changeIndex = 7;
                    }
                }
                else 
                {
                    MessageBox.Show("Vänligen välj vilken kund du vill ändra informationen på först");
                }

            }            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }//end button

        private void GetChangedInfo()
        {
            // is supposed to get info from textbox in form 3

        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //******************************************************
            //Knapp som får värde från val i listbox1, sedan tar bort fil med samma namn samt info från kundlista.txt
            string valdKund = listBox1.SelectedItem.ToString();
            string[] kundArray = System.IO.File.ReadAllLines("kundlista.txt");

            for (int i = 0; i < kundArray.Length; i++) //tar bort filen 903233435.txt
            {
                if (kundArray[i] == listBox1.SelectedItem.ToString())
                {
                    File.Delete(valdKund + ".txt");                                   
                }           
            }
            //var oldInfo = System.IO.File.ReadAllLines("kundlista.txt");
            var newInfo = kundArray.Where(line => !line.Contains(valdKund)); //tar bort valt Personummer från kundlista
            System.IO.File.WriteAllLines("kundlista.txt", newInfo); //skriver om filen utan Personummer
            
            
            listBox1.Items.Clear(); //tar bort alla objekt i listbox  
            PopulateCustomerList();  
                                        
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }     
    }        
} 
  
    


