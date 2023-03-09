using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class UserInputDialog : Form
{
    public string WindowTitle { get; set; }
    public string PromptLabel { get; set; }
    public string RegexExpression { get; set; }
    public string ErrorMessage { get; set; }
    public string InitialValue { get; set; }
    public bool TextUppercase { get; set; }
    public int MaxLength { get; set; } = 100;

    public UserInputDialog()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        Text = WindowTitle;
        lblPrompt.Text = PromptLabel;
        txtResponse.Text = InitialValue;
        txtResponse.MaxLength = MaxLength;
        txtResponse.CharacterCasing = TextUppercase ? CharacterCasing.Upper : CharacterCasing.Normal;
    }

    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    public string Value => txtResponse.Text;

    public bool IsRegexMatch => Regex.IsMatch(txtResponse.Text, RegexExpression);

    private void OnBtnClicked(object sender, EventArgs e)
    {
        DialogResult = ((Button)sender).DialogResult;
        if (DialogResult == DialogResult.OK && !string.IsNullOrEmpty(RegexExpression) && !IsRegexMatch)
        {
            DialogResult = DialogResult.None;
            MessageBox.Show(this, ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            txtResponse.SelectAll();
            txtResponse.Focus();
        }
        else
        {
            Close();
        }
    }
}