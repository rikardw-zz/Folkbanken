using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; //makes binafyformatter work

namespace Folkbanken
{
    public partial class Form1 : Form
    {
        private List<Customer> CustomerLista = new List<Customer>();
        private Customer CustomerVal = new Customer();
        private Account AccountVal = new Account();

        public Form1()
        {
            InitializeComponent();
            UpdatePeople();
        }


       private void button1_Click(object sender, EventArgs e) //lägger till en ny Customer till listan
        {
            Customer newCustomer = new Customer { foreName = textBox1.Text, lastName = textBox2.Text, birthInfo = textBox3.Text, streetAdress = textBox4.Text, postAdress = textBox5.Text, homePhone = textBox6.Text, mobilePhone = textBox7.Text};
            //creates new Customer from Customer class, sending info to get; set;
            CustomerLista.Add(newCustomer); //adds items to list
            CustomerVal = newCustomer; //sätter CustomerVal till senast skapta Customer.                      
            saveCustomers();
            UpdatePeople();
        }
 /*
        private void Form1_Load(object sender, EventArgs e) //Startar i början, hämtar info från lista
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("Customer.bin", FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            fs.Close();


        }

        private void button3_Click(object sender, EventArgs e) //skapar konto
        {
            Konto nyttKonto = new Konto { money = textBox4.Text, kontoTyp = textBox5.Text };
            CustomerVal.skaffaKonto(nyttKonto);
            FileStream fs = new FileStream("Customer.bin", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, CustomerLista); //adds info from CustomerLista
            fs.Close();
            GetPeople();

        }

       */
       private void UpdatePeople()
        {
            listBox1.Items.Clear();
            FileStream fs = new FileStream("Customer.bin", FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                CustomerLista = (List<Customer>)bf.Deserialize(fs); //hämtar alla Customers från listan
            }
            catch
            {
                MessageBox.Show("Hittade inga kunder");
            }
            fs.Close();
            for (int i = 0; i < CustomerLista.Count(); i++)
            {
                listBox1.Items.Add(CustomerLista[i]);
            }
        }

       private void UpdateAccounts() 
       {
           listBox2.Items.Clear();
           CustomerVal = (Customer)listBox1.SelectedItem;

           for (int i = 0; i < CustomerVal.privateAccounts.Count(); i++)
           {
               listBox2.Items.Add(CustomerVal.privateAccounts[i]);
           }
           for (int i = 0; i < CustomerVal.futureAccounts.Count(); i++)
           {
               listBox2.Items.Add(CustomerVal.futureAccounts[i]);
           }
           for (int i = 0; i < CustomerVal.serviceAccounts.Count(); i++)
           {
               listBox2.Items.Add(CustomerVal.serviceAccounts[i]);
           } 
       }

          private void button4_Click(object sender, EventArgs e)
          {
              //Hämtar vilken kund som är vald, tar bort den från listan och skriver om den nya listan på bin-fil
              CustomerLista.Remove(CustomerVal);
              FileStream fs = new FileStream("Customer.bin", FileMode.Open, FileAccess.Write);
              BinaryFormatter bf = new BinaryFormatter();
              bf.Serialize(fs, CustomerLista);   
              fs.Close();
              UpdatePeople();
          }

          private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
          {
              CustomerVal = (Customer)listBox1.SelectedItem;
              UpdateAccounts();
              // lägger till info från customerval
              textBox1.Text = CustomerVal.foreName;
              textBox2.Text = CustomerVal.lastName;
              textBox3.Text = CustomerVal.birthInfo;
              textBox4.Text = CustomerVal.streetAdress;
              textBox5.Text = CustomerVal.postAdress;
              textBox6.Text = CustomerVal.homePhone;
              textBox7.Text = CustomerVal.mobilePhone;
          }

          private void button2_Click(object sender, EventArgs e)
          {
              CustomerVal = (Customer)listBox1.SelectedItem;
              if (comboBox1.SelectedItem.ToString() == "Privatkonto")
              {
                  PrivateAccount pa = new PrivateAccount();
                  CustomerVal.addPrivateAccount(pa);
              }
              else if (comboBox1.SelectedItem.ToString() == "Framtidskonto")
              {
                  FutureAccount fa = new FutureAccount();
                  CustomerVal.addFutureAccount(fa);
              
              }
              else if (comboBox1.SelectedItem.ToString() == "Servicekonto")
              {
                  ServiceAccount sa = new ServiceAccount();
                  CustomerVal.addServiceAccount(sa);              
              }
              saveCustomers();
              UpdateAccounts();
          }

          private void saveCustomers() {
              FileStream fs = new FileStream("Customer.bin", FileMode.OpenOrCreate, FileAccess.Write);
              BinaryFormatter bf = new BinaryFormatter();
              bf.Serialize(fs, CustomerLista); //adds info from CustomerLista  
              fs.Close();
          }

          private void button7_Click(object sender, EventArgs e) //knapp för att ta bort konton
          {
              string acType = listBox2.SelectedItem.GetType().ToString();
              if (acType == "Folkbanken.PrivateAccount") 
              {
                  PrivateAccount pa = (PrivateAccount)listBox2.SelectedItem;
                  CustomerVal.privateAccounts.Remove(pa);
              }
              if (acType == "Folkbanken.FutureAccount")
              {
                  FutureAccount pa = (FutureAccount)listBox2.SelectedItem;
                  CustomerVal.futureAccounts.Remove(pa);
              }
              if (acType == "Folkbanken.ServiceAccount")
              {
                  ServiceAccount pa = (ServiceAccount)listBox2.SelectedItem;
                  CustomerVal.serviceAccounts.Remove(pa);
              }
              saveCustomers();
              UpdateAccounts();

          }

          private void button3_Click(object sender, EventArgs e)
          {
              //skriver om fil beroende på info i textboxar

              CustomerVal.foreName = textBox1.Text;
              CustomerVal.lastName = textBox2.Text;
              CustomerVal.birthInfo = textBox3.Text;
              CustomerVal.streetAdress = textBox4.Text;
              CustomerVal.postAdress = textBox5.Text;
              CustomerVal.homePhone = textBox6.Text;
              CustomerVal.mobilePhone = textBox7.Text;

              saveCustomers();
              UpdatePeople();

          }

          private void button5_Click(object sender, EventArgs e)
          {
              AccountVal.DepositMoney(Convert.ToDouble(textBox8.Text));
              saveCustomers();
              UpdateAccounts();
          }

          private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
          {
              AccountVal = (Account)listBox2.SelectedItem;
          }

          private void button6_Click(object sender, EventArgs e)
          {
              AccountVal.WithdrawMoney(Convert.ToDouble(textBox8.Text));
              saveCustomers();
              UpdateAccounts();
          }
    }
}
