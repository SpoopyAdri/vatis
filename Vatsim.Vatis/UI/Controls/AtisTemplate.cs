using System;
using System.Windows.Forms;
using Vatsim.Vatis.Events;

namespace Vatsim.Vatis.UI.Controls;
public partial class AtisTemplate : UserControl
{
    public string Template
    {
        get => txtAtisTemplate.Text;
        set => txtAtisTemplate.Text = value;
    }

    public AtisTemplate()
    {
        InitializeComponent();
        EventBus.Register(this);
    }

    private void txtAtisTemplate_TextChanged(object sender, EventArgs e)
    {
        if (!txtAtisTemplate.Focused)
            return;

        EventBus.Publish(this, new AtisTemplateChanged(Template));
    }
}
