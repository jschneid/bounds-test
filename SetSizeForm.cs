using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BoundsTest
{
    /// <summary>
    /// A dialog box allowing the user to set the size of the main BoundsTest form
    /// to a specific size.
    /// </summary>
    public partial class SetSizeForm : Form
    {
        /// <summary>
        /// The main BoundsTest form.
        /// </summary>
        private Form1 _boundsTestForm;

        /// <summary>
        /// Creates a new SetSizeForm instance.
        /// </summary>
        public SetSizeForm(Form1 boundsTestForm)
        {
            InitializeComponent();

            //Assumes the incoming width and height are not more than 5 digits each in length,
            //and are valid (positive) values.
            this._boundsTestForm = boundsTestForm;
            this._widthTextBox.Text = boundsTestForm.Width.ToString();
            this._heightTextBox.Text = boundsTestForm.Height.ToString();
        }

        /// <summary>
        /// Activated when the Cancel button is clicked.  Closes the form without validating
        /// or applying settings.
        /// </summary>
        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Activated when the OK button is clicked.  Validates the values on the form's fields.
        /// If they are OK, applies the new size settings and closes the form.  Otherwise, 
        /// the SetSizeForm remains open.
        /// </summary>
        private void _okButton_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                //Validation succeeded, apply the new settings and close this form.
                this._boundsTestForm.Height = Int32.Parse(this._heightTextBox.Text);
                this._boundsTestForm.Width = Int32.Parse(this._widthTextBox.Text);
                this.Close();
            }
        }

        /// <summary>
        /// Validates the height and width values in the form's fields.
        /// </summary>
        /// <returns>True if validation succeeded (values are ok); false otherwise. </returns>
        private bool validate()
        {
            //Validate the width.
            if (!validateTextBoxValue(this._widthTextBox))
            {
                return false;
            }
            //Validate the height.
            if (!validateTextBoxValue(this._heightTextBox))
            {
                return false;
            }
            //Validation was successful, return true.
            return true;
        }

        /// <summary>
        /// Validates the height or width value in the specified textBox.
        /// </summary>
        /// <param name="textBox">The textBox to validate. </param>
        /// <returns>True if validation succeeded (the value is ok); false otherwise. </returns>
        private bool validateTextBoxValue(TextBox textBox)
        {
            int intValue;
            try
            {
                intValue = Int32.Parse(textBox.Text);
            }
            catch
            {
                //The value wasn't a valid integer; see if they entered a decimal value, and if so,
                //round it to an integer and display that in the text box.  (So if the user entered
                //something like "123.4", it will be replaced by "123".)
                try
                {
                    intValue = (int)(Double.Parse(textBox.Text));
                    textBox.Text = intValue.ToString();
                }
                catch
                {
                    //If anything went wrong with converting to a Double or converting the Double
                    //to an integer, just fall through and fail validation.
                }
                failValidation(textBox);
                return false;
            }

            //We need to check here whether we are dealing with the height textBox or the width textBox
            //in order to know which constant to validate against for the mininum allowed value.
            //If the value is smaller than the minimum allowed value, replace the value with the minimum
            //value and fail validation.
            if (textBox.Equals(this._heightTextBox))
            {
                if (intValue < Form1.MINIMUM_HEIGHT)
                {
                    this._heightTextBox.Text = Form1.MINIMUM_HEIGHT.ToString();
                    failValidation(textBox);
                    return false;
                }
            }
            else if (intValue < Form1.MINIMUM_WIDTH)
            {
                this._widthTextBox.Text = Form1.MINIMUM_WIDTH.ToString();
                failValidation(textBox);
                return false;
            }

            //Note: Apparently we have no need to manually validate or enforce maximum width or height;
            //if a large value (e.g. 99999) is entered for width or height, Windows Forms apparently
            //uses the maximum combined width or height of the active monitors for the new width/height
            //instead.

            return true;
        }

        /// <summary>
        /// Sets the focus to the specified textbox, and selects all of the text in it (if there is any).
        /// </summary>
        /// <param name="textBox">The textBox on which validation failed. </param>
        private void failValidation(TextBox textBox)
        {
            textBox.SelectAll();
            textBox.Focus();
        }
    }
}