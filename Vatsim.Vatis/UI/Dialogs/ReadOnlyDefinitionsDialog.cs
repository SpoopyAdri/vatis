using System.Linq;
using System.Windows.Forms;
using Vatsim.Vatis.Profiles;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class ReadOnlyDefinitionsDialog : Form
{
    private readonly Composite mComposite;
    private readonly DefinitionType mDefinitionType;

    public enum DefinitionType
    {
        AirportConditions,
        Notams
    }

    public ReadOnlyDefinitionsDialog(Composite composite, DefinitionType type)
    {
        InitializeComponent();
        mComposite = composite;
        mDefinitionType = type;
        PopulateList();

        switch (mDefinitionType)
        {
            case DefinitionType.AirportConditions:
                Text = "Read-Only: Airport Condition Definitions";
                break;
            case DefinitionType.Notams:
                Text = "Read-Only: NOTAM Definitions";
                break;
        }
    }

    private void PopulateList()
    {
        lstConditions.Items.Clear();

        if (mDefinitionType == DefinitionType.Notams)
        {
            foreach (var condition in mComposite.NotamDefinitions.OrderBy(t => t.Ordinal))
            {
                lstConditions.Items.Add(condition, condition.Enabled);
            }
        }
        else
        {
            foreach (var condition in mComposite.AirportConditionDefinitions.OrderBy(t => t.Ordinal))
            {
                lstConditions.Items.Add(condition, condition.Enabled);
            }
        }
    }

    private void ReadOnlyDefinitionsDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    private void lstConditions_Format(object sender, ListControlConvertEventArgs e)
    {
        e.Value = (e.ListItem as DefinedTextMeta)?.ToString();
    }

    private void lstConditions_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (lstConditions.Items[e.Index] is DefinedTextMeta item)
        {
            if (mDefinitionType == DefinitionType.AirportConditions)
            {
                var configDefinition = mComposite.AirportConditionDefinitions.FirstOrDefault(t => t.Text == item.Text);
                if (configDefinition != null)
                {
                    configDefinition.Enabled = e.NewValue == CheckState.Checked;
                }
            }
            else
            {
                var configDefinition = mComposite.NotamDefinitions.FirstOrDefault(t => t.Text == item.Text);
                if (configDefinition != null)
                {
                    configDefinition.Enabled = e.NewValue == CheckState.Checked;
                }
            }
        }
    }
}