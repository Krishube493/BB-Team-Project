using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Media;

namespace BrickBreaker
{
    
    public partial class Form1 : Form
    {        
        public static List<HighScore> highScores = new List<HighScore>();
        /// <summary>
        /// The Game's Soundplayer.
        /// Note that remember that playSoundFrom and preloadSound will save lines of code when used
        /// </summary>
        public static SoundPlayer SoundPlayer = new SoundPlayer();
        public static GameScreen currentGame;
        public Form1()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(Program.FilePath);//Set the program to put files in the created directory
            Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            Location = new Point(0, 0);
        }

        /// <summary>
        /// Sets the screen height and sizes
        /// </summary>
        /// <param name="c"></param>
        [Obsolete("TODO Game Screen scales incorrectly")]
        public void ConfigScreen(ref UserControl c)
        {
            c.Width = (Width / 3) * 2;
            c.Height = (Height / 3) * 2;
            c.Location = new Point(Width / 2 - c.Width /2, Height / 2  - c.Height /2);
        }

        /// <summary>
        /// Gets a .wav file from the file path and plays it
        /// </summary>
        /// <param name="filePath"></param>
        public static void playSoundFrom(string filePath)
        {
            if (File.Exists(filePath))
            {
                SoundPlayer.SoundLocation = filePath;
                SoundPlayer.PlaySync();
            }
            else
                throw new Exception("Could not find the required file");
        }

        /// <summary>
        /// Gets the .wav file from specified location and loads it into the soundplayer
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool preloadSound(string filePath)
        {
            if(File.Exists(filePath))
            {
                SoundPlayer.SoundLocation = filePath;
                SoundPlayer.Load();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes the displaying screen and deletes the last screen
        /// </summary>
        /// <param name="remove"></param>
        /// <param name="add"></param>
        public void ChangeScreen(UserControl remove, UserControl add)
        {
            ConfigScreen(ref add);
            Controls.Add(add);
            Controls.Remove(remove);
            if(remove != null)
                remove.Dispose();

        }

        /// <summary>
        /// When the form finishes loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeScreen(null, new StartScreen());
        }
    }
}
