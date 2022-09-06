using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Asesoria
{
    public class CalendarItemViewModel
    {
        public string title { get; internal set; }
        public DateTime start { get; internal set; }
        public DateTime end { get; internal set; }
        public string url { get; internal set; }
        public string backgroundColor { get; internal set; }
    }
}