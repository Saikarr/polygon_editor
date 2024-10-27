namespace polygon_editor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picture = new PictureBox();
            groupBox = new GroupBox();
            verticesTextBox = new TextBox();
            c1button = new Button();
            g1Button = new Button();
            algorithmLabel = new Label();
            bresenhamButton = new RadioButton();
            libraryButton = new RadioButton();
            bezierButton = new Button();
            removeRelationButton = new Button();
            lengthTextBox = new TextBox();
            edgeLengthButton = new Button();
            verticalEdgeButton = new Button();
            horizontalEdgeButton = new Button();
            addVertexButton = new Button();
            removeVertexButton = new Button();
            removePolygonButton = new Button();
            addPolygonButton = new Button();
            ((System.ComponentModel.ISupportInitialize)picture).BeginInit();
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // picture
            // 
            picture.BackColor = Color.White;
            picture.Location = new Point(0, 0);
            picture.Name = "picture";
            picture.Size = new Size(757, 610);
            picture.TabIndex = 0;
            picture.TabStop = false;
            picture.Paint += picture_Paint;
            picture.MouseClick += picture_MouseClick;
            picture.MouseDown += picture_MouseDown;
            picture.MouseMove += picture_MouseMove;
            picture.MouseUp += picture_MouseUp;
            // 
            // groupBox
            // 
            groupBox.Controls.Add(verticesTextBox);
            groupBox.Controls.Add(c1button);
            groupBox.Controls.Add(g1Button);
            groupBox.Controls.Add(algorithmLabel);
            groupBox.Controls.Add(bresenhamButton);
            groupBox.Controls.Add(libraryButton);
            groupBox.Controls.Add(bezierButton);
            groupBox.Controls.Add(removeRelationButton);
            groupBox.Controls.Add(lengthTextBox);
            groupBox.Controls.Add(edgeLengthButton);
            groupBox.Controls.Add(verticalEdgeButton);
            groupBox.Controls.Add(horizontalEdgeButton);
            groupBox.Controls.Add(addVertexButton);
            groupBox.Controls.Add(removeVertexButton);
            groupBox.Controls.Add(removePolygonButton);
            groupBox.Controls.Add(addPolygonButton);
            groupBox.Location = new Point(763, 0);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(226, 610);
            groupBox.TabIndex = 1;
            groupBox.TabStop = false;
            groupBox.Text = "options";
            // 
            // verticesTextBox
            // 
            verticesTextBox.Location = new Point(6, 63);
            verticesTextBox.Name = "verticesTextBox";
            verticesTextBox.Size = new Size(206, 27);
            verticesTextBox.TabIndex = 15;
            // 
            // c1button
            // 
            c1button.Location = new Point(6, 409);
            c1button.Name = "c1button";
            c1button.Size = new Size(206, 29);
            c1button.TabIndex = 14;
            c1button.Text = "C1 continuity";
            c1button.UseVisualStyleBackColor = true;
            c1button.Click += c1button_Click;
            // 
            // g1Button
            // 
            g1Button.Location = new Point(6, 375);
            g1Button.Name = "g1Button";
            g1Button.Size = new Size(206, 28);
            g1Button.TabIndex = 13;
            g1Button.Text = "G1 continuity";
            g1Button.UseVisualStyleBackColor = true;
            g1Button.Click += g1Button_Click;
            // 
            // algorithmLabel
            // 
            algorithmLabel.AutoSize = true;
            algorithmLabel.Location = new Point(6, 492);
            algorithmLabel.Name = "algorithmLabel";
            algorithmLabel.Size = new Size(127, 20);
            algorithmLabel.TabIndex = 12;
            algorithmLabel.Text = "Choose algorithm";
            // 
            // bresenhamButton
            // 
            bresenhamButton.AutoSize = true;
            bresenhamButton.Location = new Point(6, 545);
            bresenhamButton.Name = "bresenhamButton";
            bresenhamButton.Size = new Size(172, 24);
            bresenhamButton.TabIndex = 11;
            bresenhamButton.TabStop = true;
            bresenhamButton.Text = "Bresenham algorithm";
            bresenhamButton.UseVisualStyleBackColor = true;
            // 
            // libraryButton
            // 
            libraryButton.AutoSize = true;
            libraryButton.Location = new Point(6, 515);
            libraryButton.Name = "libraryButton";
            libraryButton.Size = new Size(144, 24);
            libraryButton.TabIndex = 10;
            libraryButton.TabStop = true;
            libraryButton.Text = "Library algorithm";
            libraryButton.UseVisualStyleBackColor = true;
            libraryButton.CheckedChanged += libraryButton_CheckedChanged;
            // 
            // bezierButton
            // 
            bezierButton.Location = new Point(6, 340);
            bezierButton.Name = "bezierButton";
            bezierButton.Size = new Size(206, 29);
            bezierButton.TabIndex = 9;
            bezierButton.Text = "Add bezier";
            bezierButton.UseVisualStyleBackColor = true;
            bezierButton.Click += bezierButton_Click;
            // 
            // removeRelationButton
            // 
            removeRelationButton.Location = new Point(6, 444);
            removeRelationButton.Name = "removeRelationButton";
            removeRelationButton.Size = new Size(206, 29);
            removeRelationButton.TabIndex = 8;
            removeRelationButton.Text = "Remove relation";
            removeRelationButton.UseVisualStyleBackColor = true;
            removeRelationButton.Click += removeRelationButton_Click;
            // 
            // lengthTextBox
            // 
            lengthTextBox.Location = new Point(6, 307);
            lengthTextBox.Name = "lengthTextBox";
            lengthTextBox.Size = new Size(206, 27);
            lengthTextBox.TabIndex = 7;
            // 
            // edgeLengthButton
            // 
            edgeLengthButton.Location = new Point(6, 272);
            edgeLengthButton.Name = "edgeLengthButton";
            edgeLengthButton.Size = new Size(206, 29);
            edgeLengthButton.TabIndex = 6;
            edgeLengthButton.Text = "Define edge length";
            edgeLengthButton.UseVisualStyleBackColor = true;
            edgeLengthButton.Click += edgeLengthButton_Click;
            // 
            // verticalEdgeButton
            // 
            verticalEdgeButton.Location = new Point(6, 237);
            verticalEdgeButton.Name = "verticalEdgeButton";
            verticalEdgeButton.Size = new Size(206, 29);
            verticalEdgeButton.TabIndex = 5;
            verticalEdgeButton.Text = "Vertical edge";
            verticalEdgeButton.UseVisualStyleBackColor = true;
            verticalEdgeButton.Click += verticalEdgeButton_Click;
            // 
            // horizontalEdgeButton
            // 
            horizontalEdgeButton.Location = new Point(6, 202);
            horizontalEdgeButton.Name = "horizontalEdgeButton";
            horizontalEdgeButton.Size = new Size(206, 29);
            horizontalEdgeButton.TabIndex = 4;
            horizontalEdgeButton.Text = "Horizontal edge";
            horizontalEdgeButton.UseVisualStyleBackColor = true;
            horizontalEdgeButton.Click += horizontalEdgeButton_Click;
            // 
            // addVertexButton
            // 
            addVertexButton.Location = new Point(6, 131);
            addVertexButton.Name = "addVertexButton";
            addVertexButton.Size = new Size(206, 29);
            addVertexButton.TabIndex = 3;
            addVertexButton.Text = "Add vertex";
            addVertexButton.UseVisualStyleBackColor = true;
            addVertexButton.Click += addVertexButton_Click;
            // 
            // removeVertexButton
            // 
            removeVertexButton.Location = new Point(6, 166);
            removeVertexButton.Name = "removeVertexButton";
            removeVertexButton.Size = new Size(206, 30);
            removeVertexButton.TabIndex = 2;
            removeVertexButton.Text = "Remove vertex";
            removeVertexButton.UseVisualStyleBackColor = true;
            removeVertexButton.Click += removeVertexButton_Click;
            // 
            // removePolygonButton
            // 
            removePolygonButton.Location = new Point(6, 96);
            removePolygonButton.Name = "removePolygonButton";
            removePolygonButton.Size = new Size(206, 29);
            removePolygonButton.TabIndex = 1;
            removePolygonButton.Text = "Remove polygon";
            removePolygonButton.UseVisualStyleBackColor = true;
            removePolygonButton.Click += removePolygonButton_Click;
            // 
            // addPolygonButton
            // 
            addPolygonButton.Location = new Point(6, 26);
            addPolygonButton.Name = "addPolygonButton";
            addPolygonButton.Size = new Size(206, 29);
            addPolygonButton.TabIndex = 0;
            addPolygonButton.Text = "Add polygon";
            addPolygonButton.UseVisualStyleBackColor = true;
            addPolygonButton.Click += addPolygonButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(987, 610);
            Controls.Add(groupBox);
            Controls.Add(picture);
            MaximumSize = new Size(1005, 657);
            MinimumSize = new Size(1005, 657);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picture).EndInit();
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picture;
        private GroupBox groupBox;
        private Button verticalEdgeButton;
        private Button horizontalEdgeButton;
        private Button addVertexButton;
        private Button removeVertexButton;
        private Button removePolygonButton;
        private Button addPolygonButton;
        private TextBox lengthTextBox;
        private Button edgeLengthButton;
        private Button removeRelationButton;
        private Button bezierButton;
        private RadioButton bresenhamButton;
        private RadioButton libraryButton;
        private Label algorithmLabel;
        private Button c1button;
        private Button g1Button;
        private TextBox verticesTextBox;
    }
}
