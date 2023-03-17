using System;
using System.Linq;
using System.Windows.Forms;
using Vatsim.Vatis.Config;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class AirportConditionsDialog : Form
{
    private readonly AtisComposite mComposite;
    private readonly bool mKeepOnTop = false;

    public AirportConditionsDialog(AtisComposite composite, bool onTop = false)
    {
        InitializeComponent();

        mComposite = composite;
        mKeepOnTop = onTop;

        btnMoveDown.Text = char.ConvertFromUtf32(8595);
        btnMoveUp.Text = char.ConvertFromUtf32(8593);

        chkBeforeFreeForm.Checked = mComposite.AirportConditionsBeforeFreeText;

        PopulateList();
    }

    private void AirportConditionsDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    private void PopulateList()
    {
        lstConditions.Items.Clear();

        foreach (var condition in mComposite.AirportConditionDefinitions.OrderBy(t => t.Ordinal))
        {
            lstConditions.Items.Add(condition, condition.Enabled);
        }

        btnEdit.Enabled = false;
        btnDelete.Enabled = false;
        btnMoveUp.Enabled = false;
        btnMoveDown.Enabled = false;
    }

    private void lstConditions_Format(object sender, ListControlConvertEventArgs e)
    {
        e.Value = (e.ListItem as DefinedText)?.ToString();
    }

    private void lstConditions_SelectedIndexChanged(object sender, EventArgs e)
    {
        EvaluateButtonStates();
    }

    private void EvaluateButtonStates()
    {
        if (lstConditions.SelectedIndex == -1)
        {
            btnMoveUp.Enabled = false;
            btnMoveDown.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
        }
        else
        {
            btnMoveUp.Enabled = (lstConditions.SelectedIndex > 0);
            btnMoveDown.Enabled = (lstConditions.SelectedIndex < lstConditions.Items.Count - 1);
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
        }
    }

    private void btnMoveUp_Click(object sender, EventArgs e)
    {
        if (lstConditions.SelectedIndex >= 1)
        {
            var definition = lstConditions.SelectedItem as DefinedText;
            var newIndex = lstConditions.SelectedIndex - 1;
            if (definition != null)
            {
                lstConditions.Items.Remove(definition);
                lstConditions.Items.Insert(newIndex, definition);
                lstConditions.SelectedItem = definition;
            }

            var idx = 1;
            mComposite.AirportConditionDefinitions.Clear();
            foreach (DefinedText condition in lstConditions.Items)
            {
                condition.Ordinal = idx++;
                mComposite.AirportConditionDefinitions.Add(condition);
            }
        }
    }

    private void btnMoveDown_Click(object sender, EventArgs e)
    {
        if (lstConditions.SelectedIndex <= lstConditions.Items.Count - 1)
        {
            var definition = lstConditions.SelectedItem as DefinedText;
            var newIndex = lstConditions.SelectedIndex + 1;
            if (definition != null)
            {
                lstConditions.Items.Remove(definition);
                lstConditions.Items.Insert(newIndex, definition);
                lstConditions.SelectedItem = definition;
            }

            var idx = 1;
            mComposite.AirportConditionDefinitions.Clear();
            foreach (DefinedText condition in lstConditions.Items)
            {
                condition.Ordinal = idx++;
                mComposite.AirportConditionDefinitions.Add(condition);
            }
        }
    }

    private void lstConditions_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        var item = lstConditions.Items[e.Index] as DefinedText;
        var configDefinition = mComposite.AirportConditionDefinitions.FirstOrDefault(t => t.Text == item.Text);
        if (configDefinition != null)
        {
            configDefinition.Enabled = e.NewValue == CheckState.Checked;
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (lstConditions.SelectedItem != null)
        {
            if (MessageBox.Show(this,
                    "Are you sure you want to delete the selected definition? This action cannot be undone.",
                    "Delete Definition", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
            {
                var item = lstConditions.SelectedItem as DefinedText;
                mComposite.AirportConditionDefinitions.RemoveAll(t => t.Text == item.Text);
                PopulateList();
            }
        }
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        using (var dlg = new TextDefinitionDialog())
        {
            dlg.TopMost = mKeepOnTop;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dlg.TextValue))
                {
                    MessageBox.Show(this, "Text cannot be empty.", "New Definition", MessageBoxButtons.OK,
                        MessageBoxIcon.Hand);
                    return;
                }

                if (!string.IsNullOrEmpty(dlg.Description) && 
                    mComposite.AirportConditionDefinitions.Any(t => t.Description == dlg.Description))
                {
                    MessageBox.Show(this, "A definition with this description label already exists.", "Duplicate Description", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (mComposite.AirportConditionDefinitions.Any(t => t.Text == dlg.TextValue))
                {
                    MessageBox.Show(this, "A definition with this text already exists.", "Duplicate Definition",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    var definition = new DefinedText
                    {
                        Description = dlg.Description,
                        Text = dlg.TextValue,
                        Ordinal = mComposite.AirportConditionDefinitions.Count + 1
                    };
                    mComposite.AirportConditionDefinitions.Add(definition);
                    PopulateList();
                }
            }
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        using (var dlg = new TextDefinitionDialog())
        {
            dlg.TopMost = mKeepOnTop;
            if (lstConditions.SelectedItem is DefinedText selectedDefinition)
            {
                dlg.Description = selectedDefinition.Description;
                dlg.TextValue = selectedDefinition.Text;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(dlg.TextValue))
                    {
                        MessageBox.Show(this, "Text cannot be empty.", "Edit Definition", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }

                    if (!string.IsNullOrEmpty(dlg.Description) && mComposite.AirportConditionDefinitions.Any(t => t.Description == dlg.Description && t != selectedDefinition))
                    {
                        MessageBox.Show(this, "A definition with this description label already exists.", "Duplicate Description", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (mComposite.AirportConditionDefinitions.Any(t => t.Text == dlg.TextValue && t != selectedDefinition))
                    {
                        MessageBox.Show(this, "A definition with this text already exists.", "Duplicate Definition", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        selectedDefinition.Description = dlg.Description;
                        selectedDefinition.Text = dlg.TextValue;
                        PopulateList();
                    }
                }
            }
        }
    }

    private void chkBeforeFreeForm_CheckedChanged(object sender, EventArgs e)
    {
        mComposite.AirportConditionsBeforeFreeText = chkBeforeFreeForm.Checked;
    }
}