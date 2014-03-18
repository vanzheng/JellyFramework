using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jelly.Web.Controls
{
    /// <summary>
    /// Extend Label control, be use for modified text. 
    /// </summary>
    [ToolboxData("<{0}:ExtendLabel Text=\"\" ModifyText=\"\" runat=\"server\" />")]
    public class ExtendLabel:Label
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (HttpContext.Current.Request.QueryString["Act"] == "Edit") 
            {
                base.Text = this.ModifyText;
            }
        }

        [Bindable(true),Category("自定义"),DefaultValue(""),Localizable(true)]
        public string ModifyText 
        {
            get 
            {
                object obj = this.ViewState["ModifyText"];
                return (obj == null) ? string.Empty : (string)obj;
            }
            set 
            {
                this.ViewState["ModifyText"] = value;   
            }
        }
    }
}
