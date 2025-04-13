using System.Runtime.Versioning;

namespace Houd_s_PC_Roulette
{
    public partial class Roulette : Form
    {
        public Roulette()
        {
            InitializeComponent();
            PictureBox title = new PictureBox();
            title.Image = Properties.Resources.Title;
            title.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            title.SetBounds(Width / 2 - 197, 24, 394, 223);
            title.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(title);
            Button b = new Button();
            b.Image = Properties.Resources.Play;
            b.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            b.SetBounds(Width / 2 - 125, Height - 140, 250, 96);
            SuspendLayout();
            this.Controls.Add(b);
            ResumeLayout();
            b.Click += new EventHandler(OnClick);
        }

        private void Roulette_Load(object sender, EventArgs e)
        {
            
        }

        private void OnClick(object? sender, EventArgs e)
        {
            Game game = new Game();
            game.Show();
        }
    }
}
