using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Serialization;

namespace Core
{
    public class ResultData
    {
        public short ErrorStatus { get; set; } 
        public List<ErrorData> Errors { get; set; }

        public class ErrorData
        {
            public int? Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public ResultData()
        {
            ErrorStatus = 0;
            Errors = new List<ErrorData>();
        }

        public void AddError(string description)
        {
            AddError(new ErrorData() 
            {
                Description = description
            });
        }

        public void AddError(string name, string description)
        {
            AddError(new ErrorData()
            {
                Name = name,
                Description = description
            });
        }

        public void AddError(int code, string name, string description)
        {
            AddError(new ErrorData()
            {
                Code = code,
                Name = name,
                Description = description
            });
        }

        public bool HasErrors()
        {
            return Errors != null && Errors.Count > 0;
        }

        #region Private function

        private void AddError(ErrorData errorData)
        {
            if (ErrorStatus == 0) ErrorStatus = 1;
            Errors.Add(errorData);
        }

        #endregion
    }
}
