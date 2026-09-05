using System;
using System.Collections.Generic;
using System.Text;

namespace TaskHive.Classes
{
   public static class GlSearchJob
    {

         public static bool? CheckBoxTextSearch { get; set; }
        public static int ComboboxIndexBoxTextSearch { get; set; }
         public static string TextboxText { get; set; } = string.Empty;


        public static bool? CheckBoxFrom { get; set; }
        public static string TextboxTextDate1 { get; set; } = string.Empty;
        public static string TextboxTextDate2 { get; set; } = string.Empty;


        public static bool? CheckBoxCaption { get; set; }
        public static int ComboboxCaption { get; set; }

        public static bool? CheckBoxCondition { get; set; }
        public static int ComboboxCondition { get; set; }

        public static bool? CheckBoxResponsible { get; set; }
        public static int ComboboxResponsible { get; set; }

        public static bool? CheckBoxEnable { get; set; }
        public static int ComboboxEnable { get; set; }

        public static bool? CheckBoxTime { get; set; }
        public static string textboxttime { get; set; } = string.Empty;


    }
}
