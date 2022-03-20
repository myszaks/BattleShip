using System;
using System.Windows;

namespace BattleShips
{
    public partial class Message : Window
    {
        public Message(MainWindow m, string caption, string message)
        {
            InitializeComponent();

            this.Owner = m;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            tbCaption.Text = caption;
            tbMessage.Inlines.Add(message);
        }

        private void Ok(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}