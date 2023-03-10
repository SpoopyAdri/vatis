using System.Windows.Forms;
using Vatsim.Vatis.UI;
using Vatsim.Vatis.UI.Dialogs;

namespace Vatsim.Vatis.Core;

internal class VatisAppContext : ApplicationContext
{
    private readonly ProfileListDialog mProfileListDialog;

    public VatisAppContext(IWindowFactory windowFactory)
    {
        mProfileListDialog = windowFactory.CreateProfileListDialog();
        mProfileListDialog.FormClosed += ProfileListDialogClosed;
        windowFactory.CreateStartupWindow().Show();
    }

    private void ProfileListDialogClosed(object sender, FormClosedEventArgs e)
    {
        ExitThread();
    }
}
