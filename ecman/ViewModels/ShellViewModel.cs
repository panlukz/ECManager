using System.Collections.Generic;
using Caliburn.Micro;

namespace ecman.ViewModels
{
    public class ShellViewModel : Conductor<ITab>.Collection.OneActive, IShell
    {
        public ShellViewModel(IEnumerable<ITab> tabItems)
        {
            this.DisplayName = "E-Commerce Manager BETA";
            Items.AddRange(tabItems);

        }
    }
}