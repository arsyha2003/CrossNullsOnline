﻿namespace TcpLientPratice
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

        #region W#region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            button12 = new Button();
            button13 = new Button();
            button14 = new Button();
            button11 = new Button();
            button15 = new Button();
            button16 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(72, 59);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(83, 96);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button00CLick;
            // 
            // button2
            // 
            button2.Location = new Point(173, 59);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(83, 96);
            button2.TabIndex = 1;
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button01CLick;
            // 
            // button3
            // 
            button3.Location = new Point(273, 59);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(83, 96);
            button3.TabIndex = 2;
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button02CLick;
            // 
            // button4
            // 
            button4.Location = new Point(72, 161);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(83, 96);
            button4.TabIndex = 3;
            button4.UseVisualStyleBackColor = true;
            button4.Click += Button10CLick;
            // 
            // button5
            // 
            button5.Location = new Point(173, 161);
            button5.Margin = new Padding(3, 4, 3, 4);
            button5.Name = "button5";
            button5.Size = new Size(83, 96);
            button5.TabIndex = 4;
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button11CLick;
            // 
            // button6
            // 
            button6.Location = new Point(273, 161);
            button6.Margin = new Padding(3, 4, 3, 4);
            button6.Name = "button6";
            button6.Size = new Size(83, 96);
            button6.TabIndex = 5;
            button6.UseVisualStyleBackColor = true;
            button6.Click += Button12CLick;
            // 
            // button7
            // 
            button7.Location = new Point(72, 264);
            button7.Margin = new Padding(3, 4, 3, 4);
            button7.Name = "button7";
            button7.Size = new Size(83, 96);
            button7.TabIndex = 6;
            button7.UseVisualStyleBackColor = true;
            button7.Click += Button20CLick;
            // 
            // button8
            // 
            button8.Location = new Point(173, 264);
            button8.Margin = new Padding(3, 4, 3, 4);
            button8.Name = "button8";
            button8.Size = new Size(83, 96);
            button8.TabIndex = 7;
            button8.UseVisualStyleBackColor = true;
            button8.Click += Button21CLick;
            // 
            // button9
            // 
            button9.Location = new Point(273, 265);
            button9.Margin = new Padding(3, 4, 3, 4);
            button9.Name = "button9";
            button9.Size = new Size(83, 96);
            button9.TabIndex = 8;
            button9.UseVisualStyleBackColor = true;
            button9.Click += Button22CLick;
            // 
            // button12
            // 
            button12.Location = new Point(726, 518);
            button12.Margin = new Padding(3, 4, 3, 4);
            button12.Name = "button12";
            button12.Size = new Size(176, 44);
            button12.TabIndex = 10;
            button12.Text = "Выбрать оппонента";
            button12.UseVisualStyleBackColor = true;
            button12.Click += SelectServer;
            // 
            // button13
            // 
            button13.Location = new Point(705, 40);
            button13.Name = "button13";
            button13.Size = new Size(176, 43);
            button13.TabIndex = 11;
            button13.Text = "Признать поражение";
            button13.UseVisualStyleBackColor = true;
            button13.Click += GetLose;
            // 
            // button14
            // 
            button14.Location = new Point(705, 85);
            button14.Name = "button14";
            button14.Size = new Size(176, 43);
            button14.TabIndex = 12;
            button14.Text = "Предложить ничью";
            button14.UseVisualStyleBackColor = true;
            button14.Click += OfferADraw;
            // 
            // button11
            // 
            button11.Location = new Point(58, 376);
            button11.Name = "button11";
            button11.Size = new Size(310, 125);
            button11.TabIndex = 13;
            button11.Text = "Сервер предложил ничью, согласиться?";
            button11.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            button15.Location = new Point(58, 507);
            button15.Name = "button15";
            button15.Size = new Size(79, 31);
            button15.TabIndex = 14;
            button15.Text = "ДА";
            button15.UseVisualStyleBackColor = true;
            button15.Click += AgreeDraw;
            // 
            // button16
            // 
            button16.Location = new Point(289, 507);
            button16.Name = "button16";
            button16.Size = new Size(79, 31);
            button16.TabIndex = 15;
            button16.Text = "Нет";
            button16.UseVisualStyleBackColor = true;
            button16.Click += DisagreeDraw;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(button16);
            Controls.Add(button15);
            Controls.Add(button11);
            Controls.Add(button14);
            Controls.Add(button13);
            Controls.Add(button12);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button12;
        private Button button13;
        private Button button14;
        private Button button11;
        private Button button15;
        private Button button16;
    }
}
