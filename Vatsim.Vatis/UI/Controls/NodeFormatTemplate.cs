using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Vatsim.Vatis.UI.Controls;

public partial class NodeFormatTemplate : UserControl
{
    private List<Variable> mVariables = new();

    public NodeFormatTemplate()
    {
        InitializeComponent();
    }

    public string NodeType { get; set; }

    public string TextTemplate
    {
        get => txtTextTemplate.Text;
        set => txtTextTemplate.Text = value;
    }

    public string VoiceTemplate
    {
        get => txtVoiceTemplate.Text;
        set => txtVoiceTemplate.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<Variable> Variables => mVariables;

    public event EventHandler TextTemplateChanged;
    public event EventHandler VoiceTemplateChanged;

    private void OnVariableClicked(object sender, EventArgs e)
    {
        var variable = (sender as Label).Text;
        if (variable != null)
        {
            var activeTemplateBox = txtTextTemplate.ContainsFocus
                ? txtTextTemplate
                : (txtVoiceTemplate.ContainsFocus ? txtVoiceTemplate : null);

            activeTemplateBox?.AppendText(variable);
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        foreach (var variable in mVariables)
        {
            var label = new Label
            {
                Text = "{" + variable.Text + "}",
                Font = new System.Drawing.Font("Consolas", 9.0f),
                Cursor = Cursors.Hand,
                Margin = new Padding(0),
                AutoSize = true,
            };

            label.Click += OnVariableClicked;

            if (!string.IsNullOrEmpty(variable.ToolTip))
            {
                variableToolTip.SetToolTip(label, variable.ToolTip);
            }

            variablePanel.Controls.Add(label);
        }
    }

    private void txtTextTemplate_TextChanged(object sender, EventArgs e)
    {
        if (!txtTextTemplate.Focused)
            return;

        TextTemplateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void txtVoiceTemplate_TextChanged(object sender, EventArgs e)
    {
        if (!txtVoiceTemplate.Focused)
            return;

        VoiceTemplateChanged?.Invoke(this, EventArgs.Empty);
    }
}

[TypeConverter(typeof(VariableConverter))]
public class Variable
{
    public string Text { get; set; }
    public string ToolTip { get; set; }
    public Variable() { }
    public Variable(string text, string toolTip)
    {
        Text = text;
        ToolTip = toolTip;
    }
}

public class VariableConverter : TypeConverter
{
    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
    {
        return TypeDescriptor.GetProperties(value, attributes);
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
        return true;
    }
}