using System.Windows.Forms;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class TextDefinitionDialog : Form
{
    public TextDefinitionDialog()
    {
        InitializeComponent();
    }

    public string TextValue
    {
        get => txtDefinition.Text;
        set => txtDefinition.Text = value;
    }

    public string Description
    {
        get => txtDescription.Text;
        set => txtDescription.Text = value;
    }
}