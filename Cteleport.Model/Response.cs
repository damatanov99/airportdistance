using System;
using System.Collections.Generic;
using System.Text;

namespace Cteleport.Model
{
  

    public class ResponseResult<T>
    {
        private T _data;
        private bool _success = true;
        private List<Error> _errors;

        public bool success {
            get { return _success; }
            set { _success = value; }
        }

        public List<Error> errors
        {
            get { return _errors; }
            set { _errors = value; }
         }
     
        public T data
        {
            get { return _data; }
            set { _data = value; }
        }


        public ResponseResult()
        {
            _errors = new List<Error>();
        }

        public ResponseResult(T data)
        {
            _data = data;
            _errors = new List<Error>();
        }

        public void Error(List<Error> errors)
        {
            _success = false;
            _errors = errors;
        }

        public void Error(ErrorCodes errorCode, string errorMsg)
        {
            _success = false;
            _errors.Add(new Model.Error() { error_description = errorMsg, error_code = errorCode }); 
        }

    

    }
}
