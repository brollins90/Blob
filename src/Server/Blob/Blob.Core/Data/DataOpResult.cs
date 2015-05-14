//using System.Collections.Generic;

//namespace Blob.Core.Data
//{
//    public class DataOpResult
//    {
//        public bool Succeeded { get; protected set; }

//        public IEnumerable<string> Errors { get; protected set; }

//        #region Constructors

//        public DataOpResult(bool success)
//        {
//            Succeeded = success;
//            Errors = new string[0];
//        }

//        public DataOpResult(IEnumerable<string> errors)
//        {
//            if (errors == null)
//            {
//                errors = new[] { "error in data operation" };
//            }
//            Succeeded = false;
//            Errors = errors;
//        }

//        public DataOpResult(params string[] errors)
//            : this((IEnumerable<string>)errors)
//        {
//        }

//        #endregion

//        public static DataOpResult Success
//        {
//            get { return new DataOpResult(); }
//        }

//        public static DataOpResult Failed(params string[] errors)
//        {
//            return new DataOpResult(errors);
//        }
//    }
//}
