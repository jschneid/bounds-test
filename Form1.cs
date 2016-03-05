using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

// Bounds Test 
// 
// Utility for gauging the size in pixels and location of onscreen windows and components.
//
// Jonathan S. Schneider 

// Version History
//
// 1.0.0 (7/08/2005) - Initial version
//
// 1.0.1 (8/04/2005)
// - Added "Common Screen Resolutions" buttons
// - Added ruler tic marks along the right and bottom edges of the form
//
// 1.0.2 (2/17/2006)
// - The window size is now also displayed in the window titlebar
//
// 2.0.0 (7/21/2006)
// - Removed the form borders; implemented my own resize logic and 
//     close/minimize buttons.
// - The form can now be moved by clicking anywhere on it and dragging
// - The mouse wheel now resizes the form's height and width.
// - The large tic marks on the ruler are now labeled.
// * Known issue: If the form is resized such that one of the buttons 
//     is at the edge of the form, the form can't be resized at the edge
//     where it meets the button.  (Particularly noticable when one of the
//     buttons overlaps the form's lower-right corner.)
//
// 2.0.1 (8/20/2006)
// - Cosmetic updates: 
//   - Height/Width of window now in bold and at top
//   - Removed window X/Y properties (redundant with Left/Top)
//   - Moved window height/width, window properties, screen properties to separate labels
// - Added right-click context menu and About dialog
// - Added call to Application.EnableVisualStyles() to enable XP styles (if supported by host OS)
//
// 2.0.2 (7/4/2007)
// - New application icon.
//
// 2.0.3 (8/21/2007)
// - New "Set Size" right-click option.
// - Remade the About box, the old one's source was corrupted somehow?
// - New silver app icon; the old dark-blue-and-purple-on-light-blue was ugly!
// - Added a 48x48 version of the icon (to look nicer when Large Icons are enabled in the OS).
// - Increased the size of the "Bounds Test" logo on the main form.


namespace BoundsTest
{
	/// <summary>
	/// Displays coordinate information on the bounds of the Form1 form, and the current screen.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		#region Member Variables 

		private System.Windows.Forms.Label _buttonsLabel;
		private System.Windows.Forms.Button _button640x480;
		private System.Windows.Forms.Button _button800x600;
		private System.Windows.Forms.Button _button1024x768;
		private System.Windows.Forms.Button _button1280x1024;
		private System.Windows.Forms.Button _button1600x1200;
		private Button _closeButton;
		private Button _minimizeButton;
		private Label _titleLabel;
		private Label _widthHeightTextLabel;
		private Label _screenPropertiesTextLabel;
		private Label _screenPropertiesLabel;
		private Label _widthHeightValueLabel;
		private Label _screenPropertiesValueLabel;
		private Label _windowPropertiesTextLabel;
		private Label _windowPropertiesValueLabel;

		//Used to store the origin point in drag reposition operations.
		private Point _offsetPoint;

		//Used to store the type of resize being performed in drag resize operations.
		private EdgePosition _resizeInProgress;
		private ContextMenuStrip _contextMenu;
		private IContainer components;
		private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem setSizeToolStripMenuItem;

		/// <summary>
		/// The cursor we're using to indicate to the user that they can click 
		/// and drag to reposition the window.  (This is basically a constant but
		/// due to language constraints we can't declare it as such; next-best 
		/// option is to declare it as readonly.)
		/// </summary>
		//private readonly Cursor MayRepositionCursor = Cursors.NoMove2D;
		private readonly Cursor MayRepositionCursor = Cursors.SizeAll;

		#endregion 

		#region Private Enums

		/// <summary>
		/// The possible positions of the mouse cursor with respect to the edges of the form.
		/// </summary>
		private enum EdgePosition
		{
			TopLeftCorner,
			TopCenter,
			TopRightCorner,
			LeftCenter,
			NonEdge,
			RightCenter,
			BottomLeftCorner,
			BottomCenter,
			BottomRightCorner
		}

		#endregion 

        #region Public Constant Values

        public const int MINIMUM_WIDTH = 100;
        public const int MINIMUM_HEIGHT = 50;

        #endregion

        #region Construction/Destruction

        /// <summary>
		/// Creates a new Form1 instance.
		/// </summary>
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//The Visual Studio .NET 2005 Designer doesn't seem to know about the MouseWheel
			//event (?), so we'll just wire it up by hand.
			this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);

			//Make the minimize and close buttons be at the top of the z-order.
			this.Controls.SetChildIndex(this._minimizeButton, 0);
			this.Controls.SetChildIndex(this._closeButton, 0);
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._buttonsLabel = new System.Windows.Forms.Label();
            this._button640x480 = new System.Windows.Forms.Button();
            this._button800x600 = new System.Windows.Forms.Button();
            this._button1024x768 = new System.Windows.Forms.Button();
            this._button1280x1024 = new System.Windows.Forms.Button();
            this._button1600x1200 = new System.Windows.Forms.Button();
            this._closeButton = new System.Windows.Forms.Button();
            this._minimizeButton = new System.Windows.Forms.Button();
            this._titleLabel = new System.Windows.Forms.Label();
            this._widthHeightTextLabel = new System.Windows.Forms.Label();
            this._screenPropertiesTextLabel = new System.Windows.Forms.Label();
            this._screenPropertiesLabel = new System.Windows.Forms.Label();
            this._widthHeightValueLabel = new System.Windows.Forms.Label();
            this._screenPropertiesValueLabel = new System.Windows.Forms.Label();
            this._windowPropertiesTextLabel = new System.Windows.Forms.Label();
            this._windowPropertiesValueLabel = new System.Windows.Forms.Label();
            this._contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonsLabel
            // 
            this._buttonsLabel.Location = new System.Drawing.Point(99, 26);
            this._buttonsLabel.Name = "_buttonsLabel";
            this._buttonsLabel.Size = new System.Drawing.Size(115, 16);
            this._buttonsLabel.TabIndex = 1;
            this._buttonsLabel.Text = "Common Resolutions";
            this._buttonsLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._buttonsLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this._buttonsLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_MouseMove);
            // 
            // _button640x480
            // 
            this._button640x480.Location = new System.Drawing.Point(119, 41);
            this._button640x480.Name = "_button640x480";
            this._button640x480.Size = new System.Drawing.Size(74, 24);
            this._button640x480.TabIndex = 2;
            this._button640x480.Text = "640 x 480";
            this._button640x480.Click += new System.EventHandler(this._button640x480_Click);
            this._button640x480.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_MouseMove);
            // 
            // _button800x600
            // 
            this._button800x600.Location = new System.Drawing.Point(119, 68);
            this._button800x600.Name = "_button800x600";
            this._button800x600.Size = new System.Drawing.Size(74, 24);
            this._button800x600.TabIndex = 3;
            this._button800x600.Text = "800 x 600";
            this._button800x600.Click += new System.EventHandler(this._button800x600_Click);
            this._button800x600.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_MouseMove);
            // 
            // _button1024x768
            // 
            this._button1024x768.Location = new System.Drawing.Point(119, 95);
            this._button1024x768.Name = "_button1024x768";
            this._button1024x768.Size = new System.Drawing.Size(74, 24);
            this._button1024x768.TabIndex = 4;
            this._button1024x768.Text = "1024 x 768";
            this._button1024x768.Click += new System.EventHandler(this._button1024x768_Click);
            this._button1024x768.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_MouseMove);
            // 
            // _button1280x1024
            // 
            this._button1280x1024.Location = new System.Drawing.Point(119, 122);
            this._button1280x1024.Name = "_button1280x1024";
            this._button1280x1024.Size = new System.Drawing.Size(74, 24);
            this._button1280x1024.TabIndex = 5;
            this._button1280x1024.Text = "1280 x 1024";
            this._button1280x1024.Click += new System.EventHandler(this._button1280x1024_Click);
            this._button1280x1024.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_MouseMove);
            // 
            // _button1600x1200
            // 
            this._button1600x1200.Location = new System.Drawing.Point(119, 149);
            this._button1600x1200.Name = "_button1600x1200";
            this._button1600x1200.Size = new System.Drawing.Size(74, 24);
            this._button1600x1200.TabIndex = 6;
            this._button1600x1200.Text = "1600 x 1200";
            this._button1600x1200.Click += new System.EventHandler(this._button1600x1200_Click);
            this._button1600x1200.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_MouseMove);
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._closeButton.Image = global::BoundsTest.Properties.Resources.x;
            this._closeButton.Location = new System.Drawing.Point(229, 6);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(16, 15);
            this._closeButton.TabIndex = 7;
            this._closeButton.TabStop = false;
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            this._closeButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_MouseMove);
            // 
            // _minimizeButton
            // 
            this._minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._minimizeButton.Image = global::BoundsTest.Properties.Resources.min;
            this._minimizeButton.Location = new System.Drawing.Point(209, 6);
            this._minimizeButton.Name = "_minimizeButton";
            this._minimizeButton.Size = new System.Drawing.Size(16, 15);
            this._minimizeButton.TabIndex = 8;
            this._minimizeButton.TabStop = false;
            this._minimizeButton.UseVisualStyleBackColor = true;
            this._minimizeButton.Click += new System.EventHandler(this.closeButton_Click);
            this._minimizeButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_MouseMove);
            // 
            // _titleLabel
            // 
            this._titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._titleLabel.Location = new System.Drawing.Point(2, 3);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(116, 18);
            this._titleLabel.TabIndex = 9;
            this._titleLabel.Text = "Bounds Test";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._titleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this._titleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_MouseMove);
            // 
            // _widthHeightTextLabel
            // 
            this._widthHeightTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._widthHeightTextLabel.Location = new System.Drawing.Point(5, 26);
            this._widthHeightTextLabel.Name = "_widthHeightTextLabel";
            this._widthHeightTextLabel.Size = new System.Drawing.Size(48, 34);
            this._widthHeightTextLabel.TabIndex = 0;
            this._widthHeightTextLabel.Text = "Width:\r\nHeight:";
            this._widthHeightTextLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this._widthHeightTextLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_MouseMove);
            // 
            // _screenPropertiesTextLabel
            // 
            this._screenPropertiesTextLabel.Location = new System.Drawing.Point(5, 131);
            this._screenPropertiesTextLabel.Name = "_screenPropertiesTextLabel";
            this._screenPropertiesTextLabel.Size = new System.Drawing.Size(48, 105);
            this._screenPropertiesTextLabel.TabIndex = 10;
            this._screenPropertiesTextLabel.Text = "Width:\r\nHeight:\r\nX:\r\nY:\r\nLeft:\r\nRight:\r\nTop:\r\nBottom:\r\n";
            this._screenPropertiesTextLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this._screenPropertiesTextLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_MouseMove);
            // 
            // _screenPropertiesLabel
            // 
            this._screenPropertiesLabel.AutoSize = true;
            this._screenPropertiesLabel.Location = new System.Drawing.Point(1, 117);
            this._screenPropertiesLabel.Name = "_screenPropertiesLabel";
            this._screenPropertiesLabel.Size = new System.Drawing.Size(94, 13);
            this._screenPropertiesLabel.TabIndex = 11;
            this._screenPropertiesLabel.Text = "Screen Properties:";
            this._screenPropertiesLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this._screenPropertiesLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_MouseMove);
            // 
            // _widthHeightValueLabel
            // 
            this._widthHeightValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._widthHeightValueLabel.Location = new System.Drawing.Point(49, 26);
            this._widthHeightValueLabel.Name = "_widthHeightValueLabel";
            this._widthHeightValueLabel.Size = new System.Drawing.Size(35, 34);
            this._widthHeightValueLabel.TabIndex = 12;
            this._widthHeightValueLabel.Text = "8000\r\n8000";
            this._widthHeightValueLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this._widthHeightValueLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this._widthHeightValueLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_MouseMove);
            // 
            // _screenPropertiesValueLabel
            // 
            this._screenPropertiesValueLabel.Location = new System.Drawing.Point(46, 131);
            this._screenPropertiesValueLabel.Name = "_screenPropertiesValueLabel";
            this._screenPropertiesValueLabel.Size = new System.Drawing.Size(35, 115);
            this._screenPropertiesValueLabel.TabIndex = 13;
            this._screenPropertiesValueLabel.Text = "5000\r\n5000\r\n5000\r\n5000\r\n5000\r\n5000\r\n5000\r\n5000";
            this._screenPropertiesValueLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this._screenPropertiesValueLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this._screenPropertiesValueLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_MouseMove);
            // 
            // _windowPropertiesTextLabel
            // 
            this._windowPropertiesTextLabel.Location = new System.Drawing.Point(5, 54);
            this._windowPropertiesTextLabel.Name = "_windowPropertiesTextLabel";
            this._windowPropertiesTextLabel.Size = new System.Drawing.Size(48, 54);
            this._windowPropertiesTextLabel.TabIndex = 14;
            this._windowPropertiesTextLabel.Text = "Left:\r\nRight:\r\nTop:\r\nBottom:";
            this._windowPropertiesTextLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this._windowPropertiesTextLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_MouseMove);
            // 
            // _windowPropertiesValueLabel
            // 
            this._windowPropertiesValueLabel.Location = new System.Drawing.Point(46, 54);
            this._windowPropertiesValueLabel.Name = "_windowPropertiesValueLabel";
            this._windowPropertiesValueLabel.Size = new System.Drawing.Size(35, 54);
            this._windowPropertiesValueLabel.TabIndex = 15;
            this._windowPropertiesValueLabel.Text = "5000\r\n5000\r\n5000\r\n5000";
            this._windowPropertiesValueLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this._windowPropertiesValueLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_MouseDown);
            this._windowPropertiesValueLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_MouseMove);
            // 
            // _contextMenu
            // 
            this._contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setSizeToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this._contextMenu.Name = "_contextMenu";
            this._contextMenu.Size = new System.Drawing.Size(233, 48);
            // 
            // setSizeToolStripMenuItem
            // 
            this.setSizeToolStripMenuItem.Name = "setSizeToolStripMenuItem";
            this.setSizeToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.setSizeToolStripMenuItem.Text = "&Set Size...";
            this.setSizeToolStripMenuItem.Click += new System.EventHandler(this.setSizeToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.aboutToolStripMenuItem.Text = "&About Bounds Test...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(250, 250);
            this.ContextMenuStrip = this._contextMenu;
            this.Controls.Add(this._windowPropertiesValueLabel);
            this.Controls.Add(this._windowPropertiesTextLabel);
            this.Controls.Add(this._screenPropertiesValueLabel);
            this.Controls.Add(this._widthHeightValueLabel);
            this.Controls.Add(this._screenPropertiesLabel);
            this.Controls.Add(this._screenPropertiesTextLabel);
            this.Controls.Add(this._minimizeButton);
            this.Controls.Add(this._titleLabel);
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this._button1600x1200);
            this.Controls.Add(this._button1280x1024);
            this.Controls.Add(this._button1024x768);
            this.Controls.Add(this._button800x600);
            this.Controls.Add(this._button640x480);
            this.Controls.Add(this._buttonsLabel);
            this.Controls.Add(this._widthHeightTextLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(100, 50);
            this.Name = "Form1";
            this.Opacity = 0.85;
            this.Text = "Bounds Test";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Load += new System.EventHandler(this.Form1_Load);
            this._contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Main Method

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			//Use "Win XP" styles if the user has them enabled in their OS settings.
			Application.EnableVisualStyles();

			Application.Run(new Form1());
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Updates the label on the form with bounds information for the form and for the current
		/// screen.  (There could be more than one possible screen in the case of multiple monitors.)
		/// </summary>
		/// <remarks>
		/// This method assumes that the label is large enough to hold all of the information that 
		/// this method writes out. </remarks>
		private void UpdateLabel()
		{
			System.Text.StringBuilder output = new System.Text.StringBuilder();


			Rectangle bounds = this.Bounds;

			this._widthHeightValueLabel.Text = bounds.Width + Environment.NewLine +
				bounds.Height;
			this._windowPropertiesValueLabel.Text = bounds.Left + Environment.NewLine +
				bounds.Right + Environment.NewLine +
				bounds.Top + Environment.NewLine +
				bounds.Bottom + Environment.NewLine;

			Screen screen = Screen.FromRectangle(bounds);
			Rectangle screenBounds = screen.WorkingArea;

			this._screenPropertiesValueLabel.Text = screenBounds.Width + Environment.NewLine +
				screenBounds.Height + Environment.NewLine +
				screenBounds.X + Environment.NewLine +
				screenBounds.Y + Environment.NewLine +
				screenBounds.Left + Environment.NewLine +
				screenBounds.Right + Environment.NewLine +
				screenBounds.Top + Environment.NewLine +
				screenBounds.Bottom + Environment.NewLine;

			if (this.WindowState == FormWindowState.Minimized)
			{
				//If the form is minimized, don't show a size.
				this.Text = "Bounds Test";
			}
			else
			{
				//Show the form size in the title, which will show in the Windows Taskbar button
				//for the app.
				this.Text = bounds.Width + "," + bounds.Height + " - Bounds Test";
			}
		}

		/// <summary>
		/// Moves the form to the top-left corner of the current screen if a portion of the form 
		/// is off the screen to the right or to the bottom.
		/// </summary>
		private void MoveToTopLeftIfPartiallyOffScreen()
		{
			Screen screen = Screen.FromRectangle(this.Bounds);
			Rectangle screenBounds = screen.WorkingArea;
			if (this.Bounds.Right > screenBounds.Right || this.Bounds.Bottom > screenBounds.Bottom)
			{
				this.Location = new Point(screenBounds.X, screenBounds.Y);
			}
		}

		/// <summary>
		/// Draws tic marks along the right and bottom edges of the control to act as a simple ruler.
		/// </summary>
		private void DrawPixelRuler()
		{
			Graphics graphics = Graphics.FromHwnd(this.Handle);
			Pen blackPen = new Pen(Color.Black, 1.0f);
			Brush blackBrush = new SolidBrush(Color.Black);
			Font ticMarkLabelFont = new Font(FontFamily.GenericSansSerif, 7f);

			//Clear things we drew previously
			graphics.Clear(this.BackColor);

			const int ticMarkLength = 5;
			const int ticIncrement = 25; 

			//Figure out the size between the top and left of the window, and the window's client area
			//(excluding the window border and titlebar) -- we want to start drawing tic marks such that
			//we treat "0" as the edge of the window, not the edge of the client area.
			Point clientAreaTopLeftCorner = this.PointToScreen(new Point(0, 0));
			int titlebarHeight = clientAreaTopLeftCorner.Y - this.Bounds.Top;
			int leftWindowBorderWidth = clientAreaTopLeftCorner.X - this.Bounds.Left;

			//Draw tic marks along the bottom edge
			int firstTicMarkX = ticIncrement - leftWindowBorderWidth;
			for (int x = firstTicMarkX; x < this.ClientRectangle.Width; x += ticIncrement)
			{
				int y1 = this.ClientRectangle.Height - ticMarkLength;

				//Draw a bigger tic mark every 100
				if ((x - firstTicMarkX + ticIncrement) % 100 == 0)
				{
					y1 -= (int)(ticMarkLength * 1.5);

					//Also label the tic mark.
					PointF labelPoint = new PointF(x - 9, y1 - 12);
					graphics.DrawString(x.ToString(), ticMarkLabelFont, blackBrush, labelPoint); 
				}

				graphics.DrawLine(blackPen, x, y1, x, this.ClientRectangle.Height);
			}

			//Draw tic marks along the right edge
			int firstTicMarkY = ticIncrement - titlebarHeight;
			for (int y = firstTicMarkY; y < this.ClientRectangle.Height; y += ticIncrement)
			{
				int x1 = this.ClientRectangle.Width - ticMarkLength;

				//Draw a bigger tic mark every 100
				if ((y - firstTicMarkY + ticIncrement) % 100 == 0)
				{
					x1 -= (int)(ticMarkLength * 1.5);

					//Also label the tic mark.
					PointF labelPoint = new PointF(x1 - 22, y - 6);
					graphics.DrawString(y.ToString(), ticMarkLabelFont, blackBrush, labelPoint); 
				}

				graphics.DrawLine(blackPen, x1, y, this.ClientRectangle.Width, y);
			}
		}

		/// <summary>
		/// Stores the mouse cursor location in preparation for a drag reposition operation.
		/// </summary>
		/// <param name="clientX">The x-coordinate of the mouse cursor relative to the form. </param>
		/// <param name="clientY">The y-coordinate of the mouse cursor relative to the form. </param>
		private void StartMouseDown(int clientX, int clientY)
		{
			this._offsetPoint = new Point(-clientX, -clientY);
		}

		/// <summary>
		/// Repositions the form to the current mouse cursor location, relative to _offsetPoint.
		/// </summary>
		private void DoDragReposition()
		{
			Point screenMousePosition = Control.MousePosition;
			screenMousePosition.Offset(this._offsetPoint);
			this.Location = screenMousePosition;
		}

		/// <summary>
		/// Performs a drag-resize operation on the form.
		/// </summary>
        /// <remarks>Needed to implement this manually since the form is borderless, so we can't take advantage
        /// of the default implementation of this functionality built into Windows Forms.
        /// </remarks>
		/// <param name="edgePosition">The edge of the form being dragged. </param>
		/// <param name="mouseLocation">The location of the mouse cursor relative to the form. </param>
		private void DoDragResize(EdgePosition edgePosition, Point mouseLocation)
		{
			if (edgePosition == EdgePosition.RightCenter || edgePosition == EdgePosition.TopRightCorner || edgePosition == EdgePosition.BottomRightCorner)
			{
				if (mouseLocation.X >= this.MinimumSize.Width)
				{
					this.Width = mouseLocation.X;
				}
				else
				{
					this.Width = this.MinimumSize.Width;
				}
			}
			if (edgePosition == EdgePosition.BottomCenter || edgePosition == EdgePosition.BottomLeftCorner || edgePosition == EdgePosition.BottomRightCorner)
			{
				if (mouseLocation.Y >= this.MinimumSize.Height)
				{
					this.Height = mouseLocation.Y;
				}
				else
				{
					this.Height = this.MinimumSize.Height;
				}
			}
			if (edgePosition == EdgePosition.LeftCenter || edgePosition == EdgePosition.TopLeftCorner || edgePosition == EdgePosition.BottomLeftCorner)
			{
				if (this.Width - mouseLocation.X >= this.MinimumSize.Width)
				{
					this.Width -= mouseLocation.X;
					this.Left += mouseLocation.X;
				}
				else
				{
					//If the user drags fast and goes *past* the minimum width,
					//set our width and position as though they had dragged exactly
					//to the minimum width.
					int initialWidth = this.Width;
					this.Width = this.MinimumSize.Width;
					this.Left += (initialWidth - this.Width);
				}

			}
			if (edgePosition == EdgePosition.TopCenter || edgePosition == EdgePosition.TopLeftCorner || edgePosition == EdgePosition.TopRightCorner)
			{
				if (this.Height - mouseLocation.Y >= this.MinimumSize.Height)
				{
					this.Height -= mouseLocation.Y;
					this.Top += mouseLocation.Y;
				}
				else
				{
					//If the user drags fast and goes *past* the minimum height,
					//set our height and position as though they had dragged exactly
					//to the minimum height.
					int initialHeight = this.Height;
					this.Height = this.MinimumSize.Height;
					this.Top += (initialHeight - this.Height);
				}
			}

			//As long as the user is continuously resizing the form, the child controls
			//don't seem to get a chance to repaint, even if they are invalidated.
			//Call Refresh on the child controls to force an immediate repaint so the 
			//user doesn't need to pause their drag to see updated height/width, and so
			//the buttons don't display as ugly black rectangles in their invalidated 
			//area until they can repaint.
			RefreshChildControlsAfterResize();
		}

		/// <summary>
		/// Forces an immediate repaint of all of the form's child controls.
		/// </summary>
		private void RefreshChildControlsAfterResize()
		{
			foreach (Control childControl in this.Controls)
			{
				childControl.Refresh();
			}
		}

		/// <summary>
		/// Sets the mouse cursor icon based on the position of the mouse in the control's client area.
		/// </summary>
		/// <param name="mouseLocation"></param>
		private void SetCursor(Point mouseLocation)
		{
			EdgePosition mousePosition = GetEdgePosition(mouseLocation);
			if (mousePosition == EdgePosition.TopLeftCorner || mousePosition == EdgePosition.BottomRightCorner)
			{
				this.Cursor = Cursors.SizeNWSE;
			}
			else if (mousePosition == EdgePosition.TopRightCorner || mousePosition == EdgePosition.BottomLeftCorner)
			{
				this.Cursor = Cursors.SizeNESW;
			}
			else if (mousePosition == EdgePosition.TopCenter || mousePosition == EdgePosition.BottomCenter)
			{
				this.Cursor = Cursors.SizeNS;
			}
			else if (mousePosition == EdgePosition.LeftCenter || mousePosition == EdgePosition.RightCenter)
			{
				this.Cursor = Cursors.SizeWE;
			}
			else
			{
				this.Cursor = MayRepositionCursor;
			}
		}


		/// <summary>
		/// Returns a member of the EdgePosition enum corresponding to the specified
		/// mouse cursor location relative to the form's edges.
		/// </summary>
		/// <param name="mouseLocation">The current mouse position relative to the form. </param>
		/// <returns>An EdgePosition enum member. </returns>
		private EdgePosition GetEdgePosition(Point mouseLocation)
		{
			const int DragWidthHeight = 8;

			//TODO: Tweak this to make the corners bigger -- see behavior of standard windows (small edges, big corners)

			bool topEdge = mouseLocation.Y <= DragWidthHeight;
			bool leftEdge = mouseLocation.X <= DragWidthHeight;
			bool bottomEdge = mouseLocation.Y >= (this.Height - DragWidthHeight);
			bool rightEdge = mouseLocation.X >= (this.Width - DragWidthHeight);

			if (topEdge && leftEdge) return EdgePosition.TopLeftCorner;
			else if (topEdge && rightEdge) return EdgePosition.TopRightCorner;
			else if (bottomEdge && leftEdge) return EdgePosition.BottomLeftCorner;
			else if (bottomEdge && rightEdge) return EdgePosition.BottomRightCorner;
			else if (topEdge) return EdgePosition.TopCenter;
			else if (bottomEdge) return EdgePosition.BottomCenter;
			else if (leftEdge) return EdgePosition.LeftCenter;
			else if (rightEdge) return EdgePosition.RightCenter;
			else return EdgePosition.NonEdge;
		}

		#endregion

		#region Event Handlers

		private void Form1_Move(object sender, System.EventArgs e)
		{
			UpdateLabel();
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			UpdateLabel();
		}

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			UpdateLabel();

			//Need to redraw the ruler here as well as in the Paint event because
			//the Paint event doesn't fire when the form is resized to be smaller.
			DrawPixelRuler();
		}

		private void _button640x480_Click(object sender, System.EventArgs e)
		{
			this.Size = new Size(640, 480); 
			MoveToTopLeftIfPartiallyOffScreen();
		}

		private void _button800x600_Click(object sender, System.EventArgs e)
		{
			this.Size = new Size(800, 600); 
			MoveToTopLeftIfPartiallyOffScreen();
		}

		private void _button1024x768_Click(object sender, System.EventArgs e)
		{
			this.Size = new Size(1024, 768); 
			MoveToTopLeftIfPartiallyOffScreen();
		}

		private void _button1280x1024_Click(object sender, System.EventArgs e)
		{
			this.Size = new Size(1280, 1024);
			MoveToTopLeftIfPartiallyOffScreen();
		}

		private void _button1600x1200_Click(object sender, System.EventArgs e)
		{
			this.Size = new Size(1600, 1200);
			MoveToTopLeftIfPartiallyOffScreen();
		}

		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			DrawPixelRuler();
		}

		private void minimizeButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		/// <summary>
		/// Activated when the mouse button is pressed down on the form.
		/// Starts a drag resize or reposition operation.
		/// </summary>
		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			StartMouseDown(e.X, e.Y);
			this._resizeInProgress = GetEdgePosition(e.Location);
		}

		/// <summary>
		/// Activated when the mouse cursor is moved over the form.
		/// Sets the cursor, and continues a drag operation if the left
		/// mouse button is down.
		/// </summary>
		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
			{
				SetCursor(e.Location);
			}
			if (e.Button == MouseButtons.Left)
			{
				if (this._resizeInProgress == EdgePosition.NonEdge)
				{
					DoDragReposition();
				}
				else
				{
					DoDragResize(this._resizeInProgress, e.Location);
				}
			}
		}

		/// <summary>
		/// Activated when a mouse button is clicked on one of the labels in the control.
		/// Starts a drag reposition (move) operation.
		/// </summary>
		private void Label_MouseDown(object sender, MouseEventArgs e)
		{
			Point labelPosition = ((Label)sender).Location;
			Point cursorLocationRelativeToLabel = e.Location;
			Point cursorLocationRelativeToForm = new Point(cursorLocationRelativeToLabel.X + labelPosition.X, cursorLocationRelativeToLabel.Y + labelPosition.Y);

			StartMouseDown(cursorLocationRelativeToForm.X, cursorLocationRelativeToForm.Y);
			this._resizeInProgress = GetEdgePosition(cursorLocationRelativeToForm);
		}

		/// <summary>
		/// Activated when the mouse cursor is moved over on one of the labels in the control.
		/// Sets the cursor to the "reposition" cursor, and continues a drag reposition or
		/// resize operation if the left mouse button is being held down.
		/// </summary>
		private void Label_MouseMove(object sender, MouseEventArgs e)
		{
			Point labelPosition = ((Label)sender).Location;
			Point cursorLocationRelativeToLabel = e.Location;
			Point cursorLocationRelativeToForm = new Point(cursorLocationRelativeToLabel.X + labelPosition.X, cursorLocationRelativeToLabel.Y + labelPosition.Y);

			if (e.Button == MouseButtons.None)
			{
				SetCursor(cursorLocationRelativeToForm);
			}
			if (e.Button == MouseButtons.Left)
			{
				if (this._resizeInProgress == EdgePosition.NonEdge)
				{
					DoDragReposition();
				}
				else
				{
					DoDragResize(this._resizeInProgress, cursorLocationRelativeToForm);
				}
			}
		}

		/// <summary>
		/// Activated when the mouse cursor is moved over one of the buttons on the form.
		/// Sets the cursor icon to the Arrow.
		/// </summary>
		private void Button_MouseMove(object sender, MouseEventArgs e)
		{
			this.Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// Activated when the mouse wheel is scrolled while the form has the focus.
		/// Increases or decreases the height and width of the form.
		/// </summary>
		void Form1_MouseWheel(object sender, MouseEventArgs e)
		{
			const int increment = 10;
			if (e.Delta > 0)
			{
				this.Height += increment - ((this.Height + (increment * (e.Delta / 120))) % increment);
				this.Width += increment - ((this.Width + (increment * (e.Delta / 120))) % increment);
			}
			else
			{
				int heightChange = ((this.Height + (increment * (e.Delta / 120))) % increment);
				if (heightChange == 0)
				{
					heightChange = increment;
				}
				int widthChange = ((this.Width + (increment * (e.Delta / 120))) % increment);
				if (widthChange == 0)
				{
					widthChange = increment;
				}
				this.Height -= heightChange;
				this.Width -= widthChange;
			}
		}

		/// <summary>
		/// Activated when the "About" option is selected from the context menu.
        /// Shows the "About" message box.
		/// </summary>
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			About2 aboutWindow = new About2();
			aboutWindow.ShowDialog();
		}

        /// <summary>
        /// Activated when "Set Size..." is selected from the context menu.
        /// Shows the Set Size dialog.
        /// </summary>
        private void setSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSizeForm setSizeForm = new SetSizeForm(this);
            setSizeForm.ShowDialog();
        }
        
		#endregion

	}
}
