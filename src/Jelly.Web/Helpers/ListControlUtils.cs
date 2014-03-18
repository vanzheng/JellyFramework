using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace Jelly.Web.Helpers
{
    /// <summary>
    /// List Controls Helper
    /// </summary>
    public class ListControlUtils
    {
        public static void SetSelectedItem(ListControl listControl, string values, char separator)
        {
            if (!string.IsNullOrEmpty(values))
            {
                string[] arrValue = values.Split(separator);
                foreach (string val in arrValue)
                {
                    string itemValue = val.Trim();
                    ListItem item = listControl.Items.FindByValue(itemValue);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        public static void SetSelectedItem(ListControl listControl, string values) 
        {
            SetSelectedItem(listControl, values, ',');
        }

        public static IList<string> GetValues(ListControl listControl) 
        {
            IList<string> list = new List<string>();

            foreach (ListItem item in listControl.Items) 
            {
                list.Add(item.Value);
            }

            return list;
        }

        public static IList<string> GetTexts(ListControl listControl)
        {
            IList<string> list = new List<string>();

            foreach (ListItem item in listControl.Items)
            {
                list.Add(item.Text);
            }

            return list;
        }
    }
}
