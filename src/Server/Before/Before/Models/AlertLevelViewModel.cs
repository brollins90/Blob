
//namespace Before.Models
//{
//    public class AlertLevelViewModel
//    {
//        public string Name { get; set; }
//        public string CssClass { get; set; }

//        public static AlertLevelViewModel Ok = new AlertLevelViewModel { Name = "Ok", CssClass = "status-ok" };
//        public static AlertLevelViewModel Information = new AlertLevelViewModel { Name = "Information", CssClass = "status-info" };
//        public static AlertLevelViewModel Warning = new AlertLevelViewModel { Name = "Warning", CssClass = "status-warn" };
//        public static AlertLevelViewModel Critical = new AlertLevelViewModel { Name = "Critical", CssClass = "status-crit" };
//        public static AlertLevelViewModel Unknown = new AlertLevelViewModel { Name = "Unknown", CssClass = "status-unknown" };

//        public static AlertLevelViewModel FromInt(int level)
//        {
//            return (level == 0)
//                       ? Ok
//                       : (level == 1)
//                             ? Warning
//                             : (level == 2)
//                                   ? Critical
//                                   : Unknown;
//        }
//    }
//}
