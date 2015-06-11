using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ecman.ViewModels
{
    public class DialogService
    {
       public static async Task<MessageDialogResult> ShowMessage(
           string message, string title, MessageDialogStyle dialogStyle)
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
                return await metroWindow.ShowMessageAsync(
                    title, message, dialogStyle, metroWindow.MetroDialogOptions);
            } 
    }
}