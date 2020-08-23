using Humanizer;
using Humanizer.Localisation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace User.Service.Application.ViewModel
{
    public class MessageViewModel
    {
        public string Content { get; set; }

        public DateTime? Date { get; set; }

        public String UserName { get; set; }

        public override string ToString()
        {
            var elapsed = DateTime.Now - Date;

            return $"{UserName} - {Content} {(elapsed.HasValue ? ("(" + elapsed.Value.Humanize(precision: 3, culture: CultureInfo.GetCultureInfo("en-US"), maxUnit: TimeUnit.Day) + ")") : "")}";
        }
    }
}
