using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jelly.Web.Helpers
{
    public class ModelBinding
    {
        public static void GetPost<T>(ref T t, NameValueCollection form) 
        {
            Type type = t.GetType();
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo p in pi) 
            {
                if (form[p.Name] != null)
                {
                    try
                    {
                        p.SetValue(t, Convert.ChangeType(form[p.Name], p.PropertyType), null);//为属性赋值，并转换键值的类型为该属性的类型
                        //va++;//记录赋值成功的属性数
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Binds an object's properties to <see cref="Control"/>s with the same ID as the propery name. 
        /// </summary>
        /// <param name="obj">The object whose properties are being bound to forms Controls</param>
        /// <param name="container">The control in which the form Controls reside (usually a Page or ContainerControl)</param>
        public static void BindObjectToControls(object obj, Control container)
        {
            if (obj == null) return;

            // Get the properties of the business object
            //
            Type objType = obj.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();

            foreach (PropertyInfo objProperty in objPropertiesArray)
            {

                Control control = container.FindControl(objProperty.Name);

                if (control != null)
                {
                    // handle ListControls (DropDownList, CheckBoxList, RadioButtonList)
                    //
                    if (control is ListControl)
                    {
                        ListControl listControl = (ListControl)control;
                        object objpro = objProperty.GetValue(obj, null);
                        string propertyValue = string.Empty;
                        if (objpro != null)
                            propertyValue = objpro.ToString();
                        ListItem listItem = listControl.Items.FindByValue(propertyValue);
                        if (listItem != null) listItem.Selected = true;

                    }
                    else
                    {
                        // get the properties of the control
                        //
                        Type controlType = control.GetType();
                        PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

                        // test for common properties
                        //
                        bool success = false;
                        success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool));

                        if (!success)
                            success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime));

                        if (!success)
                            success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String));

                        if (!success)
                            success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String));

                    }
                }
            }
        }

        /// <summary>
        /// Looks for a property name and type on a control and attempts to set it to the value in an object's property 
        /// of the same name.
        /// </summary>
        /// <param name="obj">The object whose properties are being retrieved</param>
        /// <param name="objProperty">The property of the object being retrieved</param>
        /// <param name="control">The control whose ID matches the object's property name.</param>
        /// <param name="controlPropertiesArray">An array of the control's properties</param>
        /// <param name="propertyName">The name of the Control property being set</param>
        /// <param name="type">The correct type for the Control property</param>
        /// <returns>Boolean for whether the property was found and set</returns>
        private static bool FindAndSetControlProperty(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type)
        {
            // iterate through control properties
            //
            foreach (PropertyInfo controlProperty in controlPropertiesArray)
            {
                // check for matching name and type
                //
                if (controlProperty.Name == propertyName && controlProperty.PropertyType == type)
                {
                    // set the control's property to the business object property value
                    //
                    controlProperty.SetValue(control, Convert.ChangeType(objProperty.GetValue(obj, null), type), null);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Binds your the values in <see cref="Control"/>s to a business object.
        /// </summary>
        /// <param name="obj">The object whose properties are being bound to Control values</param>
        /// <param name="container">The control in which the form Controls reside (usually a Page or ContainerControl)</param>
        public static void BindControlsToObject(object obj, Control container)
        {
            if (obj == null) return;

            // Get the properties of the business object
            //			
            Type objType = obj.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();

            foreach (PropertyInfo objProperty in objPropertiesArray)
            {

                Control control = container.FindControl(objProperty.Name);

                if (control != null)
                {
                    if (control is ListControl)
                    {
                        ListControl listControl = (ListControl)control;
                        if (listControl.SelectedItem != null)
                            objProperty.SetValue(obj, Convert.ChangeType(listControl.SelectedItem.Value, objProperty.PropertyType), null);

                    }
                    else
                    {
                        // get the properties of the control
                        //
                        Type controlType = control.GetType();
                        PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

                        // test for common properties
                        //
                        bool success = false;
                        success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool));

                        if (!success)
                            success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime));

                        if (!success)
                            success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String));

                        if (!success)
                            success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String));
                    }
                }
            }
        }

        /// <summary>
        /// Looks for a property name and type on a control and attempts to set it to the value in an object's property 
        /// of the same name.
        /// </summary>
        /// <param name="obj">The object whose properties are being set</param>
        /// <param name="objProperty">The property of the object being set</param>
        /// <param name="control">The control whose ID matches the object's property name.</param>
        /// <param name="controlPropertiesArray">An array of the control's properties</param>
        /// <param name="propertyName">The name of the Control property being retrieved</param>
        /// <param name="type">The correct type for the Control property</param>
        /// <returns>Boolean for whether the property was found and retrieved</returns>
        private static bool FindAndGetControlProperty(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type)
        {
            // iterate through control properties
            //
            foreach (PropertyInfo controlProperty in controlPropertiesArray)
            {
                // check for matching name and type
                //
                if (controlProperty.Name == "Text" && controlProperty.PropertyType == typeof(String))
                {
                    // set the control's property to the business object property value
                    //
                    try
                    {
                        objProperty.SetValue(control, Convert.ChangeType(controlProperty.GetValue(obj, null), objProperty.PropertyType), null);
                        return true;
                    }
                    catch
                    {
                        // the data from the form control could not be converted to objProperty.PropertyType
                        //
                        return false;
                    }
                }
            }
            return false;
        }
    }


}
