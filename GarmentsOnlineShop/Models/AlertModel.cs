using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarmentsOnlineShop.Models
{
    public class AlertModel
    {
        public AlertModel(string key, AlertType type)
        {

            switch (type)
            {
                case AlertType.Error:
                    css = "alert-danger";
                    heading = "Error!";
                    break;
                case AlertType.Success:
                    css = "alert-success";
                    heading = "Success";
                    break;
                    case AlertType.warning:
                    css = "alert-warning";
                    heading = "Warning";
                    break;
                default:
                    css = "alert-info";
                    heading = "Info";
                    break;

                   
            }
            message = key;
        }
            private string heading;
        public string Heading
        {
            get
            {
                return heading;
            }
        }
        private string message;
        public string Message
        {
            get
            {
                return message;
            }
        }
        private string css;
        public string CSS {
        get
            {
                return css;
            }
        }

    }

    public enum AlertType
    {
        Error,
        Success,
        warning,
        Info
    }
}