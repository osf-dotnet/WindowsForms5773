using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;

namespace Messageboxbuilder
{
    public partial class MainForm : Form
    {
        int SelectedIcon = 0;

        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            InitForm();
        }

        private void InitForm()
        {
            cboButtons.SelectedIndex = 0;
            listBox1.SelectedIndex = SelectedIcon;
            txtCode.BackColor = Color.White;
            imageList1.Images.Add(System.Drawing.SystemIcons.Information);
            imageList1.Images.Add(System.Drawing.SystemIcons.Question);
            imageList1.Images.Add(System.Drawing.SystemIcons.Exclamation);
            imageList1.Images.Add(System.Drawing.SystemIcons.Error);
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        protected void btnOK_Click(object sender, System.EventArgs e)
        {
            if (rdoClipboard.Checked)
            {
                // Copy the generated code to the clipboard.
                Clipboard.SetDataObject(txtCode.Text, true);
            }
            else
            {
                // Write the generated code to a file.
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.OverwritePrompt = true;
                if (dlg.ShowDialog() == DialogResult.Cancel)
                    return;
                string strName = dlg.FileName;
                FileStream strm = null;
                StreamWriter writer = null;
                while (true)
                {
                    try
                    {
                        strm = new FileStream(strName, FileMode.Create, FileAccess.Write);
                    }
                    catch (Exception)
                    {
                        DialogResult result = MessageBox.Show("Cannot open file. Press Ignore to exit without saving.", "Warning", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        switch (result)
                        {
                            case DialogResult.Abort:
                                return;
                            case DialogResult.Retry:
                                continue;
                            case DialogResult.Ignore:
                                this.Close();
                                return;
                        }
                    }
                    try
                    {
                        writer = new StreamWriter(strm);
                        writer.WriteLine(txtCode.Text);
                        writer.Flush();
                    }
                    catch
                    {
                        DialogResult result = MessageBox.Show("Could not write to file. Press Ignore to exit anyway", "Write failed", MessageBoxButtons.AbortRetryIgnore);
                        switch (result)
                        {
                            case DialogResult.Abort:
                                writer.Close();
                                strm.Close();
                                return;
                            case DialogResult.Retry:
                                writer.Close();
                                strm.Close();
                                continue;
                            case DialogResult.Ignore:
                                break;
                        }
                    }
                    writer.Close();
                    strm.Close();
                    break;
                }
            }
            this.Close();
        }
        private void btnDefault1_Clicked(object sender, System.EventArgs e)
        {
            cbnDefault1.Checked ^= true;
            cbnDefault2.Checked = false;
            cbnDefault3.Checked = false;
            OnMessageBoxChanged(sender, e);
        }

        private void btnDefault2_Clicked(object sender, System.EventArgs e)
        {
            cbnDefault1.Checked = false;
            cbnDefault2.Checked ^= true;
            cbnDefault3.Checked = false;
            OnMessageBoxChanged(sender, e);
        }

        private void btnDefault3_Clicked(object sender, System.EventArgs e)
        {
            cbnDefault1.Checked = false;
            cbnDefault2.Checked = false;
            cbnDefault3.Checked ^= true;
            OnMessageBoxChanged(sender, e);
        }

        private void OnMessageBoxChanged(object sender, System.EventArgs e)
        {
            txtCode.Text = BuildMessageBox();
            txtCode.SelectionStart = 0;
            txtCode.SelectionLength = 0;
            lblSampleText.Text = txtMessage.Text;
        }

        protected void txtCaption_TextChanged(object sender, System.EventArgs e)
        {
            lblCaption.Text = txtCaption.Text;
            OnMessageBoxChanged(sender, e);
        }

        protected void txtMessage_TextChanged(object sender, System.EventArgs e)
        {
            lblSampleText.Text = txtMessage.Text;
            OnMessageBoxChanged(sender, e);
        }

        protected void cboButtons_OnSelectedIndexChanged(object sender, System.EventArgs e)
        {
            grpDefault.Enabled = true;
            int index = cboButtons.SelectedIndex;
            switch (index)
            {
                case 0:      // OK
                    btnDefault1.Visible = false;
                    btnDefault2.Visible = false;
                    btnDefault3.Visible = false;
                    cbnDefault1.Checked = false;
                    cbnDefault2.Checked = false;
                    cbnDefault3.Checked = false;
                    cbnDefault1.Visible = false;
                    cbnDefault2.Visible = false;
                    cbnDefault3.Visible = false;
                    this.pbIcon.Visible = false;
                    btnSample1.Visible = false;
                    btnSample2.Visible = false;
                    btnSample3.Visible = true;
                    btnSample4.Visible = false;
                    btnSample5.Visible = false;
                    btnSample3.Text = "OK";
                    break;
                case 1:    // OK/Cancel
                    cbnDefault1.Checked = false;
                    cbnDefault2.Checked = false;
                    cbnDefault3.Checked = false;
                    btnDefault1.Text = "OK";
                    btnDefault2.Text = "Cancel";
                    btnDefault1.Visible = true;
                    btnDefault2.Visible = true;
                    btnDefault3.Visible = false;
                    cbnDefault1.Visible = true;
                    cbnDefault2.Visible = true;
                    cbnDefault3.Visible = false;
                    txtCode.Text = BuildMessageBox();
                    btnSample1.Visible = false;
                    btnSample2.Visible = true;
                    btnSample3.Visible = false;
                    btnSample4.Visible = true;
                    btnSample5.Visible = false;
                    btnSample2.Text = "OK";
                    btnSample4.Text = "Cancel";
                    break;
                case 2:      // Retry/Cancel
                    cbnDefault1.Checked = false;
                    cbnDefault2.Checked = false;
                    cbnDefault3.Checked = false;
                    btnDefault1.Text = "Retry";
                    btnDefault2.Text = "Cancel";
                    btnDefault1.Visible = true;
                    btnDefault2.Visible = true;
                    btnDefault3.Visible = false;
                    cbnDefault1.Visible = true;
                    cbnDefault2.Visible = true;
                    cbnDefault3.Visible = false;
                    txtCode.Text = BuildMessageBox();
                    btnSample1.Visible = false;
                    btnSample2.Visible = true;
                    btnSample3.Visible = false;
                    btnSample4.Visible = true;
                    btnSample5.Visible = false;
                    btnSample2.Text = "Retry";
                    btnSample4.Text = "Cancel";
                    break;
                case 3:      // Abort/Retry/Ignore
                    cbnDefault1.Checked = false;
                    cbnDefault2.Checked = false;
                    cbnDefault3.Checked = false;
                    btnDefault1.Text = "Abort";
                    btnDefault2.Text = "Retry";
                    btnDefault3.Text = "Cancel";
                    btnDefault1.Visible = true;
                    btnDefault2.Visible = true;
                    btnDefault3.Visible = true;
                    cbnDefault1.Visible = true;
                    cbnDefault2.Visible = true;
                    cbnDefault3.Visible = true;
                    txtCode.Text = BuildMessageBox();
                    btnSample1.Visible = true;
                    btnSample2.Visible = false;
                    btnSample3.Visible = true;
                    btnSample4.Visible = false;
                    btnSample5.Visible = true;
                    btnSample1.Text = "Abort";
                    btnSample3.Text = "Retry";
                    btnSample5.Text = "Ignore";
                    break;
                case 4:      // Yes/No
                    cbnDefault1.Checked = false;
                    cbnDefault2.Checked = false;
                    cbnDefault3.Checked = false;
                    btnDefault1.Text = "Yes";
                    btnDefault2.Text = "No";
                    btnDefault1.Visible = true;
                    btnDefault2.Visible = true;
                    btnDefault3.Visible = false;
                    cbnDefault1.Visible = true;
                    cbnDefault2.Visible = true;
                    cbnDefault3.Visible = false;
                    txtCode.Text = BuildMessageBox();
                    btnSample1.Visible = false;
                    btnSample2.Visible = true;
                    btnSample3.Visible = false;
                    btnSample4.Visible = true;
                    btnSample5.Visible = false;
                    btnSample2.Text = "Yes";
                    btnSample4.Text = "No";
                    break;
                case 5:    // Yes/No/Cancel
                    cbnDefault1.Checked = false;
                    cbnDefault2.Checked = false;
                    cbnDefault3.Checked = false;
                    btnDefault1.Text = "Yes";
                    btnDefault2.Text = "No";
                    btnDefault3.Text = "Cancel";
                    btnDefault1.Visible = true;
                    btnDefault2.Visible = true;
                    btnDefault3.Visible = true;
                    cbnDefault1.Visible = true;
                    cbnDefault2.Visible = true;
                    cbnDefault3.Visible = true;
                    txtCode.Text = BuildMessageBox();
                    btnSample1.Visible = true;
                    btnSample2.Visible = false;
                    btnSample3.Visible = true;
                    btnSample4.Visible = false;
                    btnSample5.Visible = true;
                    btnSample1.Text = "Yes";
                    btnSample3.Text = "No";
                    btnSample5.Text = "Cancel";
                    break;
            }
            string str = BuildMessageBox();
            OnMessageBoxChanged(sender, e);
        }
        private string BuildMessageBox()
        {
            string result = "";
            if (cbnVisualBasic.Checked)
            {
                result = BuildForVisualBasic();
            }
            else if (cbnVisualCPP.Checked)
                result = BuildForCPlusPlus();
            else
                result = BuildForCSharp();
            return (result);
        }
        private string BuildForCPlusPlus()
        {
            string strButtons = "";
            string result = "";
            string strSwitch = "";
            if (cbnUseMFC.Checked)
                result = "AfxMessageBox(\"";
            else
                result = "MessageBox (NULL, \"";
            string[] lines = txtMessage.Lines;
            for (int x = 0; x < lines.Length; ++x)
            {
                result += lines[x];
                if (x < (lines.Length - 1))
                    result += "\\n";
            }
            result += "\",\"";
            result += txtCaption.Text + "\"";
            switch (cboButtons.SelectedIndex)
            {
                default:
                    break;
                case 0:
                    strSwitch = "\tcase IDOK:";
                    strSwitch += "\r\n\t\tbreak;";
                    break;
                case 1:      // OK/Cancel
                    strButtons = "MB_OKCANCEL";
                    btnStyle = MessageBoxButtons.OKCancel;
                    strSwitch = "\tcase IDOK:";
                    strSwitch += "\r\n\t\tbreak;";
                    strSwitch += "\r\n\tcase IDCANCEL:";
                    strSwitch += "\r\n\t\tbreak;";
                    break;
                case 2:      //Retry/Cancel
                    strButtons = "MB_RETRYCANCEL";
                    btnStyle = MessageBoxButtons.RetryCancel;
                    strSwitch = "\tcase IDRETRY:";
                    strSwitch += "\r\n\t\tbreak;";
                    strSwitch += "\r\n\tcase IDCANCEL:";
                    strSwitch += "\r\n\t\tbreak;";
                    break;
                case 3://Abort/Retry/Ignore
                    strButtons = "MB_ABORTRETRYIGNORE";
                    btnStyle = MessageBoxButtons.AbortRetryIgnore;
                    strSwitch = "\tcase IDABORT:";
                    strSwitch += "\r\n\t\tbreak;";
                    strSwitch += "\r\n\tcase IDRETRY:";
                    strSwitch += "\r\n\t\tbreak;";
                    strSwitch += "\r\n\tcase IDIGNORE:";
                    strSwitch += "\r\n\t\tbreak;";
                    break;
                case 4:    // Yes/No
                    strButtons = "MB_YESNO";
                    btnStyle = MessageBoxButtons.YesNo;
                    strSwitch = "\tcase IDYES:";
                    strSwitch += "\r\n\t\tbreak;";
                    strSwitch += "\r\n\tcase IDNO:";
                    strSwitch += "\r\n\t\tbreak;";
                    break;
                case 5://Yes/No/Cancel
                    strButtons = "MB_YESNOCANCEL";
                    btnStyle = MessageBoxButtons.YesNoCancel;
                    strSwitch = "\tcase IDYES:";
                    strSwitch += "\r\n\t\tbreak;";
                    strSwitch += "\r\n\tcase IDNO:";
                    strSwitch += "\r\n\t\tbreak;";
                    strSwitch += "\r\n\tcase IDCANCEL:";
                    strSwitch += "\r\n\t\tbreak;";
                    break;
            }
            if ((listBox1.SelectedIndex > 0) && (strButtons != ""))
                strButtons += " | ";
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    this.pbIcon.Visible = false;
                    break;
                case 1:
                    strButtons += "MB_ICONINFORMATION";
                    iconStyle = MessageBoxIcon.Information;
                    this.pbIcon.Visible = true;
                    this.pbIcon.Image = ((System.Drawing.Bitmap)(imageList1.Images[0]));
                    break;
                case 2:
                    strButtons += "MB_ICONQUESTION";
                    iconStyle = MessageBoxIcon.Question;
                    this.pbIcon.Visible = true;
                    this.pbIcon.Image = ((System.Drawing.Bitmap)(imageList1.Images[1]));
                    break;
                case 3:
                    strButtons += "MB_ICONEXCLAMATION";
                    iconStyle = MessageBoxIcon.Exclamation;
                    this.pbIcon.Visible = true;
                    this.pbIcon.Image = ((System.Drawing.Bitmap)(imageList1.Images[2]));
                    break;
                case 4:
                    strButtons += "MB_ICONERROR";
                    iconStyle = MessageBoxIcon.Error;
                    this.pbIcon.Visible = true;
                    this.pbIcon.Image = ((System.Drawing.Bitmap)(imageList1.Images[3]));
                    break;
            }
            int iDefButton = 0;
            if (cbnDefault1.Checked)
                iDefButton = 1;
            if (cbnDefault2.Checked)
                iDefButton = 2;
            if (cbnDefault3.Checked)
                iDefButton = 3;
            if (iDefButton > 0)
            {
                if (strButtons != "")
                    strButtons += " | ";
                strButtons += "MB_DEFBUTTON";
                strButtons += iDefButton.ToString();
            }
            switch (iDefButton)
            {
                case 1:
                    btnDefault = MessageBoxDefaultButton.Button1;
                    break;
                case 2:
                    btnDefault = MessageBoxDefaultButton.Button2;
                    break;
                case 3:
                    btnDefault = MessageBoxDefaultButton.Button3;
                    break;
            }
            if (strButtons != "")
            {
                result += ", ";
                result += strButtons;
            }
            result += ");";
            if (cbnUseReturnVar.Checked)
            {
                if (cbnDeclareIt.Checked)
                    result = "int " + txtVariable.Text + " = " + result;
                else
                    result = txtVariable.Text + " = " + result;
                if (cbnBuildSwitch.Checked)
                {
                    result += "\r\n";
                    result += "switch(" + txtVariable.Text + ")";
                    result += "\r\n{";
                    result += "\r\n";
                    result += strSwitch;
                    result += "\r\n}";
                }
            }
            return (result);
        }
        private string BuildForVisualBasic()
        {
            string strIcon = "";
            string strButtons = "";
            string strDefButton = "";
            string strSwitch = "";
            string result = "";
            if (cbnUseReturnVar.Checked)
            {
                if (cbnDeclareIt.Checked)
                    result = "dim " + txtVariable.Text + " as int32\r\n";
                result += txtVariable.Text + " = ";
            }
            result += "MessageBox.Show(\"";
            string[] lines = txtMessage.Lines;
            string strUnion;
            if (cbnVisualBasic.Checked)
            {
                strUnion = " ";
            }
            else
            {
                strUnion = "\\n";
            }
            for (int x = 0; x < lines.Length; ++x)
            {
                result += lines[x];
                if (x < (lines.Length - 1))
                    result += strUnion;
            }
            result += "\", \"";
            result += txtCaption.Text + "\"";

            ButtonsAreUs(ref strButtons, ref strSwitch, ref strIcon, ref strDefButton);
            if (strButtons.Length > 0)
            {
                result += ", ";
                result += strButtons;
            }
            if (strIcon.Length > 0)
            {
                result += ", ";
                result += strIcon;
            }
            if (strDefButton.Length > 0)
            {
                result += ", ";
                result += strDefButton;
            }
            result += ")";
            if (cbnUseReturnVar.Checked && cbnBuildSwitch.Checked)
            {
                result += "\r\n";
                strSwitch = "";
                strSwitch = "Select " + txtVariable.Text + "\r\n";
                switch (cboButtons.SelectedIndex)
                {
                    case 0:    //OK
                        strSwitch += "\tCase DialogResult.OK\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        break;
                    case 1:    //OK/Cancel
                        strSwitch += "\tCase DialogResult.OK\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        strSwitch += "\tCase DialogResult.Cancel\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        break;
                    case 2:    //Retry/Cancel
                        strSwitch += "\tCase DialogResult.Retry\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        strSwitch += "\tCase DialogResult.Cancel\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        break;
                    case 3:    //Abort/Retry/Ignore
                        strSwitch += "\tCase DialogResult.Abort\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        strSwitch += "\tCase DialogResult.Retry\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        strSwitch += "\tCase DialogResult.Ignore\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        break;
                    case 4://Yes/No
                        strSwitch += "\tCase DialogResult.Yes\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        strSwitch += "\tCase DialogResult.No\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        break;
                    case 5://Yes/No/Cancel
                        strSwitch += "\tCase DialogResult.Yes\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        strSwitch += "\tCase DialogResult.No\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        strSwitch += "\tCase DialogResult.Cancel\r\n";
                        strSwitch += "\t\t' Add your code here\r\n";
                        break;
                }
                strSwitch += "End Select\r\n";
                result += strSwitch;
            }
            return (result);
        }
        private string BuildForVisualBasicOld()
        {
            string strIcon = "";
            string strButtons = "";
            string strDefButton = "";
            string strSwitch = "";
            string result = "";
            if (cbnUseReturnVar.Checked)
            {
                if (cbnDeclareIt.Checked)
                    result = "dim " + txtVariable.Text + " as int32\r\n";
                result += txtVariable.Text + " = ";
            }
            result += "MessageBox.Show(\"";
            string[] lines = txtMessage.Lines;
            string strUnion;
            if (cbnVisualBasic.Checked)
            {
                strUnion = " ";
            }
            else
            {
                strUnion = "\\n";
            }
            for (int x = 0; x < lines.Length; ++x)
            {
                result += lines[x];
                if (x < (lines.Length - 1))
                    result += strUnion;
            }
            result += "\", \"";
            result += txtCaption.Text + "\"";

            ButtonsAreUs(ref strButtons, ref strSwitch, ref strIcon, ref strDefButton);
            if (strButtons.Length > 0)
            {
                result += ", ";
                result += strButtons;
            }
            if (strIcon.Length > 0)
            {
                result += ", ";
                result += strIcon;
            }
            if (strDefButton.Length > 0)
            {
                result += ", ";
                result += strDefButton;
            }
            result += ")";
            if (cbnUseReturnVar.Checked && cbnBuildSwitch.Checked)
            {
                result += "\r\n";
                strSwitch = "";
                switch (cboButtons.SelectedIndex)
                {
                    case 0:    //OK
                        strSwitch = "If " + txtVariable.Text + " = dialogresult.OK Then\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "End If";
                        break;
                    case 1:    //OK/Cancel
                        strSwitch = "If " + txtVariable.Text + " = DialogResult.OK Then\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "ElseIf " + txtVariable.Text + " = DialogResult.Cancel\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "End If";
                        break;
                    case 2:    //Retry/Cancel
                        strSwitch = "If " + txtVariable.Text + " = DialogResult.Retry Then\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "ElseIf " + txtVariable.Text + " = DialogResult.Cancel\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "End If";
                        break;
                    case 3:    //Abort/Retry/Ignore
                        strSwitch = "If " + txtVariable.Text + " = DialogResult.Abort Then\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "ElseIf " + txtVariable.Text + " = DialogResult.Retry\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "ElseIf " + txtVariable.Text + " = DialogResult.Ignore\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "End If";
                        break;
                    case 4://Yes/No
                        strSwitch = "If " + txtVariable.Text + " = DialogResult.Yes Then\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "ElseIf " + txtVariable.Text + " = DialogResult.No\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "End If";
                        break;
                    case 5://Yes/No/Cancel
                        strSwitch = "If " + txtVariable.Text + " = DialogResult.Yes Then\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "ElseIf " + txtVariable.Text + " = DialogResult.No\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "ElseIf " + txtVariable.Text + " = DialogResult.Cancel\r\n";
                        strSwitch += "\t' Add your code here\r\n";
                        strSwitch += "End If";
                        break;
                }
                result += strSwitch;
            }
            return (result);
        }
        private string BuildForCSharp()
        {
            string strIcon = "";
            string strButtons = "";
            string strDefButton = "";
            string result = "";
            result = "MessageBox.Show(\"";

            string[] lines = txtMessage.Lines;
            for (int x = 0; x < lines.Length; ++x)
            {
                result += lines[x];
                if (x < (lines.Length - 1))
                    result += "\\n";
            }
            string strSwitch = "";

            result += "\",\"";
            result += txtCaption.Text + "\"";
            ButtonsAreUs(ref strButtons, ref strSwitch, ref strIcon, ref strDefButton);
            if (strButtons.Length > 0)
            {
                result += ", ";
                result += strButtons;
            }
            if (strIcon.Length > 0)
            {
                result += ", ";
                result += strIcon;
            }
            if (strDefButton.Length > 0)
            {
                result += ", ";
                result += strDefButton;
            }

            result += ");";

            if (cbnUseReturnVar.Checked)
            {
                if (cbnDeclareIt.Checked)
                    result = "DialogResult " + txtVariable.Text + " = " + result;
                else
                    result = txtVariable.Text + " = " + result;
                if (cbnBuildSwitch.Checked)
                {
                    result += "\r\n";
                    result += "switch (" + txtVariable.Text + ")";
                    result += "\r\n{";
                    result += "\r\n";
                    result += strSwitch;
                    result += "\r\n}";
                }
            }
            return (result);
        }

        private void ButtonsAreUs(ref string strButtons, ref string strSwitch, ref string strIcon, ref string strDefButton)
        {
            if (cboButtons.SelectedIndex != -1)
            {
                switch (cboButtons.SelectedIndex)
                {
                    case 0:      //OK
                        strSwitch = "\tcase DialogResult.OK:";
                        strSwitch += "\r\n\t\tbreak;";
                        break;
                    case 1:      //OK/Cancel
                        strButtons = "MessageBoxButtons.OKCancel";
                        btnStyle = MessageBoxButtons.OKCancel;
                        strSwitch = "\tcase DialogResult.OK :";
                        strSwitch += "\r\n\t\tbreak;";
                        strSwitch += "\r\n\tcase DialogResult.Cancel :";
                        strSwitch += "\r\n\t\tbreak;";
                        break;
                    case 2:      //Retry/Cancel
                        strButtons = "MessageBoxButtons.RetryCancel";
                        btnStyle = MessageBoxButtons.RetryCancel;
                        strSwitch = "\tcase DialogResult.Retry :";
                        strSwitch += "\r\n\t\tbreak;";
                        strSwitch += "\r\n\tcase DialogResult.Cancel :";
                        strSwitch += "\r\n\t\tbreak;";
                        break;
                    case 3:      //Abort/Retry/Ignore
                        strButtons = "MessageBoxButtons.AbortRetryIgnore";
                        btnStyle = MessageBoxButtons.AbortRetryIgnore;
                        strSwitch = "\tcase DialogResult.Abort :";
                        strSwitch += "\r\n\t\tbreak;";
                        strSwitch += "\r\n\tcase DialogResult.Retry :";
                        strSwitch += "\r\n\t\tbreak;";
                        strSwitch += "\r\n\tcase DialogResult.Ignore :";
                        strSwitch += "\r\n\t\tbreak;";
                        break;
                    case 4:      //Yes/No
                        strButtons = "MessageBoxButtons.YesNo";
                        btnStyle = MessageBoxButtons.YesNo;
                        strSwitch = "\tcase DialogResult.Yes :";
                        strSwitch += "\r\n\t\tbreak;";
                        strSwitch += "\r\n\tcase DialogResult.No :";
                        strSwitch += "\r\n\t\tbreak;";
                        break;
                    case 5:      //Yes/No/Cancel
                        strButtons = "MessageBoxButtons.YesNoCancel";
                        btnStyle = MessageBoxButtons.YesNoCancel;
                        strSwitch = "\tcase DialogResult.Yes :";
                        strSwitch += "\r\n\t\tbreak;";
                        strSwitch += "\r\n\tcase DialogResult.No :";
                        strSwitch += "\r\n\t\tbreak;";
                        strSwitch += "\r\n\tcase DialogResult.Cancel :";
                        strSwitch += "\r\n\t\tbreak;";
                        break;
                }

                if (cbnDefault1.Checked)
                {
                    strDefButton = "MessageBoxDefaultButton.Button1";
                    btnDefault = MessageBoxDefaultButton.Button1;
                }
                else if (cbnDefault2.Checked)
                {
                    strDefButton = "MessageBoxDefaultButton.Button2";
                    btnDefault = MessageBoxDefaultButton.Button2;
                }
                else if (cbnDefault3.Checked)
                {
                    strDefButton = "MessageBoxDefaultButton.Button3";
                    btnDefault = MessageBoxDefaultButton.Button3;
                }
            }
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    this.pbIcon.Visible = false;
                    break;
                case 1:
                    strIcon = "MessageBoxIcon.Information";
                    iconStyle = MessageBoxIcon.Information;
                    this.pbIcon.Visible = true;
                    this.pbIcon.Image = ((System.Drawing.Bitmap)(imageList1.Images[0]));
                    break;
                case 2:
                    strIcon = "MessageBoxIcon.Question";
                    iconStyle = MessageBoxIcon.Question;
                    this.pbIcon.Visible = true;
                    this.pbIcon.Image = ((System.Drawing.Bitmap)(imageList1.Images[1]));
                    break;
                case 3:
                    strIcon = "MessageBoxIcon.Exclamation";
                    iconStyle = MessageBoxIcon.Exclamation;
                    this.pbIcon.Visible = true;
                    this.pbIcon.Image = ((System.Drawing.Bitmap)(imageList1.Images[2]));
                    break;
                case 4:
                    strIcon = "MessageBoxIcon.Error";
                    iconStyle = MessageBoxIcon.Error;
                    this.pbIcon.Visible = true;
                    this.pbIcon.Image = ((System.Drawing.Bitmap)(imageList1.Images[3]));
                    break;
            }
            //
            // Be sure there are no empty parameters
            if ((strDefButton != "") && (strIcon == ""))
            {
                iconStyle = MessageBoxIcon.None;
                strIcon = "MessageBoxIcon.None";
            }
            if ((strIcon != "") && (strButtons == ""))
            {
                strButtons = "MessageBoxButtons.OK";
                btnStyle = MessageBoxButtons.OK;
            }
        }

        protected void btnPreview_Click(object sender, System.EventArgs e)
        {
            OnMessageBoxChanged(sender, e);
            string str = txtMessage.Text.ToString();
            MessageBox.Show(txtMessage.Text, txtCaption.Text, btnStyle, iconStyle, btnDefault);
        }

        protected void cbnUseReturnVar_CheckedChanged(object sender, System.EventArgs e)
        {
            txtVariable.Enabled = cbnUseReturnVar.Checked;
            cbnBuildSwitch.Enabled = cbnUseReturnVar.Checked;
            lblVariable.Enabled = cbnUseReturnVar.Checked;
            cbnDeclareIt.Enabled = cbnUseReturnVar.Checked;
            OnMessageBoxChanged(sender, e);
        }

        private void OnListBox1DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            Rectangle rc = e.Bounds;
            Brush brush;
            bool bSelected = (e.State & DrawItemState.Selected) != 0;
            string str = listBox1.GetItemText(listBox1.Items[e.Index]);
            int Left = e.Bounds.Left;
            int Top = e.Bounds.Top + 3;
            int Height = 20;
            int Width = 20;
            if (e.Index > 0)
                imageList1.Draw(e.Graphics, Left, Top, Height, Width, e.Index - 1);
            rc.X += Width + 3;
            if (bSelected)
            {
                e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
                brush = Brushes.White;
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, rc);
                brush = Brushes.Black;
            }
            e.Graphics.DrawString(str, listBox1.Font, brush, rc, format);
            OnMessageBoxChanged(sender, e);
        }

        private void listBox1_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
        {
            e.ItemHeight = 24;
            e.ItemWidth = listBox1.Width;
        }

        private void OnLanguageChanged(object sender, System.EventArgs e)
        {
            CheckBox cbn = (CheckBox)sender;
            if (cbn == cbnVisualBasic)
            {
                cbnVisualBasic.Checked = true;
                cbnVisualCPP.Checked = false;
                cbnUseMFC.Checked = false;
                cbnUseMFC.Enabled = false;
                cbnVisualCSharp.Checked = false;
            }
            else if (cbn == cbnVisualCSharp)
            {
                cbnVisualBasic.Checked = false;
                cbnVisualCPP.Checked = false;
                cbnUseMFC.Checked = false;
                cbnUseMFC.Enabled = false;
                cbnVisualCSharp.Checked = true;
            }
            else if (cbn == cbnVisualCPP)
            {
                cbnVisualBasic.Checked = false;
                cbnVisualCPP.Checked = true;
                cbnUseMFC.Enabled = true;
                cbnVisualCSharp.Checked = false;
            }
            OnMessageBoxChanged(sender, e);
        }

        private void cbnUseReturnVar_OnCheckChanged(object sender, System.EventArgs e)
        {
            cbnDeclareIt.Enabled = cbnUseReturnVar.Checked;
            cbnBuildSwitch.Enabled = cbnUseReturnVar.Checked;
            lblVariable.Enabled = cbnUseReturnVar.Checked;
            txtVariable.Enabled = cbnUseReturnVar.Checked;
            OnMessageBoxChanged(sender, e);
        }

        private void btnDefault1_OnClick(object sender, System.EventArgs e)
        {
            cbnDefault1.Checked ^= true;
            cbnDefault2.Checked = false;
            cbnDefault3.Checked = false;
            OnMessageBoxChanged(sender, e);
        }

        private void btnDefault2_OnClick(object sender, System.EventArgs e)
        {
            cbnDefault1.Checked = false;
            cbnDefault2.Checked ^= true;
            cbnDefault3.Checked = false;
            OnMessageBoxChanged(sender, e);
        }

        private void btnDefault3_OnClick(object sender, System.EventArgs e)
        {
            cbnDefault1.Checked = false;
            cbnDefault2.Checked = false;
            cbnDefault3.Checked ^= true;
            OnMessageBoxChanged(sender, e);
        }
    }
}
