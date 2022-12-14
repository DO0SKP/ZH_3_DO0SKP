namespace ZH_3_DO0SKP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_megjelenit_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            UserControlTextbook userControlTextbook = new UserControlTextbook();
            panel1.Controls.Add(userControlTextbook);
            userControlTextbook.Dock = DockStyle.Fill;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormExit formExit = new FormExit();
            if (formExit.ShowDialog() == DialogResult.OK)
            {

            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}