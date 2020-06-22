namespace TomaszBuryZadanie1
{
    partial class FormMain
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonClose = new System.Windows.Forms.Button();
            this.timerResources = new System.Windows.Forms.Timer(this.components);
            this.labelGold = new System.Windows.Forms.Label();
            this.labelWood = new System.Windows.Forms.Label();
            this.labelRock = new System.Windows.Forms.Label();
            this.labelFood = new System.Windows.Forms.Label();
            this.labelIron = new System.Windows.Forms.Label();
            this.labelResourcesGold = new System.Windows.Forms.Label();
            this.labelResourcesWood = new System.Windows.Forms.Label();
            this.labelResourcesRock = new System.Windows.Forms.Label();
            this.labelResourcesFood = new System.Windows.Forms.Label();
            this.labelResourcesIron = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timerNegativeEvents = new System.Windows.Forms.Timer(this.components);
            this.timerThiefs = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonClose.Location = new System.Drawing.Point(1219, 568);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(136, 63);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Zamknij";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // timerResources
            // 
            this.timerResources.Tick += new System.EventHandler(this.timerResources_Tick);
            // 
            // labelGold
            // 
            this.labelGold.AutoSize = true;
            this.labelGold.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelGold.Location = new System.Drawing.Point(16, 11);
            this.labelGold.Name = "labelGold";
            this.labelGold.Size = new System.Drawing.Size(69, 27);
            this.labelGold.TabIndex = 1;
            this.labelGold.Text = "Złoto:";
            // 
            // labelWood
            // 
            this.labelWood.AutoSize = true;
            this.labelWood.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelWood.Location = new System.Drawing.Point(16, 43);
            this.labelWood.Name = "labelWood";
            this.labelWood.Size = new System.Drawing.Size(95, 27);
            this.labelWood.TabIndex = 2;
            this.labelWood.Text = "Drewno:";
            // 
            // labelRock
            // 
            this.labelRock.AutoSize = true;
            this.labelRock.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRock.Location = new System.Drawing.Point(16, 75);
            this.labelRock.Name = "labelRock";
            this.labelRock.Size = new System.Drawing.Size(93, 27);
            this.labelRock.TabIndex = 3;
            this.labelRock.Text = "Kamień:";
            // 
            // labelFood
            // 
            this.labelFood.AutoSize = true;
            this.labelFood.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFood.Location = new System.Drawing.Point(16, 107);
            this.labelFood.Name = "labelFood";
            this.labelFood.Size = new System.Drawing.Size(105, 27);
            this.labelFood.TabIndex = 4;
            this.labelFood.Text = "Żywność:";
            // 
            // labelIron
            // 
            this.labelIron.AutoSize = true;
            this.labelIron.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelIron.Location = new System.Drawing.Point(16, 139);
            this.labelIron.Name = "labelIron";
            this.labelIron.Size = new System.Drawing.Size(82, 27);
            this.labelIron.TabIndex = 5;
            this.labelIron.Text = "Żelazo:";
            // 
            // labelResourcesGold
            // 
            this.labelResourcesGold.AutoSize = true;
            this.labelResourcesGold.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelResourcesGold.Location = new System.Drawing.Point(118, 11);
            this.labelResourcesGold.Name = "labelResourcesGold";
            this.labelResourcesGold.Size = new System.Drawing.Size(48, 27);
            this.labelResourcesGold.TabIndex = 6;
            this.labelResourcesGold.Text = "100";
            // 
            // labelResourcesWood
            // 
            this.labelResourcesWood.AutoSize = true;
            this.labelResourcesWood.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelResourcesWood.Location = new System.Drawing.Point(118, 43);
            this.labelResourcesWood.Name = "labelResourcesWood";
            this.labelResourcesWood.Size = new System.Drawing.Size(48, 27);
            this.labelResourcesWood.TabIndex = 7;
            this.labelResourcesWood.Text = "100";
            // 
            // labelResourcesRock
            // 
            this.labelResourcesRock.AutoSize = true;
            this.labelResourcesRock.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelResourcesRock.Location = new System.Drawing.Point(118, 75);
            this.labelResourcesRock.Name = "labelResourcesRock";
            this.labelResourcesRock.Size = new System.Drawing.Size(48, 27);
            this.labelResourcesRock.TabIndex = 8;
            this.labelResourcesRock.Text = "100";
            // 
            // labelResourcesFood
            // 
            this.labelResourcesFood.AutoSize = true;
            this.labelResourcesFood.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelResourcesFood.Location = new System.Drawing.Point(118, 107);
            this.labelResourcesFood.Name = "labelResourcesFood";
            this.labelResourcesFood.Size = new System.Drawing.Size(48, 27);
            this.labelResourcesFood.TabIndex = 9;
            this.labelResourcesFood.Text = "100";
            // 
            // labelResourcesIron
            // 
            this.labelResourcesIron.AutoSize = true;
            this.labelResourcesIron.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelResourcesIron.Location = new System.Drawing.Point(118, 139);
            this.labelResourcesIron.Name = "labelResourcesIron";
            this.labelResourcesIron.Size = new System.Drawing.Size(24, 27);
            this.labelResourcesIron.TabIndex = 10;
            this.labelResourcesIron.Text = "0";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(21, 568);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 63);
            this.button1.TabIndex = 11;
            this.button1.Text = "Buduj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonOpenBuildings_Click);
            // 
            // timerNegativeEvents
            // 
            this.timerNegativeEvents.Tick += new System.EventHandler(this.timerNegativeEvents_Tick);
            // 
            // timerThiefs
            // 
            this.timerThiefs.Tick += new System.EventHandler(this.timerThiefs_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TomaszBuryZadanie1.Properties.Resources.City1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1382, 653);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelResourcesIron);
            this.Controls.Add(this.labelResourcesFood);
            this.Controls.Add(this.labelResourcesRock);
            this.Controls.Add(this.labelResourcesWood);
            this.Controls.Add(this.labelResourcesGold);
            this.Controls.Add(this.labelIron);
            this.Controls.Add(this.labelFood);
            this.Controls.Add(this.labelRock);
            this.Controls.Add(this.labelWood);
            this.Controls.Add(this.labelGold);
            this.Controls.Add(this.buttonClose);
            this.Name = "FormMain";
            this.Text = "Miasto";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Timer timerResources;
        private System.Windows.Forms.Label labelGold;
        private System.Windows.Forms.Label labelWood;
        private System.Windows.Forms.Label labelRock;
        private System.Windows.Forms.Label labelFood;
        private System.Windows.Forms.Label labelIron;
        private System.Windows.Forms.Label labelResourcesGold;
        private System.Windows.Forms.Label labelResourcesWood;
        private System.Windows.Forms.Label labelResourcesRock;
        private System.Windows.Forms.Label labelResourcesFood;
        private System.Windows.Forms.Label labelResourcesIron;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timerNegativeEvents;
        private System.Windows.Forms.Timer timerThiefs;
    }
}

