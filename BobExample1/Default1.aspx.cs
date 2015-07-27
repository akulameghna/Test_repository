using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BobExample1
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void click_Click(object sender, EventArgs e)
        {
            string firstName = firstNameTexatBox.Text;
            string lastName = lastNameTextBox.Text;
            string result = "Hello " + firstNameTexatBox.Text + " " + lastNameTextBox.Text;
            Label1.Text = result;
        }
    }
}