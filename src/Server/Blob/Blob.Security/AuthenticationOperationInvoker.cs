//using System;
//using System.ServiceModel.Dispatcher;

//namespace Blob.Security
//{
//    class AuthenticationOperationInvoker : IOperationInvoker
//    {
//        private readonly IOperationInvoker _defaultInvoker;

//        public AuthenticationOperationInvoker(IOperationInvoker defaultInvoker)
//        {
//            _defaultInvoker = defaultInvoker;
//        }

//        public object[] AllocateInputs()
//        {
//            return _defaultInvoker.AllocateInputs();
//        }

//        public object Invoke(object instance, object[] inputs, out object[] outputs)
//        {
//            //execute the custom authorization logic and set the thread principal accordingly

//            return _defaultInvoker.Invoke(instance, inputs, out outputs);
//        }

//        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
//        {
//            return _defaultInvoker.InvokeBegin(instance, inputs, callback, state);
//        }

//        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
//        {
//            return _defaultInvoker.InvokeEnd(instance, out outputs, result);
//        }

//        public bool IsSynchronous
//        {
//            get { return _defaultInvoker.IsSynchronous; }
//        }
//    }
//}
