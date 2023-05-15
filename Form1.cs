

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Disply_Evo
{

    public partial class Form1 : Form
    {
        private TextBox arraySizeTextBox;
        private TextBox populationAltruisteTextBox;
        private TextBox populationIndividualisteTextBox;
        private TextBox populationFamilialTextBox;
        private TextBox populationRancunierTextBox;
        private TableService tableService = new TableService();
        private Population[,] values;
        private Dictionary<int, Color> colorMap = new Dictionary<int, Color>()
        {
            { 1, Color.Green },
            { 2, Color.Red },
            { 3, Color.LightBlue },
            { 4, Color.Yellow },
            { 5, Color.Orange },
            { 6, Color.Purple },
            { 7, Color.Pink },
            { 8, Color.Brown },
            { 9, Color.Gray },
            { 10, Color.Black }
        };
        private int cellSize = 60;

        

        public Form1()
        {
            
            InitializeComponent();
            //this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            // Bouton pour créer une nouvelle simulation
            Button btnRefresh = new Button();
            btnRefresh.Text = "Refresh";
            btnRefresh.Width = 200;
            btnRefresh.Height = 30;
            btnRefresh.Top = 10;
            btnRefresh.Left = 600;
            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            this.panelArray.Controls.Add(btnRefresh);

            // Bouton pour simuler 10 interactions
            Button btnInteract10 = new Button();
            btnInteract10.Text = "Interact 10";
            btnInteract10.Width = 200;
            btnInteract10.Height = 30;
            btnInteract10.Top = 10;
            btnInteract10.Left = btnRefresh.Right + 10;
            btnInteract10.Click += new EventHandler(btnInteract10_Click);
            this.panelArray.Controls.Add(btnInteract10);


            // Conserver les meilleurs éléments et les fait se reproduire
            Button btnNewGen = new Button();
            btnNewGen.Text = "Kill & Reproduce";
            btnNewGen.Width = 200;
            btnNewGen.Height = 30;
            btnNewGen.Top = 10;
            btnNewGen.Left = btnInteract10.Right + 10;
            btnNewGen.Click += new EventHandler(btnNewGen_Click);
            this.panelArray.Controls.Add(btnNewGen);

            //Paramètres de simulation

            //taille de la simulation (défaut 10 par 10)
            Label arraySizeLabel = new Label();
            arraySizeLabel.Text = "Array Size:";
            arraySizeLabel.Width = 200;
            arraySizeLabel.Top = 10;
            arraySizeLabel.Left = 10;
            this.panelArray.Controls.Add(arraySizeLabel);

            arraySizeTextBox = new TextBox(); 
            arraySizeTextBox.Text = "10";
            arraySizeTextBox.Top = 10;
            arraySizeTextBox.Left = 400;
            arraySizeTextBox.Name = "arraySizeTextBox";
            this.panelArray.Controls.Add(arraySizeTextBox);

            Label populationAltruisteLabel = new Label();
            populationAltruisteLabel.Width = 250;
            populationAltruisteLabel.Text = "Population Altruiste poids:";
            populationAltruisteLabel.Top = 40;
            populationAltruisteLabel.Left = 10;
            this.panelArray.Controls.Add(populationAltruisteLabel);

            populationAltruisteTextBox = new TextBox();
            populationAltruisteTextBox.Text = "50";
            populationAltruisteTextBox.Top = 40;
            populationAltruisteTextBox.Left = 400;
            populationAltruisteTextBox.Name ="populationAltruisteTextBox";
            this.panelArray.Controls.Add(populationAltruisteTextBox);

            Label populationIndividualisteLabel = new Label();
            populationIndividualisteLabel.Width= 250;
            populationIndividualisteLabel.Text = "Population Individualiste poids:";
            populationIndividualisteLabel.Top = 70;
            populationIndividualisteLabel.Left = 10;
            this.panelArray.Controls.Add(populationIndividualisteLabel);

            populationIndividualisteTextBox = new TextBox();
            populationIndividualisteTextBox.Text = "50";
            populationIndividualisteTextBox.Top = 70;
            populationIndividualisteTextBox.Left = 400;
            populationIndividualisteTextBox.Name = "populationIndividualisteTextBox";
            this.panelArray.Controls.Add(populationIndividualisteTextBox);

            Label populationFamilialLabel = new Label();
            populationFamilialLabel.Width = 250;
            populationFamilialLabel.Text = "Population Familial poids:";
            populationFamilialLabel.Top = 100;
            populationFamilialLabel.Left = 10;
            this.panelArray.Controls.Add(populationFamilialLabel);

            populationFamilialTextBox = new TextBox();
            populationFamilialTextBox.Text = "50";
            populationFamilialTextBox.Top = 100;
            populationFamilialTextBox.Left = 400;
            populationFamilialTextBox.Name = "populationFamilialTextBox";
            this.panelArray.Controls.Add(populationFamilialTextBox);

            Label populationRancunierLabel = new Label();
            populationRancunierLabel.Width = 250;
            populationRancunierLabel.Text = "Population Rancunier poids:";
            populationRancunierLabel.Top = 130;
            populationRancunierLabel.Left = 10;
            this.panelArray.Controls.Add(populationRancunierLabel);

            populationRancunierTextBox = new TextBox();
            populationRancunierTextBox.Text = "50";
            populationRancunierTextBox.Top = 130;
            populationRancunierTextBox.Left = 400;
            populationRancunierTextBox.Name = "populationRancunierTextBox";
            this.panelArray.Controls.Add(populationRancunierTextBox);
            
            
            GenerateValues();
            AddLabels();
            ResizePanel();
        }

    


        // Fonction génératrice
        private void GenerateValues()
        {
            int arraySize = int.Parse(this.arraySizeTextBox.Text);
            int poidsA = int.Parse(this.populationAltruisteTextBox.Text);
            int poidsB = poidsA+int.Parse(this.populationIndividualisteTextBox.Text);
            int poidsC = poidsB+int.Parse(this.populationFamilialTextBox.Text);
            int poidsD = poidsC+int.Parse(this.populationRancunierTextBox.Text);
            int denominator = (poidsD);
            
            

            Random random = new Random();
            values = new Population[arraySize, arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    int colorValue = 0;
                    int choix= random.Next(1,denominator+1);
                    if (choix <= poidsA){
                        colorValue = 1;
                    }
                    else if (choix <= poidsB){
                        colorValue = 2;
                    }
                    else if (choix <=poidsC){
                        colorValue = 3;

                    }
                    else if (choix <=poidsD){
                        colorValue = 4 ;
                    }
                    
                    int displayValue = 0;
                    values[i, j] = new Population(colorValue, displayValue, i, j);
                }
            }
        }

        private Label[,] labels;
        private Panel panelArray;

        // affichage des valeurs
        public void AddLabels()
        {


            // Crée les labels si ils ne sont pas déja créés
            int arraySize = int.Parse(arraySizeTextBox.Text);
            if (labels == null)
            {
                labels = new Label[arraySize, arraySize];

                for (int i = 0; i < arraySize; i++)
                {
                    for (int j = 0; j < arraySize; j++)
                    {
                        Label label = new Label();
                        label.AutoSize = false;
                        label.Width = cellSize;
                        label.Height = cellSize;
                        label.TextAlign = ContentAlignment.MiddleCenter;
                        label.Top = i * cellSize + 200; 
                        label.Left = j * cellSize;
                        labels[i, j] = label;
                        panelArray.Controls.Add(label);
                    }
                }
            }

            // Mets a jour les labels
            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    UpdateLabel(labels[i, j], values[i, j]);
                }
            }
        }

        // mise a jour des labels
        private void UpdateLabel(Label label, Population value)
        {
            label.Text = value.points.ToString();

            int colorValue = value.type;
            if (colorMap.ContainsKey(colorValue))
            {
                label.BackColor = colorMap[colorValue];
            }
            else
            {
                label.BackColor = Color.White;
            }
        }

        //Efface les labels
        private void DisposeLabels()
        {
            if (labels != null)
            {
                foreach (Label label in labels)
                {
                    label.Dispose();

                }

                labels = null;


            }


        }

        // rend l'esthétique de la page plus plaisant
        private void ResizePanel()
        {
            int arraySize = int.Parse(arraySizeTextBox.Text);
            int panelWidth = (cellSize + 2) * arraySize + 2;
            int panelHeight = (cellSize + 2) * arraySize + 2;
            panelArray.Width = panelWidth;
            panelArray.Height = panelHeight;
        }

        // Nouvelle simulation
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
            DisposeLabels();
            GenerateValues();
            AddLabels();
            ResizePanel();

        }

        // 10 interactions
        private void btnInteract10_Click(object sender, EventArgs e)
        {

            for (int k = 0; k < 10; k++)
            {
                values = tableService.tableinteraction(values);
            }

            AddLabels();
            ResizePanel();
        }

        // Evolution
        private void btnNewGen_Click(object sender, EventArgs e)
        {
            values = tableService.evolution(values);

            DisposeLabels();
            AddLabels();
            ResizePanel();
        }

    }
}