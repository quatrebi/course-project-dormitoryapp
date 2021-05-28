using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryApp.Database;

namespace DormitoryApp.Models
{
    public class MenuButton : MenuButtonModel
    {
        public MenuButton(MenuButtonModel model)
        {
            MBID = model.MBID;
            ImageSource = model.ImageSource;
            Caption = model.Caption;
            Permission = model.Permission;
            ViewName = model.ViewName;
            ModelName = model.ModelName;
        }

        public string PageName => App.Current.TryFindResource($"i18n-{Caption}") as string;
        public Uri URI => new Uri($"/DormitoryApp;component/resources/images/icons/{ImageSource}", UriKind.Relative);
    }
}
