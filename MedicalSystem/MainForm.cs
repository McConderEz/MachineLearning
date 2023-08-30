using CodeBlogMachineLearning1;

namespace MedicalSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var pictureConverter = new PictureConverter();
                var inputs = pictureConverter.Convert(openFileDialog.FileName);
                var result = Program.Controller.ImageNetwork.Predict(inputs).Output;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var enterDataForm = new EnterData();
            var result = enterDataForm.ShowForm();
        }
    }
}