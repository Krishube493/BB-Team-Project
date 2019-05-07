using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace BrickBreaker
{
    public partial class MenuScreen : UserControl
    {
        public void scoreOutput()
        {
            highscoreLabel.Text = "High Scores\n";
            //testing: displaying the scores
            foreach (HighScore s in Form1.highScores)
            {

                highscoreLabel.Text += s.score + " " + "\n";

                //highscoreLabel.Text = s.score[0] + " " + "\n" + s.score[1] + " " + "\n" + s.score[2]
                //    + " " + "\n" + s.score[3] + " " + "\n" + s.score[4] + " " + "\n";
            }
        }
        //testing
        public void loadScoresRK()
        {
            //creating Xml reader file 
            XmlReader reader = XmlReader.Create("Resources/HighScores.xml", null);
            string newScoreString;

            //basically highScore1String is going to be highScore #1...and on...etc
            //plan: "highScores" should only contain 5 high "scores"

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    newScoreString = reader.ReadString();

                    HighScore newScore = new HighScore(newScoreString);
                    Form1.highScores.Add(newScore);

                }
            }

            reader.Close();
        }
        private static int index = 0;
        private List<Label> labels = new List<Label>();
        public MenuScreen()
        {
            InitializeComponent();
            //
            loadScoresRK();
            //compare the scores
            if (Form1.highScores.Count() == 2)
            {
                if (Form1.highScores[0] == Form1.highScores[1]) { Form1.highScores.RemoveAt(1); }
            }
            else if (Form1.highScores.Count() == 3)
            {
                if (Form1.highScores[0] == Form1.highScores[1]) { Form1.highScores.RemoveAt(1); }
                if (Form1.highScores[0] == Form1.highScores[2]) { Form1.highScores.RemoveAt(2); }

                if (Form1.highScores[1] == Form1.highScores[2]) { Form1.highScores.RemoveAt(2); }
            }
            else if (Form1.highScores.Count() == 4)
            {
                if (Form1.highScores[0] == Form1.highScores[1]) { Form1.highScores.RemoveAt(1); }
                if (Form1.highScores[0] == Form1.highScores[2]) { Form1.highScores.RemoveAt(2); }
                if (Form1.highScores[0] == Form1.highScores[3]) { Form1.highScores.RemoveAt(3); }

                if (Form1.highScores[1] == Form1.highScores[2]) { Form1.highScores.RemoveAt(2); }
                if (Form1.highScores[1] == Form1.highScores[3]) { Form1.highScores.RemoveAt(3); }

                if (Form1.highScores[2] == Form1.highScores[3]) { Form1.highScores.RemoveAt(3); }
            }
            else if (Form1.highScores.Count() == 5)
            {
                if (Form1.highScores[0] == Form1.highScores[1]) { Form1.highScores.RemoveAt(1); }
                if (Form1.highScores[0] == Form1.highScores[2]) { Form1.highScores.RemoveAt(2); }
                if (Form1.highScores[0] == Form1.highScores[3]) { Form1.highScores.RemoveAt(3); }
                if (Form1.highScores[0] == Form1.highScores[4]) { Form1.highScores.RemoveAt(4); }

                if (Form1.highScores[1] == Form1.highScores[2]) { Form1.highScores.RemoveAt(2); }
                if (Form1.highScores[1] == Form1.highScores[3]) { Form1.highScores.RemoveAt(3); }
                if (Form1.highScores[1] == Form1.highScores[4]) { Form1.highScores.RemoveAt(4); }

                if (Form1.highScores[2] == Form1.highScores[3]) { Form1.highScores.RemoveAt(3); }
                if (Form1.highScores[2] == Form1.highScores[4]) { Form1.highScores.RemoveAt(4); }

                if (Form1.highScores[3] == Form1.highScores[4]) { Form1.highScores.RemoveAt(4); }
            }


            //Sorting the code
            Form1.highScores.Sort(delegate (HighScore x, HighScore y)
            {
                return x.score.CompareTo(y.score);
            });

            //removing scores at destined number 
            if (Form1.highScores.Count() > 5)
            {
                Form1.highScores.RemoveAt(5);
            }

            //showing the score 
            scoreOutput();

            //Adding Labels to Label list for easy management
            //
            labels.Add(titleLabel);
            labels.Add(playLabel);
            labels.Add(highscoreLabel);
            labels.Add(exitLabel);
        }

        public static void ChangeScreen(UserControl current, string next)
        {
            //f is set to the form that the current control is on
            Form f = current.FindForm();
            f.Controls.Remove(current);
            UserControl ns = null;

            ///If any screens, (UserControls), are added to the program they need to
            ///be added within this switch block as well.
            switch (next)
            {
                case "MenuScreen":
                    ns = new MenuScreen();
                    break;
                case "GameScreen":
                    ns = new GameScreen();
                    break;
            }
            ns.Location = new Point((f.Width - ns.Width) / 2, (f.Height - ns.Height) / 2);
            f.Controls.Add(ns);
            ns.Focus();
        }

        /// <summary>
        /// Only for buttons made by Thayen
        /// </summary>
        private void gainFocus(object sender, EventArgs e)
        {
            var label = sender as Label;
            label.ForeColor = Color.Red;
        }

        /// <summary>
        /// Only for buttons made by Thayen
        /// </summary>
        private void lostFocus(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.ForeColor = Color.Black;
        }

        /// <summary>
        /// The event code for when the exist button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// The event code for when the play button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender, EventArgs e)
        {
            // Goes to the game screen
            var form = FindForm() as Form1;
            form.ChangeScreen(this, new GameScreen());
        }        
       
        /// <summary>
        /// The event code for when the Menu Screen finishes initializing loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuScreen_Load(object sender, EventArgs e)
        {
            var ratio = 6;
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Size = new Size(Width / ratio + 125, Height / ratio);
                var space = (Height /4) - labels[i].Height + (100 * i);
                labels[i].Location = new Point((Width / 2) - (labels[i].Width / 2), space);
            }
        }

        /// <summary>
        /// The event code for when the Main Menu detects a key press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuScreen_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    _newButton(-1).Focus();
                    break;
                case Keys.Down:
                    _newButton(1).Focus();
                    break;
            }
        }

        /// <summary>
        /// Gets the newly selected button based on where the last one was. Pass a negative to go back in the list of buttons
        /// </summary>
        /// <param name="changeInIndex"></param>
        /// <returns></returns>
        private Label _newButton(int changeInIndex)
        {
            //Thayen
            index += changeInIndex;
            //If the button is out of range set the button within range
            if (index < 0)
                index = 0;
            else if (index >= labels.Count)
                index = labels.Count - 1;
            return labels[index];

        }
    }
}
