namespace PathFinding
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            btnStopAttack = new Button();
            btnStartAttack = new Button();
            tboxNumberOfSoldiers = new TextBox();
            btnSetUnits = new Button();
            mainTimer = new System.Windows.Forms.Timer(components);
            panelTarget = new Panel();
            toolTip1 = new ToolTip(components);
            tboxSoldierMaxMovement = new TextBox();
            tboxNumberOfTowers = new TextBox();
            tboxSightRangeOfTowers = new TextBox();
            tboxPanelSize = new TextBox();
            tboxSightOfSoldier = new TextBox();
            lblHealthOfSoldier = new Label();
            lblMaxMovement = new Label();
            tboxHealthOfSoldier = new TextBox();
            tboxHitPowerOfTower = new TextBox();
            tboxNumberOfArchers = new TextBox();
            tboxArcherSight = new TextBox();
            tboxHitRangeOfArcher = new TextBox();
            tboxHitPowerOfArcher = new TextBox();
            tboxMaxMovementOfArcher = new TextBox();
            tboxMinDistanceFromTarget = new TextBox();
            lblSightRangeOfTowers = new Label();
            lblTowerControl = new Label();
            lblNumberOfTowers = new Label();
            lblSoldierControl = new Label();
            lblMaxMovementOfSoldier = new Label();
            lblNumberOfSoldiers = new Label();
            panelGodView = new Panel();
            lblPanelSize = new Label();
            lblSightOfSoldier = new Label();
            lblHitPowerOfTower = new Label();
            lblSightOfArches = new Label();
            label7 = new Label();
            lblMaxMovementOfArchers = new Label();
            lblNumberOfArchers = new Label();
            label6 = new Label();
            lblMinDistanceFromTarget = new Label();
            label9 = new Label();
            lblCurrentHealth = new Label();
            panelSoldiersEye = new Panel();
            soldierEyePictureBox = new PictureBox();
            panelTargetSoldiersEye = new Panel();
            btnSaveScenario = new Button();
            btnOpenScenario = new Button();
            btnExit = new Button();
            panelGodView.SuspendLayout();
            panelSoldiersEye.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)soldierEyePictureBox).BeginInit();
            SuspendLayout();
            // 
            // btnStopAttack
            // 
            btnStopAttack.Cursor = Cursors.Hand;
            btnStopAttack.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnStopAttack.Location = new Point(813, 626);
            btnStopAttack.Name = "btnStopAttack";
            btnStopAttack.Size = new Size(80, 23);
            btnStopAttack.TabIndex = 4;
            btnStopAttack.Text = "Stop Attack";
            btnStopAttack.UseVisualStyleBackColor = true;
            btnStopAttack.Click += btnStopAttack_Click;
            // 
            // btnStartAttack
            // 
            btnStartAttack.Cursor = Cursors.Hand;
            btnStartAttack.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnStartAttack.Location = new Point(899, 626);
            btnStartAttack.Name = "btnStartAttack";
            btnStartAttack.Size = new Size(82, 23);
            btnStartAttack.TabIndex = 3;
            btnStartAttack.Text = "Start Attack";
            btnStartAttack.UseVisualStyleBackColor = true;
            btnStartAttack.Click += btnStartAttack_Click;
            // 
            // tboxNumberOfSoldiers
            // 
            tboxNumberOfSoldiers.Location = new Point(813, 362);
            tboxNumberOfSoldiers.Name = "tboxNumberOfSoldiers";
            tboxNumberOfSoldiers.Size = new Size(70, 23);
            tboxNumberOfSoldiers.TabIndex = 6;
            toolTip1.SetToolTip(tboxNumberOfSoldiers, "Default: 1");
            // 
            // btnSetUnits
            // 
            btnSetUnits.Cursor = Cursors.Hand;
            btnSetUnits.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSetUnits.Location = new Point(987, 626);
            btnSetUnits.Name = "btnSetUnits";
            btnSetUnits.Size = new Size(70, 23);
            btnSetUnits.TabIndex = 5;
            btnSetUnits.Text = "Set Units";
            btnSetUnits.UseVisualStyleBackColor = true;
            btnSetUnits.Click += btnSetUnits_Click_1;
            // 
            // mainTimer
            // 
            mainTimer.Interval = 150;
            mainTimer.Tick += Timer_Tick;
            // 
            // panelTarget
            // 
            panelTarget.BackColor = Color.DarkGoldenrod;
            panelTarget.Location = new Point(675, 344);
            panelTarget.Name = "panelTarget";
            panelTarget.Size = new Size(25, 25);
            panelTarget.TabIndex = 3;
            panelTarget.Visible = false;
            // 
            // tboxSoldierMaxMovement
            // 
            tboxSoldierMaxMovement.Location = new Point(813, 391);
            tboxSoldierMaxMovement.Name = "tboxSoldierMaxMovement";
            tboxSoldierMaxMovement.Size = new Size(70, 23);
            tboxSoldierMaxMovement.TabIndex = 8;
            toolTip1.SetToolTip(tboxSoldierMaxMovement, "Default: God View Size /70");
            // 
            // tboxNumberOfTowers
            // 
            tboxNumberOfTowers.Location = new Point(813, 440);
            tboxNumberOfTowers.Name = "tboxNumberOfTowers";
            tboxNumberOfTowers.Size = new Size(70, 23);
            tboxNumberOfTowers.TabIndex = 11;
            toolTip1.SetToolTip(tboxNumberOfTowers, "Default: 1");
            // 
            // tboxSightRangeOfTowers
            // 
            tboxSightRangeOfTowers.Location = new Point(813, 469);
            tboxSightRangeOfTowers.Name = "tboxSightRangeOfTowers";
            tboxSightRangeOfTowers.Size = new Size(70, 23);
            tboxSightRangeOfTowers.TabIndex = 14;
            toolTip1.SetToolTip(tboxSightRangeOfTowers, "Default: 35");
            // 
            // tboxPanelSize
            // 
            tboxPanelSize.Location = new Point(811, 314);
            tboxPanelSize.Name = "tboxPanelSize";
            tboxPanelSize.Size = new Size(70, 23);
            tboxPanelSize.TabIndex = 16;
            toolTip1.SetToolTip(tboxPanelSize, "Default: 700\r\nMax: 700\r\nMin: 300");
            // 
            // tboxSightOfSoldier
            // 
            tboxSightOfSoldier.Location = new Point(987, 362);
            tboxSightOfSoldier.Name = "tboxSightOfSoldier";
            tboxSightOfSoldier.Size = new Size(70, 23);
            tboxSightOfSoldier.TabIndex = 25;
            toolTip1.SetToolTip(tboxSightOfSoldier, "Default: 35");
            // 
            // lblHealthOfSoldier
            // 
            lblHealthOfSoldier.AutoSize = true;
            lblHealthOfSoldier.Location = new Point(898, 394);
            lblHealthOfSoldier.Name = "lblHealthOfSoldier";
            lblHealthOfSoldier.Size = new Size(42, 15);
            lblHealthOfSoldier.TabIndex = 27;
            lblHealthOfSoldier.Text = "Health";
            toolTip1.SetToolTip(lblHealthOfSoldier, "Default: 100");
            // 
            // lblMaxMovement
            // 
            lblMaxMovement.AutoSize = true;
            lblMaxMovement.Location = new Point(898, 536);
            lblMaxMovement.Name = "lblMaxMovement";
            lblMaxMovement.Size = new Size(59, 15);
            lblMaxMovement.TabIndex = 36;
            lblMaxMovement.Text = "Hit Power";
            toolTip1.SetToolTip(lblMaxMovement, "Default: 100");
            // 
            // tboxHealthOfSoldier
            // 
            tboxHealthOfSoldier.Location = new Point(987, 391);
            tboxHealthOfSoldier.Name = "tboxHealthOfSoldier";
            tboxHealthOfSoldier.Size = new Size(70, 23);
            tboxHealthOfSoldier.TabIndex = 39;
            toolTip1.SetToolTip(tboxHealthOfSoldier, "Default: 100");
            // 
            // tboxHitPowerOfTower
            // 
            tboxHitPowerOfTower.Location = new Point(987, 440);
            tboxHitPowerOfTower.Name = "tboxHitPowerOfTower";
            tboxHitPowerOfTower.Size = new Size(70, 23);
            tboxHitPowerOfTower.TabIndex = 40;
            toolTip1.SetToolTip(tboxHitPowerOfTower, "Default: 1");
            // 
            // tboxNumberOfArchers
            // 
            tboxNumberOfArchers.Location = new Point(813, 533);
            tboxNumberOfArchers.Name = "tboxNumberOfArchers";
            tboxNumberOfArchers.Size = new Size(70, 23);
            tboxNumberOfArchers.TabIndex = 44;
            toolTip1.SetToolTip(tboxNumberOfArchers, "Default: 1");
            // 
            // tboxArcherSight
            // 
            tboxArcherSight.Location = new Point(813, 591);
            tboxArcherSight.Name = "tboxArcherSight";
            tboxArcherSight.Size = new Size(70, 23);
            tboxArcherSight.TabIndex = 45;
            toolTip1.SetToolTip(tboxArcherSight, "Default: 70");
            // 
            // tboxHitRangeOfArcher
            // 
            tboxHitRangeOfArcher.Location = new Point(987, 558);
            tboxHitRangeOfArcher.Name = "tboxHitRangeOfArcher";
            tboxHitRangeOfArcher.Size = new Size(70, 23);
            tboxHitRangeOfArcher.TabIndex = 46;
            toolTip1.SetToolTip(tboxHitRangeOfArcher, "Default: 40");
            // 
            // tboxHitPowerOfArcher
            // 
            tboxHitPowerOfArcher.Location = new Point(987, 528);
            tboxHitPowerOfArcher.Name = "tboxHitPowerOfArcher";
            tboxHitPowerOfArcher.Size = new Size(70, 23);
            tboxHitPowerOfArcher.TabIndex = 47;
            toolTip1.SetToolTip(tboxHitPowerOfArcher, "Default: 1");
            // 
            // tboxMaxMovementOfArcher
            // 
            tboxMaxMovementOfArcher.Location = new Point(813, 562);
            tboxMaxMovementOfArcher.Name = "tboxMaxMovementOfArcher";
            tboxMaxMovementOfArcher.Size = new Size(70, 23);
            tboxMaxMovementOfArcher.TabIndex = 48;
            toolTip1.SetToolTip(tboxMaxMovementOfArcher, "Default: God View Size /70");
            // 
            // tboxMinDistanceFromTarget
            // 
            tboxMinDistanceFromTarget.Location = new Point(987, 314);
            tboxMinDistanceFromTarget.Name = "tboxMinDistanceFromTarget";
            tboxMinDistanceFromTarget.Size = new Size(70, 23);
            tboxMinDistanceFromTarget.TabIndex = 43;
            toolTip1.SetToolTip(tboxMinDistanceFromTarget, "Default: 0");
            // 
            // lblSightRangeOfTowers
            // 
            lblSightRangeOfTowers.AutoSize = true;
            lblSightRangeOfTowers.Location = new Point(713, 473);
            lblSightRangeOfTowers.Name = "lblSightRangeOfTowers";
            lblSightRangeOfTowers.Size = new Size(91, 15);
            lblSightRangeOfTowers.TabIndex = 15;
            lblSightRangeOfTowers.Text = "Sight/Hit Range";
            // 
            // lblTowerControl
            // 
            lblTowerControl.AutoSize = true;
            lblTowerControl.Font = new Font("Consolas", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblTowerControl.Location = new Point(713, 421);
            lblTowerControl.Name = "lblTowerControl";
            lblTowerControl.Size = new Size(60, 22);
            lblTowerControl.TabIndex = 13;
            lblTowerControl.Text = "TOWER";
            // 
            // lblNumberOfTowers
            // 
            lblNumberOfTowers.AutoSize = true;
            lblNumberOfTowers.Location = new Point(713, 448);
            lblNumberOfTowers.Name = "lblNumberOfTowers";
            lblNumberOfTowers.Size = new Size(51, 15);
            lblNumberOfTowers.TabIndex = 12;
            lblNumberOfTowers.Text = "Number";
            // 
            // lblSoldierControl
            // 
            lblSoldierControl.AutoSize = true;
            lblSoldierControl.Font = new Font("Consolas", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblSoldierControl.Location = new Point(711, 343);
            lblSoldierControl.Name = "lblSoldierControl";
            lblSoldierControl.Size = new Size(80, 22);
            lblSoldierControl.TabIndex = 10;
            lblSoldierControl.Text = "SOLDIER";
            // 
            // lblMaxMovementOfSoldier
            // 
            lblMaxMovementOfSoldier.AutoSize = true;
            lblMaxMovementOfSoldier.Location = new Point(711, 394);
            lblMaxMovementOfSoldier.Name = "lblMaxMovementOfSoldier";
            lblMaxMovementOfSoldier.Size = new Size(94, 15);
            lblMaxMovementOfSoldier.TabIndex = 9;
            lblMaxMovementOfSoldier.Text = "Max Movement ";
            // 
            // lblNumberOfSoldiers
            // 
            lblNumberOfSoldiers.AutoSize = true;
            lblNumberOfSoldiers.Location = new Point(711, 365);
            lblNumberOfSoldiers.Name = "lblNumberOfSoldiers";
            lblNumberOfSoldiers.Size = new Size(51, 15);
            lblNumberOfSoldiers.TabIndex = 7;
            lblNumberOfSoldiers.Text = "Number";
            // 
            // panelGodView
            // 
            panelGodView.Anchor = AnchorStyles.None;
            panelGodView.BackColor = Color.White;
            panelGodView.Controls.Add(panelTarget);
            panelGodView.Location = new Point(5, 5);
            panelGodView.Name = "panelGodView";
            panelGodView.Size = new Size(700, 700);
            panelGodView.TabIndex = 7;
            // 
            // lblPanelSize
            // 
            lblPanelSize.AutoSize = true;
            lblPanelSize.Location = new Point(711, 314);
            lblPanelSize.Name = "lblPanelSize";
            lblPanelSize.Size = new Size(80, 15);
            lblPanelSize.TabIndex = 17;
            lblPanelSize.Text = "God View Size";
            // 
            // lblSightOfSoldier
            // 
            lblSightOfSoldier.AutoSize = true;
            lblSightOfSoldier.Location = new Point(898, 365);
            lblSightOfSoldier.Name = "lblSightOfSoldier";
            lblSightOfSoldier.Size = new Size(34, 15);
            lblSightOfSoldier.TabIndex = 26;
            lblSightOfSoldier.Text = "Sight";
            // 
            // lblHitPowerOfTower
            // 
            lblHitPowerOfTower.AutoSize = true;
            lblHitPowerOfTower.Location = new Point(898, 448);
            lblHitPowerOfTower.Name = "lblHitPowerOfTower";
            lblHitPowerOfTower.Size = new Size(59, 15);
            lblHitPowerOfTower.TabIndex = 28;
            lblHitPowerOfTower.Text = "Hit Power";
            // 
            // lblSightOfArches
            // 
            lblSightOfArches.AutoSize = true;
            lblSightOfArches.Location = new Point(713, 594);
            lblSightOfArches.Name = "lblSightOfArches";
            lblSightOfArches.Size = new Size(34, 15);
            lblSightOfArches.TabIndex = 35;
            lblSightOfArches.Text = "Sight";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Consolas", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(713, 498);
            label7.Name = "label7";
            label7.Size = new Size(70, 22);
            label7.TabIndex = 33;
            label7.Text = "ARCHER";
            // 
            // lblMaxMovementOfArchers
            // 
            lblMaxMovementOfArchers.AutoSize = true;
            lblMaxMovementOfArchers.Location = new Point(713, 565);
            lblMaxMovementOfArchers.Name = "lblMaxMovementOfArchers";
            lblMaxMovementOfArchers.Size = new Size(94, 15);
            lblMaxMovementOfArchers.TabIndex = 32;
            lblMaxMovementOfArchers.Text = "Max Movement ";
            // 
            // lblNumberOfArchers
            // 
            lblNumberOfArchers.AutoSize = true;
            lblNumberOfArchers.Location = new Point(713, 536);
            lblNumberOfArchers.Name = "lblNumberOfArchers";
            lblNumberOfArchers.Size = new Size(51, 15);
            lblNumberOfArchers.TabIndex = 30;
            lblNumberOfArchers.Text = "Number";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(898, 564);
            label6.Name = "label6";
            label6.Size = new Size(67, 30);
            label6.TabIndex = 37;
            label6.Text = "Hit Range /\r\nProbability";
            // 
            // lblMinDistanceFromTarget
            // 
            lblMinDistanceFromTarget.AutoSize = true;
            lblMinDistanceFromTarget.Location = new Point(898, 314);
            lblMinDistanceFromTarget.Name = "lblMinDistanceFromTarget";
            lblMinDistanceFromTarget.Size = new Size(109, 30);
            lblMinDistanceFromTarget.TabIndex = 38;
            lblMinDistanceFromTarget.Text = "Min Distance           \r\nFrom Target";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(711, 684);
            label9.Name = "label9";
            label9.Size = new Size(42, 15);
            label9.TabIndex = 41;
            label9.Text = "Health";
            // 
            // lblCurrentHealth
            // 
            lblCurrentHealth.AutoSize = true;
            lblCurrentHealth.Font = new Font("Candara", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblCurrentHealth.Location = new Point(759, 680);
            lblCurrentHealth.Name = "lblCurrentHealth";
            lblCurrentHealth.Size = new Size(0, 23);
            lblCurrentHealth.TabIndex = 42;
            // 
            // panelSoldiersEye
            // 
            panelSoldiersEye.BackColor = Color.White;
            panelSoldiersEye.Controls.Add(soldierEyePictureBox);
            panelSoldiersEye.Controls.Add(panelTargetSoldiersEye);
            panelSoldiersEye.Location = new Point(711, 5);
            panelSoldiersEye.Name = "panelSoldiersEye";
            panelSoldiersEye.Size = new Size(300, 300);
            panelSoldiersEye.TabIndex = 49;
            // 
            // soldierEyePictureBox
            // 
            soldierEyePictureBox.Image = PathPlanning.Properties.Resources.smallSoldier;
            soldierEyePictureBox.Location = new Point(133, 133);
            soldierEyePictureBox.Name = "soldierEyePictureBox";
            soldierEyePictureBox.Size = new Size(15, 15);
            soldierEyePictureBox.TabIndex = 8;
            soldierEyePictureBox.TabStop = false;
            soldierEyePictureBox.Visible = false;
            // 
            // panelTargetSoldiersEye
            // 
            panelTargetSoldiersEye.BackColor = Color.DarkGoldenrod;
            panelTargetSoldiersEye.Location = new Point(275, 138);
            panelTargetSoldiersEye.Name = "panelTargetSoldiersEye";
            panelTargetSoldiersEye.Size = new Size(25, 25);
            panelTargetSoldiersEye.TabIndex = 4;
            panelTargetSoldiersEye.Visible = false;
            // 
            // btnSaveScenario
            // 
            btnSaveScenario.Cursor = Cursors.Hand;
            btnSaveScenario.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSaveScenario.Location = new Point(813, 656);
            btnSaveScenario.Name = "btnSaveScenario";
            btnSaveScenario.Size = new Size(170, 23);
            btnSaveScenario.TabIndex = 50;
            btnSaveScenario.Text = "Save Scenario";
            btnSaveScenario.UseVisualStyleBackColor = true;
            btnSaveScenario.Click += btnSaveScenario_Click;
            // 
            // btnOpenScenario
            // 
            btnOpenScenario.BackColor = Color.Transparent;
            btnOpenScenario.Cursor = Cursors.Hand;
            btnOpenScenario.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnOpenScenario.Location = new Point(813, 684);
            btnOpenScenario.Name = "btnOpenScenario";
            btnOpenScenario.Size = new Size(170, 23);
            btnOpenScenario.TabIndex = 51;
            btnOpenScenario.Text = "Open Scenario";
            btnOpenScenario.UseVisualStyleBackColor = false;
            btnOpenScenario.Click += btnOpenScenario_Click;
            // 
            // btnExit
            // 
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderColor = Color.IndianRed;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseDownBackColor = Color.IndianRed;
            btnExit.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Image = PathPlanning.Properties.Resources.logout;
            btnExit.Location = new Point(1040, 0);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(40, 40);
            btnExit.TabIndex = 52;
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            btnExit.MouseEnter += btnExit_MouseEnter;
            btnExit.MouseLeave += btnExit_MouseLeave;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.IndianRed;
            ClientSize = new Size(1080, 710);
            Controls.Add(btnExit);
            Controls.Add(btnOpenScenario);
            Controls.Add(btnSaveScenario);
            Controls.Add(panelSoldiersEye);
            Controls.Add(tboxMaxMovementOfArcher);
            Controls.Add(tboxHitPowerOfArcher);
            Controls.Add(tboxHitRangeOfArcher);
            Controls.Add(tboxArcherSight);
            Controls.Add(tboxNumberOfArchers);
            Controls.Add(tboxMinDistanceFromTarget);
            Controls.Add(lblCurrentHealth);
            Controls.Add(label9);
            Controls.Add(tboxHitPowerOfTower);
            Controls.Add(tboxHealthOfSoldier);
            Controls.Add(lblMinDistanceFromTarget);
            Controls.Add(label6);
            Controls.Add(lblMaxMovement);
            Controls.Add(lblSightOfArches);
            Controls.Add(label7);
            Controls.Add(lblMaxMovementOfArchers);
            Controls.Add(lblNumberOfArchers);
            Controls.Add(lblHitPowerOfTower);
            Controls.Add(lblHealthOfSoldier);
            Controls.Add(lblSightOfSoldier);
            Controls.Add(tboxSightOfSoldier);
            Controls.Add(lblPanelSize);
            Controls.Add(tboxPanelSize);
            Controls.Add(lblSightRangeOfTowers);
            Controls.Add(btnSetUnits);
            Controls.Add(btnStopAttack);
            Controls.Add(tboxSightRangeOfTowers);
            Controls.Add(btnStartAttack);
            Controls.Add(lblTowerControl);
            Controls.Add(lblNumberOfTowers);
            Controls.Add(tboxNumberOfTowers);
            Controls.Add(lblSoldierControl);
            Controls.Add(tboxNumberOfSoldiers);
            Controls.Add(lblMaxMovementOfSoldier);
            Controls.Add(lblNumberOfSoldiers);
            Controls.Add(tboxSoldierMaxMovement);
            Controls.Add(panelGodView);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            panelGodView.ResumeLayout(false);
            panelSoldiersEye.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)soldierEyePictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public System.Windows.Forms.Timer mainTimer;
        public Button btnStartAttack;
        public Button btnStopAttack;
        public Button btnSetUnits;
        public Panel panelTarget;
        public TextBox tboxNumberOfSoldiers;
        public ToolTip toolTip1;
        public Label lblNumberOfSoldiers;
        public TextBox tboxSoldierMaxMovement;
        public Label lblMaxMovementOfSoldier;
        public Label lblSoldierControl;
        public Label lblTowerControl;
        public Label lblNumberOfTowers;
        public TextBox tboxNumberOfTowers;
        public Label lblSightRangeOfTowers;
        public TextBox tboxSightRangeOfTowers;
        public Panel panelGodView;
        public TextBox tboxPanelSize;
        public Label lblPanelSize;

        public TextBox tboxSightOfSoldier;
        public Label lblSightOfSoldier;
        public Label lblHealthOfSoldier;
        public Label lblHitPowerOfTower;
        public Label lblMaxMovement;
        public Label lblSightOfArches;
        public Label label7;
        public Label lblMaxMovementOfArchers;
        public Label lblNumberOfArchers;
        public Label label6;
        public Label lblMinDistanceFromTarget;
        public TextBox tboxHealthOfSoldier;
        public TextBox tboxHitPowerOfTower;
        public Label label9;
        public Label lblCurrentHealth;
        public TextBox tboxMinDistanceFromTarget;
        public TextBox tboxNumberOfArchers;
        public TextBox tboxHitRangeOfArcher;
        public TextBox tboxHitPowerOfArcher;
        public TextBox tboxMaxMovementOfArcher;
        public Panel panelSoldiersEye;
        public Panel panelTargetSoldiersEye;
        public Button btnSaveScenario;
        public Button btnOpenScenario;
        public TextBox tboxArcherSight;
        public PictureBox soldierEyePictureBox;
        private Button btnExit;
    }
}