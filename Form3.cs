﻿using System;
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
    public partial class Form3 : Form
    {

        public string infoChangeNumber;
        public int changeIndex;
        
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] PersonInfoArray = System.IO.File.ReadAllLines(infoChangeNumber + ".txt");
            PersonInfoArray[changeIndex] = textBox8.Text;

            FileStream fs = new FileStream(infoChangeNumber + ".txt", FileMode.Create, FileAccess.Write); //create skriver över den gamla filen            
            StreamWriter sw = new StreamWriter(fs);
            
            for (int i = 0; i < PersonInfoArray.Length; i++ )
            {
                sw.WriteLine(PersonInfoArray[i]);
            } 
            sw.Close();
            fs.Close();
            MessageBox.Show("Informationen har ändrats");
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}