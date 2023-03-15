using System.Collections.Generic;

namespace Vatsim.Vatis.Utils;

public static class TabPageExtensions
{
    static readonly Dictionary<System.Windows.Forms.TabPage, object[]> mHiddenPages = new();

    public static void SetVisible(this System.Windows.Forms.TabPage tabPage, bool visible)
    {
        if (visible)
        {
            tabPage.ShowTabPage();
        }
        else
        {
            tabPage.HideTabPage();
        }
    }

    public static bool IsVisible(this System.Windows.Forms.TabPage tabPage)
    {
        var tabControl = tabPage.Parent as System.Windows.Forms.TabControl;
        return tabControl != null && tabControl.TabPages.Contains(tabPage);
    }

    public static void ShowTabPage(this System.Windows.Forms.TabPage tabPage)
    {
        if (!mHiddenPages.ContainsKey(tabPage)) return;

        var opt = mHiddenPages[tabPage];
        var tabControl = (System.Windows.Forms.TabControl)opt[0];
        var index = (int)opt[1];

        if (index > tabControl.TabPages.Count)
            index--;

        mHiddenPages.Remove(tabPage);
        tabControl.TabPages.Insert(index, tabPage);
    }

    public static void HideTabPage(this System.Windows.Forms.TabPage tabPage)
    {
        var tabControl = (System.Windows.Forms.TabControl)tabPage.Parent;

        if (!mHiddenPages.ContainsKey(tabPage))
            mHiddenPages.Add(tabPage, new object[] { tabControl, tabPage.TabIndex });

        if (tabControl != null)
            tabControl.TabPages.Remove(tabPage);
    }
}