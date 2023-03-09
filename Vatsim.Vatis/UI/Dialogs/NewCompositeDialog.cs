using System;
using System.Windows.Forms;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class NewCompositeDialog : Form
{
    public AtisType Type { get; set; } = AtisType.Combined;
    public string Identifier
    {
        get => txtIdentifier.Text;
        set => txtIdentifier.Text = value;
    }
    public string CompositeName
    {
        get => txtCompositeName.Text;
        set => txtCompositeName.Text = value;
    }

    public NewCompositeDialog()
    {
        InitializeComponent();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        ValidateFields();

        switch (Type)
        {
            case AtisType.Arrival:
                typeArrival.Checked = true;
                break;
            case AtisType.Departure:
                typeDeparture.Checked = true;
                break;
            case AtisType.Combined:
                typeCombined.Checked = true;
                break;
        }
    }

    private void typeCombined_CheckedChanged(object sender, EventArgs e)
    {
        Type = AtisType.Combined;
        ValidateFields();
    }

    private void typeDeparture_CheckedChanged(object sender, EventArgs e)
    {
        Type = AtisType.Departure;
        ValidateFields();
    }

    private void typeArrival_CheckedChanged(object sender, EventArgs e)
    {
        Type = AtisType.Arrival;
        ValidateFields();
    }

    private void txtIdentifier_TextChanged(object sender, EventArgs e)
    {
        ValidateFields();
    }

    private void txtCompositeName_TextChanged(object sender, EventArgs e)
    {
        ValidateFields();
    }

    private void ValidateFields()
    {
        if (!string.IsNullOrEmpty(txtIdentifier.Text) && !string.IsNullOrEmpty(txtCompositeName.Text))
        {
            if (typeCombined.Checked || typeDeparture.Checked || typeArrival.Checked)
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }
        else
        {
            btnOK.Enabled = false;
        }
    }
}

public enum AtisType
{
    Combined,
    Departure,
    Arrival
}