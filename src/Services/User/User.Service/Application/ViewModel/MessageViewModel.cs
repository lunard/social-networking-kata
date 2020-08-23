using Humanizer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace User.Service.Application.ViewModel
{
    public class MessageViewModel
    {
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public String UserName { get; set; }

        public override string ToString()
        {
            var elapsed = DateTime.Now - Date;

            return $"{UserName} - {Content} ({elapsed.Humanize(culture: CultureInfo.GetCultureInfo("en-US"))})";
        }
    }
}
