using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZH_3_DO0SKP.Models;

namespace ZH_3_DO0SKP
{
    public partial class UserControlTextbook : UserControl
    {
        TextbookContext context = new TextbookContext();


        public UserControlTextbook()
        {
            InitializeComponent();

            GetStudent();

            GetOrder();

            GetTextbook();
        }

        private void GetTextbook()
        {
            var textbook = from x in context.Textbook
                           where x.Title.Contains(textBox2.Text)
                           select x;
            listBox2.DataSource = textbook.ToList();
            listBox2.DisplayMember = "Title";
        }

        private void GetStudent()
        {
            var student = from x in context.Student
                          where x.Name.Contains(textBox1.Text)
                          select x;
            listBox1.DataSource = student.ToList();
            listBox1.DisplayMember = "Name";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetStudent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetOrder();
        }

        private void GetOrder()
        {
            var valasztott = (Student)listBox1.SelectedItem;
            var order = from x in context.Order
                        where x.StudentFk == valasztott.StudentId
                        select new Hallgato
                        {
                            Title = x.TextbookFkNavigation.Title,
                            Price = x.TextbookFkNavigation.Price,
                            TextbookFK = x.TextbookFk,
                            OrderSK = x.OrderSk,
                        };
            hallgatoBindingSource.DataSource = order.ToList();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            GetTextbook();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            Order o = new Order();
            var valasztottkönyv = (Textbook)listBox2.SelectedItem;
            var valasztotthallgato = (Student)listBox1.SelectedItem;
            o.TextbookFk = valasztottkönyv.TextbookId;
            o.StudentFk = valasztotthallgato.StudentId;
            context.Order.Add(o);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            GetOrder();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            FormDelete formDelete = new FormDelete();
            if (formDelete.ShowDialog() == DialogResult.OK)
            {
                var orderid = ((Hallgato)hallgatoBindingSource.Current).OrderSK;
                var torlendo = (from x in context.Order
                                where x.OrderSk == orderid
                                select x).FirstOrDefault();
                context.Order.Remove(torlendo);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                GetOrder();
            }
            else
            {
                MessageBox.Show("Nem került törlésre elem");
            }
        }

        private void buttonStudentAdd_Click(object sender, EventArgs e)
        {
            FormStudentAdd formStudentAdd = new FormStudentAdd();
            if (formStudentAdd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Sikeres");
                GetStudent();
            }
            else
            {
                MessageBox.Show("Sikertelen");
            }
        }
    }

    public class Hallgato
    {
        public string? Title { get; set; }
        public double? Price { get; set; }
        public int? TextbookFK { get; set; }
        public int? OrderSK { get; set; }

    }
}
