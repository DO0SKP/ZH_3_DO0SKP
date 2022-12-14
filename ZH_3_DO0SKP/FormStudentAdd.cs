using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZH_3_DO0SKP.Models;

namespace ZH_3_DO0SKP
{
    public partial class FormStudentAdd : Form
    {
        TextbookContext context = new TextbookContext();

        public FormStudentAdd()
        {
            InitializeComponent();
        }

        private bool CheckName(string nev)
        {
            return !string.IsNullOrEmpty(nev);
        }

        private bool CheckNeptun(string neptun)
        {
            Regex r = new Regex("[A-Z0-9]{6}$");
            return r.IsMatch(neptun);
        }

        private void textBoxNev_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckName(textBoxNev.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxNev,"Nem jó");
            }
        }

        private void textBoxNev_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBoxNev,"");
        }

        private void textBoxNeptun_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckNeptun(textBoxNeptun.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxNeptun,"Nem jó");
            }
        }

        private void textBoxNeptun_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBoxNeptun,"");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                Student s = new Student();
                var nev = textBoxNev.Text;
                var neptun = textBoxNeptun.Text;
                s.Name = nev;
                s.Neptun = neptun;
                var ujid = from x in context.Student
                           select x.StudentId;
                s.StudentId = ujid.ToList().Max() + 1;
                context.Student.Add(s);
                try
                {
                    context.SaveChanges();
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                    this.DialogResult = DialogResult.Cancel;
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
