using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using DataAccess;
using DataAccess.Model;


namespace ecman.ViewModels
{
    public class ShellViewModel : Conductor<ITab>.Collection.OneActive, IShell
    {
        public ShellViewModel(IEnumerable<ITab> tabItems)
        {
            this.DisplayName = "ECMAN BETA";
            Items.AddRange(tabItems);

        }
    }
}