using BannerAdHost.AdsJumbo.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BannerAdHost.AdsJumbo
{
    public partial class Form1 : Form
    {
        /*
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }
        */
        public Form1()
        {
            InitializeComponent();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bannerAds1 = new AdsJumboWinForm.BannerAds();
            this.SuspendLayout();
            // 
            // bannerAds1
            // 
            this.bannerAds1.ApplicationId = null;
            this.bannerAds1.BackColor = System.Drawing.Color.AliceBlue;
            this.bannerAds1.HeightAd = 0;
            this.bannerAds1.Location = new System.Drawing.Point(0, 0);
            this.bannerAds1.Margin = new System.Windows.Forms.Padding(0);
            this.bannerAds1.Name = "bannerAds1";
            this.bannerAds1.Size = new System.Drawing.Size(728, 90);
            this.bannerAds1.TabIndex = 0;
            this.bannerAds1.WidthAd = 0;
            this.bannerAds1.ShowAd(AppInfo.Banner.Size.Width, AppInfo.Banner.Size.Height, AppInfo.Banner.ApplicationId);
            // 
            // Form1
            // 
            switch (AppInfo.DpiScaleMode)
            {
                case true:
                    this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                    break;
                case false:
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                    break;
            }
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            /*
            switch (AppInfo.AutoScroll)
            {
                default:
                    this.AutoScroll = false;
                    break;
                case true:
                    if (AppInfo.VerticalScroll.Visible && AppInfo.HorizontalScroll.Visible)
                    {
                        this.AutoScroll = true;
                    }
                    else
                    {
                        this.AutoScroll = false;
                        if (!AppInfo.VerticalScroll.Visible)
                        {
                            this.VerticalScroll.Enabled = false;
                            this.VerticalScroll.Visible = false;
                            this.VerticalScroll.Maximum = 0;
                        }
                        if (!AppInfo.HorizontalScroll.Visible)
                        {
                            this.HorizontalScroll.Enabled = false;
                            this.HorizontalScroll.Visible = false;
                            this.HorizontalScroll.Maximum = 0;
                        }
                        this.AutoScroll = true;
                    }
                    break;
            }
            */
            this.AutoScroll = AppInfo.AutoScroll;
            // this.AutoScrollMargin = new System.Drawing.Size(0, 0);
            // this.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.BackColor = System.Drawing.Color.AliceBlue;
            // this.ClientSize = new System.Drawing.Size(AppInfo.ClientSize.Width, AppInfo.ClientSize.Height);
            this.Size = new System.Drawing.Size(AppInfo.ClientSize.Width, AppInfo.ClientSize.Height);
            // this.Enabled = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(AppInfo.ClientSize.Width, AppInfo.ClientSize.Height);
            this.Controls.Add(this.bannerAds1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = AppInfo.Text;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
            this.AllowTransparency = false;
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // cp.Style &= ~WinAPIConstants.WS_BORDER;
                cp.Style &= ~WinAPIConstants.WS_CAPTION;
                return cp;
            }
        }
        private AdsJumboWinForm.BannerAds bannerAds1;
    }
}