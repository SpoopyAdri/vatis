using System;
using System.Linq;
using System.Windows.Forms;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Core;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class NotamDefinitionsDialog : Form
{
    private readonly AtisComposite mComposite;
    private readonly bool mKeepOnTop = false;

    public NotamDefinitionsDialog(AtisComposite composite, bool onTop = false)
    {
        InitializeComponent();

        mComposite = composite;
        mKeepOnTop = onTop;

        btnMoveDown.Text = char.ConvertFromUtf32(8595);
        btnMoveUp.Text = char.ConvertFromUtf32(8593);

        chkBeforeFreeForm.Checked = mComposite.NotamsBeforeFreeText;

        PopulateList();
    }

    private void PopulateList()
    {
        lstConditions.Items.Clear();

        foreach (var condition in mComposite.NotamDefinitions.OrderBy(t => t.Ordinal))
        {
            lstConditions.Items.Add(condition, condition.Enabled);
        }

        btnEdit.Enabled = false;
        btnDelete.Enabled = false;
        btnMoveUp.Enabled = false;
        btnMoveDown.Enabled = false;
    }

    private void btnMoveUp_Click(object sender, EventArgs e)
    {
        if (lstConditions.SelectedIndex >= 1)
        {
            var definition = lstConditions.SelectedItem as DefinedText;
            int newIndex = lstConditions.SelectedIndex - 1;
            lstConditions.Items.Remove(definition);
            lstConditions.Items.Insert(newIndex, definition);
            lstConditions.SelectedItem = definition;

            int idx = 1;
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
            int newIndex = lstConditions.SelectedIndex + 1;
            lstConditions.Items.Remove(definition);
            lstConditions.Items.Insert(newIndex, definition);
            lstConditions.SelectedItem = definition;

            int idx = 1;
            mComposite.AirportConditionDefinitions.Clear();
            foreach (DefinedText condition in lstConditions.Items)
            {
                condition.Ordinal = idx++;
                mComposite.AirportConditionDefinitions.Add(condition);
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

                if (mComposite.NotamDefinitions.Any(t => t.Text == dlg.TextValue))
                {
                    MessageBox.Show(this, "A definition with this text already exists.", "Duplicate Definition",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    var definition = new DefinedText
                    {
                        Text = dlg.TextValue,
                        Ordinal = mComposite.NotamDefinitions.Count + 1
                    };
                    mComposite.NotamDefinitions.Add(definition);
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
            var selectedDefinition = lstConditions.SelectedItem as DefinedText;
            dlg.TextValue = selectedDefinition.Text;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dlg.TextValue))
                {
                    MessageBox.Show(this, "Text cannot be empty.", "Edit Definition", MessageBoxButtons.OK,
                        MessageBoxIcon.Hand);
                    return;
                }

                if (mComposite.NotamDefinitions.Any(t => t.Text == dlg.TextValue && t != selectedDefinition))
                {
                    MessageBox.Show(this, "A definition with this text already exists.", "Duplicate Definition",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    selectedDefinition.Text = dlg.TextValue;
                    PopulateList();
                }
            }
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
                mComposite.NotamDefinitions.RemoveAll(t => t.Text == item.Text);
                PopulateList();
            }
        }
    }

    private void NotamDefintionsDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    private void lstConditions_Format(object sender, ListControlConvertEventArgs e)
    {
        e.Value = (e.ListItem as DefinedText)?.Text;
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

    private void lstConditions_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        var item = lstConditions.Items[e.Index] as DefinedText;
        var configDefinition = mComposite.NotamDefinitions.FirstOrDefault(t => t.Text == item.Text);
        if (configDefinition != null)
        {
            configDefinition.Enabled = e.NewValue == CheckState.Checked;
        }
    }

    private void chkBeforeFreeForm_CheckedChanged(object sender, EventArgs e)
    {
        mComposite.NotamsBeforeFreeText = chkBeforeFreeForm.Checked;
    }
}