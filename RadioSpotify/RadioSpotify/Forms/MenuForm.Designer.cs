namespace RadioSpotify
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.lblSpotifyStatus = new System.Windows.Forms.TextBox();
            this.lblSpotifyStatusCaption = new System.Windows.Forms.TextBox();
            this.lblSRPrev = new System.Windows.Forms.TextBox();
            this.lblSRCurrent = new System.Windows.Forms.TextBox();
            this.lblSRNext = new System.Windows.Forms.TextBox();
            this.lblSR = new System.Windows.Forms.TextBox();
            this.lblSRPrevCaption = new System.Windows.Forms.TextBox();
            this.lblSRCurrentCaption = new System.Windows.Forms.TextBox();
            this.lblSRNextCaption = new System.Windows.Forms.TextBox();
            this.cbChannelSelector = new System.Windows.Forms.ComboBox();
            this.lblChannelSelector = new System.Windows.Forms.TextBox();
            this.lblSRPrevStartTime = new System.Windows.Forms.TextBox();
            this.lblSRCurrentStartTime = new System.Windows.Forms.TextBox();
            this.lblSRNextStartTime = new System.Windows.Forms.TextBox();
            this.lblSRNextStartTimeCaption = new System.Windows.Forms.TextBox();
            this.lblSRCurrentStartTimeCaption = new System.Windows.Forms.TextBox();
            this.lblSRPrevStartTimeCaption = new System.Windows.Forms.TextBox();
            this.lblSRNextStopTimeCaption = new System.Windows.Forms.TextBox();
            this.lblSRCurrentStopTimeCaption = new System.Windows.Forms.TextBox();
            this.lblSRPrevStopTimeCaption = new System.Windows.Forms.TextBox();
            this.lblSRNextStopTime = new System.Windows.Forms.TextBox();
            this.lblSRCurrentStopTime = new System.Windows.Forms.TextBox();
            this.lblSRPrevStopTime = new System.Windows.Forms.TextBox();
            this.btnChangeSRMode = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNextReplacement = new System.Windows.Forms.TextBox();
            this.lblCurrentReplacement = new System.Windows.Forms.TextBox();
            this.lblPrevReplacement = new System.Windows.Forms.TextBox();
            this.btnPrevReplacement = new System.Windows.Forms.Button();
            this.btnCurrentReplacement = new System.Windows.Forms.Button();
            this.btnNextReplacement = new System.Windows.Forms.Button();
            this.btnChangeSong = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSpotifyStatus
            // 
            this.lblSpotifyStatus.Location = new System.Drawing.Point(12, 28);
            this.lblSpotifyStatus.Name = "lblSpotifyStatus";
            this.lblSpotifyStatus.ReadOnly = true;
            this.lblSpotifyStatus.Size = new System.Drawing.Size(55, 20);
            this.lblSpotifyStatus.TabIndex = 0;
            this.lblSpotifyStatus.Text = "Spotify is:";
            // 
            // lblSpotifyStatusCaption
            // 
            this.lblSpotifyStatusCaption.Location = new System.Drawing.Point(73, 28);
            this.lblSpotifyStatusCaption.Name = "lblSpotifyStatusCaption";
            this.lblSpotifyStatusCaption.ReadOnly = true;
            this.lblSpotifyStatusCaption.Size = new System.Drawing.Size(229, 20);
            this.lblSpotifyStatusCaption.TabIndex = 1;
            // 
            // lblSRPrev
            // 
            this.lblSRPrev.Location = new System.Drawing.Point(12, 97);
            this.lblSRPrev.Name = "lblSRPrev";
            this.lblSRPrev.ReadOnly = true;
            this.lblSRPrev.Size = new System.Drawing.Size(55, 20);
            this.lblSRPrev.TabIndex = 2;
            this.lblSRPrev.Text = "Prev:";
            // 
            // lblSRCurrent
            // 
            this.lblSRCurrent.Location = new System.Drawing.Point(12, 123);
            this.lblSRCurrent.Name = "lblSRCurrent";
            this.lblSRCurrent.ReadOnly = true;
            this.lblSRCurrent.Size = new System.Drawing.Size(55, 20);
            this.lblSRCurrent.TabIndex = 3;
            this.lblSRCurrent.Text = "Current:";
            // 
            // lblSRNext
            // 
            this.lblSRNext.Location = new System.Drawing.Point(12, 149);
            this.lblSRNext.Name = "lblSRNext";
            this.lblSRNext.ReadOnly = true;
            this.lblSRNext.Size = new System.Drawing.Size(55, 20);
            this.lblSRNext.TabIndex = 4;
            this.lblSRNext.Text = "Next:";
            // 
            // lblSR
            // 
            this.lblSR.Location = new System.Drawing.Point(12, 71);
            this.lblSR.Name = "lblSR";
            this.lblSR.ReadOnly = true;
            this.lblSR.Size = new System.Drawing.Size(117, 20);
            this.lblSR.TabIndex = 5;
            this.lblSR.Text = "SR is currently playing:";
            // 
            // lblSRPrevCaption
            // 
            this.lblSRPrevCaption.Location = new System.Drawing.Point(77, 97);
            this.lblSRPrevCaption.Name = "lblSRPrevCaption";
            this.lblSRPrevCaption.ReadOnly = true;
            this.lblSRPrevCaption.Size = new System.Drawing.Size(183, 20);
            this.lblSRPrevCaption.TabIndex = 6;
            // 
            // lblSRCurrentCaption
            // 
            this.lblSRCurrentCaption.Location = new System.Drawing.Point(77, 123);
            this.lblSRCurrentCaption.Name = "lblSRCurrentCaption";
            this.lblSRCurrentCaption.ReadOnly = true;
            this.lblSRCurrentCaption.Size = new System.Drawing.Size(183, 20);
            this.lblSRCurrentCaption.TabIndex = 7;
            // 
            // lblSRNextCaption
            // 
            this.lblSRNextCaption.Location = new System.Drawing.Point(77, 149);
            this.lblSRNextCaption.Name = "lblSRNextCaption";
            this.lblSRNextCaption.ReadOnly = true;
            this.lblSRNextCaption.Size = new System.Drawing.Size(183, 20);
            this.lblSRNextCaption.TabIndex = 8;
            // 
            // cbChannelSelector
            // 
            this.cbChannelSelector.FormattingEnabled = true;
            this.cbChannelSelector.Location = new System.Drawing.Point(77, 175);
            this.cbChannelSelector.Name = "cbChannelSelector";
            this.cbChannelSelector.Size = new System.Drawing.Size(96, 21);
            this.cbChannelSelector.TabIndex = 9;
            this.cbChannelSelector.SelectedIndexChanged += new System.EventHandler(this.cbChannelSelector_SelectedIndexChanged);
            // 
            // lblChannelSelector
            // 
            this.lblChannelSelector.Location = new System.Drawing.Point(13, 176);
            this.lblChannelSelector.Name = "lblChannelSelector";
            this.lblChannelSelector.ReadOnly = true;
            this.lblChannelSelector.Size = new System.Drawing.Size(54, 20);
            this.lblChannelSelector.TabIndex = 10;
            this.lblChannelSelector.Text = "On:";
            // 
            // lblSRPrevStartTime
            // 
            this.lblSRPrevStartTime.Location = new System.Drawing.Point(266, 97);
            this.lblSRPrevStartTime.Name = "lblSRPrevStartTime";
            this.lblSRPrevStartTime.ReadOnly = true;
            this.lblSRPrevStartTime.Size = new System.Drawing.Size(36, 20);
            this.lblSRPrevStartTime.TabIndex = 11;
            this.lblSRPrevStartTime.Text = "Start:";
            // 
            // lblSRCurrentStartTime
            // 
            this.lblSRCurrentStartTime.Location = new System.Drawing.Point(266, 123);
            this.lblSRCurrentStartTime.Name = "lblSRCurrentStartTime";
            this.lblSRCurrentStartTime.ReadOnly = true;
            this.lblSRCurrentStartTime.Size = new System.Drawing.Size(36, 20);
            this.lblSRCurrentStartTime.TabIndex = 12;
            this.lblSRCurrentStartTime.Text = "Start:";
            // 
            // lblSRNextStartTime
            // 
            this.lblSRNextStartTime.Location = new System.Drawing.Point(266, 149);
            this.lblSRNextStartTime.Name = "lblSRNextStartTime";
            this.lblSRNextStartTime.ReadOnly = true;
            this.lblSRNextStartTime.Size = new System.Drawing.Size(36, 20);
            this.lblSRNextStartTime.TabIndex = 13;
            this.lblSRNextStartTime.Text = "Start:";
            // 
            // lblSRNextStartTimeCaption
            // 
            this.lblSRNextStartTimeCaption.Location = new System.Drawing.Point(308, 149);
            this.lblSRNextStartTimeCaption.Name = "lblSRNextStartTimeCaption";
            this.lblSRNextStartTimeCaption.ReadOnly = true;
            this.lblSRNextStartTimeCaption.Size = new System.Drawing.Size(136, 20);
            this.lblSRNextStartTimeCaption.TabIndex = 16;
            // 
            // lblSRCurrentStartTimeCaption
            // 
            this.lblSRCurrentStartTimeCaption.Location = new System.Drawing.Point(308, 123);
            this.lblSRCurrentStartTimeCaption.Name = "lblSRCurrentStartTimeCaption";
            this.lblSRCurrentStartTimeCaption.ReadOnly = true;
            this.lblSRCurrentStartTimeCaption.Size = new System.Drawing.Size(136, 20);
            this.lblSRCurrentStartTimeCaption.TabIndex = 15;
            // 
            // lblSRPrevStartTimeCaption
            // 
            this.lblSRPrevStartTimeCaption.Location = new System.Drawing.Point(308, 97);
            this.lblSRPrevStartTimeCaption.Name = "lblSRPrevStartTimeCaption";
            this.lblSRPrevStartTimeCaption.ReadOnly = true;
            this.lblSRPrevStartTimeCaption.Size = new System.Drawing.Size(136, 20);
            this.lblSRPrevStartTimeCaption.TabIndex = 14;
            // 
            // lblSRNextStopTimeCaption
            // 
            this.lblSRNextStopTimeCaption.Location = new System.Drawing.Point(492, 149);
            this.lblSRNextStopTimeCaption.Name = "lblSRNextStopTimeCaption";
            this.lblSRNextStopTimeCaption.ReadOnly = true;
            this.lblSRNextStopTimeCaption.Size = new System.Drawing.Size(136, 20);
            this.lblSRNextStopTimeCaption.TabIndex = 22;
            // 
            // lblSRCurrentStopTimeCaption
            // 
            this.lblSRCurrentStopTimeCaption.Location = new System.Drawing.Point(492, 123);
            this.lblSRCurrentStopTimeCaption.Name = "lblSRCurrentStopTimeCaption";
            this.lblSRCurrentStopTimeCaption.ReadOnly = true;
            this.lblSRCurrentStopTimeCaption.Size = new System.Drawing.Size(136, 20);
            this.lblSRCurrentStopTimeCaption.TabIndex = 21;
            // 
            // lblSRPrevStopTimeCaption
            // 
            this.lblSRPrevStopTimeCaption.Location = new System.Drawing.Point(492, 97);
            this.lblSRPrevStopTimeCaption.Name = "lblSRPrevStopTimeCaption";
            this.lblSRPrevStopTimeCaption.ReadOnly = true;
            this.lblSRPrevStopTimeCaption.Size = new System.Drawing.Size(136, 20);
            this.lblSRPrevStopTimeCaption.TabIndex = 20;
            // 
            // lblSRNextStopTime
            // 
            this.lblSRNextStopTime.Location = new System.Drawing.Point(450, 149);
            this.lblSRNextStopTime.Name = "lblSRNextStopTime";
            this.lblSRNextStopTime.ReadOnly = true;
            this.lblSRNextStopTime.Size = new System.Drawing.Size(36, 20);
            this.lblSRNextStopTime.TabIndex = 19;
            this.lblSRNextStopTime.Text = "Stop:";
            // 
            // lblSRCurrentStopTime
            // 
            this.lblSRCurrentStopTime.Location = new System.Drawing.Point(450, 123);
            this.lblSRCurrentStopTime.Name = "lblSRCurrentStopTime";
            this.lblSRCurrentStopTime.ReadOnly = true;
            this.lblSRCurrentStopTime.Size = new System.Drawing.Size(36, 20);
            this.lblSRCurrentStopTime.TabIndex = 18;
            this.lblSRCurrentStopTime.Text = "Stop:";
            // 
            // lblSRPrevStopTime
            // 
            this.lblSRPrevStopTime.Location = new System.Drawing.Point(450, 97);
            this.lblSRPrevStopTime.Name = "lblSRPrevStopTime";
            this.lblSRPrevStopTime.ReadOnly = true;
            this.lblSRPrevStopTime.Size = new System.Drawing.Size(36, 20);
            this.lblSRPrevStopTime.TabIndex = 17;
            this.lblSRPrevStopTime.Text = "Stop:";
            // 
            // btnChangeSRMode
            // 
            this.btnChangeSRMode.Location = new System.Drawing.Point(135, 71);
            this.btnChangeSRMode.Name = "btnChangeSRMode";
            this.btnChangeSRMode.Size = new System.Drawing.Size(75, 21);
            this.btnChangeSRMode.TabIndex = 23;
            this.btnChangeSRMode.Text = "Pause";
            this.btnChangeSRMode.UseVisualStyleBackColor = true;
            this.btnChangeSRMode.Click += new System.EventHandler(this.btnChangeSRMode_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 204);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(637, 22);
            this.statusStrip1.TabIndex = 30;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripTime
            // 
            this.toolStripTime.Name = "toolStripTime";
            this.toolStripTime.Size = new System.Drawing.Size(79, 17);
            this.toolStripTime.Text = "toolStripTime";
            // 
            // lblNextReplacement
            // 
            this.lblNextReplacement.Location = new System.Drawing.Point(179, 301);
            this.lblNextReplacement.Name = "lblNextReplacement";
            this.lblNextReplacement.ReadOnly = true;
            this.lblNextReplacement.Size = new System.Drawing.Size(265, 20);
            this.lblNextReplacement.TabIndex = 28;
            this.lblNextReplacement.Visible = false;
            // 
            // lblCurrentReplacement
            // 
            this.lblCurrentReplacement.Location = new System.Drawing.Point(179, 271);
            this.lblCurrentReplacement.Name = "lblCurrentReplacement";
            this.lblCurrentReplacement.ReadOnly = true;
            this.lblCurrentReplacement.Size = new System.Drawing.Size(265, 20);
            this.lblCurrentReplacement.TabIndex = 29;
            this.lblCurrentReplacement.Visible = false;
            // 
            // lblPrevReplacement
            // 
            this.lblPrevReplacement.Location = new System.Drawing.Point(179, 243);
            this.lblPrevReplacement.Name = "lblPrevReplacement";
            this.lblPrevReplacement.ReadOnly = true;
            this.lblPrevReplacement.Size = new System.Drawing.Size(265, 20);
            this.lblPrevReplacement.TabIndex = 27;
            this.lblPrevReplacement.Visible = false;
            // 
            // btnPrevReplacement
            // 
            this.btnPrevReplacement.Location = new System.Drawing.Point(12, 240);
            this.btnPrevReplacement.Name = "btnPrevReplacement";
            this.btnPrevReplacement.Size = new System.Drawing.Size(161, 23);
            this.btnPrevReplacement.TabIndex = 24;
            this.btnPrevReplacement.Text = "Find replacement for prev";
            this.btnPrevReplacement.UseVisualStyleBackColor = true;
            this.btnPrevReplacement.Visible = false;
            this.btnPrevReplacement.Click += new System.EventHandler(this.btnReplacement_Click);
            // 
            // btnCurrentReplacement
            // 
            this.btnCurrentReplacement.Location = new System.Drawing.Point(12, 269);
            this.btnCurrentReplacement.Name = "btnCurrentReplacement";
            this.btnCurrentReplacement.Size = new System.Drawing.Size(161, 23);
            this.btnCurrentReplacement.TabIndex = 26;
            this.btnCurrentReplacement.Text = "Find replacement for current";
            this.btnCurrentReplacement.UseVisualStyleBackColor = true;
            this.btnCurrentReplacement.Visible = false;
            this.btnCurrentReplacement.Click += new System.EventHandler(this.btnReplacement_Click);
            // 
            // btnNextReplacement
            // 
            this.btnNextReplacement.Location = new System.Drawing.Point(12, 298);
            this.btnNextReplacement.Name = "btnNextReplacement";
            this.btnNextReplacement.Size = new System.Drawing.Size(161, 23);
            this.btnNextReplacement.TabIndex = 25;
            this.btnNextReplacement.Text = "Find replacement for next";
            this.btnNextReplacement.UseVisualStyleBackColor = true;
            this.btnNextReplacement.Visible = false;
            this.btnNextReplacement.Click += new System.EventHandler(this.btnReplacement_Click);
            // 
            // btnChangeSong
            // 
            this.btnChangeSong.Location = new System.Drawing.Point(308, 26);
            this.btnChangeSong.Name = "btnChangeSong";
            this.btnChangeSong.Size = new System.Drawing.Size(83, 23);
            this.btnChangeSong.TabIndex = 31;
            this.btnChangeSong.Text = "Change song";
            this.btnChangeSong.UseVisualStyleBackColor = true;
            this.btnChangeSong.Click += new System.EventHandler(this.btnChangeSong_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(397, 26);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(83, 23);
            this.btnDebug.TabIndex = 32;
            this.btnDebug.Text = "Change song";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(637, 226);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnChangeSong);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblCurrentReplacement);
            this.Controls.Add(this.lblNextReplacement);
            this.Controls.Add(this.lblPrevReplacement);
            this.Controls.Add(this.btnCurrentReplacement);
            this.Controls.Add(this.btnNextReplacement);
            this.Controls.Add(this.btnPrevReplacement);
            this.Controls.Add(this.btnChangeSRMode);
            this.Controls.Add(this.lblSRNextStopTimeCaption);
            this.Controls.Add(this.lblSRCurrentStopTimeCaption);
            this.Controls.Add(this.lblSRPrevStopTimeCaption);
            this.Controls.Add(this.lblSRNextStopTime);
            this.Controls.Add(this.lblSRCurrentStopTime);
            this.Controls.Add(this.lblSRPrevStopTime);
            this.Controls.Add(this.lblSRNextStartTimeCaption);
            this.Controls.Add(this.lblSRCurrentStartTimeCaption);
            this.Controls.Add(this.lblSRPrevStartTimeCaption);
            this.Controls.Add(this.lblSRNextStartTime);
            this.Controls.Add(this.lblSRCurrentStartTime);
            this.Controls.Add(this.lblSRPrevStartTime);
            this.Controls.Add(this.lblChannelSelector);
            this.Controls.Add(this.cbChannelSelector);
            this.Controls.Add(this.lblSRNextCaption);
            this.Controls.Add(this.lblSRCurrentCaption);
            this.Controls.Add(this.lblSRPrevCaption);
            this.Controls.Add(this.lblSR);
            this.Controls.Add(this.lblSRNext);
            this.Controls.Add(this.lblSRCurrent);
            this.Controls.Add(this.lblSRPrev);
            this.Controls.Add(this.lblSpotifyStatusCaption);
            this.Controls.Add(this.lblSpotifyStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MenuForm";
            this.Text = "Radio Moscow";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox lblSpotifyStatus;
        private System.Windows.Forms.TextBox lblSpotifyStatusCaption;
        private System.Windows.Forms.TextBox lblSRPrev;
        private System.Windows.Forms.TextBox lblSRCurrent;
        private System.Windows.Forms.TextBox lblSRNext;
        private System.Windows.Forms.TextBox lblSR;
        private System.Windows.Forms.TextBox lblSRPrevCaption;
        private System.Windows.Forms.TextBox lblSRCurrentCaption;
        private System.Windows.Forms.TextBox lblSRNextCaption;
        private System.Windows.Forms.ComboBox cbChannelSelector;
        private System.Windows.Forms.TextBox lblChannelSelector;
        private System.Windows.Forms.TextBox lblSRPrevStartTime;
        private System.Windows.Forms.TextBox lblSRCurrentStartTime;
        private System.Windows.Forms.TextBox lblSRNextStartTime;
        private System.Windows.Forms.TextBox lblSRNextStartTimeCaption;
        private System.Windows.Forms.TextBox lblSRCurrentStartTimeCaption;
        private System.Windows.Forms.TextBox lblSRPrevStartTimeCaption;
        private System.Windows.Forms.TextBox lblSRNextStopTimeCaption;
        private System.Windows.Forms.TextBox lblSRCurrentStopTimeCaption;
        private System.Windows.Forms.TextBox lblSRPrevStopTimeCaption;
        private System.Windows.Forms.TextBox lblSRNextStopTime;
        private System.Windows.Forms.TextBox lblSRCurrentStopTime;
        private System.Windows.Forms.TextBox lblSRPrevStopTime;
        private System.Windows.Forms.Button btnChangeSRMode;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox lblNextReplacement;
        private System.Windows.Forms.TextBox lblCurrentReplacement;
        private System.Windows.Forms.TextBox lblPrevReplacement;
        private System.Windows.Forms.Button btnPrevReplacement;
        private System.Windows.Forms.Button btnCurrentReplacement;
        private System.Windows.Forms.Button btnNextReplacement;
        private System.Windows.Forms.ToolStripStatusLabel toolStripTime;
        private System.Windows.Forms.Button btnChangeSong;
        private System.Windows.Forms.Button btnDebug;
    }
}

