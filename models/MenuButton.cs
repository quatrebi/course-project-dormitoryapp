using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryApp.Models
{
    public class MenuButton : MenuButtonModel
    {
        public MenuButton(MenuButtonModel model)
        {
            this.MBID = model.MBID;
            this.ImageSource = model.ImageSource;
            this.Caption = model.Caption;
            this.Permission = model.Permission;
            this.ViewName = model.ViewName;
        }

        public string PageName => App.Current.TryFindResource($"i18n-{Caption}") as string;
        public Uri URI => new Uri($"/DormitoryApp;component/resources/images/icons/{ImageSource}", UriKind.Relative);
    }
}
